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

public class EntrancOut_Clock_heroAnim
{

    private static bool a = false;
    private static FastBoolSerializer _game;

    internal static void Hook_HiddenTrigger_trigger(Hook_HiddenTrigger.orig_trigger orig, HiddenTrigger self, Entity dh)
    {

        Hero owen = ModCore.Modules.Game.Instance.HeroInstance!;
        orig(self, dh);
        if (self.genericEventId == null)
        {
            return;
        }
        if (self.genericEventId != null)
        {
            if (!_game.Value)
            {
                Log.Debug("genericEventId不为空");
                if (self.genericEventId.ToString() == "ow111")
                {

                    EntrancOut_Clock entrancOut_ = new EntrancOut_Clock(owen);
                    //new PlayerSettings().ToggleMusic();
                    Log.Debug("获取到ow");
                    _game.Value = true;


                    var data = _game.GetData();
                    _game.SetData(data);
                }
            }



            if (self.genericEventId.ToString() == "ow2")
            {
                if (a == false)
                {
                    if (self.genericEventId.ToString() == "ow2")
                    {
                        EnterTimeKeeperRoomFirst enterTimeKeeperRoomFirst = new EnterTimeKeeperRoomFirst((Hero)dh);
                        Log.Debug("获取到ow");
                    }
                    a = true;
                }
            }
            return;
        }
    }
}