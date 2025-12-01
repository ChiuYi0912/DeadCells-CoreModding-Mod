using dc;
using dc.tool;
using dc.tool.weap;
using HaxeProxy.Runtime;
using Hero = dc.en.Hero;
using Mob = dc.en.Mob;

public class Other_katana : Katana

{
    public Other_katana(Hero i, InventItem skill) : base(i, skill)
    {
    }

    public override bool onExecute()
    {

        return base.onExecute();
    }

    public void dash(Mob slowMo, Ref<bool> slowM)
    {
        base.tryHitDash(slowMo, slowM);
    }

    public override void fixedUpdate()
    {
        base.fixedUpdate();
    }

    public override void dispose()
    {
        base.dispose();
    }

    public override void doAreaEffect(Area area)
    {
        base.doAreaEffect(area);
    }

    public override bool tryToCancel(bool s)
    {
        return base.tryToCancel(s);
    }

    public override void hitFromWeapon(Entity _cycle, Ref<int> _cycl)
    {
        base.hitFromWeapon(_cycle, _cycl);
    }

    public override void onOwnerCooldownEnd(dc.String k, int idx)
    {
        base.onOwnerCooldownEnd(k, idx);
    }
}