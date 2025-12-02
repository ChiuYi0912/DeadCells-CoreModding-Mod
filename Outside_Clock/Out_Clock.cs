using System;
using dc;
using dc.hl.types;
using dc.level;
using dc.libs;
using dc.tool;
using Hashlink.Virtuals;
using ModCore.Utitities;

namespace Outside_Clock;

public class Out_Clock : LevelStruct
{
    public Out_Clock(User user, virtual_baseLootLevel_biome_bonusTripleScrollAfterBC_cellBonus_dlc_doubleUps_eliteRoomChance_eliteWanderChance_flagsProps_group_icon_id_index_loreDescriptions_mapDepth_minGold_mobDensity_mobs_name_nextLevels_parallax_props_quarterUpsBC3_quarterUpsBC4_specificLoots_specificSubBiome_transitionTo_tripleUps_worldDepth_ level, Rand rng) : base(user, level, rng)
    {

    }

    public override RoomNode buildMainRooms()
    {

        RoomNode entranceNode = base.createNode(null, "TU_BasicEntrance".AsHaxeString(), null, "start".AsHaxeString());
        entranceNode.addFlag(new RoomFlag.Outside());
        entranceNode.addFlag(new RoomFlag.NoExitSizeCheck());
        entranceNode.addFlag(new RoomFlag.Holes());
        entranceNode.setConstraint(new LinkConstraint.NeverUp());


        var forcedBiome = "ClockTower".AsHaxeString();
        var virtual_add = new virtual_specificBiome_();
        virtual_add.specificBiome = forcedBiome;
        var clockTowerGenData = virtual_add.ToVirtual<virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_>();


        entranceNode.addGenData(clockTowerGenData);


        RoomNode combatNode = base.createNode(null, "OutsideTower4".AsHaxeString(), null, null)
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes());


        combatNode.set_parent(entranceNode);


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

        RoomNode demonNode2 = base.createNode(null, "Teleport_UD".AsHaxeString(), null, null)
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .addNpc(new NpcId.CryptDemon());

        demonNode2.set_parent(demonNode1);



        RoomNode knightNode = base.createNode(null, "RoofSpacer1".AsHaxeString(), null, null)
            .addFlag(new RoomFlag.DarkRoom())
            .addFlag(new RoomFlag.Outside())
            .addFlag(new RoomFlag.Holes())
            .addNpc(new NpcId.Knight());


        knightNode.set_parent(demonNode);




        RoomNode exitNode = base.createExit("T_PrisonCorrupt".AsHaxeString(), "RoofEndExit".AsHaxeString(), null, "end".AsHaxeString())
        .addFlag(new RoomFlag.Outside())
        .addFlag(new RoomFlag.Holes());
        exitNode.set_parent(knightNode);


        return base.nodes.get("start".AsHaxeString());
    }

    public override void addTeleports()
    {

    }

    public override void finalize()
    {
        int num = 0;
        ArrayObj all = base.all;
        for (; ; )
        {
            int length = all.length;
            if (num >= length)
            {
                break;
            }
            length = all.length;
            RoomNode? roomNode;
            if (num >= length)
            {
                roomNode = null;
            }
            else
            {

                roomNode = all.array[num] as RoomNode;
            }
            num++;
            if (roomNode != null && roomNode.parent != null)
            {
                if ((roomNode.flags & 8) != 0)
                {
                    roomNode.childPriority = 1;

                    RoomNode roomNode2 = roomNode.setConstraint(new LinkConstraint.NeverUp());
                }
                else if ((roomNode.parent.flags & 8) != 0)
                {

                    RoomNode roomNode2 = roomNode.setConstraint(new LinkConstraint.NeverUp());
                }
            }
        }
    }

    public override void buildSecondaryRooms()
    {
        base.buildSecondaryRooms();

        RoomNode roomNode = base.createNode("Combat".AsHaxeString(), null, 69, null).addFlag(new RoomFlag.Outside()).addBefore(base.getId("exit".AsHaxeString()), null);
        roomNode = base.createNode("Combat".AsHaxeString(), null, 6, null).addFlag(new RoomFlag.Outside()).addBefore(base.getId("exit".AsHaxeString()), null);
        roomNode = base.createNode("Combat".AsHaxeString(), null, 7, null).addFlag(new RoomFlag.Outside()).addBefore(base.getId("exit".AsHaxeString()), null);
    }
}

