using dc;
using dc.cine;
using dc.cine.kf;
using dc.en;
using dc.en.inter;
using dc.h2d;
using dc.hl;
using dc.libs.misc;
using dc.pr;
using dc.ui;
using HaxeProxy.Runtime;
using ModCore.Mods;
using ModCore.Utitities;
using Serilog;
using static dc.h2d.BlendMode;
using Math = dc.Math;
using ModCore.Modules;
using ModCore.Events.Interfaces.Game.Hero;
using System;
using System.IO;
using ModCore;
using ModCore.Events.Interfaces.Game;
using dc.hxd;

namespace DeadCellsMultiplayer
{
    public class MultiplayerMod : ModBase, IOnHeroUpdate, IOnGameExit
    {
        public MultiplayerMod(ModInfo info) : base(info)
        {
        }

        public void OnGameExit()
        {
            throw new NotImplementedException();
        }

        public void OnHeroUpdate(double dt)
        {
            throw new NotImplementedException();
        }
    }
}