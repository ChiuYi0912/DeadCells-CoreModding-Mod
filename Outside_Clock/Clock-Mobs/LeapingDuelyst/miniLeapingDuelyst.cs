using System;
using System.Runtime.CompilerServices;
using dc;
using dc.en;
using dc.en.mob;
using dc.h3d.mat;
using dc.h3d.pass;
using dc.hl;
using dc.hxd;
using dc.hxd.res;
using dc.libs.heaps;
using dc.libs.heaps.slib;
using dc.pr;
using HaxeProxy.Runtime;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using Serilog;

namespace Outside_Clock.Clock_Mobs;

public class miniLeapingDuelyst : LeapingDuelyst
{
    public miniLeapingDuelyst(Level lvl, int x, int y, int dmgTier, int lifeTier) : base(lvl, x, y, dmgTier, lifeTier)
    {
    }

    private SpriteLib Hook_AssetsLibManager_get(Hook_AssetsLibManager.orig_get orig, AssetsLibManager self, dc.String o)
    {
        var mob_atlas = ToolsMob.Replace_atlas(orig, self, o, "atlas/LeapingDuelyst.atlas", "atlas/miniLeapingDuelyst.atlas");
        return mob_atlas;
    }
    private Resource Hook_Loader_loadCache(Hook_Loader.orig_loadCache orig, Loader self, dc.String c, Class res)
    {
        if (this == null)
        {
            return orig(self, c, res);
        }
        else if (c.ToString().Equals("atlas/LeapingDuelyst_n.png", StringComparison.CurrentCultureIgnoreCase))
        {
            Log.Debug($" 法线图替换: {c.ToString()} -> atlas/minHeapingDuelyst_n.png");
            return orig(self, "atlas/miniLeapingDuelyst_n.png".AsHaxeString(), res);
        }
        return orig(self, c, res);
    }

    public override void initGfx()
    {

        Hook_AssetsLibManager.get += Hook_AssetsLibManager_get;
        Hook_Loader.loadCache += Hook_Loader_loadCache;
        Log.Debug("判断成功");
        base.initGfx();
        Hook_AssetsLibManager.get -= Hook_AssetsLibManager_get;
        Hook_Loader.loadCache -= Hook_Loader_loadCache;
    }


}
