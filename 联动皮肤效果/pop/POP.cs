using dc;
using dc.en;
using dc.hxd;
using dc.libs.heaps;
using dc.libs.heaps.slib;
using dc.tool;
using HaxeProxy.Runtime;
using ModCore.Events.Interfaces;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using Serilog;

namespace POP;

public class PopATT : ModBase, IOnHeroUpdate
{
    public PopATT(ModInfo info) : base(info) { }

    public override void Initialize()
    {
        Log.Information("特殊滤镜");
        base.Initialize();

    }

    void IOnHeroUpdate.OnHeroUpdate(double dt)
    {
        SpawnCliffRain();
        //biomeAncientTemple();
        //SpawnDustStorm();
        //SpawnCastleFire();
        //SpawnSnowStorm();
        //SpawnCemeteryGhosts();
        //SpawnClockTowerAsh();
        //SpawnOssuaryGhosts();
        //SpawnShipwreckDrips();
        //SpawnThroneFoliage();
        //SpawnTumulusStorm();
        SpawnSimpleSmoke();
    }




    private void SpawnCliffRain()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        HSpriteBatch bgWaterColor = fx.bgDisplaceSb;
        int rainLayer = 0xFFFFFF;
        int? waterColor = null;
        double bgWaterColorValue = 1;
        HSpriteBatch nearBorderRatio = fx.bgNormalSb;
        fx.biomeCliff(bgWaterColor, rainLayer, waterColor, bgWaterColorValue, nearBorderRatio);

    }

    private void biomeAncientTemple()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        fx.biomeAncientTemple(0);
    }

    private void SpawnDustStorm()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        fx.biomeBase(
            0xFFFFFF,
            0.6,
            0xFFFFFF,
            0.2
        );
    }
    private void SpawnCastleFire()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        fx.biomeCastle(0xFFFF55);
    }

    private void SpawnSnowStorm()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        fx.biomeCavern();
    }

    private void SpawnCemeteryGhosts()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        fx.biomeCemetery(0, false);
    }

    private void SpawnClockTowerAsh()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;

        fx.biomeClockTower();
    }

    private void SpawnOssuaryGhosts()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        fx.biomeOssuary(0);
    }

    private void SpawnShipwreckDrips()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        double ratio = 0.9;
        fx.biomeShipwreck(ratio);
    }
    private void SpawnThroneFoliage()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        fx.biomeThrone();
    }

    private void SpawnTumulusStorm()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;
        var spawnWind = fx.bgAddSb;
        var spawnLeaves = fx.bgAddSb;
        int dustColor = 0xFFFFFF;
        double windLayer = 1.5;
        bool rainLayer = true;
        bool bgWindLayer = true;

        fx.biomeTumulus(spawnWind, spawnLeaves, dustColor, windLayer, rainLayer, bgWindLayer);
    }

    private void SpawnSimpleSmoke()
    {
        var fx = ModCore.Modules.Game.Instance.HeroInstance?._level?.fx;
        if (fx == null) return;

        var viewport = fx.viewport;

        dc._Math _Math = dc.Math.Class;
        double x = viewport.realX + _Math.random() * viewport.wid;
        double y = viewport.realY + _Math.random() * viewport.hei;

        // alloc粒子
        FxTileCache tiles = Assets.Class.fxTile;
        dc._Std _Std = dc.Std.Class;
        if (fx == null) return;
        FxTile smokeTile = tiles._snowParallaxB.getDyn(_Std.random(tiles._snowParallaxB.length));
        HParticle p = fx.allocTop(smokeTile, x, y, Ref<bool>.Null, null, Ref<bool>.Null); // 前景层

        //设置参数
        p.r = p.g = p.b = 0.8 + _Math.random() * 0.2; // 灰白烟色
        p.a = 1 + _Math.random() * 1; // 半透明
        p.scaleX = p.scaleY = 1.0 + _Math.random() * 1.5; // 大小随机
        p.dx = (_Math.random() - 0.5) * 2; // 左右微飘
        p.dy = -0.5 + _Math.random() * 0.5; // 微上飘
        p.data0 = 0.3 + _Math.random() * 0.4; // 视差深度（远近慢快差）
        p.setFadeS(0.4, 0.6, 1.2); // 0.4s淡入 → 0.6s保持 → 1.2s淡出
        p.set_lifeS(2.0 + _Math.random()); // 生命2~3秒

        // 绑定更新函数
        p.onUpdate = new HlAction<HParticle>(fx._parallax);
    }

}
