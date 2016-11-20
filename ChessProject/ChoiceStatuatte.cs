using System;
using System.Windows.Forms;
using log4net;

namespace ChessProject
{
    public partial class ChoiceStatuatte : Form
    {
        private static readonly ILog Log = LogManager.GetLogger("ChoiseStatuatteLog");
        public ChoiceStatuatte()
        {
            InitializeComponent();
        }

        private Cell[] cells = new Cell[4];

        private Cell.Color Color; //Переменная для вывода фигур нужного цвета 
        public Cell.Chessman Type; //Переменная для возврата типа выбранной фигуры 
        private void Drawing(Cell.Color Color)
        {
            Log.Info("Start: Drawing();");
            for (int i = 0; i < 4; i++)
            {
                cells[i] = new Cell(i, Color) { Parent = this };
                cells[i].Click += new EventHandler(Click);
            }
            Log.Info("End: Drawing();");
        }

        public ChoiceStatuatte(Cell.Color color)
        {
            Log.Info("Start: ChoiceStatuatte();");
            Color = color;
            Drawing(color);
            InitializeComponent();
            Log.Info("End: ChoiceStatuatte();");
        }

        private void Click(object sender, EventArgs e)
        {
            Log.Info("Start: Click();");
            Cell cell = sender as Cell;
            Type = cell.ChessmanType;
            Dispose();
            Log.Info("End: Click();");
        }
    }
}