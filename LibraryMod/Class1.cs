using dc;
using dc.en;
using dc.h3d;
using dc.hl.types;
using dc.tool;
using dc.tool.atk;
using dc.tool.weap;
using HaxeProxy.Runtime;
using System;
using static dc.hxsl.Component;

namespace SampleSimple
{
    public class OtherDashSword : DashSword
    {
        public static string name = "OtherDashSword";
        private Hero hero;
        private dc.h2d.Object parent;

        // 攻击类型配置
        private enum AttackType { RisingSword, FallingSword }
        private AttackType currentAttackType = AttackType.RisingSword;

        public OtherDashSword(Hero hero, InventItem item) : base(hero, item)
        {
            this.hero = hero;
        }

        public override bool onExecute()
        {
            Log.Information("OtherDashSword 发动特殊攻击");

            // 切换攻击模式
            currentAttackType = (currentAttackType == AttackType.RisingSword)
                ? AttackType.FallingSword
                : AttackType.RisingSword;

            // 根据当前类型执行不同攻击
            switch (currentAttackType)
            {
                case AttackType.RisingSword:
                    ExecuteRisingSwordAttack();
                    break;

                case AttackType.FallingSword:
                    ExecuteFallingSwordAttack();
                    break;
            }

            return base.onExecute();
        }

        private void ExecuteRisingSwordAttack()
        {
            // 1A: 从地面向上冒出的巨剑
            CreateRisingGreatsword();

            // 在巨剑两侧生成弹射弹
            CreateRicochetProjectiles();

            // 播放特效和音效
            PlayAttackEffects("RisingSword");
        }

        private void ExecuteFallingSwordAttack()
        {
            // 2A: 从上方插入地面的巨剑
            CreateFallingGreatsword();

            // 在巨剑两侧生成母射弹
            CreateMotherProjectiles();

            // 播放特效和音效
            PlayAttackEffects("FallingSword");
        }

        private void CreateRisingGreatsword()
        {
            // 计算巨剑生成位置（角色前方）
            double frontDistance = 2.5; // 距离角色前方2.5单位
            double swordWidth = 1.8;    // 巨剑宽度

            // 创建巨剑实体
            GreatswordEntity sword = new GreatswordEntity(
                owner: hero,
                x: hero.x + frontDistance * hero.getFacingDirection(),
                y: hero.y - 2.0, // 从地面下方开始
                width: swordWidth,
                height: 4.0,
                damage: GetSwordDamage(),
                riseSpeed: 8.0,
                maxHeight: hero.y + 3.0
            );

            // 设置巨剑特效
            sword.SetVisualEffect("RisingSwordTrail");
            sword.SetHitEffect("GroundCrack");

            // 注册到游戏世界
            Game.inst.addEntity(sword);
        }

        private void CreateFallingGreatsword()
        {
            // 计算巨剑生成位置（角色前方上方）
            double frontDistance = 2.0; // 距离角色前方2单位
            double startHeight = hero.y + 5.0; // 起始高度
            double swordWidth = 1.8;    // 巨剑宽度

            // 创建巨剑实体
            GreatswordEntity sword = new GreatswordEntity(
                owner: hero,
                x: hero.x + frontDistance * hero.getFacingDirection(),
                y: startHeight,
                width: swordWidth,
                height: 4.0,
                damage: GetSwordDamage(),
                fallSpeed: 10.0,
                penetrateDepth: 1.5 // 插入地面的深度
            );

            // 设置巨剑特效
            sword.SetVisualEffect("FallingSwordTrail");
            sword.SetHitEffect("ImpactCrater");

            // 注册到游戏世界
            Game.inst.addEntity(sword);
        }

        private void CreateRicochetProjectiles()
        {
            // 在巨剑两侧生成弹射弹
            double swordX = hero.x + 2.5 * hero.getFacingDirection();
            double offsetX = 1.2; // 距离巨剑中心的偏移

            // 左侧弹射弹
            RicochetProjectile leftProj = new RicochetProjectile(
                owner: hero,
                x: swordX - offsetX,
                y: hero.y + 1.0,
                direction: -1.0, // 向左
                speed: 6.0,
                ricochetCount: 3
            );
            Game.inst.addEntity(leftProj);

            // 右侧弹射弹
            RicochetProjectile rightProj = new RicochetProjectile(
                owner: hero,
                x: swordX + offsetX,
                y: hero.y + 1.0,
                direction: 1.0, // 向右
                speed: 6.0,
                ricochetCount: 3
            );
            Game.inst.addEntity(rightProj);
        }

