using System;
using dc;
using dc.hl.types;
using dc.libs.heaps.slib;
using ModCore.Utitities;
using Serilog;

namespace Outside_Clock;

public class ToolsMob : SpriteLib
{
    public ToolsMob(ArrayObj pages, ArrayObj normalPages) : base(pages, normalPages)
    {
    }

    public static dc.libs.heaps.slib.SpriteLib Replace_atlas(Hook_AssetsLibManager.orig_get orig, AssetsLibManager self, dc.String o, string oldatlas, string atlas_name)
    {

        if (o.ToString().Equals(oldatlas, StringComparison.CurrentCultureIgnoreCase))
        {
            Log.Debug($" 主图替换: {o.ToString()} -> {atlas_name}");
            return orig(self, atlas_name.AsHaxeString());
        }
        return orig(self, o);
    }

}
