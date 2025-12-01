using dc;
using dc.cine;
using dc.en;
using dc.en.inter;
using dc.en.mob;
using dc.en.mob.boss;
using dc.hxd;
using dc.tool;
using dc.tool.utils;
using Hashlink.Marshaling;
using HaxeProxy.Runtime;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using System.Diagnostics;

namespace EnemiesVsEnemies
{
    public class ModMain(ModInfo info) : ModBase(info),
        IOnHeroUpdate
    {
        public int EnemiesCount = 1;

        public string EA = "Giant";
        public string BA = "GiantHand";
        public string CA = "Comboter";
        public string NA = "U28_VacuumCleaner";
        public string EB = "GiantHand";
        public int lifeTier = 30;
        public int dmgTier = 20;


        private Team teamA = null!;
        private Team teamB = null!;
        private ForceField? forceField;
        private nint origTeamA;
        private nint origTeamB;
        public override void Initialize()
        {
            teamA = new();
            teamB = new();

            teamA.opposingTeams = new()
            {
                array = new(HashlinkMarshal.Module.KnownTypes.Dynamic, 0)
            };

            teamB.opposingTeams = new()
            {
                array = new(HashlinkMarshal.Module.KnownTypes.Dynamic, 0)
            };

            origTeamA = teamA.opposingTeams.HashlinkPointer;
            origTeamB = teamB.opposingTeams.HashlinkPointer;

            teamA.opposingTeams.HashlinkObj.MarkStateful();
            teamB.opposingTeams.HashlinkObj.MarkStateful();

            teamA.opposingTeams.push(teamB);
            teamB.opposingTeams.push(teamA);

            Hook_Hero.canBeHitBy += Hook_Hero_canBeHitBy;
            dc.en.Hook_Mob.init += Hook_Mob_init;
            Hook_MeetCollector.onComplete += Hook_MeetCollector_onComplete;
            Hook_Throne.inBossBattle += Hook_Throne_inBossBattle;





            Hook_Golem.setAttackTarget += Hook_Golem_setAttackTarget;

            Hook_Behemoth.behaviourAi += Hook_Behemoth_behaviourAi;
            dc.en.Hook_Mob.setNemesisTarget += Hook_Mob_setNemesisTarget;
            dc.en.Hook_Mob.tpHeroBackToTraining += Hook_Mob_tpHeroBackToTraining;
        }

        private bool Hook_Throne_inBossBattle(Hook_Throne.orig_inBossBattle orig, Throne self)
        {
            throw new NotImplementedException();
        }

        private void Hook_MeetCollector_onComplete(Hook_MeetCollector.orig_onComplete orig, MeetCollector self)
        {

        }

        private void Hook_Mob_tpHeroBackToTraining(dc.en.Hook_Mob.orig_tpHeroBackToTraining orig, dc.en.Mob self)
        {
            if (self is Behemoth)
            {
                return;
            }
            orig(self);
        }

        private void Hook_Mob_init(dc.en.Hook_Mob.orig_init orig, dc.en.Mob self)
        {
            orig(self);
            if (self._team != Game.Instance.HeroInstance?._team &&
                self._team != teamA &&
                self._team != teamB)
            {
                self.set_team(teamB);
            }
            if (self is Boss boss)
            {
                //boss.cameraTrackingDisabled = true;
            }
        }
        private void Hook_Mob_setNemesisTarget(dc.en.Hook_Mob.orig_setNemesisTarget orig, dc.en.Mob self,
            Entity e)
        {
            if (e == Game.Instance.HeroInstance)
            {
                var team = self._team;
                var th = team.get_targetHelper();
                th.filterUntargetables();
                e = th.getBest();

                orig(self, th.getBest());
                return;
            }
            orig(self, e);
        }
        private void Hook_Behemoth_behaviourAi(Hook_Behemoth.orig_behaviourAi orig, Behemoth self)
        {
            var at = self.aTarget;
            if (at == Game.Instance.HeroInstance)
            {
                self.aTarget = null;
                self.clearNemesisTarget();
                return;
            }
            self.nemesisTarget = at;
            self.cameraTrackingDisabled = true;
            orig(self);
        }
        private void Hook_Golem_setAttackTarget(Hook_Golem.orig_setAttackTarget orig, Golem self, dc.Entity t)
        {
            var on = self.nemesisTarget;
            orig(self, t);
            self.nemesisTarget = on;
        }

        private bool Hook_Hero_canBeHitBy(Hook_Hero.orig_canBeHitBy orig, Hero self, dc.Entity by)
        {
            return false;
        }
        private void Spawn(string name, Team team)
        {
            var hero = Game.Instance.HeroInstance!;
            for (int i = 0; i < EnemiesCount; i++)
            {
                var lifeTier = this.lifeTier;
                var mob = Mob.Class.create(name.AsHaxeString(), hero._level, hero.cx, hero.cy, dmgTier,
                    Ref<int>.From(ref lifeTier));

                mob.init();

                Debug.Assert(teamA.opposingTeams.HashlinkPointer == origTeamA &&
                    teamB.opposingTeams.HashlinkPointer == origTeamB);

                mob.set_team(team);
                if (mob is Boss boss)
                {
                    boss.setReady();
                }
            }
        }
        void IOnHeroUpdate.OnHeroUpdate(double dt)
        {
            var hero = Game.Instance.HeroInstance!;
            if (Key.Class.isPressed(37 /* 1 */)) //Team A
            {
                Spawn(EA, teamA);
                Spawn(BA, teamA);
                //Spawn(CA, teamA);
                //Spawn(NA, teamA);

            }
            else if (Key.Class.isPressed(39 /* 2 */)) //Team B
            {
                Spawn(EB, teamB);
            }
            else if (Key.Class.isPressed(67 /* 2 */)) //Team B
            {
                GoldNugget go = new GoldNugget(hero._level, hero.cx, hero.cy + 20, 10);
                go.init();
            }
            else if (Key.Class.isPressed(38 /* 3 */)) //Spawn ForceField
            {
                forceField = new(hero._level, hero.cx, hero.cy, false);
                forceField.init();
            }
            else if (Key.Class.isPressed(40 /* 4 */)) //Close ForceField
            {
                forceField?.close(null);
            }
            else if (Key.Class.isPressed(112 /* 0 */)) //End
            {
                forceField?.open();
                forceField = null;

                hero.visible = true;
                hero.hasGravity = true;
            }
            else if (Key.Class.isPressed(113 /* 5 */)) //Begin
            {
                hero.visible = false;
                hero.hasGravity = false;


                forceField?.open();
            }
        }
    }
}
