using dc;
using dc.en;
using dc.en.active;
using dc.en.inter;
using dc.en.ltrap;
using dc.en.mob;
using dc.en.mob.boss;
using dc.en.mob.boss.giant;
using dc.h2d;
using dc.h3d;
using dc.haxe.io;
using dc.hl;
using dc.hl.types;
using dc.hxbit.enumSer;
using dc.hxd.fs;
using dc.hxd.res;
using dc.hxd.snd;
using dc.pr;
using dc.tool;
using dc.tool.mod.script;
using dc.tool.utils;
using dc.tool.weap;
using HaxeProxy.Runtime;
using HaxeProxy.Runtime.Internals;
using HaxeProxy.Runtime.Internals.Cache;
using Iced.Intel;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Runtime.InteropServices;
using static dc.en.Hook_Hero;
using static dc.en.inter.Hook_FloatingPlatform;
using static dc.FoodKind;
using static dc.hxd.PixelFormat;
using static dc.hxsl.ARead;
using static dc.hxsl.Component;
using static SampleSimple.SimpleMod;

namespace SampleSimple
{
    public class SimpleMod(ModInfo info) : ModBase(info), IOnHeroUpdate
    {
        dc.pr.Game game;
        private const int VK_T = 0x54; // T 键

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetAsyncKeyState(int vkey);

        // 记录按键状态，防止重复触发
        private bool _wasTPressed = false;

        public override void Initialize()
        {
            Logger.Information("英雄创造模组已加载!");
            Hook_Game.init += Hook_mygameinit;
        }

        public void Hook_mygameinit(Hook_Game.orig_init orig, dc.pr.Game self)
        {
            game = self;
            orig(self);
        }

        void IOnHeroUpdate.OnHeroUpdate(double dt)
        {
            bool isTPressed = (GetAsyncKeyState(VK_T) & 0x8000) != 0;

            if (isTPressed && !_wasTPressed)
            {
                CreateHeroAtPlayerPosition();
            }

            _wasTPressed = isTPressed;
        }

        private void CreateHeroAtPlayerPosition()
        {
            Hero me = ModCore.Modules.Game.Instance.HeroInstance;
            if (me == null) return;

            Hero hero = Hero.Class.create(game, "Richter".AsHaxeString());
            hero.init();
            hero.awake = false;
            hero.set_level(me._level);
            hero.set_team(me._team);
            hero.initGfx();
            hero.setPosCase(me.cx, me.cy, me.xr, me.yr);
            hero.visible = true;
            hero.initAnims();
            hero.wakeup(me._level, me.cx, me.cy);

            Logger.Information($"在位置 ({me.cx}, {me.cy}) 创建了新英雄");


        }
    }
}