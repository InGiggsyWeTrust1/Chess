using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Windows.Forms;
using ChessProject.Properties;
using log4net;
using System.IO;

namespace ChessProject
{
    public partial class Chess : Form
    {
        private static readonly ILog Log = LogManager.GetLogger("ChessLog");
        private bool ChekemateActive = false;

        private ChessmanReader globalReader = new ChessmanReader(); //Обработчик записи в файл

        private bool CastlingEnebleforBlack = true;
        private bool CastlingEnebleforWhite = true;

        private Cell[,] Cells = new Cell[8, 8]; //Шахматная доска

        private Label[] MarkerA = new Label[16]; //Маасив меток с буквами по кроям доски
        private Label[] MarkerN = new Label[16]; //Маасив меток с цифрами по кроям доски

        private int VNP = 0; // Флажок для отслеживания следующего хода, когда активировано взятие на проходе
        private bool forValidation = false; // Флажок, показывающий пройденную валидацию (относится к взятию на проходе)

        private bool inCheckmate = false;
            // Флажок, показывающий, что функция "Checkmate(...)" начала выполнение (относиться к взятию на проходе) 

        private int currentX; //  Текущая позиция координаты Х
        private int currentY; //  Текущая позиция координаты Y

        private int enPassantX; //  
        private int enPassantY; //  Переменные для отслеживания взятия на проходе
        private int enPassantYY; //

        private int directionX; //  Определяем направление хода по координате X
        private int directionY; //  Определяем направление хода по координате Y

        private bool firstClick = true;
        private int emptyPath = 1; //  Считает сколько пустых клеток до пункта назначения

        //Переменые для хранения позиции королей
        private int BKingPosX = 4;
        private int BKingPosY = 0;

        private int WKingPosX = 4;
        private int WKingPosY = 7;

        List<int> fireArea = new List<int>();
            //Динамический масив где хронятс клетки через которые пройдет фигура котороя пытается убить короля

        private bool IsShah = false; //Если значение true то объявлен шах

        Color color;

        public Cell.Color Iam = Cell.Color.White;
        public Cell.Color Enemy = Cell.Color.Black;

        public Cell.Chessman choiceChessman;

        /// <summary>
        /// Основной конструктор
        /// </summary>
        public Chess()
        {
            InitializeComponent();
            //Log.Info("Start app");
            KeyPreview = true;
        }

        /// <summary>
        /// Смена черных на белые
        /// </summary>
        private void SwapColor()
        {
            //Log.Info("Start: SwapColor();");
            Cell.Color tmp = Iam;
            Iam = Enemy;
            Enemy = tmp;
            //Log.Info("End: SwapColor();");
        }

        /// <summary>
        /// Рисует буквы и цифры рядом с доской
        /// </summary>
        private void DrawingAlpha()
        {
            //Log.Info("Start: DrawingAlpha();");
            try
            {
                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        char[] alpha = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
                        MarkerA[i + 8*j] = new Label();
                        this.Controls.Add(MarkerA[i + 8*j]);
                        MarkerA[i + 8*j].Text = alpha[i].ToString();
                        MarkerA[i + 8*j].Size = new Size(30, 30);
                        switch (j)
                        {
                            case 0:
                                MarkerA[i + 8*j].Location = new Point(i*60 + 285, 100);
                                break;
                            case 1:
                                MarkerA[i + 8*j].Location = new Point(i*60 + 285, 610);
                                break;
                        }
                    }
                }

