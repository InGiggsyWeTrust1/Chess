using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChessProject
{
    public sealed partial class Cell : UserControl
    {
        /// <summary>
        /// Стандартный конструктор
        /// </summary>
        public Cell()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Перечисление: определяет, есть ли на клетке фигура или нет.
        /// </summary>
        public enum Chessman
        {
            Null,
            Pawn,
            Queen,
            King,
            Bishop,
            Knigth,
            Rook
        };

        /// <summary>
        /// Перечисление: цвет фигуры, которая стоит на клетке
        /// </summary>
        public enum Color
        {
            Null,
            White,
            Black
        };

        /// <summary>
        /// Перечисление: первый ход был или не был
        /// </summary>
        public enum FirstStep
        {
            No,
            Yes
        }

        /// <summary>
        /// Перечисление: Атака на проходе
        /// </summary>
        public enum EnPassant
        {
            No,
            Yes
        };

        /// <summary>
        /// Номер строки
        /// </summary>
        public int LineNumber { get; set; }
        
        /// <summary>
        /// Номер столбца
        /// </summary>
        public int ColumnNumber { get; set; } 
        
        /// <summary>
        /// Свойство "Атака на проходе"
        /// </summary>
        public EnPassant enPassant { get; set; }
        
        /// <summary>
        /// Тип фигуры
        /// </summary>
        public Chessman ChessmanType { get; set; }
        
        /// <summary>
        /// Цвет фигуры
        /// </summary>
        public Color ChessmanColor { get; set; }

        /// <summary>
        /// Первый ход фигуры
        /// </summary>
        public FirstStep StatuatteFirstStep { get; set; }

        /// <summary>
        /// Метод установки цвета для клетки
        /// </summary>
        /// <param name="_lineNumber">Номер строки</param>
        /// <param name="_colomnNumber">Номер столбца</param>
        private void SetupColor(int _lineNumber, int _colomnNumber)
        {
            if (_colomnNumber%2 == 0)
                BackColor = _lineNumber%2 == 0 ? System.Drawing.Color.White : System.Drawing.Color.Chocolate;
            else
            {
                BackColor = _lineNumber%2 == 0 ? System.Drawing.Color.Chocolate : System.Drawing.Color.White;
            }
        }

        /// <summary>
        /// Метод установки картинок фигур
        /// </summary>
        /// <param name="_lineNumber">Номер строки</param>
        /// <param name="_colomnNumber">Номер столбца</param>
        private void SetupImage(int _lineNumber, int _colomnNumber)
        {
            if (_colomnNumber < 8 && _colomnNumber >= 0)
            {
                switch (_lineNumber)
                {
                    case 1:
                        BackgroundImage = Properties.Resources.PawnBlack;
                        ChessmanColor = Color.Black;
                        ChessmanType = Chessman.Pawn;
                        enPassant = EnPassant.No; 
                        StatuatteFirstStep = FirstStep.Yes;
                        break;
                    case 6:
                        BackgroundImage = Properties.Resources.PawnWhite;
                        ChessmanColor = Color.White;
                        ChessmanType = Chessman.Pawn;
                        enPassant = EnPassant.No;
                        StatuatteFirstStep = FirstStep.Yes;
                        break;
                }
            }
            if (_lineNumber == 0)
            {
                ChessmanColor = Color.Black;
                if (_colomnNumber == 0 || _colomnNumber == 7)
                {
                    BackgroundImage = Properties.Resources.RookBlack;
                    ChessmanType = Chessman.Rook;
                    StatuatteFirstStep = FirstStep.Yes;
                }
                else if (_colomnNumber == 1 || _colomnNumber == 6)
                {
                    BackgroundImage = Properties.Resources.KnightBlack;
                    ChessmanType = Chessman.Knigth;
                    ;
                }
                else if (_colomnNumber == 2 || _colomnNumber == 5)
                {
                    BackgroundImage = Properties.Resources.BishopBlack;
                    ChessmanType = Chessman.Bishop;
                }
                else if (_colomnNumber == 3)
                {
                    BackgroundImage = Properties.Resources.QueenBlack;
                    ChessmanType = Chessman.Queen;
                }
                else if (_colomnNumber == 4)
                {
                    BackgroundImage = Properties.Resources.KingBlack;
                    ChessmanType = Chessman.King;
                    StatuatteFirstStep = FirstStep.Yes;
                }
            }

            if (_lineNumber == 7)
            {
                ChessmanColor = Color.White;
                if (_colomnNumber == 0 || _colomnNumber == 7)
                {
                    BackgroundImage = Properties.Resources.RookWhite;
                    ChessmanType = Chessman.Rook;
                    StatuatteFirstStep = FirstStep.Yes;
                }
                else if (_colomnNumber == 1 || _colomnNumber == 6)
                {
                    BackgroundImage = Properties.Resources.KnightWhite;
                    ChessmanType = Chessman.Knigth;
                }
                else if (_colomnNumber == 2 || _colomnNumber == 5)
                {
                    BackgroundImage = Properties.Resources.BishopWhite;
                    ChessmanType = Chessman.Bishop;
                }
                else if (_colomnNumber == 3)
                {
                    BackgroundImage = Properties.Resources.QueenWhite;
                    ChessmanType = Chessman.Queen;
                }
                else if (_colomnNumber == 4)
                {
                    BackgroundImage = Properties.Resources.KingWhite;
                    ChessmanType = Chessman.King;
                    StatuatteFirstStep = FirstStep.Yes;
                }
            }
        }

        /// <summary>
        /// Перегруженный конструктор класса.
        /// </summary>
        /// <param name="_lineNumber">Номер строки</param>
        /// <param name="_columnNumber">Номер колонки</param>
        public Cell(int _lineNumber, int _columnNumber)
        {
            LineNumber = _lineNumber;
            ColumnNumber = _columnNumber;
            Size = new Size(60, 60);
            Location = new Point(ColumnNumber*60 + 262, LineNumber*60 + 120);
            ChessmanType = Chessman.Null;
            SetupColor(LineNumber, ColumnNumber);
            SetupImage(LineNumber, ColumnNumber);
            BackgroundImageLayout = ImageLayout.Center;
            InitializeComponent();
        }

        /// <summary>
        /// Конструктор для окна "Выбор фигуры"
        /// </summary>
        /// <param name="_columnNumber">Номер стол</param>
        /// <param name="Color">Цвет фигуры</param>
        public Cell(int _columnNumber, Cell.Color Color)
        {
            ColumnNumber = _columnNumber;
            LineNumber = 0;
            ChessmanColor = Color;
            Size = new Size(50, 50);
            Location = new Point(50 + ColumnNumber*50, 20);
            switch (ColumnNumber)
            {
                case 0:
                    ChessmanType = Cell.Chessman.Bishop;
                    BackgroundImage = (ChessmanColor == Color.White)
                        ? Properties.Resources.BishopWhite
                        : Properties.Resources.BishopBlack;
                    break;
                case 1:
                    ChessmanType = Cell.Chessman.Queen;
                    BackgroundImage = (ChessmanColor == Color.White)
                        ? Properties.Resources.QueenWhite
                        : Properties.Resources.QueenBlack;
                    break;
                case 2:
                    ChessmanType = Cell.Chessman.Knigth;
                    BackgroundImage = (ChessmanColor == Color.White)
                        ? Properties.Resources.KnightWhite
                        : Properties.Resources.KnightBlack;
                    break;
                case 3:
                    ChessmanType = Cell.Chessman.Rook;
                    BackgroundImage = (ChessmanColor == Color.White)
                        ? Properties.Resources.RookWhite
                        : Properties.Resources.RookBlack;
                    break;
            } 
        }
    }
}
