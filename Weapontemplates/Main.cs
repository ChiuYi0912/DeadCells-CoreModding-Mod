using HaxeProxy.Runtime;
using ModCore.Events.Interfaces.Game;
using ModCore.Mods;
using ModCore.Utitities;
using Hero = dc.en.Hero;
using dc.en;
using ModCore.Events.Interfaces;
using ModCore.Modules;
using dc.hxd;
using dc.tool;
using Weapontemplates;
using SharpPdb.Windows.SymbolRecords;
using System.Collections.Generic;
using System;


namespace Weapontemplates;

public class Main(ModInfo info) : ModBase(info),
        IOnGameExit,
        IOnAfterLoadingAssets
{

    private static readonly Dictionary<string, Func<Hero, InventItem, Weapon>> _weaponFactories = new()
    {
        ["OtherDashSword"] = (o, i) => new WeaponMain(o, i),

    };


    public override void Initialize()
    {
        Logger.Information("ChiuYi:你好");
        Hook__Weapon.create += Hook__Weapon_create;
    }


    void IOnAfterLoadingAssets.OnAfterLoadingAssets()
    {
        var res = Info.ModRoot!.GetFilePath("res.pak");
        FsPak.Instance.FileSystem.loadPak(res.AsHaxeString());

    }


    void IOnGameExit.OnGameExit()
    {
        Logger.Information("Game is exit");
    }

    private Weapon Hook__Weapon_create(Hook__Weapon.orig_create orig, Hero o, InventItem i)
    {

        var itemId = i._itemData.id.ToString();

        if (_weaponFactories.TryGetValue(itemId, out var factory))
        {
            Logger.Debug($"创建自定义武器: {itemId}");
            return factory(o, i);
        }

        return orig(o, i);

    }

}
