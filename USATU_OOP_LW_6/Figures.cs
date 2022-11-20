using System.Drawing;

namespace USATU_OOP_LW_6
{
    public enum Figures
    {
        Circle,
        Triangle,
        Square,
        Line
    }

    public static class SelectionBorder
    {
        private const int SelectionBorderWidth = 5;
        private static readonly Color SelectionColor = Color.Black;
        private static readonly float[] DashValues = {1, 1};
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

    public abstract class Figure // TODO: GeometricObject -> (Figure -> Circle etc), Line
    {
        protected Rectangle FigureRectangle;
        protected readonly SolidBrush CurrentBrush;
        protected virtual Size DefaultSize { get; } = Size.Empty;

        private bool _isSelected;

        protected Figure(Color color, Point centerLocation)
        {
            var leftTopPoint = new Point(centerLocation.X - DefaultSize.Width / 2,
                centerLocation.Y - DefaultSize.Height / 2);
            FigureRectangle = new Rectangle(leftTopPoint, DefaultSize);
            CurrentBrush = new SolidBrush(color);
        }

        public bool IsFigureOutside(Rectangle backgroundRectangle)
        {
            //backgroundRectangle.IntersectsWith() // TODO: check this realization variant

            return 0 <= FigureRectangle.Right && FigureRectangle.Left <= backgroundRectangle.Width &&
                   backgroundRectangle.Height <= FigureRectangle.Top && FigureRectangle.Bottom <= 0;
        }

        public void Color(Color newColor) => CurrentBrush.Color = newColor;

        public void Resize(Size newSize) => FigureRectangle.Size = newSize;

        public void Move(Point newLocation) => FigureRectangle.Location = newLocation;

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

        public abstract bool IsPointInside(Point pointToCheck);
        protected abstract void DrawFigureOnGraphics(Graphics graphics);

        private void DrawSelectionBorders(Graphics graphics)
        {
            SelectionBorder.DrawSelectionBorder(graphics, FigureRectangle);
        }
    }

    public class Circle : Figure
    {
        protected override Size DefaultSize { get; } = new Size(50, 50);

        public Circle(Color color, Point location) : base(color, location)
        {
        }

        public override bool IsPointInside(Point pointToCheck)
        {
            var circleRadius = FigureRectangle.Width / 2;
            var tmpX = pointToCheck.X - FigureRectangle.Location.X - circleRadius;
            var tmpY = pointToCheck.Y - FigureRectangle.Location.Y - circleRadius;
            return tmpX * tmpX + tmpY * tmpY <= circleRadius * circleRadius;
        }

        protected override void DrawFigureOnGraphics(Graphics graphics)
        {
            graphics.FillEllipse(CurrentBrush, FigureRectangle);
        }
    }

    //TODO: Triangle
    //TODO: Square, Line
    //TODO: Line
}