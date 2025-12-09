using dc;
using dc.en;
using dc.en.bu;
using dc.tool;
using dc.tool.weap;
using HaxeProxy.Runtime;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static dc.h3d.mat.TextureFlags;
using Serilog;
using Hashlink.Virtuals;
using ModCore.Utitities;
using dc.libs.misc;
using dc.h3d.shader;
using dc.h3d.pass;

namespace Kaguya
{
    internal class Other_Starfury : Starfury
    {
        public Other_Starfury(Hero o, InventItem i) : base(o, i)
        {
        }

        public override bool onExecute()
        {
            var hero = base.owner;
            Outside_Clock.Clock_Mobs.miniLeapingDuelyst miniLeapingDuelyst = new Outside_Clock.Clock_Mobs.miniLeapingDuelyst(hero._level, hero.cx, hero.cy, 10, 10);
            miniLeapingDuelyst.init();
            return base.onExecute();
        }


        public override void fixedUpdate()
        {
            base.fixedUpdate();
        }

        public override void hitFromWeapon(Entity _cycle, Ref<int> _cycl)
        {
            base.hitFromWeapon(_cycle, _cycl);
        }
    }
}