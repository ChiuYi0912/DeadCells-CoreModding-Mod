using System;
using dc;
using dc.cine;
using dc.pr;
using MonoMod.Utils;

namespace Outside_Clock;

public class _EntrancOut_Clock
{
    public static void _int_game_cm()
    {


        _GameCinematic cinematic = GameCinematic.Class;

        cinematic.__type__.GetExport("__inst_construct__");
        _EnterGiantRoom _EnterGiant = EnterGiantRoom.Class;



    }

}
