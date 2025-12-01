using System.ComponentModel;
using dc;
using dc.cine;
using dc.en;
using dc.en.hero;
using dc.en.inter;
using dc.en.mob;
using dc.en.mob.boss;
using dc.en.tpet;
using dc.h2d;
using dc.hl.types;
using dc.level.disp;
using dc.libs.heaps.slib;
using dc.libs.misc;
using dc.pow;
using dc.pr;
using dc.shader;
using dc.tool;
using dc.tool.atk;
using dc.ui.popd;
using Hashlink.Virtuals;
using HaxeProxy.Runtime;
using Iced.Intel;
using ModCore.Events.Interfaces.Game.Hero;
using ModCore.Mods;
using ModCore.Utitities;
using Serilog;
using Slay;

namespace POP;

public class PopATT : ModBase, IOnHeroUpdate

{

    public PopATT(ModInfo info) : base(info)
    {

    }
    private Game? Game;
    public override void Initialize()
    {
        Log.Information("PopATT 运行");
        base.Initialize();
        Hook_Beheaded.postUpdate += Hook_Beheaded_uptata;
        Hook_Game.init += Hook_Game_init;
        Hook_EnterThroneRoomAsKing.update += Hook_EnterThroneRoomAsKing_update;
        Hook__EnterThroneRoomAsKing.__constructor__ += Hook__EnterThroneRoomAsKing__constructor__;
        Hook__HomunculusAnal.__constructor__ += Hook_HomunculusAnal___constructor__;

    }

    private void Hook_HomunculusAnal___constructor__(Hook__HomunculusAnal.orig___constructor__ orig, HomunculusAnal hom,
    Homunculus b, UsableBody fromExistingBody, bool skin, dc.String deathItemKind)
    {
        orig(hom, b, fromExistingBody, skin, deathItemKind);
        hom.baseColor = 16711680;
        hom.furyColor = 16711680;

    }

    private void Hook__EnterThroneRoomAsKing__constructor__(Hook__EnterThroneRoomAsKing.orig___constructor__ orig, EnterThroneRoomAsKing _hero, Hero game)
    {
        orig(_hero, game);

        var boss = _hero.boss;
        if (boss.cx - _hero.hero.cx > 3)
        {
            if (boss.spr.groupName?.ToString() != "runShield")
            {
                boss.spr.get_anim().play("runShield".AsHaxeString(), 0, false).loop(999);
            }
        }

    }

    private void Hook_EnterThroneRoomAsKing_update(Hook_EnterThroneRoomAsKing.orig_update orig, EnterThroneRoomAsKing self)
    {
        orig(self);
        self.cm = new Cinematic((int)self.tmod);
        var boss = self.boss;

        boss.dx = 0.5 * (double)boss.dir;
    }
    void IOnHeroUpdate.OnHeroUpdate(double dt)
    {

    }

    private void Hook_Game_init(Hook_Game.orig_init orig, Game self)
    {
        orig(self);
        Game = self;

    }

    private void Hook_Beheaded_uptata(Hook_Beheaded.orig_postUpdate orig, Beheaded self)
    {
        orig(self);
        double sec = 0.3;
        double alpha = 1;
        var refsec = new HaxeProxy.Runtime.Ref<double>(ref sec);
        var refalpha = new HaxeProxy.Runtime.Ref<double>(ref alpha);
        _OnionSkin _OnionSkin = OnionSkin.Class;
        OnionSkin onionSkin = _OnionSkin.fromEntity(self, null, null, refsec, refalpha, Ref<bool>.Null, Ref<bool>.Null, Ref<double>.Null);
        dc.h2d._BlendMode blend = dc.h2d.BlendMode.Class;
        //onionSkin.blendMode = blend;
        onionSkin.ds = 0;
        ColorMap shader = (ColorMap)self.spr.getShader(ColorMap.Class);
        int color;
        ColorBlend s;
        if (shader == null)
        {
            color = 0;
            s = new ColorBlend(color, 0.7);
            onionSkin.addAdditionnalShader(s);
            return;
        }
        onionSkin.addAdditionnalShader(shader);
        color = 0;
        s = new ColorBlend(color, 0.7);
        onionSkin.addAdditionnalShader(s);
        //new CaptainChicken(Game.Class.ME.curLevel, Game.Class.ME.hero, "hallo".AsHaxeString(), null).init();
    }


}
