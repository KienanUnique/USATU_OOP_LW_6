using System.Drawing;

namespace USATU_OOP_LW_6
{
    public static class SelectionBorder
    {
        private const int SelectionBorderWidth = 5;
        private static readonly Color SelectionColor = Color.Black;
        private static readonly float[] DashValues = {2, 2};
        private static readonly Pen BorderPen;

        static SelectionBorder()
        {
            BorderPen = new Pen(SelectionColor, SelectionBorderWidth);
            BorderPen.DashPattern = DashValues;
        }

        public static void DrawSelectionBorder(Graphics graphics, Rectangle figureRectangle)
        {
            graphics.DrawRectangle(BorderPen, figureRectangle);
        }
    }

    public abstract class Figure
    {
        protected Color _color;
        protected Rectangle _rectangle;
        protected SolidBrush _currentBrush;
        protected bool _isSelected;
        public abstract bool IsPointInside(Point pointToCheck);
        public abstract void DrawFigureOnGraphics(Graphics graphics);

        public bool IsFigureOutside(Rectangle backgroundRectangle)
        {
        }

        public void Color(Color newColor)
        {
        }

        public void Resize(Rectangle newSize)
        {
        }

        public void Move(Point newPosition)
        {
        }

        public void DrawOnGraphics(Graphics graphics)
        {
            DrawFigureOnGraphics(graphics);
            if (_isSelected)
            {
                DrawSelectionBorders(graphics);
            }
        }

        public bool IsSelected()
        {
            return _isSelected;
        }

        public void Select()
        {
            _isSelected = true;
        }

        public void Unselect()
        {
            _isSelected = false;
        }

        public void ProcessClick()
        {
            _isSelected = !_isSelected;
        }

        private void DrawSelectionBorders(Graphics graphics)
        {
            SelectionBorder.DrawSelectionBorder(graphics, _rectangle);
        }
    }

    public class Circle : Figure
    {
        public override bool IsPointInside(Point pointToCheck)
        {
            var circleRadius = _rectangle.Width / 2;
            var tmpX = pointToCheck.X - _rectangle.Location.X - circleRadius;
            var tmpY = pointToCheck.Y - _rectangle.Location.Y - circleRadius;
            return tmpX * tmpX + tmpY * tmpY <= circleRadius * circleRadius;
        }

        public override void DrawFigureOnGraphics(Graphics graphics)
        {
            graphics.FillEllipse(_currentBrush, _rectangle);
        }
    }
}