                for (int j = 0; j < 2; j++)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        MarkerN[i + 8*j] = new Label();
                        this.Controls.Add(MarkerN[i + 8*j]);
                        MarkerN[i + 8*j].Text = (i + 1).ToString();
                        MarkerN[i + 8*j].Size = new Size(60, 60);
                        switch (j)
                        {
                            case 0:
                                MarkerN[i + 8*j].Location = new Point(235, i*60 + 135);
                                break;
                            case 1:
                                MarkerN[i + 8*j].Location = new Point(760, i*60 + 135);
                                break;
                        }
                    }
                }
                //Log.Info("End: DrawingAlpha();");
            }
            catch (Exception ex)
            {
                //Log.Info("DrawingAlpha(); ", ex);
            }
        }

        /// <summary>
        /// Метод отрисовки шахматной доски
        /// </summary>
        private void Drawing()
        {
            //Log.Info("Start: Drawing();");
            try
            {
                var N = 8;
                for (var i = 0; i < N; i++)
                {
                    for (var j = 0; j < N; j++)
                    {
                        Cells[i, j] = new Cell(i, j) {Parent = this};
                        Cells[i, j].Click += new EventHandler(Cell_Click);
                    }
                }

                DrawingAlpha();
                //Log.Info("End: Drawing();");
            }
            catch (Exception ex)
            {
                //Log.Info("Drawing(); ", ex);
            }
        }

        /// <summary>
        /// Метод смены фигуры, когда пешка доходит до конца поля
        /// </summary>
        /// <param name="newX">Координата пешки по X</param>
        /// <param name="newY">Координата пешки по Y</param>
        /// <param name="color">Цвет фигуры</param>
        public void ChoiceChessman(int newX, int newY, Cell.Color color)
        {
            //Log.Info("Start: ChoiceChessman();");
            try
            {
                ChoiceStatuatte Choice = new ChoiceStatuatte(color);
                Choice.ShowDialog();
                Cells[newY, newX].ChessmanType = Choice.Type;
                switch (Choice.Type)
                {
                    case Cell.Chessman.Bishop:
                        Cells[newY, newX].BackgroundImage = (color == Cell.Color.White)
                            ? Properties.Resources.BishopWhite
                            : Properties.Resources.BishopBlack;
                        break;
                    case Cell.Chessman.Queen:
                        Cells[newY, newX].BackgroundImage = (color == Cell.Color.White)
                            ? Properties.Resources.QueenWhite
                            : Properties.Resources.QueenBlack;
                        break;
                    case Cell.Chessman.Knigth:
                        Cells[newY, newX].BackgroundImage = (color == Cell.Color.White)
                            ? Properties.Resources.KnightWhite
                            : Properties.Resources.KnightBlack;
                        break;
                    case Cell.Chessman.Rook:
                        Cells[newY, newX].BackgroundImage = (color == Cell.Color.White)
                            ? Properties.Resources.RookWhite
                            : Properties.Resources.RookBlack;
                        break;
                }
                //Log.Info("End: ChoiceChessman();");
            }
            catch (Exception ex)
            {
                //Log.Info("ChoiceChessman(); ", ex);
            }
        }

        /// <summary>
        /// Запуск игры по клику на кнопку "Start Game"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startGameButton_Click(object sender, EventArgs e)
        {
            //Log.Info("Start: startGameButton_Click();");
            Drawing();
            startGameButton.Visible = false;
            figureCourses.Visible = true;
            menuStrip.Visible = true;

            if (!File.Exists("SaveGame/Save1.txt"))
                продолжитьToolStripMenuItem.Enabled = false;
            globalReader.Open("globalReader.txt", "sw");

            //SoundPlayer player = new SoundPlayer(Resources.BattleCity);
            //player.Play();
            //Log.Info("End: startGameButton_Click();");
        }

        /// <summary>
        /// Метод перестановки фигур
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        private void Movement(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: Movement();");
            bool can = true;
            try
            {
                if (IsShah)
                {
                    if (!(OnEmbrasure(newX, newY) || Cells[previosY, previosX].ChessmanType == Cell.Chessman.King))
                        can = false;
                }

                bool problem = TraitorChackmate(previosX, previosY, newX, newY);

                if (can && !problem)
                {
                    Cells[newY, newX].BackgroundImage = Cells[previosY, previosX].BackgroundImage;
                    Cells[newY, newX].ChessmanType = Cells[previosY, previosX].ChessmanType;
                    Cells[newY, newX].ChessmanColor = Cells[previosY, previosX].ChessmanColor;
                    Cells[previosY, previosX].BackgroundImage = null;
                    Cells[previosY, previosX].ChessmanType = Cell.Chessman.Null;
                    Cells[previosY, previosX].ChessmanColor = Cell.Color.Null;
                    SwapColor();

                    globalReader.WriteStep(previosX, previosY, newX, newY);
                }
                //Log.Info("End: Movement();");
            }
            catch (Exception ex)
            {
                //Log.Info("Movement(); ", ex);
            }
        }

        /// <summary>
        /// Метод запаси ходов в текстбокс
        /// </summary>
        /// <param name="typeStatuatte">Тип фигуры</param>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую поставили фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую поставили фигуру</param>
        private void FigureCourses(string typeStatuatte, int previosX, int previosY, int newX, int newY)
        {
            char[] alpha = {'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H'};
            string previosCell = alpha[previosX].ToString() + (previosY + 1).ToString();
            string newCell = alpha[newX].ToString() + (newY + 1).ToString();
            string color;
            if (Iam == Cell.Color.White) color = "Black";
            else color = "White";
            figureCourses.AppendText(typeStatuatte + " " + color + ": " + previosCell + " -> " + newCell + "\n");
        }

        /// <summary>
        /// Валидация хода.
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        /// <returns>Возвращает true, когда ход возможен. В противном случае вернет false.</returns>
        private bool ValidationMove(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: ValidationMove();");
            try
            {
                if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Pawn)
                {
                    if (Cells[previosY, previosX].ChessmanColor == Cell.Color.White)
                    {
                        if (Cells[previosY, previosX].pawnFirstStep == Cell.PawnFirstStep.Yes &&
                            previosY - 2 == newY && previosX == newX &&
                            Cells[previosY - 1, previosX].ChessmanType == Cell.Chessman.Null &&
                            Cells[previosY - 2, previosX].ChessmanType == Cell.Chessman.Null)
                        {
                            VNP = 1;
                            enPassantX = newX;
                            enPassantYY = newY;
                            enPassantY = newY + 1;
                            ChekemateActive = true;
                            Cells[enPassantY, enPassantX].enPassant = Cell.EnPassant.Yes;
                            Cells[previosY, previosX].pawnFirstStep = Cell.PawnFirstStep.No;
                            return true;
                        }
                        else if ((previosX == newX && previosY - 1 == newY &&
                                  Cells[previosY - 1, previosX].ChessmanType == Cell.Chessman.Null) ||
                                 previosY - 1 == newY && ((previosX - 1 == newX || previosX + 1 == newX) &&
                                                          Cells[newY, newX].ChessmanColor == Enemy) ||
                                 newY == enPassantY && previosY != newY && newX == enPassantX &&
                                 Cells[enPassantY, enPassantX].ChessmanType == Cell.Chessman.Null && (!ChekemateActive))
                        {
                            Cells[newY, newY].pawnFirstStep = Cell.PawnFirstStep.No;
                            return true;
                        }
                    }
                    else
                    {
                        if (Cells[previosY, previosX].pawnFirstStep == Cell.PawnFirstStep.Yes &&
                            previosY + 2 == newY && previosX == newX &&
                            Cells[previosY + 1, previosX].ChessmanType == Cell.Chessman.Null &&
                            Cells[previosY + 2, previosX].ChessmanType == Cell.Chessman.Null)
                        {
                            VNP = 1;
                            enPassantX = newX;
                            enPassantYY = newY;
                            enPassantY = newY - 1;
                            ChekemateActive = false;
                            Cells[enPassantY, enPassantX].enPassant = Cell.EnPassant.Yes;
                            Cells[previosY, previosX].pawnFirstStep = Cell.PawnFirstStep.No;
                            return true;
                        }
                        else if ((previosX == newX && previosY + 1 == newY &&
                                  Cells[previosY + 1, previosX].ChessmanType == Cell.Chessman.Null) ||
                                 previosY + 1 == newY && ((previosX - 1 == newX || previosX + 1 == newX) &&
                                                          Cells[newY, newX].ChessmanColor == Enemy) ||
                                 newY == enPassantY && previosY != newY && newX == enPassantX &&
                                 Cells[enPassantY, enPassantX].ChessmanType == Cell.Chessman.Null && (ChekemateActive))
                        {
                            Cells[previosY, previosX].pawnFirstStep = Cell.PawnFirstStep.No;
                            return true;
                        }
                    }
                }

                if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Rook)
                {
                    if (previosX == newX)
                    {
                        directionY = (newY - previosY)/Math.Abs(newY - previosY);
                        int j = previosY + directionY;
                        while (j != newY && Cells[j, previosX].ChessmanType == Cell.Chessman.Null)
                        {
                            j += directionY;
                            emptyPath++;
                        }
                        if (emptyPath == (Math.Abs(newY - previosY)))
                        {
                            emptyPath = 1;
                            Cells[previosY, previosX].rookFirstStep = Cell.RookFirstStep.No;
                            return true;
                        }
                    }
                    if (previosY == newY)
                    {
                        directionX = (newX - previosX)/Math.Abs(newX - previosX);
                        int i = previosX + directionX;
                        while (i != newX && Cells[previosY, i].ChessmanType == Cell.Chessman.Null)
                        {
                            i += directionX;
                            emptyPath++;
                        }
                        if (emptyPath == (Math.Abs(newX - previosX)))
                        {
                            emptyPath = 1;
                            Cells[previosY, previosX].rookFirstStep = Cell.RookFirstStep.No;
                            return true;
                        }
                    }
                    emptyPath = 1;
                }

                if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Bishop)
                {
                    if (Math.Abs(previosY - newY) == Math.Abs(previosX - newX))
                    {
                        directionY = (newY - previosY)/Math.Abs(previosY - newY);
                        directionX = (newX - previosX)/Math.Abs(previosX - newX);
                        int i = previosY + directionY;
                        int j = previosX + directionX;
                        while ((i != newY) && (j != newX) && Cells[i, j].ChessmanType == Cell.Chessman.Null)
                        {
                            emptyPath++;
                            i += directionY;
                            j += directionX;
                        }
                        if (emptyPath == (Math.Abs(newX - previosX)))
                        {
                            emptyPath = 1;
                            return true;
                        }
                    }
                    emptyPath = 1;
                }
                if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Queen)
                {
                    if (newX == previosX || newY == previosY)
                    {
                        if (previosX == newX)
                        {
                            directionY = (newY - previosY)/Math.Abs(newY - previosY);
                            int j = previosY + directionY;
                            while (j != newY && Cells[j, previosX].ChessmanType == Cell.Chessman.Null)
                            {
                                j += directionY;
                                emptyPath++;
                            }
                            if (emptyPath == (Math.Abs(newY - previosY)))
                            {
                                emptyPath = 1;
                                return true;
                            }
                        }
                        if (previosY == newY)
                        {
                            directionX = (newX - previosX)/Math.Abs(newX - previosX);
                            int i = previosX + directionX;
                            while (i != newX && Cells[previosY, i].ChessmanType == Cell.Chessman.Null)
                            {
                                i += directionX;
                                emptyPath++;
                            }
                            if (emptyPath == (Math.Abs(newX - previosX)))
                            {
                                emptyPath = 1;
                                return true;
                            }
                        }
                        emptyPath = 1;
                    }
                    else
                    {
                        if (Math.Abs(previosY - newY) == Math.Abs(previosX - newX))
                        {
                            directionY = (newY - previosY)/Math.Abs(previosY - newY);
                            directionX = (newX - previosX)/Math.Abs(previosX - newX);
                            int i = previosY + directionY;
                            int j = previosX + directionX;
                            while ((i != newY) && (j != newX) && Cells[i, j].ChessmanType == Cell.Chessman.Null)
                            {
                                emptyPath++;
                                i += directionY;
                                j += directionX;
                            }
                            if (emptyPath == (Math.Abs(newX - previosX)))
                            {
                                emptyPath = 1;
                                return true;
                            }
                        }
                        emptyPath = 1;
                    }
                }
                if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Knigth)
                {
                    if ((previosY + 1 == newY && previosX + 2 == newX) || (previosY + 2 == newY && previosX + 1 == newX) ||
                        (previosY - 1 == newY && previosX + 2 == newX) || (previosY - 2 == newY && previosX + 1 == newX) ||
                        (previosY - 1 == newY && previosX - 2 == newX) || (previosY - 2 == newY && previosX - 1 == newX) ||
                        (previosY + 2 == newY && previosX - 1 == newX) || (previosY + 1 == newY && previosX - 2 == newX))
                        return true;
                }
                if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.King)
                {
                    int x;
                    if ((previosY == newY && Math.Abs(previosX - newX) == 1 ||
                         previosX == newX && Math.Abs(previosY - newY) == 1 ||
                         Math.Abs(previosY - newY) == 1 && Math.Abs(previosX - newX) == 1) &&
                        (Cells[newY, newX].ChessmanType == Cell.Chessman.Null && !CheckAttack(newX, newY) ||
                         Cells[newY, newX].ChessmanColor == Enemy) ||
                        (Cells[previosY, previosX].kingFirstStep == Cell.KingFirstStep.Yes &&
                         Math.Abs(newX - previosX) == 2 && previosY == newY && !CheckAttack(previosX, previosY, out x) &&
                         ((Cells[previosY, previosX + 1].ChessmanType == Cell.Chessman.Null &&
                           !CheckAttack(previosX + 1, newY) &&
                           Cells[previosY, previosX + 2].ChessmanType == Cell.Chessman.Null &&
                           !CheckAttack(previosX + 2, newY) &&
                           Cells[previosY, previosX + 3].rookFirstStep == Cell.RookFirstStep.Yes) ||
                          (Cells[previosY, previosX - 1].ChessmanType == Cell.Chessman.Null &&
                           !CheckAttack(previosX - 1, newY) &&
                           Cells[previosY, previosX - 2].ChessmanType == Cell.Chessman.Null &&
                           !CheckAttack(previosX - 2, newY) &&
                           Cells[previosY, previosX - 3].ChessmanType == Cell.Chessman.Null &&
                           Cells[previosY, previosX - 4].rookFirstStep == Cell.RookFirstStep.Yes))))
                    {
                        Cells[previosY, previosX].kingFirstStep = Cell.KingFirstStep.No;
                        return true;
                    }

                    /*if (Cells[previosY, previosX].kingFirstStep == Cell.KingFirstStep.Yes && (Cells[previosY, previosX + 3].rookFirstStep == Cell.RookFirstStep.Yes &&
                    previosX + 2 == newX && Cells[previosY, previosX + 1].ChessmanType == Cell.Chessman.Null && Cells[previosY, previosX + 2].ChessmanType == Cell.Chessman.Null
                    || Cells[previosY, previosX - 4].rookFirstStep == Cell.RookFirstStep.Yes &&
                    previosX - 2 == newX && Cells[previosY, previosX - 1].ChessmanType == Cell.Chessman.Null && Cells[previosY, previosX - 2].ChessmanType == Cell.Chessman.Null 
                    && Cells[previosY, previosX - 3].ChessmanType == Cell.Chessman.Null) 
                    && previosY == newY)  
                    return true;
                if ((previosY == newY && Math.Abs(previosX - newX) == 1) ||
                    (previosX == newX && Math.Abs(previosY - newY) == 1) ||
                    (Math.Abs(previosY - newY) == 1 && Math.Abs(previosX - newX) == 1))
                    return true;*/
                }
                //Log.Info("End: ValidationMove();");
            }
            catch (Exception ex)
            {
                //Log.Info("ValidationMove(); ", ex);
            }
            if (!inCheckmate)
                forValidation = false;
            return false;
        }

        /// <summary>
        /// Рокировка
        /// </summary>
        /// <param name="previosX"></param>
        /// <param name="previosY"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        private void CastlinMovment(int previosX, int previosY, int newX, int newY)
        {
            if (previosX + 2 == newX)
            {
                Cells[newY, previosX + 2].BackgroundImage = Cells[previosY, previosX].BackgroundImage;
                Cells[newY, previosX + 2].ChessmanType = Cells[previosY, previosX].ChessmanType;
                Cells[newY, previosX + 2].ChessmanColor = Cells[previosY, previosX].ChessmanColor;
                Cells[previosY, previosX].BackgroundImage = null;
                Cells[previosY, previosX].ChessmanType = Cell.Chessman.Null;
                Cells[previosY, previosX].ChessmanColor = Cell.Color.Null;

                Cells[newY, previosX + 1].BackgroundImage = Cells[previosY, previosX + 3].BackgroundImage;
                Cells[newY, previosX + 1].ChessmanType = Cells[previosY, previosX + 3].ChessmanType;
                Cells[newY, previosX + 1].ChessmanColor = Cells[previosY, previosX + 3].ChessmanColor;
                Cells[previosY, previosX + 3].BackgroundImage = null;
                Cells[previosY, previosX + 3].ChessmanType = Cell.Chessman.Null;
                Cells[previosY, previosX + 3].ChessmanColor = Cell.Color.Null;
            }

            if (previosX - 2 == newX)
            {
                Cells[newY, previosX - 2].BackgroundImage = Cells[previosY, previosX].BackgroundImage;
                Cells[newY, previosX - 2].ChessmanType = Cells[previosY, previosX].ChessmanType;
                Cells[newY, previosX - 2].ChessmanColor = Cells[previosY, previosX].ChessmanColor;
                Cells[previosY, previosX].BackgroundImage = null;
                Cells[previosY, previosX].ChessmanType = Cell.Chessman.Null;
                Cells[previosY, previosX].ChessmanColor = Cell.Color.Null;

                Cells[newY, previosX - 1].BackgroundImage = Cells[previosY, previosX - 4].BackgroundImage;
                Cells[newY, previosX - 1].ChessmanType = Cells[previosY, previosX - 4].ChessmanType;
                Cells[newY, previosX - 1].ChessmanColor = Cells[previosY, previosX - 4].ChessmanColor;
                Cells[previosY, previosX - 4].BackgroundImage = null;
                Cells[previosY, previosX - 4].ChessmanType = Cell.Chessman.Null;
                Cells[previosY, previosX - 4].ChessmanColor = Cell.Color.Null;
            }
            SwapColor();
        }

        /// <summary>
        /// Атака aka. "Взятие на проходе"
        /// </summary>
        /// <param name="previosX"></param>
        /// <param name="previosY"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        private void MovementEnPassant(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: MovementEnPassant");
            try
            {
                bool IsShah = false;
                int kingX, kingY, quantityKiller;
                if (Iam == Cell.Color.White)
                {
                    kingX = WKingPosX;
                    kingY = WKingPosY;
                }
                else
                {
                    kingX = BKingPosX;
                    kingY = BKingPosY;
                }
                Cell.Chessman tempChessman = Cells[enPassantYY, enPassantX].ChessmanType;
                Cell.Color tempColor = Cells[enPassantYY, enPassantX].ChessmanColor;


                Cells[newY, newX].ChessmanType = Cells[previosY, previosX].ChessmanType;
                Cells[newY, newX].ChessmanColor = Cells[previosY, previosX].ChessmanColor;

                Cells[enPassantYY, enPassantX].ChessmanType = Cell.Chessman.Null;
                Cells[enPassantYY, enPassantX].ChessmanColor = Cell.Color.Null;

                Cells[previosY, previosX].ChessmanType = Cell.Chessman.Null;
                Cells[previosY, previosX].ChessmanColor = Cell.Color.Null;

                IsShah = CheckAttack(kingX, kingY, out quantityKiller);

                if (!IsShah)
                {
                    Cells[newY, newX].BackgroundImage = Cells[previosY, previosX].BackgroundImage;

                    Cells[enPassantYY, enPassantX].BackgroundImage = null;

                    Cells[enPassantY, enPassantX].enPassant = Cell.EnPassant.No;
                    Cells[previosY, previosX].BackgroundImage = null;

                    SwapColor();
                    enPassantYY = -1;
                    enPassantX = -1;
                    enPassantY = -1;
                }
                else
                {
                    Cells[enPassantYY, enPassantX].ChessmanType = tempChessman;
                    Cells[enPassantYY, enPassantX].ChessmanColor = tempColor;

                    Cells[previosY, previosX].ChessmanType = Cells[newY, newX].ChessmanType;
                    Cells[previosY, previosX].ChessmanColor = Cells[newY, newX].ChessmanColor;

                    Cells[newY, newX].ChessmanType = Cell.Chessman.Null;
                    Cells[newY, newX].ChessmanColor = Cell.Color.Null;
                }
                //Log.Info("End: MovementEnPassant();");
            }
            catch (Exception ex)
            {
                //Log.Info("MovementEnPassant(); ", ex);
            }
        }

        /// <summary>
        /// Метод движения для Коня
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        private void MoveKnigth(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: MoveKnigth();");
            try
            {
                if (ValidationMove(previosX, previosY, newX, newY))
                {
                    Movement(previosX, previosY, newX, newY);
                    forValidation = true;
                    FigureCourses("Knigth", previosX, previosY, newX, newY);
                }
                //Log.Info("End: MoveKnigth();");
            }
            catch (Exception ex)
            {
                //Log.Info("MoveKnigth(); ", ex);
            }
        }


        /// <summary>
        /// Метод движения для Пешки, реализовано взятие на проходе
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        private void MovePawn(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: MovePawn();");
            try
            {
                if (ValidationMove(previosX, previosY, newX, newY))
                {
                    if ((enPassantY > 0 && enPassantX > 0) &&
                        (Cells[enPassantY, enPassantX].enPassant == Cell.EnPassant.Yes) && newX == enPassantX &&
                        newY == enPassantY)
                    {
                        MovementEnPassant(previosX, previosY, newX, newY);
                        forValidation = true;
                    }
                    else
                    {
                        Movement(previosX, previosY, newX, newY);
                        forValidation = true;
                        if (newY == 0 && Cells[newY, newX].ChessmanColor == Cell.Color.White)
                            ChoiceChessman(newX, newY, Cells[newY, newX].ChessmanColor);
                        if (newY == 7 && Cells[newY, newX].ChessmanColor == Cell.Color.Black)
                            ChoiceChessman(newX, newY, Cells[newY, newX].ChessmanColor);
                    }
                    FigureCourses("Pawn", previosX, previosY, newX, newY);
                }
                //Log.Info("End: MovePawn();");
            }
            catch (Exception ex)
            {
                //Log.Info("MovePawn(); ", ex);
            }
        }

        /// <summary>
        /// Метод движения для Ладьи
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        private void MoveRook(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: MoveRook();");
            try
            {
                if (ValidationMove(previosX, previosY, newX, newY))
                {
                    Movement(previosX, previosY, newX, newY);
                    forValidation = true;
                    FigureCourses("Rook", previosX, previosY, newX, newY);
                }
                //Log.Info("End: MoveRook();");
            }
            catch (Exception ex)
            {
                //Log.Info("MoveRook(); ", ex);
            }
        }

        /// <summary>
        /// Метод движения для Слона
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        private void MoveBishop(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: MoveBishop();");
            try
            {
                if (ValidationMove(previosX, previosY, newX, newY))
                {
                    Movement(previosX, previosY, newX, newY);
                    forValidation = true;
                    FigureCourses("Bishop", previosX, previosY, newX, newY);
                }

                //Log.Info("End: MoveBishop();");
            }
            catch (Exception ex)
            {
                //Log.Info("MoveBishop(); ", ex);
            }
        }


        /// <summary>
        /// Метод движения для Короля
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        private void MoveKing(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: MoveKing();");
            try
            {
                if (ValidationMove(previosX, previosY, newX, newY))
                {
                    /*if ((Cells[newY, newX].ChessmanType == Cell.Chessman.Null && !CheckAttack(newX, newY) ||
                         Cells[newY, newX].ChessmanColor == Enemy))*/
                    {
                        if (previosX + 2 == newX || previosX - 2 == newX)
                        {
                            forValidation = true;
                            CastlinMovment(previosX, previosY, newX, newY);
                        }
                        else
                        {
                            forValidation = true;
                            Movement(previosX, previosY, newX, newY);
                        }
                        if (Cells[newY, newX].ChessmanColor == Cell.Color.White)
                        {
                            WKingPosX = newX;
                            WKingPosY = newY;
                        }
                        else
                        {
                            BKingPosX = newX;
                            BKingPosY = newY;
                        }
                        FigureCourses("King", previosX, previosY, newX, newY);
                    }
                }
                //Log.Info("End: MoveKing();");
            }
            catch (Exception ex)
            {
                //Log.Info("MoveKing(); ", ex);
            }
        }

        /// <summary>
        /// Метод движения для Королевы
        /// </summary>
        /// <param name="previosX"></param>
        /// <param name="previosY"></param>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        private void MoveQueen(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: MoveQueen();");
            try
            {
                if (ValidationMove(previosX, previosY, newX, newY))
                {
                    Movement(previosX, previosY, newX, newY);
                    forValidation = true;
                    FigureCourses("Queen", previosX, previosY, newX, newY);
                }
                //Log.Info("End: MoveQueen();");
            }
            catch (Exception ex)
            {
                //Log.Info("MoveQueen(); ", ex);
            }
        }


        /// <summary>
        /// Метод, описывающий ходы и атаки фигур
        /// </summary>
        /// <param name="previosX">Х-координата клетки, с которой взяли фигуру</param>
        /// <param name="previosY">Y-координата клетки, с которой взяли фигуру</param>
        /// <param name="newX">Х-координата клетки, на которую хотим поставить фигуру</param>
        /// <param name="newY">Y-координата клетки, на которую хотим поставить фигуру</param>
        private void Move(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: Move();");
            try
            {
                if (Iam == Cells[newY, newX].ChessmanColor)
                {
                    ;
                }
                else if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Knigth) //Конь
                {
                    MoveKnigth(previosX, previosY, newX, newY);
                }
                else if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Pawn) //Пешка
                {
                    MovePawn(previosX, previosY, newX, newY);
                }
                else if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Rook)
                {
                    MoveRook(previosX, previosY, newX, newY);
                }
                else if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Bishop)
                {
                    MoveBishop(previosX, previosY, newX, newY);
                }
                else if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.Queen)
                {
                    MoveQueen(previosX, previosY, newX, newY);
                }
                else if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.King)
                {
                    MoveKing(previosX, previosY, newX, newY);
                }
                else if (Cells[previosY, previosX].ChessmanType != Cell.Chessman.Null)
                {
                    Movement(previosX, previosY, newX, newY);
                }
                if (VNP != 1 && forValidation)
                {
                    if (enPassantYY > 0)
                        Cells[enPassantY, enPassantX].enPassant = Cell.EnPassant.No;
                    enPassantYY = -1;
                    enPassantX = -1;
                    enPassantY = -1;
                    forValidation = false;
                }
                if (forValidation)
                    VNP = 0;
                //Log.Info("End: Move();");
            }
            catch (Exception ex)
            {
                //Log.Info("Move(); ", ex);
            }
        }

        /// <summary>
        /// Праверяте на шах мат после своего хода
        /// </summary>
        private bool TraitorChackmate(int previosX, int previosY, int newX, int newY)
        {
            //Log.Info("Start: TraitorChackmate();");
            try
            {
                bool result = false;
                int quantityKiller, kingX, kingY;

                if (Iam == Cell.Color.White)
                {
                    kingX = WKingPosX;
                    kingY = WKingPosY;
                }
                else
                {
                    kingX = BKingPosX;
                    kingY = BKingPosY;
                }

                Cell.Color tempColor = Iam;
                Cell.Chessman tempChessman = Cells[previosY, previosX].ChessmanType;
                Cell.Color tempColorEnemy = Cells[newY, newX].ChessmanColor;
                Cell.Chessman tempChessmanEnemy = Cells[newY, newX].ChessmanType;

                Cells[newY, newX].ChessmanType = Cells[previosY, previosX].ChessmanType;
                Cells[newY, newX].ChessmanColor = Cells[previosY, previosX].ChessmanColor;
                Cells[previosY, previosX].ChessmanType = Cell.Chessman.Null;
                Cells[previosY, previosX].ChessmanColor = Cell.Color.Null;

                IsShah = CheckAttack(kingX, kingY, out quantityKiller);
                if (IsShah)
                {
                    result = true;
                }

                Cells[previosY, previosX].ChessmanType = tempChessman;
                Cells[previosY, previosX].ChessmanColor = tempColor;
                Cells[newY, newX].ChessmanType = tempChessmanEnemy;
                Cells[newY, newX].ChessmanColor = tempColorEnemy;

                if (Cells[previosY, previosX].ChessmanType == Cell.Chessman.King)
                {
                    result = false;
                }

                //Log.Info("End: TraitorChackmate();");
                return result;
            }
            catch (Exception ex)
            {
                //Log.Info("TraitorChackmate(); ", ex);
                return false;
            }
        }

        /// <summary>
        /// Метод определения шаха и мата
        /// </summary>
        private void Checkmate()
        {
            inCheckmate = true;
            //Log.Info("Start: Checkmate();");
            try
            {
                IsShah = false;
                int kingX, kingY, quantityKiller = 0, quantityEmpty = 0;

                if (Iam == Cell.Color.White)
                {
                    kingX = WKingPosX;
                    kingY = WKingPosY;
                }
                else
                {
                    kingX = BKingPosX;
                    kingY = BKingPosY;
                }

                for (int i = -1; i <= 1; i += 2)
                    for (int j = -1; j <= 1; j++)
                    {
                        if (kingX + j > 0 && kingX + j < 8 && kingY + i > 0 && kingY + i < 8)
                        {
                            if (Cells[kingY + i, kingX + j].ChessmanType == Cell.Chessman.Null)
                            {
                                if (!CheckAttack(kingX + j, kingY + i))
                                    quantityEmpty++;
                            }
                        }
                    }

                IsShah = CheckAttack(kingX, kingY, out quantityKiller);

                if (IsShah)
                {
                    MessageBox.Show("\tШAX");
                    bool DefenderExisist = ExistKingDefender(kingX, kingY);

                    if (quantityKiller == 1 && quantityEmpty == 0 &&
                        !ValidationMove(kingX, kingY, fireArea[0], fireArea[1]) &&
                        !DefenderExisist)
                        EndGame();
                }


                if (quantityKiller >= 2)
                    EndGame();
                //Log.Info("End: Checkmate();");
            }
            catch (Exception ex)
            {
                //Log.Info("Checkmate();", ex);
            }
            inCheckmate = false;
        }

        /// <summary>
        /// Определяет можно ли атаковать указаную клетку
        /// </summary>
        /// <param name="newX"></param>
        /// <param name="newY"></param>
        /// <param name="quantityKiller"></param>
        /// <returns></returns>
        private bool CheckAttack(int newX, int newY, out int quantityKiller)
        {
            //Log.Info("Start: CheckAttack();");
            quantityKiller = 0;
            try
            {
                int KillerPosX = 0, KillerPosY = 0;
                bool result = false;
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (Cells[i, j].ChessmanColor == Enemy)
                        {
                            SwapColor();
                            if (ValidationMove(j, i, newX, newY) == true)
                            {
                                quantityKiller++;
                                KillerPosX = j;
                                KillerPosY = i;
                            }
                            SwapColor();
                        }
                if (quantityKiller != 0)
                    result = true;
                if (quantityKiller == 1)
                    СreateTrajectory(KillerPosX, KillerPosY, newX, newY);
                //Log.Info("End: CheckAttack();");
                return result;
            }
            catch (Exception ex)
            {
                //Log.Info("При ошибке вернется false!\n");
                //Log.Info("CheckAttack(); ", ex);
                return false;
            }
        }

        /// <summary>
        /// Создание траектории убийцы короля
        /// </summary>
        /// <param name="KillerX">Координата убийцы х</param>
        /// <param name="KillerY">Координата убийцы у</param>
        /// <param name="KingX">Координата короля х</param>
        /// <param name="KingY">Координата короля у</param>
        private void СreateTrajectory(int KillerX, int KillerY, int KingX, int KingY)
        {
            //Log.Info("Start: СreateTrajectory();");
            try
            {
                fireArea.Clear();

                fireArea.Add(KillerX);
                fireArea.Add(KillerY);

                switch (Cells[KillerY, KillerX].ChessmanType)
                {
                    case Cell.Chessman.Rook:
                    case Cell.Chessman.Bishop:
                    case Cell.Chessman.Queen:
                        int tY = 0, tX = 0, i = 2;
                        if (KingY - KillerY != 0)
                            tY = (KingY - KillerY)/Math.Abs(KingY - KillerY);

                        if (KingX - KillerX != 0)
                            tX = (KingX - KillerX)/Math.Abs(KingX - KillerX);

                        while (fireArea[i - 2] + tX != KingX || fireArea[i - 1] + tY != KingY)
                        {
                            fireArea.Add(fireArea[i - 2] + tX);
                            fireArea.Add(fireArea[i - 1] + tY);
                            i += 2;
                        }

                        break;
                }
                //Log.Info("End: СreateTrajectory();");
            }
            catch (Exception ex)
            {
                //Log.Info("СreateTrajectory(); ", ex);
            }
        }

        /// <summary>
        /// Определяет можно ли атаковать указаную клетку
        /// </summary>
        /// <param name="newY">Координата Y точки проверки</param>
        /// <param name="newX">Координата X точки проверки</param>
        /// <returns>Возвращает true если в клетку может сходить враг</returns>
        private bool CheckAttack(int newX, int newY)
        {
            //Log.Info("Start: CheckAttack();");
            try
            {
                int quantityKiller = 0;
                bool result = false;
                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (Cells[j, i].ChessmanColor == Enemy)
                        {
                            if (ValidationMove(i, j, newX, newY) == true &&
                                Cells[j, i].ChessmanType != Cell.Chessman.Pawn)
                            {
                                quantityKiller++;
                            }
                            else if (Cells[j, i].ChessmanType == Cell.Chessman.Pawn)
                            {
                                Cells[newY, newX].ChessmanType = Cell.Chessman.King;
                                Cells[newY, newX].ChessmanColor = Iam;
                                SwapColor();
                                if (ValidationMove(i, j, newX, newY) == true
                                    /*&& Cells[j, i].Chessman != Cell.Chessman.Pawn*/)
                                {
                                    quantityKiller++;
                                }
                                SwapColor();
                                Cells[newY, newX].ChessmanType = Cell.Chessman.Null;
                                Cells[newY, newX].ChessmanColor = Cell.Color.Null;
                            }
                        }
                if (quantityKiller != 0)
                    result = true;
                //Log.Info("End: CheckAttack();");
                return result;
            }
            catch (Exception ex)
            {
                //Log.Info("При ошибке вернется false!\n");
                //Log.Info("CheckAttack(); ", ex);
                return false;
            }
        }

        /// <summary>
        /// Метод конца игры
        /// </summary>
        private void EndGame()
        {
            globalReader.Dispose();
            //Log.Info("Start: EndGame();");
            EndGame End = new EndGame();
            End.ShowDialog();
            Drawing();
            //Log.Info("End: EndGame();");
        }

        /// <summary>
        /// Проверяет защищает ли фигура короля
        /// </summary>
        /// <param name="defenderWayX">Координат куда ходит фигура х</param>
        /// <param name="defenderWayY">Координат куда ходит фигура у</param>
        /// <returns></returns>
        private bool OnEmbrasure(int defenderWayX, int defenderWayY)
        {
            //Log.Info("Start: OnEmbrasure();");
            try
            {
                int i = 0;
                bool flag = true, result = false;
                while (i < fireArea.Count && flag)
                {
                    if (defenderWayX == fireArea[i] && defenderWayY == fireArea[i + 1])
                    {
                        flag = false;
                        result = true;
                    }
                    i += 2;
                }
                //Log.Info("End: OnEmbrasure();");
                return result;
            }
            catch (Exception ex)
            {
                //Log.Info("При ошибке вернется false!\n");
                //Log.Info("OnEmbrasure(); ", ex);
                return false;
            }
        }

        /// <summary>
        /// Проверяет, может ли хоть какая-нибудь фигура защитить короля
        /// </summary>
        /// <param name="kingX"></param>
        /// <param name="kingY"></param>
        private bool ExistKingDefender(int kingX, int kingY)
        {
            //Log.Info("Start: ExistKingDefender();");
            try
            {
                bool result = false;

                for (int i = 0; i < 8; i++)
                    for (int j = 0; j < 8; j++)
                        if (Cells[i, j].ChessmanColor == Iam && Cells[i, j].ChessmanType != Cell.Chessman.King)
                            for (int k = 0; k < fireArea.Count; k += 2)
                                if (ValidationMove(j, i, fireArea[k], fireArea[k + 1]))
                                    result = true;

                if (ValidationMove(kingX, kingY, fireArea[0], fireArea[1]))
                    result = true;

                /*if (!result)
                {
                    EndGame();
                }*/
                //Log.Info("End: ExistKingDefender();");
                return result;
            }
            catch (Exception ex)
            {
                //Log.Info("При ошибке вернется false!\n");
                //Log.Info("ExistKingDefender();", ex);
                return false;
            }
        }

        /// <summary>
        /// Метод обработки клика по фигуре
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cell_Click(object sender, EventArgs e)
        {
            //Log.Info("Start: Cell_Click();");
            try
            {
                Cell cell = sender as Cell;
                if (firstClick && Iam == cell.ChessmanColor)
                {
                    currentX = cell.ColumnNumber;
                    currentY = cell.LineNumber;
                    firstClick = false;
                    color = Cells[currentY, currentX].BackColor;
                    Cells[currentY, currentX].BackColor = Color.LightSkyBlue;
                }
                else if (!firstClick)
                {
                    firstClick = true;
                    Move(currentX, currentY, cell.ColumnNumber, cell.LineNumber);
                    Checkmate();
                    Cells[currentY, currentX].BackColor = color;
                }
                //Log.Info("End: Cell_Click();");
            }
            catch (Exception ex)
            {
                //Log.Info("Cell_Click(); ", ex);
            }
        }

        /// <summary>
        /// Начинает новую игру по клику на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void новаяИграToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
            Drawing();

            globalReader = new ChessmanReader();
            globalReader.Open("globalReader.txt", "sw");
        }

        /// <summary>
        /// Сохраняет игру по клику на кнопку
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void сохранитьИгруToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists("SaveGame"))
                Directory.CreateDirectory("SaveGame");

            if (File.Exists("SaveGame/Save1.txt"))
                File.Delete("SaveGame/Save1.txt");

            File.Copy("globalReader.txt", "SaveGame/Save1.txt");
            продолжитьToolStripMenuItem.Enabled = true;
        }

        /// <summary>
        /// Загружает сохраненую игру
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void продолжитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Dispose();
            Drawing();
            int chessmanX, chessmanY, goToX, goToY;
            ChessmanReader chReader = new ChessmanReader();
            chReader.Open(@"SaveGame/Save1.txt", "sr");
            while (chReader.ReadStep(out chessmanX, out chessmanY, out goToX, out goToY))
            {
                Move(chessmanX, chessmanY, goToX, goToY);
            }
            chReader.Dispose();
            File.Delete("globalReader.txt");
            File.Copy("SaveGame/Save1.txt", "globalReader.txt");
            globalReader = new ChessmanReader();
            globalReader.Open("globalReader.txt", "sw");
        }

        /// <summary>
        /// Выход из игры
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            globalReader.Dispose();
            Application.Exit();
        }

        /// <summary>
        /// Вычищает массив Cells и MarkerA и приводит гллобальные переменые к стандартным значениям
        /// </summary>
        private void Dispose()
        {
            for (var i = 0; i < 8; i++)
            {
                for (var j = 0; j < 8; j++)
                {
                    Cells[i, j].Dispose();
                    Cells[i, j] = null;
                }
            }

            for (int j = 0; j < 2; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    MarkerA[i + 8*j].Dispose();
                    MarkerN[i + 8*j].Dispose();
                    MarkerA[i + 8*j] = null;
                    MarkerN[i + 8*j] = null;
                }
            }
            globalReader.Dispose();
            ChekemateActive = false;
            CastlingEnebleforBlack = true;
            CastlingEnebleforWhite = true;
            VNP = 0;
            forValidation = false;
            inCheckmate = false;
            firstClick = true;
            emptyPath = 1;
            BKingPosX = 4;
            BKingPosY = 0;
            WKingPosX = 4;
            WKingPosY = 7;
            IsShah = false;
            Iam = Cell.Color.White;
            Enemy = Cell.Color.Black;
            figureCourses.Clear();
        }
    }
}