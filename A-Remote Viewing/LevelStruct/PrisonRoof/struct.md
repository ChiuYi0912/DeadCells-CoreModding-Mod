## buildMainRooms

```csharp
public override RoomNode buildMainRooms()
{
	Rand rng = base.rng;
	double num = rng.seed * 16807.0 % 2147483647.0;
	rng.seed = num;
	bool flag = ((int)num & 1073741823) % 100 < 70;
	this.exitInBranch = flag;
	RoomNode roomNode;
	if (base.user.itemMeta.hasPermanentItem("CustomKey"))
	{
		roomNode = base.createNode(null, "RoofEntrance", null, "start").addFlag(RoomFlag.Class);
	}
	else
	{
		roomNode = base.createNode(null, "RoofEntrance", null, "entrance").addFlag(RoomFlag.Class);
		RoomNode parent = base.createNode(null, "RoofMiniBoss", null, "start").addFlag(RoomFlag.Class).set_parent(roomNode);
	}
	int num2;
	if (this.exitInBranch)
	{
		RandList randList = new RandList(new HlFunc<int, int>(base.rng.random), (ArrayDyn)null);
		if (!base.user.itemMeta.hasRevealedItem("StunningGrenade"))
		{
			num2 = 2;
			int? e = num2;
			RandList randList2 = randList.add("RoofEndSubRoom", e);
		}
		if (base.user.itemMeta.hasRevealedItem("PreciseBow"))
		{
			num2 = 2;
			int? e = num2;
			RandList randList2 = randList.add("RoofEndNothing", e);
			dc.String group = randList.draw(null);
			roomNode = base.createNode(null, group, null, "end").addFlag(RoomFlag.Class).addAfter(null, "start");
		}
		else
		{
			num2 = 2;
			int? e = num2;
			RandList randList2 = randList.add("RoofEndBridge", e);
			num2 = 2;
			e = num2;
			randList2 = randList.add("RoofEndNothing", e);
			dc.String group = randList.draw(null);
			roomNode = base.createNode(null, group, null, "end").addFlag(RoomFlag.Class).addAfter(null, "start");
		}
	}
	else
	{
		roomNode = base.createExit("T_Bridge", "RoofEndExit", null, "end").addFlag(RoomFlag.Class).addAfter(null, "start");
	}
	Rand rng2 = base.rng;
	double num3 = rng2.seed * 16807.0 % 2147483647.0;
	rng2.seed = num3;
	if ((double)(((int)num3 & 1073741823) % 10007) / 10007.0 < 0.4)
	{
		roomNode = base.createNode(null, "RoofSecret", null, null).addFlag(RoomFlag.Class).addBetween("start", "end", null);
	}
	roomNode = base.createNode("Treasure", null, null, null).branchBetween("start", "end", null, "crossTreasure");
	roomNode = base.createNode("Shop", null, null, null).branchBetween("crossTreasure", "end", null, null);
	ArrayObj arrayObj = base.createCursedChestNodes(0.1);
	if (arrayObj.length != 0)
	{
		num2 = 0;
	}
	else
	{
		roomNode = base.createNode("Treasure", null, null, null);
		num2 = arrayObj.push(roomNode);
		num2 = 0;
	}
	for (;;)
	{
		int length = arrayObj.length;
		if (num2 >= length)
		{
			break;
		}
		length = arrayObj.length;
		if (num2 >= length)
		{
			roomNode = null;
			num2++;
			RoomNode parent = base.createNode("BreakableGroundGate", null, null, null).branchBetween("crossTreasure", "end", null, null);
			RoomNode roomNode2 = roomNode.set_parent(parent);
		}
		else
		{
			roomNode = arrayObj.array[num2];
			num2++;
			RoomNode parent = base.createNode("BreakableGroundGate", null, null, null).branchBetween("crossTreasure", "end", null, null);
			RoomNode roomNode2 = roomNode.set_parent(parent);
		}
	}
	if (this.exitInBranch)
	{
		roomNode = base.createExit("T_Bridge", "RoofInnerExit", null, "innerExit").branchBetween("start", "end", 2, null);
		roomNode = base.getId("innerExit");
		Rand rng3 = base.rng;
		System.IntPtr typeFromHandle = typeof(RoomNode);
		ArrayObj arrayObj2 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
		num2 = 0;
		ArrayObj all = base.all;
		RoomNode roomNode2;
		for (;;)
		{
			int length = all.length;
			if (num2 >= length)
			{
				break;
			}
			length = all.length;
			if (num2 >= length)
			{
				roomNode2 = null;
			}
			else
			{
				roomNode2 = all.array[num2];
			}
			num2++;
			if (!roomNode2.isInZBranch())
			{
				flag = false;
				bool? p = flag;
				if (roomNode.hasParent(roomNode2, p))
				{
					if (roomNode2.parent != null)
					{
						int num4 = arrayObj2.push(roomNode2);
					}
				}
			}
		}
		ArrayDyn a = (ArrayDyn)arrayObj2;
		roomNode2 = rng3.arrayPick(a);
		if (roomNode2 != null)
		{
			RoomNode parent = base.createNode(null, "RoofInnerExitClue", null, null).addFlag(RoomFlag.Class).addBefore(roomNode2, null);
		}
		else
		{
			roomNode2 = roomNode;
			RoomNode parent = base.createNode(null, "RoofInnerExitClue", null, null).addFlag(RoomFlag.Class).addBefore(roomNode2, null);
		}
	}
	if (base.nodes.exists("entrance"))
	{
		return base.getId("entrance");
	}
	return base.getId("start");
}
```

