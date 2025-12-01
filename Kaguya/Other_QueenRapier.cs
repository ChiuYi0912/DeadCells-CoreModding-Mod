using dc;
using dc.h2d;
using dc.h3d.mat;
using dc.h3d.pass;
using dc.haxe;
using dc.level.@struct;
using dc.libs;
using dc.libs.heaps;
using dc.libs.heaps.slib;
using dc.tool;
using dc.tool.atk;
using dc.tool.weap;
using HaxeProxy.Runtime;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Utitities;
using Serilog;
using System.Collections.Generic;
using System.Security.Cryptography;
using static dc.h3d.mat.TextureFlags;
using static dc.hxsl.Component;
using static dc.tool.weap.Hook_QueenRapier;
using Hero = dc.en.Hero;
using Log = Serilog.Log;
using System;


namespace Kaguya
{
    internal unsafe class Other_QueenRapier : QueenRapier
    {
        #region 字段和常量
        private readonly List<Queencut> _queenStrikeStates = new List<Queencut>();
        private readonly Random _random = new Random();
        private readonly Dictionary<Entity, int> _chainCounts = new Dictionary<Entity, int>();
        private const int MAX_CHAIN_COUNT = 5;
        private bool _isChainAttacking = false;

        internal static bool _queenfx = false;

        private string _customEffectId = "HYTCB";
        #endregion

        #region 构造函数
        public Other_QueenRapier(Hero i, InventItem? spriteBatch) : base(i, spriteBatch)
        {
            Hook_QueenRapier.queenStrike += Hook_myqueenStrike;
            Hook_HParticle.playAnimAndKill += hook_queenfx;
        }
        #endregion

        #region 数据结构
        public struct Queencut
        {
            public Entity angle;
            public double y;
            public double x;
            public double target;
            public double time;
            public double time_max;
            public int chainCount;
        }
        #endregion

        #region 公共方法重写
        public override bool onExecute()
        {
            _queenfx = true;
            return base.onExecute();
        }

        public override void fixedUpdate()
        {
            base.fixedUpdate();

            if (_queenStrikeStates.Count > 0)
            {
                double deltaTime = GetOptimizedFrameDeltaTime();
                CheckAndUpdateStrikeStates(deltaTime);
            }
        }

        public override void hitFromWeapon(Entity _cycle, Ref<int> _cycl)
        {
            base.hitFromWeapon(_cycle, _cycl);
        }

        public new unsafe void queenStrike(Entity angle, double y, double x, double target)
        {
            base.queenStrike(angle, y, x, target);
        }
        #endregion

        #region Hook处理方法
        private void Hook_myqueenStrike(orig_queenStrike orig, QueenRapier self, Entity angle, double y, double x, double target)
        {
            orig(self, angle, y, x, target);

            if (_isChainAttacking || self != this || angle == null)
                return;

            if (!_chainCounts.TryGetValue(angle, out int currentCount))
            {
                currentCount = 0;
            }

            if (currentCount < MAX_CHAIN_COUNT)
            {
                _chainCounts[angle] = currentCount + 1;

                var newCut = new Queencut
                {
                    angle = angle,
                    y = y,
                    x = x,
                    target = target,
                    time = 0.0,
                    time_max = 0.1,
                    chainCount = currentCount + 1
                };

                _queenStrikeStates.Add(newCut);
            }

        }

        private void hook_queenfx(Hook_HParticle.orig_playAnimAndKill orig, HParticle self, SpriteLib k, dc.String spd1, Ref<double> spd)
        {
            if (!_queenfx)
            {
                orig(self, k, spd1, spd);
                return;
            }

            if (spd1.ToString() == "fxQueenRapierCut")
            {
                spd1 = _customEffectId.AsHaxeString();
            }

            orig(self, k, spd1, spd);
        }
        #endregion

        #region 链式攻击核心逻辑
        public void CheckAndUpdateStrikeStates(double deltaTime)
        {
            if (_queenStrikeStates.Count == 0)
            {
                _queenfx = false;
                return;
            }

            int strikeCount = _queenStrikeStates.Count;

            for (int i = strikeCount - 1; i >= 0; i--)
            {
                Queencut currentCut = _queenStrikeStates[i];

                if (!IsValidTarget(currentCut.angle))
                {
                    RemoveStrikeState(currentCut, i);
                    continue;
                }
                currentCut.time += deltaTime;

                if (currentCut.time >= currentCut.time_max)
                {
                    ProcessChainAttack(currentCut, i);
                }
                else
                {
                    _queenStrikeStates[i] = currentCut;
                }
            }
        }

        private void ProcessChainAttack(Queencut cut, int index)
        {
            if (cut.chainCount >= MAX_CHAIN_COUNT || this == null || this.destroyed)
            {
                RemoveStrikeState(cut, index);
                return;
            }

            _isChainAttacking = true;
            _queenfx = true;

            try
            {
                double randomY = (_random.NextDouble() - 0.5) * 3;
                double randomX = (_random.NextDouble() - 0.5) * 3;
                double randomAngle = _random.NextDouble() * 3.1415926;

                this.queenStrike(
                    cut.angle,
                    cut.y + randomY,
                    cut.x + cut.angle.xr + randomX,
                    randomAngle
                );
            }
            finally
            {
                ScheduleOptimizedFlagReset();
            }
            cut.time = 0;
            cut.chainCount++;
            cut.time_max = 0.1;

            _queenStrikeStates[index] = cut;
        }
        #endregion

        #region 优化工具方法
        private bool IsValidTarget(Entity target)
        {
            return target != null && !target.destroyed;
        }

        private void RemoveStrikeState(Queencut cut, int index)
        {
            if (index >= 0 && index < _queenStrikeStates.Count)
            {
                _queenStrikeStates.RemoveAt(index);
            }

            if (cut.angle != null && _chainCounts.ContainsKey(cut.angle))
            {
                _chainCounts.Remove(cut.angle);
            }

            if (_queenStrikeStates.Count == 0)
            {
                _queenfx = false;
            }
        }

        private void ScheduleOptimizedFlagReset()
        {
            if (_queenStrikeStates.Count == 0)
            {
                _isChainAttacking = false;
                _queenfx = false;
                return;
            }

            if (base.owner?.delayer != null)
            {
                base.owner.delayer.addS("ResetChainFlag".AsHaxeString(), () =>
                {
                    _isChainAttacking = false;
                }, 0.05);
            }
            else
            {
                _isChainAttacking = false;
            }
        }

        private double GetOptimizedFrameDeltaTime()
        {
            if (_queenStrikeStates.Count > 3)
            {
                return 0.0334;
            }
            return 0.0167;
        }
        #endregion

        #region 资源清理
        public override void dispose()
        {
            base.dispose();
            _queenStrikeStates.Clear();
            _chainCounts.Clear();
            _queenfx = false;
            _isChainAttacking = false;
        }
        #endregion
    }

}