using System.Runtime.CompilerServices;
using dc;
using dc.cdb;
using dc.en;
using dc.h3d.pass;
using dc.haxe.ds;
using dc.hl.types;
using dc.level;
using dc.level.@struct;
using dc.libs;
using dc.pr;
using Hashlink.Virtuals;
using HaxeProxy.Runtime;
using ModCore.Events.Interfaces.Game;
using ModCore.Mods;
using ModCore.Storage;
using ModCore.Utitities;
using Serilog;

namespace RandomLevel;

public class Main : ModBase
{
    public Main(ModInfo info) : base(info)
    {
    }

    public override void Initialize()
    {
        Logger.Information("随机模组初始化");


        Hook__LevelStruct.get += Hook_LevelStruct_get;
        Hook_Level.init += Hook_Level_init;
        //Hook_StringMap.get += Hook_StringMap_get;
    }

    private object Hook_StringMap_get(Hook_StringMap.orig_get orig, StringMap self, dc.String key)
    {
        return "Throne".AsHaxeString();
    }

    private static readonly List<string> allLevels = new List<string>
    {
        "PrisonCourtyard", "SewerShort", "PrisonDepths", "PrisonCorrupt", "PrisonRoof",
        "Ossuary", "SewerDepths", "Bridge", "BeholderPit", "StiltVillage",
        "AncientTemple", "Cemetery", "ClockTower", "Crypt", "TopClockTower",
        "Cavern", "Giant", "Castle", "Distillery", "Throne",
        "Astrolab", "Observatory", "BoatDock", "Greenhouse",
        "Swamp", "SwampHeart", "Tumulus", "Cliff", "GardenerStage",
        "Shipwreck", "Lighthouse", "QueenArena", "Bank", "PurpleGarden",
        "DookuCastle", "DookuCastleHard", "DeathArena", "DookuArena"
    };

    public void CreateLevelDisplay(Level self, dc.String randomLevelId)
    {
        var map = self.map;

        if (randomLevelId == "PrisonCourtyard".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.PrisonCourtyard(self, map, randomLevelId);
        }
        else if (randomLevelId == "SewerShort".AsHaxeString())
        {
            var biome = false;
            self.lDisp = new dc.level.disp.Sewer(self, map, new Ref<bool>(ref biome));
        }
        else if (randomLevelId == "PrisonDepths".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Prison(self, map, randomLevelId);
        }
        else if (randomLevelId == "PrisonCorrupt".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Prison(self, map, randomLevelId);
        }
        else if (randomLevelId == "PrisonRoof".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.PrisonRoof(self, map);
        }
        else if (randomLevelId == "Ossuary".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Ossuary(self, map);
        }
        else if (randomLevelId == "SewerDepths".AsHaxeString())
        {
            var biome = true;
            self.lDisp = new dc.level.disp.Sewer(self, map, new Ref<bool>(ref biome));
        }
        else if (randomLevelId == "Bridge".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Bridge(self, map);
        }
        else if (randomLevelId == "BeholderPit".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.BeholderPit(self, map, randomLevelId);
        }
        else if (randomLevelId == "StiltVillage".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.StiltVillage(self, map);
        }
        else if (randomLevelId == "AncientTemple".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.AncientTemple(self, map, randomLevelId);
        }
        else if (randomLevelId == "Cemetery".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Cemetery(self, map, randomLevelId);
        }
        else if (randomLevelId == "ClockTower".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.ClockTower(self, map, randomLevelId);
        }
        else if (randomLevelId == "Crypt".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Crypt(self, map);
        }
        else if (randomLevelId == "TopClockTower".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.TopClockTower(self, map, randomLevelId);
        }
        else if (randomLevelId == "Cavern".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Cavern(self, map, randomLevelId);
        }
        else if (randomLevelId == "Giant".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Cavern(self, map, randomLevelId);
        }
        else if (randomLevelId == "Castle".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Castle(self, map, randomLevelId);
        }
        else if (randomLevelId == "Distillery".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Distillery(self, map);
        }
        else if (randomLevelId == "Throne".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Throne(self, map);
        }
        else if (randomLevelId == "Astrolab".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Astrolab(self, map);
        }
        else if (randomLevelId == "Observatory".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Observatory(self, map);
        }
        else if (randomLevelId == "BoatDock".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Docks(self, map);
        }
        else if (randomLevelId == "Greenhouse".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Greenhouse(self, map, randomLevelId, "Greenhouse_underground".AsHaxeString());
        }
        else if (randomLevelId == "Swamp".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Swamp(self, map);
        }
        else if (randomLevelId == "SwampHeart".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.SwampHeart(self, map);
        }
        else if (randomLevelId == "Tumulus".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Tumulus(self, map, randomLevelId);
        }
        else if (randomLevelId == "Cliff".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Cliff(self, map, randomLevelId, "Cliff_outside".AsHaxeString());
        }
        else if (randomLevelId == "GardenerStage".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.GardenerStage(self, map, randomLevelId, "Gardener_outside".AsHaxeString());
        }
        else if (randomLevelId == "Shipwreck".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Shipwreck(self, map, randomLevelId, "Shipwreck_underground".AsHaxeString());
        }
        else if (randomLevelId == "Lighthouse".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Lighthouse(self, map, randomLevelId, "LighthouseTop".AsHaxeString());
        }
        else if (randomLevelId == "QueenArena".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.QueenArena(self, map, randomLevelId);
        }
        else if (randomLevelId == "Bank".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.Bank(self, map);
        }
        else if (randomLevelId == "PurpleGarden".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.PurpleGarden(self, map);
        }
        else if (randomLevelId == "DookuCastle".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.DookuCastle(self, map);
        }
        else if (randomLevelId == "DookuCastleHard".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.DookuCastle(self, map);
        }
        else if (randomLevelId == "DeathArena".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.DeathArena(self, map);
        }
        else if (randomLevelId == "DookuArena".AsHaxeString())
        {
            self.lDisp = new dc.level.disp.DookuArena(self, map, randomLevelId, "DookuBeastArena".AsHaxeString());
        }
        else
        {
            Logger.Error($"未知的关卡类型: {randomLevelId}");
        }
    }