## buildSecondaryRooms

``` csharp
public override void buildSecondaryRooms()
{
	System.IntPtr typeFromHandle = typeof(RoomNode);
	ArrayObj arrayObj = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
	int i = 0;
	ArrayObj arrayObj2 = base.all;
	int num;
	RoomNode roomNode;
	int num2;
	for (;;)
	{
		num = arrayObj2.length;
		if (i >= num)
		{
			break;
		}
		num = arrayObj2.length;
		if (i >= num)
		{
			roomNode = null;
		}
		else
		{
			roomNode = arrayObj2.array[i];
		}
		i++;
		if (!roomNode.isInZBranch())
		{
			if ((roomNode.flags & 8) == 0)
			{
				if (roomNode.children.length == 0)
				{
					num2 = arrayObj.push(roomNode);
				}
			}
		}
	}
	arrayObj2 = arrayObj;
	roomNode = base.createNode("Combat", null, null, null).addBefore(base.rng.arrayPick((ArrayDyn)arrayObj), null);
	roomNode = base.createNode("Combat", null, null, null).addBefore(base.rng.arrayPick((ArrayDyn)arrayObj), null);
	roomNode = base.createNode("Combat", null, null, null).addBefore(base.rng.arrayPick((ArrayDyn)arrayObj), null);
	roomNode = base.createNode("Combat", null, null, null).addBefore(base.rng.arrayPick((ArrayDyn)arrayObj), null);
	typeFromHandle = typeof(HlFunc<bool, RoomNode>);
	System.Array array = Lib_std.alloc_array.Invoke(typeFromHandle, 1);
	array[0] = null;
	ArrayObj arrayObj3 = _Assets.ArrowFunction_preloadSfx_6000(array);
	i = 0;
	HlFunc<bool, RoomNode> hlFunc = PseudocodeHelper.CreateClosure<HlFunc<bool, RoomNode>>(arrayObj3, methodof(PrisonRoof.ArrowFunction_buildSecondaryRooms_33606(ArrayObj, RoomNode)).MethodHandle);
	num2 = arrayObj3.length;
	ArrayObj arrayObj4;
	ArrayObj arrayObj5;
	if (i < num2)
	{
		arrayObj3.array[i] = hlFunc;
		typeFromHandle = typeof(RoomNode);
		arrayObj4 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
		i = 0;
		arrayObj5 = arrayObj;
	}
	else
	{
		arrayObj3.__expand(i);
		arrayObj3.array[i] = hlFunc;
		typeFromHandle = typeof(RoomNode);
		arrayObj4 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
		i = 0;
		arrayObj5 = arrayObj;
	}
	for (;;)
	{
		num = arrayObj5.length;
		if (i >= num)
		{
			break;
		}
		num = arrayObj5.length;
		if (i >= num)
		{
			roomNode = null;
		}
		else
		{
			roomNode = arrayObj5.array[i];
		}
		i++;
		num2 = 0;
		num = arrayObj3.length;
		if (num2 >= num)
		{
			hlFunc = null;
		}
		else
		{
			hlFunc = arrayObj3.array[num2];
		}
		if (!hlFunc.Invoke(roomNode))
		{
			num2 = arrayObj4.push(roomNode);
		}
	}
	roomNode = base.createExit("T_BeholderPit", null, null, null);
	roomNode = base.createRunicZDoor(null, roomNode, 3, null, 0, null, null).addBefore(base.rng.arrayPick((ArrayDyn)arrayObj4), null);
	i = 0;
	Rand rng = base.rng;
	double num3 = rng.seed * 16807.0 % 2147483647.0;
	rng.seed = num3;
	num = ((int)num3 & 1073741823) % 2;
	num2 = 1 + num;
	RoomNode roomNode2;
	while (i < num2)
	{
		i++;
		roomNode = base.createNode("Combat", null, null, null).branchBetween("start", "end", null, null);
		roomNode2 = base.createNode("Teleport", null, null, null).addAfter(roomNode, null);
	}
	dc.String templateId = "Combat";
	dc.String group = null;
	i = 17;
	int? num4 = i;
	if (this.exitInBranch)
	{
		i = 6;
		arrayObj3 = base.addRooms(templateId, group, num4, i, "start", "end", null);
		i = 0;
	}
	else
	{
		i = 5;
		arrayObj3 = base.addRooms(templateId, group, num4, i, "start", "end", null);
		i = 0;
	}
	for (;;)
	{
		num = arrayObj3.length;
		if (i >= num)
		{
			break;
		}
		num = arrayObj3.length;
		if (i >= num)
		{
			roomNode = null;
			i++;
			RoomFlag @class = RoomFlag.Class;
			roomNode2 = roomNode.addFlag(@class);
		}
		else
		{
			roomNode = arrayObj3.array[i];
			i++;
			RoomFlag @class = RoomFlag.Class;
			roomNode2 = roomNode.addFlag(@class);
		}
	}
	if (this.exitInBranch)
	{
		roomNode = base.createNode("Combat", null, null, null).addBefore(null, "innerExit");
	}
	if (this.exitInBranch)
	{
		roomNode = base.getId("innerExit");
		typeFromHandle = typeof(RoomNode);
		arrayObj4 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
		i = 0;
		arrayObj5 = arrayObj2;
	}
	else
	{
		roomNode = null;
		typeFromHandle = typeof(RoomNode);
		arrayObj4 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
		i = 0;
		arrayObj5 = arrayObj2;
	}
	for (;;)
	{
		num = arrayObj5.length;
		if (i >= num)
		{
			break;
		}
		num = arrayObj5.length;
		if (i >= num)
		{
			roomNode2 = null;
		}
		else
		{
			roomNode2 = arrayObj5.array[i];
		}
		i++;
		bool flag = true;
		bool? p = flag;
		if (!roomNode2.hasParent(roomNode, p))
		{
			num2 = arrayObj4.push(roomNode2);
		}
	}
	roomNode2 = base.rng.arrayPick((ArrayDyn)arrayObj4);
	rng = base.rng;
	num3 = rng.seed * 16807.0 % 2147483647.0;
	rng.seed = num3;
	RoomNode roomNode3;
	RoomNode roomNode4;
	RoomNode roomNode5;
	if ((double)(((int)num3 & 1073741823) % 10007) / 10007.0 < 0.5)
	{
		templateId = "TeleportGate";
		roomNode3 = base.createNode(templateId, null, null, null);
		roomNode4 = roomNode3.set_parent(roomNode2);
		roomNode5 = base.createNode("Combat", null, null, null).set_parent(roomNode3);
		return;
	}
	templateId = "WallJumpGate";
	roomNode3 = base.createNode(templateId, null, null, null);
	roomNode4 = roomNode3.set_parent(roomNode2);
	roomNode5 = base.createNode("Combat", null, null, null).set_parent(roomNode3);
}

```

