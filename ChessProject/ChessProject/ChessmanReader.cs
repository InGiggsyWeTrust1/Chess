﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace ChessProject
{
    class ChessmanReader : IDisposable
    {
        private FileStream fileStream;
        private StreamReader streamReader;
        private StreamWriter streamWriter;
        private bool isOpen = false;
        private bool isDispose = false;

        /// <summary>
        /// Открывает файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="config">sr - чтение, sw - Запись, wr - Чтение и запись</param>
        public void Open(string fileName, string config)
        {
            if (isDispose)
            {
                throw new ObjectDisposedException("chRaeder");
            }
            if (isOpen)
            {
                throw new Exception("Файл уже открыт");
            }
            switch (config)
            {
                case "sr":
                    fileStream = new FileStream(fileName, FileMode.Open);
                    streamReader = new StreamReader(fileStream);
                    break;
                case "sw":
                    fileStream = new FileStream(fileName, FileMode.Create);
                    streamWriter = new StreamWriter(fileStream);
                    break;
                case "wr":
                    fileStream = new FileStream(fileName, FileMode.Open);
                    streamReader = new StreamReader(fileStream);
                    streamWriter = new StreamWriter(fileStream);
                    break;
            }
            isOpen = true;
        }

        /// <summary>
        /// Считывает строку из файла преобразует в координаты
        /// </summary>
        /// <param name="chessmanX">Взвращает координату откуда надо сходить X</param>
        /// <param name="chessmanY">Взвращает координату откуда надо сходить Y</param>
        /// <param name="goToX">Взвращает координату куда надо сходить X</param>
        /// <param name="goToY">Взвращает координату куда надо сходить Y</param>
        /// <returns>Ture - если смог прочитать шаг, иначе - False</returns>
        public bool ReadStep(out int chessmanX, out int chessmanY, out int goToX, out int goToY)
        {
            if (isDispose)
            {
                throw new ObjectDisposedException("chRaeder");
            }
            if (!isOpen)
            {
                throw new Exception("Попытка обратитьс к файлу который еще не открыт");
            }
            if (streamReader == null)
            {
                throw new ArgumentNullException("config");
            }

            string step = streamReader.ReadLine();
            bool result = true;
            if (step != null && CheckOnCorectness(step))
            {
                int i = 0;
                chessmanX = (int) step[i] - 65;
                chessmanY = (int) step[i + 1] - 49;
                i += 2;
                while (i < step.Length && !Char.IsLetterOrDigit(step[i])) i++;
                goToX = (int) step[i] - 65;
                goToY = (int) step[i + 1] - 49;
            }
            else
            {
                chessmanX = 0;
                chessmanY = 0;
                goToX = 0;
                goToY = 0;
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Проверяет является выражение допустимым для обработки и совершения кода
        /// </summary>
        /// <param name="line">Обрабатываема строка</param>
        /// <returns>True - если шаг корректный, иначе - false</returns>
        private bool CheckOnCorectness(string line)
        {
            bool result = false;
            int checker = 0, i = 0;
            for (int j = 0; j < 2 && line.Length - 1 != i; j++)
            {
                while (i < line.Length && !Char.IsLetterOrDigit(line[i])) i++;
                if (Char.IsLetter(line[i]) && Char.IsDigit(line[i + 1]))
                    if ((int) line[i] >= 65 && (int) line[i] < 73)
                        if ((int) line[i + 1] >= 48 && (int) line[i + 1] < 57)
                            checker++;
                i += 2;
            }

            if (checker == 2)
            {
                result = true;
            }

            return result;
        }

        /// <summary>
        /// Запись шагов в файл
        /// </summary>
        /// <param name="chessmanX">Координата фигуры по Х</param>
        /// <param name="chessmanY">Координата фигуры по Y</param>
        /// <param name="goToX">Координата новой клетки по Х</param>
        /// <param name="goToY">Координата новой клетки по Y</param>
        public void WriteStep(int chessmanX, int chessmanY, int goToX, int goToY)
        {
            if (streamWriter == null)
            {
                throw new ArgumentNullException("config");
            }
            char[] step = new char[5];
            step[0] = Convert.ToChar(chessmanX + 65);
            step[1] = Convert.ToChar(chessmanY + 49);
            step[2] = ' ';
            step[3] = Convert.ToChar(goToX + 65);
            step[4] = Convert.ToChar(goToY + 49);
            if (CheckOnCorectness(new string(step)))
            {
                streamWriter.WriteLine(step);
                streamWriter.Flush();
            }
            else
                throw new ArgumentException("не правильные координаты хода");
        }

        /// <summary>
        /// Закрытие файла
        /// </summary>
        public void Dispose()
        {
            if (isDispose)
            {
                return;
            }
            isOpen = false;
            isDispose = true;
            if (fileStream != null)
            {
                fileStream.Close();
                fileStream = null;
            }
        }
    }
}