    private LevelStruct CreateRandomLevelStruct(string levelId, User user, dynamic data, Rand rng)
    {
#pragma warning disable CS8603 
        return levelId switch
        {
            "PrisonCourtyard" => new PrisonCourtyard(user, data, rng),
            "SewerShort" => new SewerShort(user, data, rng),
            "PrisonDepths" => new PrisonDepths(user, data, rng),
            "PrisonCorrupt" => new PrisonCorrupt(user, data, rng),
            "PrisonRoof" => new PrisonRoof(user, data, rng),
            "Ossuary" => new Ossuary(user, data, rng),
            "SewerDepths" => new SewerDepths(user, data, rng),
            "Bridge" => new Bridge(user, data, rng),
            "BeholderPit" => new BeholderPit(user, data, rng),
            "StiltVillage" => new StiltVillage(user, data, rng),
            "AncientTemple" => new AncientTemple(user, data, rng),
            "Cemetery" => new Cemetery(user, data, rng),
            "ClockTower" => new ClockTower(user, data, rng),
            "Crypt" => new Crypt(user, data, rng),
            "TopClockTower" => new TopClockTower(user, data, rng),
            "Cavern" => new Cavern(user, data, rng),
            "Giant" => new Giant(user, data, rng),
            "Castle" => new Castle(user, data, rng),
            "Distillery" => new Distillery(user, data, rng),
            "Throne" => new Throne(user, data, rng),
            "Astrolab" => new Astrolab(user, data, rng),
            "Observatory" => new Observatory(user, data, rng),
            "BoatDock" => new BoatDock(user, data, rng),
            "Greenhouse" => new Greenhouse(user, data, rng),
            "Swamp" => new Swamp(user, data, rng),
            "SwampHeart" => new SwampHeart(user, data, rng),
            "Tumulus" => new Tumulus(user, data, rng),
            "Cliff" => new Cliff(user, data, rng),
            "GardenerStage" => new GardenerStage(user, data, rng),
            "Shipwreck" => new Shipwreck(user, data, rng),
            "Lighthouse" => new Lighthouse(user, data, rng),
            "QueenArena" => new QueenArena(user, data, rng),
            "Bank" => new Bank(user, data, rng),
            "PurpleGarden" => new PurpleGarden(user, data, rng),
            "DookuCastle" => new DookuCastle(user, data, rng),
            "DookuCastleHard" => new DookuCastleHard(user, data, rng),
            "DeathArena" => new DeathArena(user, data, rng),
            "DookuArena" => new DookuArena(user, data, rng),
            _ => null
        };
#pragma warning restore CS8603 
    }


