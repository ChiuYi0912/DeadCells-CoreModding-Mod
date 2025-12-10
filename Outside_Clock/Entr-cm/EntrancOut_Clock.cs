using System;
using dc;
using dc.cine;
using dc.en;
using dc.en.inter;
using dc.h3d.pass;
using dc.libs.misc;
using HaxeProxy.Runtime.Internals;
using ModCore.Utitities;
using ModCore.Modules;
using Serilog;
using HaxeProxy.Runtime;
using ModCore.Storage;

namespace Outside_Clock;

public class EntrancOut_Clock : GameCinematic
{


    Hero owen = ModCore.Modules.Game.Instance.HeroInstance!;
    public EntrancOut_Clock(Hero owen)
    {
        _EntrancOut_Clock.heroanimOut_Clock(owen, this);
    }
    public override void init()
    {
        base.init();

    }

    public override void update()
    {
        base.update();

    }
}

