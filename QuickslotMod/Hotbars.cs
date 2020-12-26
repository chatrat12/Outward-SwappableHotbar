using System.Collections.Generic;
using System.Linq;

namespace SwappableHotbar
{
    public class Hotbars
    {
        public const int HOTBAR_LENGTH = 12;

        public Hotbar Frontbar { get; } = new Hotbar(0);
        public Hotbar Backbar { get; } = new Hotbar(HOTBAR_LENGTH, false);
        public Hotbar ActiveBar => _frontbarActive ? Frontbar : Backbar;

        private bool _frontbarActive = true;

        public void AddDisplays(IEnumerable<QuickSlotDisplay> _displays)
        {
            Frontbar.AddDisplays(_displays.Where(d => d.RefSlotID < Backbar.StartIndex));
            Backbar.AddDisplays(_displays.Where(d => d.RefSlotID >= Backbar.StartIndex));
        }

        public void SwapBars()
        { 
            _frontbarActive = !_frontbarActive;
            Frontbar.Visible = _frontbarActive;
            Backbar.Visible = !_frontbarActive;
        }
    }
}