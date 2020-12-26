using SharedModConfig;
using UnityEngine;
using UnityEngine.UI;

namespace SwappableHotbar
{
    public class HotbarLayout
    {
        private const float DEFAULT_X_CENTER = 0;
        private const float DEFAULT_X_RIGHT = -12;
        private const float DEFAULT_Y = 14f;
        private static Vector2 PIVOT_CENTER = Vector2.right * 0.5f;
        private static Vector2 PIVOT_RIGHT = Vector2.right;

        public int Rows
        {
            get => _grid != null ? _grid.constraintCount : 1;
            set { if (_grid != null) _grid.constraintCount = value; }
        }
        public bool Centered
        {
            get => _centered;
            set
            {
                _centered = value;
                SetRectPivotAndAnchors(_centered ? PIVOT_CENTER : PIVOT_RIGHT);
                UpdateXPosition();
            }
        }
        public float XOffset
        {
            get => _xOffset;
            set
            {
                _xOffset = value;
                UpdateXPosition();
            }
        }
        public float YOffset
        {
            get => _rect.anchoredPosition.y;
            set
            {
                var pos = _rect.anchoredPosition;
                pos.y = value + DEFAULT_Y;
                _rect.anchoredPosition = pos;
            }
        }
        public float HorizontalSpacing
        {
            get => _grid.spacing.x;
            set
            {
                var spacing = _grid.spacing;
                spacing.x = value;
                _grid.spacing = spacing;
            }
        }

        public float VerticalSpacing
        {
            get => _grid.spacing.y;
            set
            {
                var spacing = _grid.spacing;
                spacing.y = value;
                _grid.spacing = spacing;
            }
        }

        private bool _centered = false;
        private float _xOffset = 0;
        private GridLayoutGroup _grid;
        private RectTransform _rect;

        public HotbarLayout(GridLayoutGroup grid)
        {
            _grid = grid;
            _rect = grid.GetComponent<RectTransform>();
        }

        public void ApplySettings(ModConfig settings)
        {
            Rows = (int)(float)settings.GetValue(ConfigSettings.SETTING_NAME_ROWS);
            Centered = (bool)settings.GetValue(ConfigSettings.SETTING_NAME_CENTER);
            XOffset = (float)settings.GetValue(ConfigSettings.SETTING_NAME_XOFFSET);
            YOffset = (float)settings.GetValue(ConfigSettings.SETTING_NAME_YOFFSET);
            HorizontalSpacing = (float)settings.GetValue(ConfigSettings.SETTING_NAME_XSPACING);
            VerticalSpacing = (float)settings.GetValue(ConfigSettings.SETTING_NAME_YSPACING);
        }

        private void SetRectPivotAndAnchors(Vector2 value)
        {
            if (_rect == null) return;
            _rect.anchorMin = value;
            _rect.anchorMax = value;
            _rect.pivot = value;
        }

        private void UpdateXPosition()
        {
            if (_rect == null) return;
            var pos = _rect.anchoredPosition;
            pos.x = (_centered ? DEFAULT_X_CENTER : DEFAULT_X_RIGHT) + _xOffset;
            _rect.anchoredPosition = pos;
        }
    }
}