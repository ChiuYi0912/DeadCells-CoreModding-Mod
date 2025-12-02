using dc;
using dc.level;
using dc.libs;
using dc.tool.mod;
using Hashlink.Virtuals;
using ModCore.Events.Interfaces.Game;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;

namespace Outside_Clock
{
    public class Mian : ModBase,
        IOnGameExit,
        IOnGameEndInit
    {
        public Mian(ModInfo info) : base(info)
        {

        }
        public override void Initialize()
        {
            Logger.Information("你好，世界");
            Hook__LevelStruct.get += Hook__LevelStruct_get;
        }

        private LevelStruct Hook__LevelStruct_get(Hook__LevelStruct.orig_get orig, User user, virtual_baseLootLevel_biome_bonusTripleScrollAfterBC_cellBonus_dlc_doubleUps_eliteRoomChance_eliteWanderChance_flagsProps_group_icon_id_index_loreDescriptions_mapDepth_minGold_mobDensity_mobs_name_nextLevels_parallax_props_quarterUpsBC3_quarterUpsBC4_specificLoots_specificSubBiome_transitionTo_tripleUps_worldDepth_ l, Rand rng)
        {
            var idStr = l.id.ToString();

            if (idStr == "Out_Clock")
            {
                return new Out_Clock(user, l, rng);
            }
            else return orig(user, l, rng);
        }

        void IOnGameExit.OnGameExit()
        {
            Logger.Information("游戏正在退出");
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
}