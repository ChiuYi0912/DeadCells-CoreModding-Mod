using System;
using dc.en;
using dc.pr;
using HaxeProxy.Runtime;
using ModCore.Utitities;

namespace Outside_Clock.Clock_Mobs;

public class MobcreateMain
{
    internal static Mob Hook__Mob_create(Hook__Mob.orig_create orig, dc.String k, Level level, int cx, int cy, int dmgTier, Ref<int> lifeTier)
    {
        if (k.ToString().Equals("miniLeapingDuelyst", StringComparison.CurrentCultureIgnoreCase))
        {
            var mob1Leaping = new miniLeapingDuelyst(level, cx, cy, dmgTier, lifeTier.value);
            mob1Leaping.init();
            return mob1Leaping;
        }
        return orig(k, level, cx, cy, dmgTier, lifeTier);
    }
}
