using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class PcPlayer
    {
        public double[,] prob;
        private Board m_Board;
        public double[] values;

        const double DIFFICULTY = 0.8;

        public void InitGame(Board board)
        {
            m_Board = board;
            prob = new double[board.Width, board.Height];
            values = new double[board.Width * board.Height / 2];
        }

        public void Turn(Game game)
        {
            int card = BesValueToStart();
            Point[] points = MostRememberdCardsByValue(card);

            if ((prob[points[0].X, points[0].Y] + prob[points[1].X, points[1].Y]) / 2 > 30)
            {
                game.PcGuess(points);
            }
            else
            {
                game.PcRandomGuess();
            }
        }
        public Point[] MostRememberdCardsByValue(int card)
        {
            bool first = true;

            Point[] points = new Point[2];

            for (int i = 0; i < prob.GetLength(0); i++)
            {
                for (int j = 0; j < prob.GetLength(1); j++)
                {
                    if ((card == m_Board.GetCellByPoint(i,j).Letter - 1 - 'A'))
                    {
                        if (first)
                        {
                            points[0] = new Point(i, j);
                            first = false;
                        }
                        else
                        {
                            points[1] = new Point(i, j);
                        }
                    }

                }
            }
            return SortPointsArray(points);
        }

        public Point[] SortPointsArray(Point[] points)
        {
            if (prob[points[0].X, points[0].Y] > prob[points[1].X, points[1].Y])
            {
                Point swap = points[0];
                points[0] = points[1];
                points[1] = swap;
            }

            return points;
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
        }

        public int BesValueToStart()
        {
            ResetValues();
            SumProb();
            return MaxCardProb();
        }

        public void SumProb()
        {
            for (int i = 0; i < prob.GetLength(0); i++)
            {
                for (int j = 0; j < prob.GetLength(1); j++)
                {
                    values[m_Board.GetCellByPoint(i, j).Letter - 'A'] += prob[i, j];
                }
            }
        }

        public void ResetValues()
        {
            for (int i = 0; i < values.GetLength(0); i++)
            {
                values[i] = 0;
            }
        }

        public int MaxCardProb()
        {
            int maxCardProb = -1;
            for (int i = 0; i < values.Length; i++)
            {
                if (values[i] >= maxCardProb)
                    maxCardProb = i;
            }

            return maxCardProb;
        }

        public double[,] Prob
        {
            get { return prob; }
            set { prob = value; }
        }

        //(game) public void FirstPcGuess(Point point)
    }
}
