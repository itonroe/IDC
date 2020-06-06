using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace B20_EX02
{
    public class Board
    {
        private Cell[,] m_Board;
        private int m_BoardWidth;
        private int m_BoardHeight;

        public int Width
        {
            get
            {
                return m_BoardWidth;
            }

            set
            {
                m_BoardWidth = value;
            }  
        }

        public int Height
        {
            get
            {
                return m_BoardHeight;
            }

            set
            {
                m_BoardHeight = value;
            }
        }

        public void SetSize()
        {
            m_Board = new Cell[m_BoardHeight, m_BoardWidth];
        }

        public void Build()
        {
            SetSize();

            int countLetters = NumOfCards();

            for (int i = 0; i < countLetters; i++)
            {
                char currentLetter = (char)('A' + i);

                AddCell(currentLetter);
                AddCell(currentLetter);
            }
        }

        private void AddCell(char i_Letter)
        {
            Point point = RandCellPoint();

            while (m_Board[point.X, point.Y] != null)
            {
                point = RandCellPoint();
            }

            int x = point.X;
            int y = point.Y;

            m_Board[x, y] = new Cell(new Point(x, y), i_Letter);
        }

        private Point RandCellPoint()
        {
            Random random = new Random();

            return new Point(random.Next(0, m_Board.GetLength(0)), random.Next(0, m_Board.GetLength(1)));
        }

        public Cell GetRandomCell()
        {
            Point point = RandCellPoint();
            Cell cell = GetCell(point);

            while (cell.Visible)
            {
                point = RandCellPoint();
                cell = GetCell(point);
            }

            return GetCell(point);
        }

        public bool CellIsVisible(Cell i_Cell)
        {
            return i_Cell.Visible;
        }

        public void ShowCell(string i_Cell)
        {
            ShowCell(GetCell(i_Cell));
        }

        public void ShowCell(Cell i_Cell)
        {
            i_Cell.Visible = true;
        }

        public void HideCell(string i_Cell)
        {
            HideCell(GetCell(i_Cell));
        }

        public void HideCell(Cell i_Cell)
        {
            i_Cell.Visible = false;
        }

        public Cell GetCell(string i_Cell)
        {
            int y = i_Cell[0] - 'A';
            int x = i_Cell[1] - '1';

            return GetCell(new Point(x, y));
        }

        public Cell GetCell(Point i_Point)
        {
            return m_Board[i_Point.X, i_Point.Y];
        }

        public bool ValidCell(string i_Cell)
        {
            int y = i_Cell[0] - 'A';
            int x = i_Cell[1] - '1';

            bool valid = true;

            if (x < 0 || x >= m_Board.GetLength(0) || y >= m_Board.GetLength(1) || y < 0)
            {
                valid = false;
            }
            else
            {
                if (CellIsVisible(GetCell(i_Cell)))
                {
                    valid = false;
                }
            }

            return valid;
        }

        public int NumOfCards()
        {
            return (m_Board.GetLength(0) * m_Board.GetLength(1)) / 2;
        }

        public bool BouardIsFull()
        {
            bool isFull = true;

            for (int i = 0; i < m_Board.GetLength(0); i++)
            {
                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    if (!CellIsVisible(GetCell(new Point(i, j))))
                    {
                        isFull = false;
                        break;
                    }
                }
            }

            return isFull;
        }

        public override string ToString()
        {
            StringBuilder boardDisplay = new StringBuilder();

            string[] lines = new string[(m_Board.GetLength(0) + 1) * 2];

            // Offset
            lines[0] = "   ";
            lines[1] = "  =";

            // First and Seconds Lines           
            for (int j = 0; j < m_Board.GetLength(1); j++)
            {
                lines[0] += string.Format(" {0}  ", (char)('A' + j));
                lines[1] += "====";
            }

            for (int i = 2; i < lines.Length; i += 2)
            {
                lines[i] = (i / 2) + " ";
                lines[i + 1] = "  ";

                for (int j = 0; j < m_Board.GetLength(1); j++)
                {
                    if (j == 0)
                    {
                        lines[i] += "|";
                        lines[i + 1] += "=";
                    }

                    lines[i] += string.Format(" {0} |", m_Board[(i / 2) - 1, j].Visible ? m_Board[(i / 2) - 1, j].Letter : ' ');
                    lines[i + 1] += "====";
                }
            }

            for (int i = 0; i < lines.GetLength(0); i++)
            {
                boardDisplay.AppendLine(lines[i]);
            }

            return boardDisplay.ToString();
        }
    }
}
