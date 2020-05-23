using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace B20_EX02
{
    public class Board
    {
        private Cell[,] m_Board;
        
        public Board()
        {}

        public void SetSize(int i_Width, int i_Height) { 
            m_Board = new Cell[i_Height, i_Width];
        }

        public void Build()
        {
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

            m_Board[x, y] = new Cell();
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

        public void ShowCell(int i_X, int i_Y)
        {
            m_Board[i_X, i_Y].Visible = true;
        }

        public void HideCell(int i_X, int i_Y)
        {
            m_Board[i_X, i_Y].Visible = false;
        }

        public Cell GetCell(string i_Cell)
        {
            int x = i_Cell[0] - 'A';
            int y = int.Parse(i_Cell[1].ToString()) - 1;

            return m_Board[x, y];
        }

        public bool ValidCell(string i_Cell)
        {
            int x = i_Cell[0] - 'A';
            int y = int.Parse(i_Cell[1].ToString()) - 1;

            bool valid = true;

            if (x > m_Board.GetLength(0) || y > m_Board.GetLength(1))
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
                lines[0] += " " + (char)('A' + j) + "  ";
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

                    lines[i] += " " + m_Board[(i / 2) - 1, j].Letter + " |";
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
