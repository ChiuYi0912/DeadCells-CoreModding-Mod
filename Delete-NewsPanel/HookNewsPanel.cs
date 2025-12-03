using System.Runtime.InteropServices;
using dc.ui;
using ModCore.Events.Interfaces.Game;
using ModCore.Mods;

namespace Delete_NewsPanel
{
    public class HookNewsPanel : ModBase,
        IOnGameExit
    {
        public HookNewsPanel(ModInfo info) : base(info)
        {

        }
        public override void Initialize()
        {
            Hook_NewsPanel.updateVisible += Hook_NewsPanel_updateVisible;
            Hook_NewsPanel.focusIn += Hook_NewsPanel_focusIn;
        }

        private void Hook_NewsPanel_focusIn(Hook_NewsPanel.orig_focusIn orig, NewsPanel self)
        {

        }

        private void Hook_NewsPanel_updateVisible(Hook_NewsPanel.orig_updateVisible orig, NewsPanel self)
        {
            self.clean();
            self.text = null;
        }


        void IOnGameExit.OnGameExit()
        {
        }
    }
}