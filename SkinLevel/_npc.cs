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
using dc.libs.heaps.slib;
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
using dc.tool.weap;
using dc.ui;
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
using Weapon = dc.tool.Weapon;

namespace ChiuYiLevel
{
    //public class BuildNpc : NpcId
    //{
    //    public static _NpcId Class { get; }

    //    public override  enum  Indexes
    //    {
    //        BuildNpc = 37
    //    }

    //    public override Indexes Index { get; } =  Indexes.BuildNpc;
        
    //}
}
