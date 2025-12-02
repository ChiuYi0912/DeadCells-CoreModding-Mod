## buildMainRooms
```csharp
public override RoomNode buildMainRooms()
{
	int num = base.user.story.getNpcProgress(NpcId.Class);
	RoomNode parent;
	StoryManager story;
	int? num2;
	int? num3;
	RoomNode roomNode;
	if (1 >= num && !this.ldTool)
	{
		parent = base.createNode(null, "PrisonTube1", null, "start");
	}
	else
	{
		story = base.user.story;
		if (story.counters.exists("kingKilled"))
		{
			num2 = story.counters.get("kingKilled");
		}
		else
		{
			num = 0;
			num2 = num;
		}
		num = 1;
		num3 = num;
		bool flag;
		if (num2 != num3)
		{
			StoryManager story2 = base.user.story;
			if (story2.counters.exists("kickedBackByBerserk"))
			{
				num2 = story2.counters.get("kickedBackByBerserk");
			}
			else
			{
				num = 0;
				num2 = num;
			}
			num = 1;
			num3 = num;
			flag = (num2 == num3);
		}
		else
		{
			flag = true;
		}
		if (flag)
		{
			parent = base.createNode(null, "PrisonTube2NoSkel", null, "tube");
			parent = base.getId("tube");
			roomNode = base.createNode(null, "PrisonFlaskRoom", null, "start").set_parent(parent);
			this.buildPrisonHUBZDoor();
		}
		else
		{
			parent = base.createNode(null, "PrisonTube2", null, "tube");
			parent = base.getId("tube");
			roomNode = base.createNode(null, "PrisonFlaskRoom", null, "start").set_parent(parent);
			this.buildPrisonHUBZDoor();
		}
	}
	parent = base.getId("start");
	RoomNode roomNode2 = base.createCross("exitCross");
	roomNode = roomNode2.set_parent(parent);
	RoomNode roomNode3 = base.createExit("T_Courtyard", "PrisonStartExit", null, "eCourt").set_parent(roomNode2);
	if (!base.user.itemMeta.hasPermanentItem("WallJumpKey"))
	{
		parent = base.createNode("LadderGate", null, null, "gSewer");
	}
	else if (base.user.itemMeta.hasPermanentItem("LadderKey"))
	{
		parent = base.createNode("LadderGate", null, null, "gSewer");
	}
	else
	{
		parent = base.createNode(null, "LadderGate1", null, "gSewer");
	}
	roomNode = base.getId("exitCross");
	roomNode2 = roomNode2.set_parent(roomNode);
	roomNode = base.createExit("T_SewerShort", null, null, "eSewer").set_parent(parent);
	bool flag2 = DLC.Class.installMaskCacheDirty;
	ArrayObj arrayObj;
	if (flag2)
	{
		num = 0;
		int num4 = 1;
		int num5 = 0;
		arrayObj = (ArrayObj)_Type.allEnums(DLCId.Class);
		for (;;)
		{
			int length = arrayObj.length;
			if (num5 >= length)
			{
				break;
			}
			length = arrayObj.length;
			DLCId id;
			if (num5 >= length)
			{
				id = null;
			}
			else
			{
				id = arrayObj.array[num5];
			}
			num5++;
			if (!_Api.isDlcInstalled(_DLC.getSteamId(id)))
			{
				num4 <<= 1;
			}
			else
			{
				num |= num4;
				num4 <<= 1;
			}
		}
		DLC.Class.installMask = num;
		flag2 = false;
		DLC.Class.installMaskCacheDirty = flag2;
	}
	num = DLCId.Class.RawIndex;
	switch (this)
	{
	case 0:
		if ((DLC.Class.installMask & 1) == 0)
		{
			bool flag = false;
			flag2 = flag;
		}
		else
		{
			bool flag = true;
			flag2 = flag;
		}
		break;
	case 1:
		if ((DLC.Class.installMask & 2) == 0)
		{
			bool flag = false;
			flag2 = flag;
		}
		else
		{
			bool flag = true;
			flag2 = flag;
		}
		break;
	case 2:
		if ((DLC.Class.installMask & 4) == 0)
		{
			bool flag = false;
			flag2 = flag;
		}
		else
		{
			bool flag = true;
			flag2 = flag;
		}
		break;
	case 3:
		if ((DLC.Class.installMask & 8) == 0)
		{
			bool flag = false;
			flag2 = flag;
		}
		else
		{
			bool flag = true;
			flag2 = flag;
		}
		break;
	case 4:
		if ((DLC.Class.installMask & 16) == 0)
		{
			bool flag = false;
			flag2 = flag;
		}
		else
		{
			bool flag = true;
			flag2 = flag;
		}
		break;
	}
	RoomNode roomNode4;
	if (flag2)
	{
		num = base.user.story.getNpcProgress(NpcId.Class);
		if (1 < num)
		{
			roomNode2 = base.getId("exitCross");
			roomNode = base.createNode(null, "PurpleCorridor", null, "PCorridor");
			roomNode3 = roomNode.set_parent(roomNode2);
			roomNode4 = base.createExit("T_PurpleGarden", null, null, "ePurpleGarden").set_parent(roomNode);
		}
	}
	RoomNode roomNode5 = base.createCross("exitCrossGreenhouse");
	Rand rng = base.rng;
	System.IntPtr typeFromHandle = typeof(dc.String);
	System.Array array = Lib_std.alloc_array.Invoke(typeFromHandle, 1);
	array[0] = "eCourt";
	arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
	roomNode2 = roomNode5.branchOrAddBetween(rng, "exitCross", arrayObj, null);
	roomNode = base.createNode("TeleportGate", null, null, "gGreenhouse");
	roomNode3 = roomNode.set_parent(roomNode2);
	roomNode4 = base.createExit("T_Greenhouse", null, null, "eGreenhouse").set_parent(roomNode);
	Rand rng2 = base.rng;
	double num6 = rng2.seed * 16807.0 % 2147483647.0;
	rng2.seed = num6;
	if ((double)(((int)num6 & 1073741823) % 10007) / 10007.0 < 0.05)
	{
		RoomNode roomNode6 = base.createNode("MultiTreasure", null, null, null);
		Rand rng3 = base.rng;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 2);
		array[0] = "eCourt";
		array[1] = "gSewer";
		arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
		roomNode2 = roomNode6.branchOrAddBetween(rng3, "start", arrayObj, 2);
	}
	else
	{
		RoomNode roomNode7 = base.createNode("Treasure", null, null, null);
		Rand rng4 = base.rng;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 2);
		array[0] = "eCourt";
		array[1] = "gSewer";
		arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
		roomNode2 = roomNode7.branchOrAddBetween(rng4, "start", arrayObj, 2);
	}
	RoomNode roomNode8 = base.createNode("Shop", null, null, null);
	Rand rng5 = base.rng;
	typeFromHandle = typeof(dc.String);
	array = Lib_std.alloc_array.Invoke(typeFromHandle, 1);
	array[0] = "eCourt";
	arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
	roomNode2 = roomNode8.branchOrAddBetween(rng5, "exitCross", arrayObj, null);
	Rand rng6 = base.rng;
	double num7 = rng6.seed * 16807.0 % 2147483647.0;
	rng6.seed = num7;
	if ((double)(((int)num7 & 1073741823) % 10007) / 10007.0 < 0.7)
	{
		RoomNode roomNode9 = base.createNode("BuyableTreasure", null, null, null);
		Rand rng7 = base.rng;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 1);
		array[0] = "eCourt";
		arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
		roomNode2 = roomNode9.branchOrAddBetween(rng7, "exitCross", arrayObj, null);
	}
	Rand rng8 = base.rng;
	double num8 = rng8.seed * 16807.0 % 2147483647.0;
	rng8.seed = num8;
	if ((double)(((int)num8 & 1073741823) % 10007) / 10007.0 < 0.2)
	{
		RoomNode roomNode10 = base.createNode("BuyableTreasure", null, null, null);
		Rand rng9 = base.rng;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 1);
		array[0] = "eCourt";
		arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
		roomNode2 = roomNode10.branchOrAddBetween(rng9, "start", arrayObj, null);
	}
	Rand rng10 = base.rng;
	double num9 = rng10.seed * 16807.0 % 2147483647.0;
	rng10.seed = num9;
	if ((double)(((int)num9 & 1073741823) % 10007) / 10007.0 < 0.05)
	{
		RoomNode roomNode11 = base.createNode("Treasure", null, null, null);
		Rand rng11 = base.rng;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 2);
		array[0] = "eCourt";
		array[1] = "eSewer";
		arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
		roomNode2 = roomNode11.branchOrAddBetween(rng11, "start", arrayObj, 3);
	}
	Rand rng12 = base.rng;
	double num10 = rng12.seed * 16807.0 % 2147483647.0;
	rng12.seed = num10;
	bool flag3;
	if ((double)(((int)num10 & 1073741823) % 10007) / 10007.0 < 0.35)
	{
		story = base.user.story;
		if (story.counters.exists("newGame"))
		{
			num2 = story.counters.get("newGame");
		}
		else
		{
			num = 0;
			num2 = num;
		}
		flag3 = (num2 >= 2);
	}
	else
	{
		flag3 = false;
	}
	if (!flag3)
	{
		num = 0;
		arrayObj = base.createCursedChestNodes(0.01);
	}
	else
	{
		RoomNode roomNode12 = base.createNode("BuyableTreasure", null, null, null);
		Rand rng13 = base.rng;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 2);
		array[0] = "eCourt";
		array[1] = "eSewer";
		arrayObj = _Assets.ArrowFunction_preloadSfx_6000(array);
		roomNode2 = roomNode12.branchOrAddBetween(rng13, "start", arrayObj, 3);
		num = 0;
		arrayObj = base.createCursedChestNodes(0.01);
	}
	for (;;)
	{
		int num5 = arrayObj.length;
		if (num >= num5)
		{
			break;
		}
		num5 = arrayObj.length;
		if (num >= num5)
		{
			roomNode2 = null;
			num++;
			roomNode2.group = 1;
			roomNode = roomNode2.addBetween("start", "exitCross", null);
		}
		else
		{
			roomNode2 = arrayObj.array[num];
			num++;
			roomNode2.group = 1;
			roomNode = roomNode2.addBetween("start", "exitCross", null);
		}
	}
	roomNode2 = base.createNode(null, "GardenerCorridor", null, null).addBefore(null, "eGreenhouse");
	story = base.user.story;
	dc.String templateId = null;
	if (story.counters.exists("greenhouseReached"))
	{
		num2 = story.counters.get("greenhouseReached");
	}
	else
	{
		num = 0;
		num2 = num;
	}
	num = 1;
	num3 = num;
	dc.String group;
	if (num2 == num3)
	{
		group = "GreenhouseEntranceOpen";
	}
	else
	{
		group = "GreenhouseEntrance";
	}
	roomNode2 = base.createNode(templateId, group, null, "ghEntrance").addBefore(null, "eGreenhouse");
	if (base.nodes.exists("tube"))
	{
		return base.getId("tube");
	}
	return base.getId("start");
}

```

