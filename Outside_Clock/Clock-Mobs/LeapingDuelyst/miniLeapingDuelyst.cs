using System;
using dc.en.mob;
using dc.pr;

namespace Outside_Clock.Clock_Mobs;

public class miniLeapingDuelyst : LeapingDuelyst
{
    public miniLeapingDuelyst(Level lvl, int x, int y, int dmgTier, int lifeTier) : base(lvl, x, y, dmgTier, lifeTier)
    {
    }
    public override void initGfx()
    {
        base.initGfx();

    }

}
