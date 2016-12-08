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
            for (int i = 0; i < 4; i++)
            {
                cells[i] = new Cell(i, Color) { Parent = this };
                cells[i].Click += new EventHandler(Click);
            }
        }

        public ChoiceStatuatte(Cell.Color color)
        {
            Color = color;
            Drawing(color);
            InitializeComponent();
        }

        private void Click(object sender, EventArgs e)
        {
            Cell cell = sender as Cell;
            Type = cell.ChessmanType;
            Dispose();
        }
    }
}