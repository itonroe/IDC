using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class PcPlayer
    {
        public static double[,] s_Prob;
        private static Board m_Board;
        public static double[] s_Values;
        public Cell[] m_Picks;
        public bool m_FirstCardShown;

        public static int Score;

        const double DIFFICULTY = 0.9;//(easy) [0.7 , 1] (hard)

        public void InitGame(Board board)
        {
            m_Board = board;
            s_Prob = new double[board.Width, board.Height];
            s_Values = new double[board.Width * board.Height / 2];
            Score = 0;
            m_FirstCardShown = false;
        }

        public void SetPicks()
        {
            m_Picks = new Cell[2];

            if(GetHighestProb() / 2 > 60)
                m_Picks = GetPicks();

            m_Picks = SortCellArray(m_Picks);
        }

        public static double GetHighestProb()
        {
            double maxProb = 0;

            for(int i=0; i< s_Values.GetLength(0); i++)
            {
                if (s_Values[i] > maxProb)
                    maxProb = s_Values[i];
            }

            return maxProb;
        }
        public Cell[] SortCellArray(Cell[] m_Picks)
        {
            if (m_Picks[0] != null && m_Picks[1] != null)
            {
                if (s_Prob[m_Picks[0].Point.X, m_Picks[0].Point.Y] < s_Prob[m_Picks[1].Point.X, m_Picks[1].Point.Y])
                {
                    Cell TempForSwap = m_Picks[0];
                    m_Picks[0] = m_Picks[1];
                    m_Picks[1] = TempForSwap;
                }
            }

            return m_Picks;
        }

        public static Cell[] GetPicks()
        {
            ResetValues();
            SumProb();
            return FindPairFromMemoryByValue(BestValue(GetHighestValueByProb()));

        }

        public static void ResetValues()
        {
            for (int i = 0; i < s_Values.GetLength(0); i++)
            {
                s_Values[i] = 0;
            }
        }

        public static void SumProb()
        {
            for (int i = 0; i < s_Prob.GetLength(0); i++)
            {
                for (int j = 0; j < s_Prob.GetLength(1); j++)
                {
                    if (s_Prob[i, j] > 0)
                    {
                        s_Values[m_Board.GetCellByPoint(new Point(i, j)).Letter - 'A'] += s_Prob[i, j];
                    }
                }
            }
        }

        public static Cell[] FindPairFromMemoryByValue(Char value)
        {
            Cell[] m_Picks = new Cell[2];
            bool firstPick = true;

            for (int i = 0; i < s_Prob.GetLength(0); i++)
            {
                for (int j = 0; j < s_Prob.GetLength(1); j++)
                {
                    if (m_Board.GetCellByPoint(new Point(i, j)).Letter == value)
                    {
                        if (firstPick)
                        {
                            m_Picks[0] = m_Board.GetCellByPoint(new Point(i, j));
                            firstPick = false;
                        }
                        else
                            m_Picks[1] = m_Board.GetCellByPoint(new Point(i, j));
                    }
                }
            }

            return m_Picks;
        }

        public static double GetHighestValueByProb()
        {
            double maxProb = 0;

            for(int i=0; i< s_Values.GetLength(0); i++)
            {
                if (s_Values[i] > maxProb)
                    maxProb = s_Values[i];
            }

            return maxProb;
        }

        public static char BestValue(double maxProb)
        {
            int maxValue = 0;

            for (int i = 0; i < s_Values.GetLength(0); i++)
            {
                if (s_Values[i] >= maxProb)
                    maxValue = i;
            }

            return (char)('A' + maxValue);
        }

        public void RefreshPcMemory(Cell cell)
        {
            for (int i = 0; i < s_Prob.GetLength(0); i++)
            {
                for (int j = 0; j < s_Prob.GetLength(1); j++)
                {
                    s_Prob[i, j] *= DIFFICULTY;
                }
            }

            s_Prob[cell.Point.X, cell.Point.Y] = 100;

            ResetValues();
            SumProb();
        }

        public static void ResetProbByValue(char c)
        {
            for (int i = 0; i < s_Prob.GetLength(0); i++)
            {
                for (int j = 0; j < s_Prob.GetLength(1); j++)
                {
                    if (m_Board.GetCellByPoint(new Point(i, j)).Letter == c)
                        s_Prob[i, j] = 0;
                }
            }
        }

    }
}
