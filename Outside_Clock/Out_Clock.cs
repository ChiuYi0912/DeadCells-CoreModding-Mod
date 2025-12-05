using System;
using Amazon.Runtime.Internal.Util;
using dc;
using dc.haxe;
using dc.hl.types;
using dc.level;
using dc.level.gen.mapbuilder;
using dc.libs;
using dc.tool;
using Hashlink.Virtuals;
using ModCore.Utitities;
using Serilog;
using Serilog.Debugging;
using Log = Serilog.Log;

namespace Outside_Clock;

public class Out_Clock : LevelStruct
{

    public Out_Clock(User user, virtual_baseLootLevel_biome_bonusTripleScrollAfterBC_cellBonus_dlc_doubleUps_eliteRoomChance_eliteWanderChance_flagsProps_group_icon_id_index_loreDescriptions_mapDepth_minGold_mobDensity_mobs_name_nextLevels_parallax_props_quarterUpsBC3_quarterUpsBC4_specificLoots_specificSubBiome_transitionTo_tripleUps_worldDepth_ lInfos, Rand rng) : base(user, lInfos, rng)
    {

    }

    public override RoomNode buildMainRooms()
    {
        #region 入口
        RoomNode entranceNode = base.createNode(null, "RoofEntrance".AsHaxeString(), null, "start".AsHaxeString());
        entranceNode.AddFlags(new RoomFlag.NoExitSizeCheck(), new RoomFlag.Outside());
        entranceNode.setConstraint(new LinkConstraint.RightOnly());

        var forcedBiome = "ClockTower".AsHaxeString();
        var virtual_add = new virtual_specificBiome_();
        virtual_add.specificBiome = forcedBiome;
        var clockTowerGenData = virtual_add.ToVirtual<virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_>();


        entranceNode.addGenData(clockTowerGenData);
        #endregion



        #region 入口向右连接
        RoomNode combatNode = base.createNode(null, "OutsideTower4".AsHaxeString(), null, "A1".AsHaxeString())
            .AddFlags(new RoomFlag.Outside(), new RoomFlag.Holes());

        combatNode.set_parent(entranceNode);
        #endregion




        #region 入口后向上连接
        RoomNode up = base.createNode("Teleport".AsHaxeString(), null, null, "upOnly".AsHaxeString())
            .addFlag(new RoomFlag.Holes())
            .setConstraint(new LinkConstraint.UpOnly());


        up.set_parent(entranceNode);
        #endregion




        #region 入口后向左连接
        RoomNode combatNode1 = base.createNode("Teleport".AsHaxeString(), null, null, "A2".AsHaxeString())
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .setConstraint(new LinkConstraint.LeftOnly());

        combatNode1.set_parent(entranceNode);
        #endregion





        #region 入口向右连接（暂留）
        RoomNode demonNode = base.createNode(null, "OutsideCross1".AsHaxeString(), null, "exit".AsHaxeString())
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .addNpc(new NpcId.CryptDemon());

        demonNode.set_parent(combatNode);

        RoomNode demonNode1 = base.createNode(null, "Teleport_UD".AsHaxeString(), null, null)
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .addNpc(new NpcId.CryptDemon());

        demonNode1.set_parent(demonNode);

        RoomNode demonNode2 = base.createNode(null, "Teleport_UD".AsHaxeString(), null, "B1".AsHaxeString())
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .addNpc(new NpcId.CryptDemon());

        demonNode2.set_parent(demonNode1);


        RoomNode knightNode = base.createNode("Combat".AsHaxeString(), null, null, null)
            .addNpc(new NpcId.Knight());

        knightNode.set_parent(demonNode);
        #endregion





        #region 出口前向上连接
        RoomNode knightNode1 = base.createNode("Teleport".AsHaxeString(), null, null, "up2".AsHaxeString())
            .AddFlags(new RoomFlag.Holes(), new RoomFlag.Outside())
            .setConstraint(new LinkConstraint.UpOnly());

        knightNode1.set_parent(knightNode);
        #endregion




        #region 出口
        RoomNode exitNode = base.createExit("T_PrisonCorrupt".AsHaxeString(), "RoofEndExit".AsHaxeString(), null, "end".AsHaxeString())
        .addFlag(new RoomFlag.Outside())
        .addFlag(new RoomFlag.Holes());
        exitNode.set_parent(knightNode);
        #endregion



        return base.nodes.get("start".AsHaxeString());
    }

    public override void addTeleports()
    {

    }

    #region 约束配置
    public override void finalize()
    {
        // int num = 0;
        // ArrayObj all = base.all;
        // for (; ; )
        // {
        //     int length = all.length;
        //     if (num >= length)
        //     {
        //         break;
        //     }
        //     length = all.length;
        //     RoomNode? roomNode;
        //     if (num >= length)
        //     {
        //         roomNode = null;
        //     }
        //     else
        //     {

        //         roomNode = all.array[num] as RoomNode;
        //     }
        //     num++;
        //     if (roomNode != null && roomNode.parent != null)
        //     {
        //         if ((roomNode.flags & 8) != 0)
        //         {
        //             roomNode.childPriority = 1;

        //             RoomNode roomNode2 = roomNode.setConstraint(new LinkConstraint.NeverUp());
        //         }
        //         else if ((roomNode.parent.flags & 8) != 0)
        //         {

        //             RoomNode roomNode2 = roomNode.setConstraint(new LinkConstraint.NeverUp());
        //         }
        //     }
        // }


    }
    #endregion



