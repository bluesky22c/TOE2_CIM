using System.Drawing;
using System.Windows.Forms;

namespace TOECIM.Controls
{
    /// <summary>
    /// WinForms TabControl의 네이티브 렌더링을 오버라이드하여
    /// 다크 테마 배경을 완전히 제어하는 커스텀 TabControl.
    /// </summary>
    public class DarkTabControl : TabControl
    {
        /// <summary>
        /// 탭 컨트롤 전체 배경색 (탭 헤더 영역 + 테두리 포함)
        /// </summary>
        public Color DarkBackColor { get; set; } = Color.FromArgb(25, 38, 52);

        /// <summary>
        /// 선택된 탭의 하단 강조선 색상
        /// </summary>
        public Color HighlightColor { get; set; } = Color.FromArgb(129, 212, 250); // LightBlue200

        public DarkTabControl()
        {
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.DoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            DrawMode = TabDrawMode.OwnerDrawFixed;
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            using var brush = new SolidBrush(DarkBackColor);
            pevent.Graphics.FillRectangle(brush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            // 전체 배경 채우기 (테두리 영역 포함)
            using (var bgBrush = new SolidBrush(DarkBackColor))
            {
                e.Graphics.FillRectangle(bgBrush, ClientRectangle);
            }

            // 각 탭 헤더 그리기
            for (int i = 0; i < TabCount; i++)
            {
                DrawTabHeader(e.Graphics, i);
            }
        }

        private void DrawTabHeader(Graphics g, int index)
        {
            var tabRect = GetTabRect(index);
            bool isSelected = (SelectedIndex == index);

            // 탭 헤더 배경
            using (var brush = new SolidBrush(DarkBackColor))
            {
                g.FillRectangle(brush, tabRect);
            }

            // 텍스트 색상
            Color textColor = isSelected
                ? Color.White
                : Color.FromArgb(200, 255, 255, 255);

            // 선택 탭 하단 강조선 (Material Style)
            if (isSelected)
            {
                using var pen = new Pen(HighlightColor, 3);
                g.DrawLine(pen,
                    tabRect.Left + 5, tabRect.Bottom - 2,
                    tabRect.Right - 5, tabRect.Bottom - 2);
            }

            // 텍스트 렌더링
            TextRenderer.DrawText(g, TabPages[index].Text, Font, tabRect, textColor,
                TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter);
        }
    }
}