        private void CreateMotherProjectiles()
        {
            // 在巨剑两侧生成母射弹
            double swordX = hero.x + 2.0 * hero.getFacingDirection();
            double offsetX = 1.2; // 距离巨剑中心的偏移

            // 左侧母射弹
            MotherProjectile leftProj = new MotherProjectile(
                owner: hero,
                x: swordX - offsetX,
                y: hero.y + 1.0,
                direction: -1.0, // 向左
                speed: 5.0,
                splitCount: 3 // 分裂为3个子射弹
            );
            Game.inst.addEntity(leftProj);

            // 右侧母射弹
            MotherProjectile rightProj = new MotherProjectile(
                owner: hero,
                x: swordX + offsetX,
                y: hero.y + 1.0,
                direction: 1.0, // 向右
                speed: 5.0,
                splitCount: 3 // 分裂为3个子射弹
            );
            Game.inst.addEntity(rightProj);
        }

        private double GetSwordDamage()
        {
            // 基于武器等级和角色属性计算伤害
            return hero.getBrutality() * 1.5 + item.tier * 2.0;
        }

        private void PlayAttackEffects(string attackType)
        {
            // 播放攻击音效
            SoundManager.Play($"Sword_{attackType}");

            // 播放屏幕震动效果
            CameraFX.Shake(0.3f, 0.15f);

            // 播放角色攻击动画
            hero.playAnim($"Attack_{attackType}");
        }
    }

    // 巨剑实体类
    public class GreatswordEntity : Entity
    {
        public GreatswordEntity(
            Hero owner,
            double x, double y,
            double width, double height,
            double damage,
            double moveSpeed,
            double penetrateDepth = 0.0
        ) : base(owner._level, x, y)
        {
            // 初始化物理属性
            setHitbox(width, height);
            setDamage(damage);
            setOwner(owner);

            // 设置移动行为
            if (penetrateDepth > 0)
            {
                // 下落插入地面的巨剑
                setVelocity(0, -moveSpeed);
                setPenetrationDepth(penetrateDepth);
            }
            else
            {
                // 从地面升起的巨剑
                setVelocity(0, moveSpeed);
            }
        }

        // ... 其他巨剑特有的方法和属性
    }

    // 弹射弹实体类
    public class RicochetProjectile : Projectile
    {
        private int remainingRicochets;

        public RicochetProjectile(
            Hero owner,
            double x, double y,
            double direction,
            double speed,
            int ricochetCount
        ) : base(owner, x, y, direction, speed)
        {
            this.remainingRicochets = ricochetCount;
            setVisualEffect("RicochetTrail");
        }

        public override void onCollide(Entity other)
        {
            if (other is Enemy)
            {
                // 对敌人造成伤害
                other.takeDamage(getDamage());

                // 减少弹射次数
                if (remainingRicochets > 0)
                {
                    remainingRicochets--;
                    // 改变方向（弹射）
                    setDirection(-getDirection());
                    setSpeed(getSpeed() * 0.8); // 每次弹射速度降低
                }
                else
                {
                    destroy();
                }
            }
        }
    }

    // 母射弹实体类
    public class MotherProjectile : Projectile
    {
        private int splitCount;

        public MotherProjectile(
            Hero owner,
            double x, double y,
            double direction,
            double speed,
            int splitCount
        ) : base(owner, x, y, direction, speed)
        {
            this.splitCount = splitCount;
            setVisualEffect("MotherProjectileGlow");
        }

        public override void onCollide(Entity other)
        {
            if (other is Enemy)
            {
                // 对敌人造成伤害
                other.takeDamage(getDamage());

                // 分裂为子射弹
                if (splitCount > 0)
                {
                    SplitIntoChildProjectiles();
                    splitCount = 0;
                }
                destroy();
            }
        }

        private void SplitIntoChildProjectiles()
        {
            for (int i = 0; i < 3; i++) // 分裂为3个子射弹
            {
                double angle = -45 + i * 45; // -45°, 0°, 45°
                double rad = angle * Math.PI / 180;

                ChildProjectile child = new ChildProjectile(
                    owner: getOwner(),
                    x: x,
                    y: y,
                    directionX: Math.Cos(rad),
                    directionY: Math.Sin(rad),
                    speed: 4.0,
                    damage: getDamage() * 0.6
                );
                Game.inst.addEntity(child);
            }
        }
    }

    // 子射弹实体类
    public class ChildProjectile : Projectile
    {
        public ChildProjectile(
            Hero owner,
            double x, double y,
            double directionX, double directionY,
            double speed,
            double damage
        ) : base(owner, x, y, directionX, speed)
        {
            setDirectionY(directionY);
            setDamage(damage);
            setVisualEffect("ChildProjectileSpark");
        }
    }
}
