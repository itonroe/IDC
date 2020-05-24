using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class PcPlayer
    {
        public static double[,] prob;
        private static Board m_Board;
        public static double[] values;
        private Cell[] picks;

        public static int Score;

        const double DIFFICULTY = 0.9;//(easy) [0.7 , 1] (hard)

        public void InitGame(Board board)
        {
            m_Board = board;
            prob = new double[board.Width, board.Height];
            values = new double[board.Width * board.Height / 2];
            Score = 0;
        }

        public Cell[] Turn()
        {
            Cell[] picks = new Cell[2];

            if(GetHighestProb() / 2 > 60)
                picks = GetPicks();

            return SortCellArray(picks);

        }

        public static double GetHighestProb()
        {
            double maxProb = 0;

            for(int i=0; i< values.GetLength(0); i++)
            {
                if (values[i] > maxProb)
                    maxProb = values[i];
            }

            return maxProb;
        }
        public Cell[] SortCellArray(Cell[] picks)
        {
            if (picks[0] != null && picks[1] != null)
            {
                if (prob[picks[0].Point.X, picks[0].Point.Y] < prob[picks[1].Point.X, picks[1].Point.Y])
                {
                    Cell TempForSwap = picks[0];
                    picks[0] = picks[1];
                    picks[1] = TempForSwap;
                }
            }

            return picks;
        }

        public static Cell[] GetPicks()
        {
            ResetValues();
            SumProb();
            return FindPairFromMemoryByValue(BestValue(GetHighestValueByProb()));

        }

        public static void ResetValues()
        {
            for (int i = 0; i < values.GetLength(0); i++)
            {
                values[i] = 0;
            }
        }

        public static void SumProb()
        {
            for (int i = 0; i < prob.GetLength(0); i++)
            {
                for (int j = 0; j < prob.GetLength(1); j++)
                {
                    if (prob[i, j] > 0)
                    {
                        values[m_Board.GetCellByPoint(new Point(i, j)).Letter - 'A'] += prob[i, j];
                    }
                }
            }
        }

        public static Cell[] FindPairFromMemoryByValue(Char value)
        {
            Cell[] picks = new Cell[2];
            bool firstPick = true;

            for (int i = 0; i < prob.GetLength(0); i++)
            {
                for (int j = 0; j < prob.GetLength(1); j++)
                {
                    if (m_Board.GetCellByPoint(new Point(i, j)).Letter == value)
                    {
                        if (firstPick)
                        {
                            picks[0] = m_Board.GetCellByPoint(new Point(i, j));
                            firstPick = false;
                        }
                        else
                            picks[1] = m_Board.GetCellByPoint(new Point(i, j));
                    }
                }
            }

            return picks;
        }

        public static double GetHighestValueByProb()
        {
            double maxProb = 0;

            for(int i=0; i< values.GetLength(0); i++)
            {
                if (values[i] > maxProb)
                    maxProb = values[i];
            }

            return maxProb;
        }

        public static Char BestValue(double maxProb)
        {
            int maxValue = 0;

            for (int i = 0; i < values.GetLength(0); i++)
            {
                if (values[i] >= maxProb)
                    maxValue = i;
            }

            return (char)('A' + maxValue);
        }

        public void RefreshPcMemory(Cell cell)
        {
            for (int i = 0; i < prob.GetLength(0); i++)
            {
                for (int j = 0; j < prob.GetLength(1); j++)
                {
                    prob[i, j] *= DIFFICULTY;
                }
            }

            prob[cell.Point.X, cell.Point.Y] = 100;

            ResetValues();
            SumProb();

        }

        public static void ResetProbByValue(Char c)
        {
            for (int i = 0; i < prob.GetLength(0); i++)
            {
                for (int j = 0; j < prob.GetLength(1); j++)
                {
                    if (m_Board.GetCellByPoint(new Point(i, j)).Letter == c)
                        prob[i, j] = 0;
                }
            }
        }

    }
}
