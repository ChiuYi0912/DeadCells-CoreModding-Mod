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
//using HashlinkNET;
using HaxeProxy.Runtime;
using dc.achievements;
using dc.h2d;
using dc.level.@struct;
using dc.tool;
using dc.en;
using dc.pr;
using dc.cine;
using dc.en.inter;
using System.Data.Common;
using dc.libs.misc;
using System.ComponentModel;
//using HashlinkNET.Bytecode;
using Hashlink;
using dc.cdb;
using dc.hxsl;
using System.Security.Cryptography;
using ModCore.Storage;

namespace Outside_Clock
{
    public class Mian : ModBase,
        IOnGameEndInit,
        IOnGameExit


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

            Hook_Level.attachSpecialEquipments += hook_Level_attachSpecialEquipments;

            Hook_LevelTransition.entranceWalk += Hook_LevelTransition_entranceWalk;

            dc.pr.Hook_Level.init += Hook_Level_init;

            Hook_HiddenTrigger.trigger += CinematicOut_Clock_Main.Hook_HiddenTrigger_trigger;
        }



        private void Hook_Level_init(Hook_Level.orig_init orig, Level self)
        {
            // dc.String id = self.map.biome.id;
            // if (id != null)
            // {
            //     if (id.ToString() == "ClockTower")
            //     {
            //         var map = self.map;
            //         bool a = true;
            //         var lDisp2 = new dc.level.disp.Cliff(self, map, id, "Cliff_outside".AsHaxeString());

            //     }
            // }
            orig(self);


        }

        private void Hook_LevelTransition_entranceWalk(Hook_LevelTransition.orig_entranceWalk orig, LevelTransition self, int xFrom, int xTo, Exit exit)
        {
            virtual_exit_from_to_ virtual_exit_from_to_ = new virtual_exit_from_to_();
            virtual_exit_from_to_.from = xFrom;
            virtual_exit_from_to_.to = xTo;
            virtual_exit_from_to_.exit = exit;
            self.walk = virtual_exit_from_to_;

        }

        private void hook_Level_attachSpecialEquipments(Hook_Level.orig_attachSpecialEquipments orig, Level self, Room rseed, Rand cineTrans, LevelTransition pt)
        {

            orig(self, rseed, cineTrans, pt);
            if (rseed == null) return;
            dc.String rtype = rseed.rType;
            if (@rtype != null)
            {
                if (rseed != null)
                {
                    Marker? marker = null;
                    if (marker == null)
                    {
                        try
                        {
                            bool found = true;
                            marker = rseed.getMarker("SpecialEquipment".AsHaxeString(), null, new Ref<bool>(ref found));
                            Logger.Debug("maker:不为空");
                        }
                        catch
                        {
                            Logger.Debug("majer:为空");
                        }

                        if (marker != null)
                        {
                            // if (marker.customId?.ToString() == "tower".ToString())
                            // {
                            // Hero hero = ModCore.Modules.Game.Instance.HeroInstance!;
                            // int walkxy = rseed.x + marker.cx;
                            // int roomxy = self.lastHeroCX;
                            // pt.entranceWalk(walkxy, roomxy, null);
                            //hero.say("门终于开了".AsHaxeString(), 1, hero.cx, hero.cy);
                            Logger.Debug("接管:tower");
                            Hero hero = ModCore.Modules.Game.Instance.HeroInstance!;
                            Outside_Clock.EntrancOut_Clock entrancOut_ = new Outside_Clock.EntrancOut_Clock(hero);

                            // if (hero != null && rseed != null)
                            // {
                            //     GameCinematic cm = new GameCinematic();
                            //     cm.init();
                            //     cm.update();
                            //     HlAction hlAction = new HlAction(() =>
                            //     {
                            //         hero.spr.get_anim().play("travolta".AsHaxeString(), null, null).loop(1);
                            //     });
                            //     cm.cm.__beginNewQueue();
                            //     cm.cm.__add(hlAction, 1000, null);
                            // }
                            // }

                        }
                    }
                }
            }
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



        void IOnGameEndInit.OnGameEndInit()
        {
            var res = Info.ModRoot!.GetFilePath("res.pak");
            FsPak.Instance.FileSystem.loadPak(res.AsHaxeString());
            var json = CDBManager.Class.instance.getAlteredCDB();
            dc.Data.Class.loadJson(
               json,
               default);

        }
        private static Config<Out_Clock_Config> config { get; } = new("Out_Clock_Enter");
        void IOnGameExit.OnGameExit()
        {

            config.Value.Out_Clock_Enter = false;
            config.Save();
            Logger.Debug("已初始化过场动画");
        }

    }
}