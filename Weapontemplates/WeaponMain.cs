using Amazon.Runtime.Internal.Util;
using dc;
using dc.en;
using dc.h3d.pass;
using dc.hxd;
using dc.tool;
using HaxeProxy.Runtime;
using ModCore.Utitities;

namespace Weapontemplates;

public class WeaponMain : Weapon
{
    public WeaponMain(Hero i, InventItem idx) : base(i, idx)
    {
        weaponicon();
    }
    public static string name = "myweapon";

    public override bool onExecute()
    {
        return base.onExecute();
    }

    public override void fixedUpdate()
    {
        base.fixedUpdate();
    }

    public override void dispose()
    {
        base.dispose();
    }

    public override void hitFromWeapon(Entity _cycle, Ref<int> _cyc)
    {
        base.hitFromWeapon(_cycle, _cyc);
    }


    public void weaponicon()
    {
        var icon = Res.Class.load("Weapontemplates/weapon-icon.png".AsHaxeString());
        var virtualFile = new Hashlink.Virtuals.virtual_file_height_size_width_x_y_();
        virtualFile.file = icon.toString();
        base.item._itemData.icon = virtualFile;
    }
}
