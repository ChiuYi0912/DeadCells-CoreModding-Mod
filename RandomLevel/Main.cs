using dc;
using dc.en;
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

    private LevelStruct Hook_LevelStruct_get(Hook__LevelStruct.orig_get orig, User user, dynamic data, Rand rng)
    {

        if (Game.Class.ME == null || !Game.Class.ME.isBossRush())
        {
            string originalLevelId = data.id.ToString();

            if (allLevels.Contains(originalLevelId))
            {
                List<string> availableLevels = allLevels.Where(l => l != originalLevelId).ToList();

                if (availableLevels.Count > 0)
                {
                    int randomIndex = (int)(rng.random(availableLevels.Count));
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
}