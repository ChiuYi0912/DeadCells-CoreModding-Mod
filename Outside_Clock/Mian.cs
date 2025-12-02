using dc;
using dc.haxe;
using dc.level;
using dc.libs;
using dc.tool.mod;
using dc.ui.hud;
using Hashlink.Virtuals;
using ModCore.Events.Interfaces.Game;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using HashlinkNET;
using HaxeProxy.Runtime;
using dc.achievements;
using dc.h2d;
using dc.level.@struct;
using dc.tool;
using dc.en;

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

            Hook_LevelLogos.getLevelLogo += Hook_LevelLogos_getLevelLogo;

            Hook_T_SewerShort.buildMainRooms += Hook_T_SewerShort_buildMainRooms;

        }

        private RoomNode Hook_T_SewerShort_buildMainRooms(Hook_T_SewerShort.orig_buildMainRooms orig, T_SewerShort self)
        {
            RoomNode parent = self.createNode(null, "EntranceSewerDown".AsHaxeString(), null, "start".AsHaxeString());
            RoomNode roomNode = self.createNode("Collector".AsHaxeString(), null, null, null);
            RoomNode roomNode2 = roomNode.set_parent(parent);
            roomNode2 = self.createNode(null, "PerkShop".AsHaxeString(), null, null);
            RoomNode roomNode3 = roomNode2.set_parent(roomNode);
            roomNode3 = self.createNode("Healing".AsHaxeString(), null, null, null);
            RoomNode roomNode4 = roomNode3.set_parent(roomNode2);
            dc.String transitionTo = self.lInfos.transitionTo;
            RoomNode roomNode5 = self.createExit(transitionTo, null, null, "exit".AsHaxeString()).set_parent(roomNode3);
            RoomNode roomNode6 = self.createExit("Out_Clock".AsHaxeString(), null, null, "exit1".AsHaxeString()).addBefore(self.getId("exit".AsHaxeString()), null);
            return self.getId("start".AsHaxeString());
        }

        private Tile Hook_LevelLogos_getLevelLogo(Hook_LevelLogos.orig_getLevelLogo orig, LevelLogos self, dc.String levelLogoCoordinate)
        {
            if (!self.textureCoordinateByLevelKind.exists.Invoke(levelLogoCoordinate))
            {
                return orig(self, "ClockTower".AsHaxeString());
            }
            else return orig(self, levelLogoCoordinate);

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