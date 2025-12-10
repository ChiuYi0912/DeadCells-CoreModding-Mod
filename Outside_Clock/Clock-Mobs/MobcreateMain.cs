using System;
using dc;
using dc.en;
using dc.en.mob;
using dc.pr;
using dc.tool;
using HaxeProxy.Runtime;
using ModCore.Storage;
using ModCore.Utitities;
using Serilog;

namespace Outside_Clock.Clock_Mobs;

public class MobcreateMain
{
    public readonly SaveData<Data> save = new("miniLeapingDuelyst");

    public class Data
    {

    }
    private class sData
    {
        public int build = 0;
    }
    private sData data1 = new();
    public Mob Hook__Mob_create(Hook__Mob.orig_create orig, dc.String k, Level level, int cx, int cy, int dmgTier, Ref<int> lifeTier)
    {
        if (k.ToString().Equals("miniLeapingDuelyst", StringComparison.CurrentCultureIgnoreCase))
        {
            var mob1Leaping = new miniLeapingDuelyst(level, cx, cy, dmgTier, lifeTier.value);
            if (Outside_Clock.CinematicOut_Clock_Main.Out_Clock_Enter_save.Value.createminileaping == false)
            {
                Hook_LeapingDuelyst.initGfx += mob1Leaping.Hook_LeapingDuelyst_initGfx;
                Outside_Clock.CinematicOut_Clock_Main.Out_Clock_Enter_save.Value.createminileaping = true;
                Log.Information("已修改创造mob:minilea");
            }
            mob1Leaping.init();
            // BonePillar bonePillar = new BonePillar(level, cx, cy, dmgTier, lifeTier.value);
            // bonePillar.init();
            return mob1Leaping;
        }
        return orig(k, level, cx, cy, dmgTier, lifeTier);
    }

}
