using System.Collections.Generic;

namespace SwappableHotbar
{
    public class Hotbar
    {
        public int StartIndex { get; private set; }

        public bool Visible
        {
            get => _visible;
            set
            {
                _visible = value;
                UpdateVisibility();
            }
        }

        private List<QuickSlotDisplay> _displays = new List<QuickSlotDisplay>();
        private bool _visible = true;

        public Hotbar(int startIndex, bool visible = true)
        {
            StartIndex = startIndex;
            Visible = visible;
        }

        public void QuickSlotInput(int index, Character character)
            => character.QuickSlotMngr.QuickSlotInput(StartIndex + index);

        public void AddDisplays(IEnumerable<QuickSlotDisplay> displays)
        {
            _displays.AddRange(displays);
            UpdateVisibility();
        }

        private void UpdateVisibility()
        {
            foreach (var display in _displays)
                display.transform.parent.gameObject.SetActive(_visible);
        }

    }
}
