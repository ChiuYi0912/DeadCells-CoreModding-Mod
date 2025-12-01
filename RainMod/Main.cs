using dc;
using dc.h2d;
using dc.h3d.mat;
using dc.h3d.pass;
using dc.haxe;
using dc.level.@struct;
using dc.libs;
using dc.libs.heaps;
using dc.libs.heaps.slib;
using dc.tool;
using dc.tool.atk;
using dc.tool.weap;
using HaxeProxy.Runtime;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Utitities;
using Serilog;
using System.Collections.Generic;
using System.Security.Cryptography;
using static dc.h3d.mat.TextureFlags;
using static dc.hxsl.Component;
using static dc.tool.weap.Hook_QueenRapier;
using Hero = dc.en.Hero;
using Log = Serilog.Log;
using System;
using dc.level.disp;
using dc.en;
using ModCore.Events.Interfaces;
using ModCore.Modules;
using dc.hxd;


namespace RainMod;

public class RainMain(ModInfo info) : ModBase(info),
        IOnGameExit,
        IOnGameEndInit,
        IOnAfterLoadingAssets
{
    public override void Initialize()
    {
        Logger.Information("ChiuYi:你好");
        Hook_Hero.addMoney += hook_hero_addmony;
    }

    private void hook_hero_addmony(Hook_Hero.orig_addMoney orig, Hero self, int noStats, Ref<bool> noStat)
    {
        orig(self, noStats, noStat);
        var test1 = Res.Class.load("png/QQ20251112-001235.png".AsHaxeString());
        Logger.Information("The content of test1.txt is {text}", test1.toImage());
        test1.toSound();
    }

    void IOnAfterLoadingAssets.OnAfterLoadingAssets()
    {
        var res = Info.ModRoot!.GetFilePath("res.pak");
        FsPak.Instance.FileSystem.loadPak(res.AsHaxeString());
    }

    void IOnGameEndInit.OnGameEndInit()
    {


    }

    void IOnGameExit.OnGameExit()
    {
        Logger.Information("Game is exit");
    }
}
