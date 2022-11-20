using System.Drawing;
using CustomDoublyLinkedListLibrary;

namespace USATU_OOP_LW_6
{
    public class FiguresHandler
    {
        public delegate void NeedUpdateHandler();

        public event NeedUpdateHandler NeedUpdate;

        private readonly CustomDoublyLinkedList<Figure> _figures = new CustomDoublyLinkedList<Figure>();
        private bool _isMultipleSelectionEnabled;

        // TODO: Resize????

        public void DrawAllCirclesOnGraphics(Graphics graphics)
        {
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                i.Current.DrawOnGraphics(graphics);
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
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsPointInside(clickPoint))
                {
                    wasOnCircleClick = true;
                    if (!i.Current.IsSelected() && !_isMultipleSelectionEnabled)
                    {
                        TryUnselectAll();
                    }

                    i.Current.ProcessClick();
                }
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
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsPointInside(clickLocation))
                {
                    wasFigureClicked = true;
                    if (i.Current.IsSelected())
                    {
                        for (var k = _figures.GetPointerOnBeginning(); !k.IsBorderReached(); k.MoveNext())
                        {
                            if (k.Current.IsSelected())
                            {
                                k.Current.Color(color);
                            }
                        }
                    }
                    else
                    {
                        i.Current.Color(color);
                        TryUnselectAll();
                    }

                    break;
                }
            }

            if (wasFigureClicked || TryUnselectAll())
            {
                NeedUpdate?.Invoke();
            }
        }

        public void DeleteAllSelected()
        {
            bool wasFigureDeleted = false;
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsSelected())
                {
                    wasFigureDeleted = true;
                    _figures.RemovePointerElement(i);
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
            for (var i = _figures.GetPointerOnBeginning(); !i.IsBorderReached(); i.MoveNext())
            {
                if (i.Current.IsSelected())
                {
                    wasSomethingUnselected = true;
                    i.Current.Unselect();
                }
            }

            return wasSomethingUnselected;
        }
    }
}