    public override void buildSecondaryRooms()
    {
        base.buildSecondaryRooms();

        #region 配置
        dc.String exitId = "exit".AsHaxeString();
        dc.String upOnlyId = "upOnly".AsHaxeString();
        dc.String a2Id = "A2".AsHaxeString();
        dc.String b1Id = "B1".AsHaxeString();
        dc.String up2id = "up2".AsHaxeString();

        RoomNode exitNode = base.getId(exitId);
        RoomNode upOnlyNode = base.getId(upOnlyId);
        RoomNode a2Node = base.getId(a2Id);
        RoomNode b1Node = base.getId(b1Id);
        RoomNode up2Node = base.getId(up2id);
        #endregion




        #region 出口前支线
        RoomNode roomNode = base.createNode("Combat".AsHaxeString(), null, null, null)
            .addFlag(new RoomFlag.Outside())
            .addBefore(exitNode, null);


        int[] exitCombatData = new int[] { 69, 1, 7, 3, 2, 6, 1 };
        int i = 0;
        for (; ; )
        {
            if (i >= exitCombatData.Length) break;
            int data = exitCombatData[i];
            i++;

            roomNode = base.createNode(null, "TU_teleportSecret".AsHaxeString(), null, null)
            .AddFlags(new RoomFlag.Outside(), new RoomFlag.Holes())
                .addBefore(exitNode, null);

            roomNode = base.createNode("Combat".AsHaxeString(), null, data, null)
               .AddFlags(new RoomFlag.Outside(), new RoomFlag.Holes())
                .addBefore(exitNode, null);
        }


        roomNode = base.createNode("Shop".AsHaxeString(), null, null, null)
           .AddFlags(new RoomFlag.Outside(), new RoomFlag.Holes())
            .addBefore(exitNode, null);
        #endregion




        #region 入口向上连接支线
        List<RoomNode> roomList = new List<RoomNode>();
        i = 0;
        for (; ; )
        {
            if (i >= 3) break;
            i++;

            RoomNode roomNodebetween1 = base.createNode("Teleport".AsHaxeString(), null, null, null)
            .AddFlags(new RoomFlag.Outside())
            .setConstraint(new LinkConstraint.UpOnly())
            .addBefore(upOnlyNode, null);
            roomList.Add(roomNodebetween1);

            RoomNode roomNodebetween = base.createNode("Combat".AsHaxeString(), null, null, null)
            .AddFlags(new RoomFlag.Outside())
            .setConstraint(new LinkConstraint.UpOnly())
            .addBefore(upOnlyNode, null);

            roomList.Add(roomNodebetween);


            RoomNode roomNodebetween2 = base.createNode(null, "CT_VSpacer".AsHaxeString(), null, null)
            .AddFlags(new RoomFlag.Outside())
            .setConstraint(new LinkConstraint.UpOnly())
            .addBefore(upOnlyNode, null);

            roomList.Add(roomNodebetween2);
        }

        for (i = 0; i < roomList.Count; i++)
        {
            RoomNode current = roomList[i];
            var LevelLogos = current.ToString();
            Log.Debug("入口向上的房间数组:{LevelLogos}", LevelLogos);
        }

        #endregion




        #region 出口前向上
        i = 0;
        for (; ; )
        {
            if (i >= 3) break;
            i++;
            roomNode = base.createNode("Teleport".AsHaxeString(), null, null, null)
            .AddFlags(new RoomFlag.Outside())
            .setConstraint(new LinkConstraint.UpOnly())
            .addBefore(up2Node, null);

            roomNode = base.createNode("Combat".AsHaxeString(), null, null, null)
            .AddFlags(new RoomFlag.Outside())
            .setConstraint(new LinkConstraint.UpOnly())
            .addBefore(up2Node, null);

            roomNode = base.createNode(null, "CT_VSpacer".AsHaxeString(), null, null)
            .AddFlags(new RoomFlag.Outside())
            .setConstraint(new LinkConstraint.UpOnly())
            .addBefore(up2Node, null);
        }
        #endregion




        roomNode = base.createNode("Combat".AsHaxeString(), null, null, null)
            .setConstraint(new LinkConstraint.All());





        #region 左连接支线
        int[] a2NodeLeftOnly = new int[] { 93, 93, 93 };
        i = 0;
        for (; ; )
        {
            if (i >= a2NodeLeftOnly.Length) break;
            int data = a2NodeLeftOnly[i];
            i++;

            roomNode = base.createNode(null, "TU_teleport0".AsHaxeString(), null, null)
            .setConstraint(new LinkConstraint.LeftOnly())
            .AddFlags(new RoomFlag.Outside(), new RoomFlag.Holes())

            .addBefore(a2Node, null);

            roomNode = base.createNode("Combat".AsHaxeString(), null, data, null)
                .AddFlags(new RoomFlag.Outside(), new RoomFlag.Holes())
                .setConstraint(new LinkConstraint.LeftOnly())

                .addBefore(a2Node, null);
        }

        roomNode = base.createNode("CursedTreasure".AsHaxeString(), null, null, null)
            .AddFlags(new RoomFlag.Outside(), new RoomFlag.Holes())
            .setConstraint(new LinkConstraint.LeftOnly())
            .addBefore(a2Node, null);

        #endregion




        #region 地下连接支线
        roomNode = base.createNode("Combat".AsHaxeString(), null, null, null)
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .setConstraint(new LinkConstraint.DownOnly())
            .addZChild(b1Node, null);

        roomNode = base.createNode("Teleport".AsHaxeString(), null, null, null)
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .setConstraint(new LinkConstraint.DownOnly())
            .addAfter(b1Node, null);
        #endregion




    }
}