## finalize

```csharp
public override void finalize()
{
	RoomNode id = base.getId("end");
	int num = 0;
	ArrayObj arrayObj = base.all;
	RoomNode roomNode;
	for (;;)
	{
		int length = arrayObj.length;
		if (num >= length)
		{
			break;
		}
		length = arrayObj.length;
		if (num >= length)
		{
			roomNode = null;
		}
		else
		{
			roomNode = arrayObj.array[num];
		}
		num++;
		if (roomNode.isParentOf(id, null))
		{
			if (roomNode.rType == "Corridor" || roomNode.rType == "Teleport")
			{
				roomNode.group = 17;
				RoomFlag @class = RoomFlag.Class;
				RoomNode roomNode2 = roomNode.addFlag(@class);
				if (roomNode.rType == "Corridor")
				{
					if (roomNode.calcTypeDistance("Teleport", false) >= 2)
					{
						roomNode.rType = "Teleport";
					}
				}
			}
		}
	}
	System.IntPtr typeFromHandle = typeof(RoomNode);
	arrayObj = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
	num = 0;
	ArrayObj arrayObj2 = base.all;
	for (;;)
	{
		int length = arrayObj2.length;
		if (num >= length)
		{
			break;
		}
		length = arrayObj2.length;
		if (num >= length)
		{
			roomNode = null;
		}
		else
		{
			roomNode = arrayObj2.array[num];
		}
		num++;
		if (!roomNode.isInZBranch())
		{
			if ((roomNode.flags & 8) != 0)
			{
				if (roomNode.parent != null)
				{
					int num2 = roomNode.children.length;
					if (1 < num2)
					{
						num2 = arrayObj.push(roomNode);
					}
				}
			}
		}
	}
	arrayObj2 = arrayObj;
	num = 0;
	for (;;)
	{
		int length = arrayObj2.length;
		if (num >= length)
		{
			break;
		}
		length = arrayObj2.length;
		if (num >= length)
		{
			roomNode = null;
			num++;
			int num2 = 18;
			int? id2 = num2;
			RoomNode roomNode2 = base.createNode("Corridor", null, id2, null).addBefore(roomNode, null).addFlag(RoomFlag.Class);
		}
		else
		{
			roomNode = arrayObj2.array[num];
			num++;
			int num2 = 18;
			int? id2 = num2;
			RoomNode roomNode2 = base.createNode("Corridor", null, id2, null).addBefore(roomNode, null).addFlag(RoomFlag.Class);
		}
	}
	typeFromHandle = typeof(RoomNode);
	ArrayObj arrayObj3 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
	num = 0;
	ArrayObj arrayObj4 = base.all;
	for (;;)
	{
		int length = arrayObj4.length;
		if (num >= length)
		{
			break;
		}
		length = arrayObj4.length;
		if (num >= length)
		{
			roomNode = null;
		}
		else
		{
			roomNode = arrayObj4.array[num];
		}
		num++;
		if (!roomNode.isInZBranch())
		{
			if ((roomNode.flags & 8) != 0)
			{
				if (roomNode.parent != null)
				{
					int num2 = roomNode.parent.children.length;
					if (1 < num2)
					{
						if (roomNode.group != 18)
						{
							num2 = arrayObj3.push(roomNode);
						}
					}
				}
			}
		}
	}
	arrayObj4 = arrayObj3;
	num = 0;
	for (;;)
	{
		int length = arrayObj4.length;
		if (num >= length)
		{
			break;
		}
		length = arrayObj4.length;
		if (num >= length)
		{
			roomNode = null;
		}
		else
		{
			roomNode = arrayObj4.array[num];
		}
		num++;
		Rand rng = base.rng;
		double num3 = rng.seed * 16807.0 % 2147483647.0;
		rng.seed = num3;
		if ((double)(((int)num3 & 1073741823) % 10007) / 10007.0 < 0.75)
		{
			int num2 = 18;
			int? id2 = num2;
			RoomNode roomNode2 = base.createNode("Corridor", null, id2, null).addBefore(roomNode, null).addFlag(RoomFlag.Class);
		}
	}
	typeFromHandle = typeof(RoomNode);
	ArrayObj arrayObj5 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
	num = 0;
	ArrayObj all = base.all;
	for (;;)
	{
		int length = all.length;
		if (num >= length)
		{
			break;
		}
		length = all.length;
		if (num >= length)
		{
			roomNode = null;
		}
		else
		{
			roomNode = all.array[num];
		}
		num++;
		if (!roomNode.isInZBranch())
		{
			if (roomNode.parent != null)
			{
				if ((roomNode.flags & 8) == 0)
				{
					if ((roomNode.parent.flags & 8) == 0)
					{
						if (!roomNode.hasParentType("TimedDoor"))
						{
							int num2 = arrayObj5.push(roomNode);
						}
					}
				}
			}
		}
	}
	RoomNode roomNode3 = base.createNode("Shop", null, null, null);
	MerchantType class2 = MerchantType.Class;
	virtual_forcedMerchantType_ virtual_forcedMerchantType_;
	virtual_forcedMerchantType_.forcedMerchantType = class2;
	roomNode = roomNode3.addGenData((virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_)virtual_forcedMerchantType_);
	roomNode = base.createRunicZDoor(null, roomNode, 2, null, null, null, arrayObj5);
	ArrayObj arrayObj6;
	if (base.user.itemMeta.hasRevealedItem("P_EasierCurse"))
	{
		num = 0;
		arrayObj6 = base.all;
	}
	else
	{
		typeFromHandle = typeof(RoomNode);
		arrayObj6 = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
		num = 0;
		ArrayObj all2 = base.all;
		for (;;)
		{
			int length = all2.length;
			if (num >= length)
			{
				break;
			}
			length = all2.length;
			if (num >= length)
			{
				roomNode = null;
			}
			else
			{
				roomNode = all2.array[num];
			}
			num++;
			if (!roomNode.isInZBranch())
			{
				if (roomNode.parent != null)
				{
					if (roomNode.spawnDistance >= 4)
					{
						if ((roomNode.flags & 8) == 0)
						{
							if ((roomNode.parent.flags & 8) == 0)
							{
								int num2 = arrayObj6.push(roomNode);
							}
						}
					}
				}
			}
		}
		roomNode = base.createNode(null, "RoseKeyConverterRoof", null, null).addBefore(base.rng.arrayPick((ArrayDyn)arrayObj6), null);
		num = 0;
		arrayObj6 = base.all;
	}
	for (;;)
	{
		int length = arrayObj6.length;
		if (num >= length)
		{
			break;
		}
		length = arrayObj6.length;
		if (num >= length)
		{
			roomNode = null;
		}
		else
		{
			roomNode = arrayObj6.array[num];
		}
		num++;
		if (!roomNode.isInZBranch())
		{
			if (roomNode.parent != null)
			{
				if ((roomNode.flags & 8) != 0)
				{
					roomNode.childPriority = 1;
					LinkConstraint class3 = LinkConstraint.Class;
					RoomNode roomNode2 = roomNode.setConstraint(class3);
				}
				else if ((roomNode.parent.flags & 8) != 0)
				{
					LinkConstraint class3 = LinkConstraint.Class;
					RoomNode roomNode2 = roomNode.setConstraint(class3);
				}
			}
		}
	}
}

```

