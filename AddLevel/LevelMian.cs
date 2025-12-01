using System;
using System.ComponentModel;
using dc;
using dc.en;
using dc.en.mob.boss.death;
using dc.h2d;
using dc.haxe;
using dc.hl;
using dc.hl.types;
using dc.level;
using dc.level.@struct;
using dc.libs;
using dc.shader;
using dc.tool;
using dc.tool.mod;
using Hashlink.Virtuals;
using ModCore.Events.Interfaces.Game;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using Serilog;
using Log = Serilog.Log;
using virtual_ = Hashlink.Virtuals.virtual_baseLootLevel_biome_bonusTripleScrollAfterBC_cellBonus_dlc_doubleUps_eliteRoomChance_eliteWanderChance_flagsProps_group_icon_id_index_loreDescriptions_mapDepth_minGold_mobDensity_mobs_name_nextLevels_parallax_props_quarterUpsBC3_quarterUpsBC4_specificLoots_specificSubBiome_transitionTo_tripleUps_worldDepth_;

namespace AddLevel;

public class LevelMian : ModBase, IOnAfterLoadingCDB, IOnGameEndInit
{
    public LevelMian(ModInfo info) : base(info)
    {
    }


    public override void Initialize()
    {
        base.Initialize();
        Hook__LevelStruct.get += Hook__LevelStruct_get;
    }



    void IOnAfterLoadingCDB.OnAfterLoadingCDB(_Data cdb)
    {

    }

    private LevelStruct Hook__LevelStruct_get(Hook__LevelStruct.orig_get orig, User user, virtual_ l, Rand rng)
    {

        var idStr = l.id.ToString();

        if (idStr == "SampleLevel")
        {
            return new Addsamp(user, l, rng);
        }
        else if (idStr == "Corrupt")
        {
            return new PrisonRoofCorrupt(user, l, rng);
        }

        return orig(user, l, rng);
    }

    void IOnGameEndInit.OnGameEndInit()
    {
        var res = Info.ModRoot!.GetFilePath("res.pak");
        FsPak.Instance.FileSystem.loadPak(res.AsHaxeString());
        var json = CDBManager.Class.instance.getAlteredCDB();
        dc.Data.Class.loadJson(
           json,
           default);

    }
}