## buildEssentialLoreRooms
```csharp
public override void buildEssentialLoreRooms()
{
	ArrayObj arrayObj = (ArrayObj)Data.Class.loreRoom.all;
	int num = 0;
	for (;;)
	{
		int length = arrayObj.length;
		if (num >= length)
		{
			break;
		}
		int num2 = num;
		num++;
		length = arrayObj.length;
		virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_ virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_;
		if (num2 >= length)
		{
			virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_ = null;
		}
		else
		{
			virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_ = (virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_)arrayObj.array[num2];
		}
		dc.String room = virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_.room;
		if (room != null)
		{
			if (room == null || room.length != 15)
			{
				goto IL_EB;
			}
			System.IntPtr bytes = room.bytes;
			if (Lib_std.string_compare.Invoke(bytes, "BlasphemousRoom", num2) != 0)
			{
				goto IL_EB;
			}
			IL_4FC:
			if (!base.user.game.canSpawnEvents())
			{
				continue;
			}
			if (!this.canGenerateThisLoreRoom(virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_))
			{
				continue;
			}
			base.addLoreRoom(virtual_arc_examinables_fxEmitters_Intention_levels_onlyUseOnce_rarity_requiredLore_requiredMeta_room_roomLoot_sprites_status_structMode_, null);
			continue;
			IL_EB:
			if (room != null && room.length == 22)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "CurseofTheDeadGodsRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 14)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "GuacameleeRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 6)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "HLRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 16)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "HollowKnightRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 16)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "HotlineMiamiRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 21)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "HyperLightDrifterRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 14)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "KatanaZeroRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 13)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "LegacyBugRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 15)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "NunchuckPanRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 14)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "RiskOfRainRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 16)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "ShovelKnightRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 8)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "SkulRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null && room.length == 16)
			{
				bytes = room.bytes;
				if (Lib_std.string_compare.Invoke(bytes, "SlayTheSpireRoom", num2) == 0)
				{
					goto IL_4FC;
				}
			}
			if (room != null)
			{
				if (room.length == 12)
				{
					bytes = room.bytes;
					if (Lib_std.string_compare.Invoke(bytes, "TerrariaRoom", num2) == 0)
					{
						goto IL_4FC;
					}
				}
			}
		}
	}
}
```

