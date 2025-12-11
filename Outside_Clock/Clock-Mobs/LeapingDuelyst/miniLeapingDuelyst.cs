using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using dc;
using dc.en;
using dc.en.mob;
using dc.h2d;
using dc.h3d.mat;
using dc.h3d.pass;
using dc.haxe;
using dc.hl;
using dc.hl.types;
using dc.hxd;
using dc.hxd.res;
using dc.libs.heaps;
using dc.libs.heaps.slib;
using dc.pr;
using dc.tool.skill;
using HaxeProxy.Runtime;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Serialization;
using ModCore.Storage;
using ModCore.Utitities;
using Serilog;
using Log = Serilog.Log;

namespace Outside_Clock.Clock_Mobs;

public class miniLeapingDuelyst : LeapingDuelyst,
    IHxbitSerializeCallback,
    IHxbitSerializable<miniLeapingDuelyst.Data>
{
    private class Data
    {
        public miniLeapingDuelyst mobminiLeapingDuelyst = null!;
        public int maxlife;
        public double sprx;
        public double spry;
    }


    public miniLeapingDuelyst(Level lvl, int x, int y, int dmgTier, int lifeTier) : base(lvl, x, y, dmgTier, lifeTier)
    {

    }

    public static SpriteLib Hook_AssetsLibManager_get(Hook_AssetsLibManager.orig_get orig, AssetsLibManager self, dc.String o)
    {
        var mob_atlas = ToolsMob.Replace_atlas(orig, self, o, "atlas/LeapingDuelyst.atlas", "atlas/miniLeapingDuelyst.atlas");
        return mob_atlas;
    }
    public static dc.hxd.res.Resource Hook_Loader_loadCache(Hook_Loader.orig_loadCache orig, Loader self, dc.String c, Class res)
    {
        var mob_n_png = ToolsMob.Hook_Loader_loadCache(orig, self, c, res, "atlas/LeapingDuelyst_n.png", "atlas/miniLeapingDuelyst_n.png");
        return mob_n_png;
    }

    public override void initGfx()
    {

        base.initGfx();
    }
    private bool heroatargrt = true;
    private bool dodgebool = false;
    public override void behaviourAi()
    {

        base.behaviourAi();
        var owen = ((double)this.cx + this.xr) * 24.0;
        if (this.aTarget != null && dodgebool == false)
        {

            if (((double)aTarget.cx + aTarget.xr) * 24.0 > owen)
            {
                owen = 1;
                var prepare = owen;
                heroatargrt = dodge.prepare((int?)prepare);
                OldSkill oldSkill2 = base.getOldSkill("jump2".AsHaxeString());
                base.queueAttack((OldMobSkill)oldSkill2, false, null);
                return;
            }

        }







    }

    private bool loadspr = false;
    public override void preUpdate()
    {
        base.preUpdate();

        if (life < maxLife / 2)
        {
            if (loadspr == false)
            {
                int length = 3;
                for (int i = 0; i < length; i++)
                {

                    sprScaleX += 0.1;
                    sprScaleY += 0.1;
                    data.maxlife = life;
                }
                loadspr = true;
            }
        }
        if (life < maxLife / 2)
        {
            this.elite = false;
            setElite(false);
            createLight(5, null, null, 1);
            data.sprx = sprScaleX;
            data.spry = sprScaleY;
            data.maxlife = life;
        }

    }
    #region 序列化

    private Data data = new();


    Data IHxbitSerializable<Data>.GetData()
    {
        data.maxlife = maxLife;
        data.sprx = sprScaleX;
        data.spry = sprScaleY;
        return data;
    }


    void IHxbitSerializable<Data>.SetData(Data data)
    {
        maxLife = data.maxlife;
        sprScaleX = data.sprx;
        sprScaleY = data.spry;
        this.data = data;
    }

    void IHxbitSerializeCallback.OnBeforeSerializing()
    {

    }
    void IHxbitSerializeCallback.OnAfterDeserializing()
    {
        Debug.Assert(data != null);
    }
    #region 贴图
    public void Hook_LeapingDuelyst_initGfx(Hook_LeapingDuelyst.orig_initGfx orig, LeapingDuelyst self)
    {
        if (self._level.map.id.ToString().Equals("Out_Clock", StringComparison.CurrentCultureIgnoreCase))
        {
            Hook_AssetsLibManager.get += miniLeapingDuelyst.Hook_AssetsLibManager_get;
            Hook_Loader.loadCache += miniLeapingDuelyst.Hook_Loader_loadCache;
        }
        else
        {
            Hook_AssetsLibManager.get -= miniLeapingDuelyst.Hook_AssetsLibManager_get;
            Hook_Loader.loadCache -= miniLeapingDuelyst.Hook_Loader_loadCache;
        }
        orig(self);
    }
    #endregion
    #endregion
}
