using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace USATU_OOP_LW_6
{
    public class FiguresHandler
    {
        public delegate void NeedUpdateHandler();

        public event NeedUpdateHandler NeedUpdate;

        private readonly List<Figure> _figures = new List<Figure>(); //TODO: change to custom list
        private bool _isMultipleSelectionEnabled;

        // TODO: Resize????

        public void DrawAllCirclesOnGraphics(Graphics graphics)
        {
            foreach (var figure in _figures)
            {
                figure.DrawOnGraphics(graphics);
            }
        }

        public void EnableMultipleSelection()
        {
            _isMultipleSelectionEnabled = true;
        }

        public void DisableMultipleSelection()
        {
            _isMultipleSelectionEnabled = false;
        }

        public bool ProcessSelectionClick(Point clickPoint)
        {
            bool wasOnCircleClick = false;
            foreach (var figure in _figures.Where(figure => figure.IsPointInside(clickPoint)))
            {
                wasOnCircleClick = true;
                if (!figure.IsSelected() && !_isMultipleSelectionEnabled)
                {
                    TryUnselectAll();
                }

                figure.ProcessClick();
            }

            if (wasOnCircleClick || TryUnselectAll())
            {
                NeedUpdate?.Invoke();
            }

            return wasOnCircleClick;
        }

        public void AddFigure(Figures figureType, Color color, Point location)
        {
            switch (figureType)
            {
                case Figures.Circle:
                    _figures.Add(new Circle(color, location));
                    break;
            }

            TryUnselectAll();
            NeedUpdate?.Invoke();
        }

        public void ProcessColorClick(Point clickLocation, Color color)
        {
            bool wasFigureClicked = false;
            foreach (var figure in _figures.Where(figure => figure.IsPointInside(clickLocation)))
            {
                wasFigureClicked = true;
                if (figure.IsSelected())
                {
                    foreach (var currentFigure in _figures.Where(currentFigure => currentFigure.IsSelected()))
                    {
                        currentFigure.Color(color);
                    }
                }
                else
                {
                    figure.Color(color);
                    TryUnselectAll();
                }
                break;
            }

            if (wasFigureClicked || TryUnselectAll())
            {
                NeedUpdate?.Invoke();
            }
        }

        public void DeleteAllSelected()
        {
            bool wasFigureDeleted = false;
            for (int i = 0; i < _figures.Count; i++)
            {
                if (_figures[i].IsSelected())
                {
                    wasFigureDeleted = true;
                    _figures.RemoveAt(i);
                    i--;
                }
            }

            if (wasFigureDeleted)
            {
                NeedUpdate?.Invoke();
            }
        }

        private bool TryUnselectAll()
        {
            bool wasSomethingUnselected = false;
            foreach (var figure in _figures.Where(figure => figure.IsSelected()))
            {
                wasSomethingUnselected = true;
                figure.Unselect();
            }

            return wasSomethingUnselected;
        }
    }
}