## buildPrisonHUBZDoor

```csharp
public void buildPrisonHUBZDoor()
{
	HlFunc<RoomNode, dc.String, dc.String, int?, dc.String> hlFunc = new HlFunc<RoomNode, dc.String, dc.String, int?, dc.String>(this.ArrowFunction_buildPrisonHUBZDoor_33607);
	RoomNode roomNode = hlFunc.Invoke(null, "PZEntrance", null, null);
	RoomNode roomNode2 = base.getId("start").addZChild(roomNode, null);
	roomNode2 = hlFunc.Invoke(null, "PZTailor", null, null).addAfter(roomNode, null);
	RoomNode roomNode3 = hlFunc.Invoke(null, "GenericZDoor_LongLR", null, null);
	ZDoorType @class = ZDoorType.Class;
	virtual_specificBiome_zDoorType_ virtual_specificBiome_zDoorType_;
	virtual_specificBiome_zDoorType_.zDoorType = @class;
	virtual_specificBiome_zDoorType_.specificBiome = "SkinningBiome";
	RoomNode roomNode4 = roomNode3.addGenData((virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_)virtual_specificBiome_zDoorType_);
	RoomNode id = hlFunc.Invoke(null, "SkinningRoom", null, null).addAfter(roomNode4, null);
	@class = ZDoorType.Class;
	virtual_zDoorType_ virtual_zDoorType_;
	virtual_zDoorType_.zDoorType = @class;
	virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_ f = (virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_)virtual_zDoorType_;
	id = roomNode2.addGenData(f).addZChild(roomNode4, null);
	id = hlFunc.Invoke(null, "PZTraining", null, null).addAfter(roomNode2, null);
	StoryManager story = base.user.story;
	int? num;
	int num2;
	if (story.counters.exists("BRUnlockPopUp"))
	{
		num = story.counters.get("BRUnlockPopUp");
	}
	else
	{
		num2 = 0;
		num = num2;
	}
	num2 = 1;
	int? num3 = num2;
	if (num != num3)
	{
		if (Game.Class.ME != null)
		{
			num2 = Game.Class.ME.getBiomeVisitCount("Throne");
			if (0 < num2)
			{
				goto IL_219;
			}
		}
		num2 = Game.Class.ME.getBiomeVisitCount("QueenArena");
		if (0 >= num2)
		{
			return;
		}
	}
	IL_219:
	RoomNode roomNode5 = hlFunc.Invoke(null, "PZBossRush", null, null).addAfter(id, null);
	RoomNode roomNode6 = hlFunc.Invoke(null, "BossRushEntrance", null, null);
	@class = ZDoorType.Class;
	virtual_specificBiome_zDoorType_.zDoorType = @class;
	virtual_specificBiome_zDoorType_.specificBiome = "BossRushZone";
	RoomNode roomNode7 = roomNode6.addGenData((virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_)virtual_specificBiome_zDoorType_);
	RoomNode id2 = hlFunc.Invoke(null, "BossRushStatue", null, null).addAfter(roomNode7, null);
	id2 = hlFunc.Invoke(null, "BossRushDoorSS", null, null).addAfter(id2, null);
	id2 = hlFunc.Invoke(null, "BossRushDoorSH", null, null).addAfter(id2, null);
	id2 = hlFunc.Invoke(null, "BossRushDoorLS", null, null).addAfter(id2, null);
	RoomNode roomNode8 = hlFunc.Invoke(null, "BossRushDoorLH", null, null).addAfter(id2, null);
	@class = ZDoorType.Class;
	virtual_zDoorType_.zDoorType = @class;
	f = (virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_)virtual_zDoorType_;
	roomNode8 = roomNode5.addGenData(f).addZChild(roomNode7, null);
	roomNode8 = hlFunc.Invoke(null, "PZReturn", null, null).addAfter(roomNode5, null);
}
```