## buildTriggeredDoors

```csharp
public override void buildTriggeredDoors(ArrayObj cRooms)
{
	base.buildTriggeredDoors(cRooms);
	System.IntPtr typeFromHandle = typeof(RoomNode);
	ArrayObj arrayObj = _Assets.ArrowFunction_preloadSfx_6000(Lib_std.alloc_array.Invoke(typeFromHandle, 0));
	int num = 0;
	for (;;)
	{
		int length = cRooms.length;
		if (num >= length)
		{
			break;
		}
		length = cRooms.length;
		RoomNode roomNode;
		if (num >= length)
		{
			roomNode = null;
		}
		else
		{
			roomNode = cRooms.array[num];
		}
		num++;
		if ((roomNode.flags & 8) == 0)
		{
			if ((roomNode.flags & 16) == 0)
			{
				int num2 = arrayObj.push(roomNode);
			}
		}
	}
	num = arrayObj.length;
	bool flag;
	if (0 < num)
	{
		Rand rng = base.rng;
		double num3 = rng.seed * 16807.0 % 2147483647.0;
		rng.seed = num3;
		flag = (((int)num3 & 1073741823) % 100 < 40);
	}
	else
	{
		flag = false;
	}
	if (!flag)
	{
		return;
	}
	base.rng.arrayPick((ArrayDyn)arrayObj).group = 4;
}

```