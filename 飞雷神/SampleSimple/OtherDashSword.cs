using Amazon.DynamoDBv2.Model;
using dc;
using dc.en;
using dc.en.dookuInteractions;
using dc.en.hero;
using dc.en.inter;
using dc.en.mob;
using dc.h2d;
using dc.hl.types;
using dc.hxsl;
using dc.libs.misc;
using dc.light;
using dc.pow;
using dc.pr;
using dc.pr.infection;
using dc.tool;
using dc.tool.atk;
using dc.tool.mod;
using dc.tool.mod.script;
using dc.tool.weap;
using dc.ui;
using dc.ui.sel;
using Hashlink;
using Hashlink.Marshaling;
using Hashlink.Proxy;
using Hashlink.Proxy.Clousre;
using Hashlink.Proxy.DynamicAccess;
using Hashlink.Proxy.Objects;
using Hashlink.Proxy.Values;
using Hashlink.Virtuals;
using HaxeProxy.Runtime;
using HaxeProxy.Runtime.Internals;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using Newtonsoft.Json;
using Serilog;
using System;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using static dc.en.mob.boss.BCMode;
using static dc.en.mob.Variant;
using static dc.h3d.mat.TextureFlags;
using static dc.hxsl.Component;
using static dc.libs.data.SoundType;
using static dc.tool.Area;
using static dc.tool.InventItemKind;
using Console = dc.ui.Console;
using Hero = dc.en.Hero;
using Weapon = dc.tool.Weapon;

namespace SampleSimple
{
    public class OtherDashSword : DashSword
        
    {
        public static string name = "OtherDashSword";
        private Hero hero;
        private dc.h2d.Object parent;

        public OtherDashSword(Hero hero, InventItem item) : base(hero, item)
        {
            this.hero = hero;


        }


        public override void fixedUpdate()
        {
            base.fixedUpdate();
            
            
        }


        public override bool onExecute()
        {

            Log.Information("OtherDashSword 造成伤害");
            dc.pow.Dash a = new dc.pow.Dash(hero, item, true);

            a.startX = hero.cx;
            a.startY = hero.cy;
            a.startCX = hero.cx;
            a.startCY = hero.cy;
            a.range = 5.0;
            a.dir = -hero.dir;
            a.isBack = true;
            a.cd = hero.cd ;
            a.setDurationS(0.5f);
            a.onOwnerDeath();
            
            a.isDashing = true;
            a.setDurationS(1);
            a.onDurationEnd();
    
            

            return base.onExecute();


        }   }

    }

  



