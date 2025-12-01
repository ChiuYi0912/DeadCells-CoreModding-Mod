using dc;
using dc.cine;
using dc.en;
using dc.en.dookuInteractions;
using dc.en.hero;
using dc.en.inter;
using dc.en.inter.npc;
using dc.en.ltrap;
using dc.en.mob;
using dc.h2d;
using dc.haxe;
using dc.hl;
using dc.hl.types;
using dc.hxd;
using dc.hxd.snd;
using dc.hxsl;
using dc.level;
using dc.level.@struct;
using dc.libs;
using dc.libs.misc;
using dc.pow;
using dc.pr;
using dc.pr.infection;
using dc.steam;
using dc.steam.ugc;
using dc.tool;
using dc.tool.atk;
using dc.tool.mod;
using dc.tool.mod.script;
using dc.tool.mv;
using dc.tool.utils;
using dc.tool.weap;
using dc.ui;
using dc.ui.hud;
using dc.ui.popd;
using dc.ui.sel;
using dc.uicore;
using Hashlink;
using Hashlink.Marshaling;
using Hashlink.Proxy;
using Hashlink.Proxy.Clousre;
using Hashlink.Proxy.DynamicAccess;
using Hashlink.Proxy.Objects;
using Hashlink.Proxy.Values;
using Hashlink.Virtuals;
using Hashlink.Wrapper.Callbacks;
using HaxeProxy.Runtime;
using HaxeProxy.Runtime.Internals;
using ModCore.Events;
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
using static dc.DamageType;
using static dc.en.mob.boss.BossAction;
using static dc.en.mob.Variant;
using static dc.h3d.mat.TextureFlags;
using static dc.Hook_Fx;
using static dc.hxbit.PropTypeDesc;
using static dc.hxsl.Channel;
using static dc.hxsl.Component;
using static dc.libs.data.SoundType;
using static dc.tool.Area;
using static dc.tool.InventItemKind;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Console = dc.ui.Console;
using Hero = dc.en.Hero;
using Log = Serilog.Log;
using Object = dc.h2d.Object;
using Text = dc.h2d.Text;
using Weapon = dc.tool.Weapon;


namespace FxMod
{

   
    public class FxMod : ModBase,
        IOnHeroUpdate
    {
        private Object parent;

        public FxMod(ModInfo info) : base(info)
        {
            Log.Information("MyTextMod 运行");

            
        }

        public override void Initialize()
        {
            Hero hero = ModCore.Modules.Game.Instance.HeroInstance;
            
            base.Initialize();

            
            Object parent = new Object(null);
            Object child1 = new Object(parent);
            Object child2 = new Object(parent);

            
            parent.addChild(child1);
            parent.addChildAt(child2, 0);
            
            string a = "smokeBomb";
            dc.String b = StringUtils.ToDCString(a);

            // 查找对象
            Object found = parent.getObjectByName(b);
            Fx fx = new Fx(hero._level , parent, child1, child2);
            fx.smokeBomb (hero.cy, 100, 10, 10, 0,100);

        }
        public void OnHeroUpdate(double dt)
        {
            

        }





        
    }

}

