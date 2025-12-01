using dc;
using dc.ui;
using Hashlink.Virtuals;
using HaxeProxy.Runtime;
using ModCore.Utitities;
using Hook_Options = dc.ui.Hook_Options;
using Options = dc.ui.Options;
using System;
using static dc.ui.OptionsSection;
using dc.h2d;
using Serilog;
using S_ChiuYi;

namespace ChiuYiUI;

public class ChiuYiUI
{
    #region 主函数
    private readonly CHIUYIMain _mod;
    private readonly Scraf _scraf;

    public ChiuYiUI(CHIUYIMain mod)
    {
        _mod = mod;
        _scraf = new Scraf(mod);

    }

    public void Hook_Options_buildCurSection(Hook_Options.orig_buildCurSection orig, Options self)
    {
        int MY_BLANK_PAGE_INDEX = self.curSection.RawIndex;
        if (self.curSection.RawIndex == 17)
        {
            Graphics graphics = self.createScroller(0.75);
            AddCustomOptionsToSoundPage();

            Flow scrollerFlow = self.scrollerFlow;
            self.addSeparator("More Usability Settings".AsHaxeString(), scrollerFlow);
            addoverskin(self);
            self.addSeparator("Scarf Material Settings".AsHaxeString(), scrollerFlow);
            _scraf.AddSprIdToggle();
            self.addSeparator("Scarf 1 Configuration".AsHaxeString(), scrollerFlow);
            _scraf.AddScarfOption(0);
            scrollerFlow = self.scrollerFlow;
            self.addSeparator("Scarf 2 Configuration".AsHaxeString(), scrollerFlow);
            _scraf.AddScarfOption(1);
            scrollerFlow = self.scrollerFlow;
            self.addSeparator("Scarf 3 Configuration".AsHaxeString(), scrollerFlow);
            _scraf.AddScarfOption(2);
            scrollerFlow = self.scrollerFlow;
            self.addSeparator("Scarf 4 Configuration".AsHaxeString(), scrollerFlow);
            _scraf.AddScarfOption(3);
            scrollerFlow = self.scrollerFlow;
            self.addSeparator("Scarf 5 Configuration".AsHaxeString(), scrollerFlow);
            _scraf.AddScarfOption(4);
            return;
        }
        orig(self);
    }

    public void Hook_Options_showmain(Hook_Options.orig_showMain orig, Options self)
    {
        HlAction offsetX;
        OptionWidget optionWidge;
        dc.String subStr2 = Lang.Class.t.get("ChiuYi Mod Settings".AsHaxeString(), null);
        offsetX = new HlAction(() => ArrowFunction_showMain_Custom());
        optionWidge = self.addSimpleWidget(subStr2, null, offsetX, new Ref<int>(), null);
        orig(self);
    }


    private void ArrowFunction_showMain_Custom()
    {
        S_ChiuYi.S_ChiuYi customSection = new S_ChiuYi.S_ChiuYi();
        var options = Options.Class.ME;
        options.setSection(customSection);
    }

    private void AddCustomOptionsToSoundPage()
    {
        var options = Options.Class.ME;
        options.title.set_text("DCCM Mod Switch by bilibili ChiuYi.秋".AsHaxeString());
        _Assets _Assets = Assets.Class;
        //dc.ui.Text text = _Assets.makeText("这是一个空白页面".AsHaxeString(), dc.ui.Text.Class.COLORS.get("ST".AsHaxeString()), true, options.title);
    }
    #endregion

    #region 联动皮肤开关
    private void addoverskin(Options self)
    {
        var options = Options.Class.ME;
        var scrollerFlow = options.scrollerFlow;


        HlFunc<bool> toggleFunction = static () =>
        {
            bool newValue = !CHIUYIMain.SkinEnabled;
            CHIUYIMain.SkinEnabled = newValue;
            return newValue;
        };
        bool proxyValue = CHIUYIMain.SkinEnabled;
        ref bool proxyRef = ref proxyValue;
        OptionWidget allskin = options.addToggleWidget(
            "One-click Enable All Linked Costume Effects".AsHaxeString(),
            "Enable/Disable all costume effects including King and other special skins".AsHaxeString(),
            toggleFunction,
            Ref<bool>.From(ref proxyRef),
            scrollerFlow
        );


        scrollerFlow = options.scrollerFlow;
        HlFunc<bool> katanazero = static () =>
        {
            bool katanabool = !CHIUYIMain.skinkatana;
            CHIUYIMain.skinkatana = katanabool;
            return katanabool;
        };
        bool Katan1 = CHIUYIMain.skinkatana;
        ref bool proxyRef1 = ref Katan1;
        OptionWidget katanaskin = options.addToggleWidget(
            "Katana Zero Costume Effect".AsHaxeString(),
            "Enable/Disable Katana Zero costume effect".AsHaxeString(),
            katanazero,
            Ref<bool>.From(ref proxyRef1),
            scrollerFlow
        );


        scrollerFlow = options.scrollerFlow;
        HlFunc<bool> teleportToggle = static () =>
        {
            bool newValue = !CHIUYIMain.teleport;
            CHIUYIMain.teleport = newValue;
            return newValue;
        };
        bool teleportProxyValue = CHIUYIMain.teleport;
        ref bool teleportProxyRef = ref teleportProxyValue;
        options.addToggleWidget(
            "Risk of Rain Teleport Function".AsHaxeString(),
            "Enable/Disable teleport function".AsHaxeString(),
            teleportToggle,
            Ref<bool>.From(ref teleportProxyRef),
            scrollerFlow
        );


        scrollerFlow = options.scrollerFlow;
        HlFunc<bool> popd = static () =>
        {
            bool newValue = !CHIUYIMain.pop;
            CHIUYIMain.pop = newValue;
            return newValue;
        };
        bool opod1 = CHIUYIMain.pop;
        ref bool popDamage = ref opod1;
        options.addToggleWidget(
            "Slay the Spire Critical Effect".AsHaxeString(),
            "Enable/Disable Slay the Spire critical effect".AsHaxeString(),
            popd,
            Ref<bool>.From(ref popDamage),
            scrollerFlow
        );


        scrollerFlow = options.scrollerFlow;
        HlFunc<bool> sty = static () =>
        {
            bool newValue = !CHIUYIMain.rsty;
            CHIUYIMain.rsty = newValue;
            return newValue;
        };
        bool styy = CHIUYIMain.rsty;
        ref bool styy1 = ref styy;
        options.addToggleWidget(
            "Hotline Miami Critical Effect".AsHaxeString(),
            "Enable/Disable Hotline Miami critical effect".AsHaxeString(),
            sty,
            Ref<bool>.From(ref styy1),
            scrollerFlow
        );



    }
    #endregion
}