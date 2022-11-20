using System.Drawing;
using System.Windows.Forms;

namespace USATU_OOP_LW_6
{
    public partial class FormMain : Form
    {
        private readonly FiguresHandler _figuresHandler;
        private bool _wasControlAlreadyPressed;

        public FormMain()
        {
            InitializeComponent();
            _figuresHandler = new FiguresHandler();
            this.KeyPreview = true;
            _figuresHandler.NeedUpdate += panelForDrawing_Update;
        }

        private void panelForDrawing_Paint(object sender, PaintEventArgs e)
        {
            _figuresHandler.DrawAllCirclesOnGraphics(e.Graphics);
        }

        private void panelForDrawing_Update()
        {
            panelForDrawing.Invalidate();
        }

        private void panelForDrawing_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (!_figuresHandler.ProcessSelectionClick(e.Location))
                {
                    _figuresHandler.AddFigure(Figures.Circle, Color.Black, e.Location);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                _figuresHandler.ProcessColorClick(e.Location, Color.Coral);
            }
        }

        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey && !_wasControlAlreadyPressed)
            {
                _figuresHandler.EnableMultipleSelection();
                _wasControlAlreadyPressed = true;
            }
        }

        private void FormMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.ControlKey:
                    _figuresHandler.DisableMultipleSelection();
                    _wasControlAlreadyPressed = false;
                    break;
                case Keys.Delete:
                    _figuresHandler.DeleteAllSelected();
                    break;
            }
        }
    }
}