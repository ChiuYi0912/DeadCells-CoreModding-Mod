using System.Runtime.InteropServices;
using dc;
using dc.en;
using dc.en.inter;
using dc.h2d;
using dc.pr;
using dc.tool;
using dc.tool.mod;
using dc.ui;
using HaxeProxy.Runtime;
using ModCore.Events.Interfaces.Game;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Modules;
using ModCore.Utitities;
using Serilog;



namespace Kaguya
{
    public class Kaguya(ModInfo info) : ModBase(info), IOnGameEndInit, IOnGameExit, IOnHeroUpdate
    {
        public static ILogger? logger;
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern short GetAsyncKeyState(int vkey);

        public override void Initialize()
        {
            logger = Logger;

            Logger.Information("哈喽!");

            Hook__Weapon.create += Hook__Weapon_create;
            Hook_TitleScreen.mainMenu += addmenu;

        }


        private void addmenu(Hook_TitleScreen.orig_mainMenu orig, TitleScreen self)
        {
            orig(self);

            if (self != null)
            {
                bool modding = true;
                var modding2 = new HaxeProxy.Runtime.Ref<bool>(ref modding);
                var popup = new dc.ui.ModalPopUp(modding2, 1);
                popup.text("游玩前须知：\n. 请在游戏内备份原始存档\n. 删除模组后，相关存档可能失效".AsHaxeString(), 0xFF0000, modding2);

            }
        }


        private Weapon Hook__Weapon_create(Hook__Weapon.orig_create orig, dc.en.Hero o, InventItem i)
        {
            var id = i._itemData.id.ToString();
            if (id == "OtherDashSword")
            {
                return new OtherDashSword(o, i);
            }
            else if (id == "Other_katana")
            {
                return new Other_katana(o, i);
            }
            else if (id == "Other_QueenRapier")
            {
                return new Other_QueenRapier(o, i);
            }
            else if (id == "Other_Starfury")
            {
                return new Other_Starfury(o, i);
            }
            else
            {
                return orig(o, i);
            }
        }

        void IOnGameEndInit.OnGameEndInit()
        {
            var res = Info.ModRoot!.GetFilePath("Kaguya.pak");
            FsPak.Instance.FileSystem.loadPak(res.AsHaxeString());
            var json = CDBManager.Class.instance.getAlteredCDB();
            dc.Data.Class.loadJson(
               json,
               default);

        }

        void IOnGameExit.OnGameExit()
        {
            Logger.Debug("模组已卸载");
            Hook_TitleScreen.mainMenu -= addmenu;
            Hook__Weapon.create -= Hook__Weapon_create;
            Hero hero = ModCore.Modules.Game.Instance.HeroInstance!;

        }

        private void SpawnWeapon(Hero hero)
        {
            InventItem testItem = new InventItem(new InventItemKind.Weapon("Other_Starfury".AsHaxeString()));
            bool test_boolean = false;
            ItemDrop itemDrop = new ItemDrop(hero._level, hero.cx, hero.cy, testItem, true, new HaxeProxy.Runtime.Ref<bool>(ref test_boolean));
            itemDrop.init();
            itemDrop.onDropAsLoot();
            itemDrop.dx = hero.dx;
        }
        private bool wasKeyPressed = false;

        void IOnHeroUpdate.OnHeroUpdate(double dt)
        {
            bool isKeyPressed = Key.Class.isPressed(0xDC);


            if (isKeyPressed && !wasKeyPressed)
            {
                SpawnWeapon(ModCore.Modules.Game.Instance.HeroInstance!);
            }


            wasKeyPressed = isKeyPressed;
        }


    }

    internal class PseudocodeHelper
    {
        internal static T CreateClosure<T>(object value, object methodHandle)
        {
            throw new NotImplementedException();
        }
    }

    internal static class Key
    {
        public static class Class
        {
            public static bool isPressed(int vkey)
            {
                return (GetAsyncKeyState(vkey) & 0x8000) != 0;
            }
        }

        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, ExactSpelling = true)]
        private static extern short GetAsyncKeyState(int vkey);

    }


}