## buildSecondaryRooms
```csharp
public override void buildSecondaryRooms()
{
	int num = 15;
	int? num2 = num;
	ArrayObj minSpawnDist = base.addRooms("Combat", null, num2, 1, "start", "exitCross", null);
	Rand rng = base.rng;
	double num3 = rng.seed * 16807.0 % 2147483647.0;
	rng.seed = num3;
	System.IntPtr typeFromHandle;
	System.Array array;
	if ((double)(((int)num3 & 1073741823) % 10007) / 10007.0 < 0.4)
	{
		num = 7;
		num2 = num;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 2);
		array[0] = "eCourt";
		array[1] = "gSewer";
		minSpawnDist = _Assets.ArrowFunction_preloadSfx_6000(array);
		minSpawnDist = base.addRooms("Combat", null, num2, 1, "exitCross", minSpawnDist, null);
	}
	else
	{
		num = 15;
		num2 = num;
		minSpawnDist = base.addRooms("Combat", null, num2, 1, "start", "exitCross", null);
	}
	num = 15;
	num2 = num;
	minSpawnDist = base.addRooms("Combat", null, num2, 1, "start", "eCourt", null);
	Rand rng2 = base.rng;
	double num4 = rng2.seed * 16807.0 % 2147483647.0;
	rng2.seed = num4;
	if ((double)(((int)num4 & 1073741823) % 10007) / 10007.0 < 0.6)
	{
		num = 6;
		num2 = num;
		typeFromHandle = typeof(dc.String);
		array = Lib_std.alloc_array.Invoke(typeFromHandle, 2);
		array[0] = "eCourt";
		array[1] = "gSewer";
		minSpawnDist = _Assets.ArrowFunction_preloadSfx_6000(array);
		minSpawnDist = base.addRooms("Combat", null, num2, 1, "exitCross", minSpawnDist, null);
	}
	num = 15;
	num2 = num;
	typeFromHandle = typeof(dc.String);
	array = Lib_std.alloc_array.Invoke(typeFromHandle, 1);
	array[0] = "eCourt";
	minSpawnDist = _Assets.ArrowFunction_preloadSfx_6000(array);
	minSpawnDist = base.addRooms("Combat", null, num2, 1, "exitCross", minSpawnDist, null);
	typeFromHandle = typeof(dc.String);
	array = Lib_std.alloc_array.Invoke(typeFromHandle, 2);
	array[0] = "eCourt";
	array[1] = "gSewer";
	minSpawnDist = _Assets.ArrowFunction_preloadSfx_6000(array);
	minSpawnDist = base.addRooms("Combat", null, 15, 1, "exitCross", minSpawnDist, null);
	RoomNode roomNode;
	if (base.user.br_getDifficulty() < 4)
	{
		num = 15;
		num2 = num;
		roomNode = base.createNode("Combat", null, num2, null).addBefore(null, "eSewer");
		return;
	}
	num = 15;
	num2 = num;
	minSpawnDist = base.addRooms("Combat", null, num2, 1, "start", "exitCross", null);
	num = 15;
	num2 = num;
	roomNode = base.createNode("Combat", null, num2, null).addBefore(null, "eSewer");
}

```