    public void randomMain(Level l, dc.String id)
    {
        this.CreateLevelDisplay(l, id);
    }

    private static List<string> availableLevels = allLevels.ToList();
    public static Rand rng = new Rand(7);
    private static int randomIndex = (rng.random(availableLevels.Count));
    private static int getRand = randomIndex;
    private static int initRand = getRand;
    private LevelStruct Hook_LevelStruct_get(Hook__LevelStruct.orig_get orig, User user, dynamic data, Rand rng)
    {

        if (Game.Class.ME == null || !Game.Class.ME.isBossRush())
        {
            string originalLevelId = data.id.ToString();

            if (allLevels.Contains(originalLevelId))
            {
                if (availableLevels.Count > 0)
                {
                    string randomLevelId = availableLevels[randomIndex];


                    Logger.Information($"[随机关卡] {originalLevelId} -> {randomLevelId}");

                    LevelStruct randomLevel = CreateRandomLevelStruct(randomLevelId, user, data, rng);

                    if (randomLevel != null)
                    {
                        return randomLevel;
                    }
                }
            }
        }
        return orig(user, data, rng);
    }

    private void Hook_Level_init(Hook_Level.orig_init orig, Level self)
    {

        orig(self);
        var id = self.map.biome.id;
        if (id != null)
        {
            Log.Debug("id:{id}不为空", id);
            string idStr = id.ToString();
            string randomLevelId = availableLevels[randomIndex];
            Log.Debug("随机关卡为：{randomLevelId}", randomLevelId);
            _LevelStruct _LevelStruct = LevelStruct.Class;
            var mapid = _LevelStruct.get.ToString();

            Log.Debug("当前的关卡id{mapid}", mapid);
            if (allLevels.Contains(randomLevelId))
            {
                try
                {
                    CreateLevelDisplay(self, randomLevelId.AsHaxeString());
                    Logger.Information($"在Hook_Level_init中创建关卡显示: {idStr} -> {randomLevelId}");
                    var forcedBiome = "ClockTower".AsHaxeString();
                    var virtual_add = new virtual_specificBiome_();
                    virtual_add.specificBiome = forcedBiome;
                    var virtual1 = Data.Class.level.byId.ToVirtual<virtual_baseLootLevel_biome_bonusTripleScrollAfterBC_cellBonus_dlc_doubleUps_eliteRoomChance_eliteWanderChance_flagsProps_group_icon_id_index_loreDescriptions_mapDepth_minGold_mobDensity_mobs_name_nextLevels_parallax_props_quarterUpsBC3_quarterUpsBC4_specificLoots_specificSubBiome_transitionTo_tripleUps_worldDepth_>();

                    var clockTowerGenData = virtual_add.ToVirtual<virtual_altarItemGroup_brLegendaryMultiTreasure_broken_cells_doorCost_doorCurse_flaskRefill_forcedMerchantType_forcePauseTimer_isCliffPath_itemInWall_itemLevelBonus_killsMultiTreasure_locked_maxPerks_mins_noHealingShop_shouldBeFlipped_specificBiome_subTeleportTo_timedMultiTreasure_zDoorLock_zDoorType_>();
                    _Data data = Data.Class;
                    var gatdataid = data.level.byId.get(self.map.id);

                }
                catch (Exception ex)
                {
                    Logger.Error($"创建关卡显示失败: {ex.Message}");
                }
            }
        }
    }
}