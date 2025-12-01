using dc.en.inter;
using dc.en.inter.npc;
using dc.level;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Utitities;
using Hero = dc.en.Hero;
using Log = Serilog.Log;



namespace SkinLevel
{



    public class SkinLevel : ModBase,
        IOnGameExit, IOnHeroUpdate
    {

        private const int VK_OEM_5 = 0xDC;
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern short GetAsyncKeyState(int vkey);
        private static bool isLastFramePressed = false;
        private static bool isHookInstalled = false;
        private static bool isCurrentFramePressed = false;
        private static bool isCurrentFrameReleased = false;

        public SkinLevel(ModInfo info) : base(info)
        {

        }
        public override void Initialize()
        {
            base.Initialize();


        }

        public void OnHeroUpdate(double dt)
        {
            double timeDelt = 0;
            timeDelt += dt;

            Hero hero = ModCore.Modules.Game.Instance.HeroInstance;
            bool isCurrentFramePressed = (GetAsyncKeyState(VK_OEM_5) & 0x8000) != 0;
            if (isCurrentFramePressed && !isLastFramePressed && hero != null)
            {


                Room room = new Room(hero._level.map, 1, "PrisonTube2NoSkel".AsHaxeString(), "PrisonTube2NoSkel".AsHaxeString(), 1);
                room.revealed = false;
                room.spawnDistance = 0;
                room.isZChild = false;
                room.wid = 2048;
                room.hei = 2048;

                Tailor tailor = new Tailor(hero._level, room);
                tailor.init();
                tailor.daughter.cx = hero.cx - 4;
                tailor.daughter.cy = hero.cy;
                tailor.cx = hero.cx;
                tailor.cy = hero.cy;
                tailor.xr = hero.xr;
                tailor.yr = hero.yr;
                tailor.setPosCase(hero.cx, hero.cy, hero.xr, hero.yr);

                Skinner skinner = new Skinner(hero._level, hero.cx + 4, hero.cy);
                skinner.init();

                timeDelt += dt;
                Log.Information("生成裁缝");
            }

            isLastFramePressed = isCurrentFramePressed;
        }

        void IOnGameExit.OnGameExit()
        {

        }
    }



}