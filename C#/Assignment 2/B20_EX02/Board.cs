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

        public Board()
        {}


        public void SetSize()
        {
            m_Board = new Cell[m_BoardHeight, m_BoardWidth];
        }

        /*public void SetSize(int i_Width, int i_Height) 
        {
            Width = i_Width;
            Height = i_Height;

            m_Board = new Cell[i_Height, i_Width];
        }*/

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
            int[] point = RandCellPoint();

            while (m_Board[point[0], point[1]] != null)
            {
                point = RandCellPoint();
            }

            int x = point[0];
            int y = point[1];

            m_Board[x, y] = new Cell(x, y);
            m_Board[x, y].Letter = i_Letter;
        }

        private int[] RandCellPoint()
        {
            Random random = new Random();

            int[] point = new int[2];

            point[0] = random.Next(0, m_Board.GetLength(0));
            point[1] = random.Next(0, m_Board.GetLength(1));

            return point;
        }

        public Point ShowRandomCell()
        {
            int[] point = RandCellPoint();
            Cell cell = GetCellByPoint(point[0], point[1]);

            while (cell.Visible)
            {
                point = RandCellPoint();
                cell = GetCellByPoint(point[0], point[1]);
            }

            cell.Visible = true;

            return new Point(point[0], point[1]);
        }

        public void ShowCell(string i_Cell)
        {
            GetCell(i_Cell).Visible = true;
        }

        public void HideCell(string i_Cell)
        {
            GetCell(i_Cell).Visible = false;
        }

        public Cell GetCell(string i_Cell)
        {
            int y = i_Cell[0] - 'A';
            int x = int.Parse(i_Cell[1].ToString()) - 1;

            return m_Board[x, y];
        }

        public Cell GetCellByPoint(int i_X, int i_Y)
        {
            return m_Board[i_X, i_Y];
        }

        public bool ValidCell(string i_Cell)
        {
            int y = i_Cell[0] - 'A';
            int x = int.Parse(i_Cell[1].ToString()) - 1;

            bool valid = true;

            if (x < 0 || x > m_Board.GetLength(0) || y > m_Board.GetLength(1) || y < 0)
            {
                valid = false;
            }
            else
            {
                if (GetCell(i_Cell).Visible)
                {
                    valid = false;
                }
            }

            return valid;
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }

        public int NumOfCards()
        {
            return (m_Board.GetLength(0) * m_Board.GetLength(1)) / 2;
        }

        public override string ToString()
        {
            StringBuilder boardDisplay = new StringBuilder();

            string[] lines = new string[(m_Board.GetLength(0) + 1) * 2];

            //Offset
            lines[0] = "   ";
            lines[1] = "  =";

            //First and Seconds Lines           
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
