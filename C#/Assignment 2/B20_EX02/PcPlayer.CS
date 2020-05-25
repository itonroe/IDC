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

        public static Cell[] picks; // need to be private with property 

        public int Score;

        const double DIFFICULTY = 0.9;//(EASY FOR USER TO WIN) [0.7 , 1] (hard FOR USER TO WIN)

        public void InitGame(Board board)
        {
            m_Board = board;
            prob = new double[board.Width, board.Height];
            values = new double[board.Width * board.Height / 2];
            Score = 0;
        }

        public void TryToRemember()
        {
            Cell[] tempPicks = new Cell[2];

            if(GetHighestProb() / 2 > 50)
                tempPicks = Thinking();

            picks = SortCellArray(tempPicks);

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

        public static Cell[] Thinking()
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

        public void ResetProbByValue(Char c)
        {
            for (int i = 0; i < prob.GetLength(0); i++)
            {
                for (int j = 0; j < prob.GetLength(1); j++)
                {
                    if (m_Board.GetCellByPoint(new Point(i, j)).Letter == c)
                        prob[i, j] = 0;
                }
            }

            ResetValues();
            SumProb();
        }

        public static Cell SingleRandomGuess()
        {
            return m_Board.ShowAndReturnRandomCell();
        }

        public static bool PcPickIsValid(ref Cell pick)
        {
            bool returnValue = true;

            if (pick == null)
                returnValue = false;
            else
            {
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Pc remember card : ");
                pick.Visible = true;
            }

            return returnValue;
        }

        public void SetCorrectPcGuesses()
        {
            if (!PcPickIsValid(ref picks[0]))// if he could not find a pair -> so he is uding Random
            {
                picks[0] = SingleRandomGuess();
                Console.WriteLine("using random method");
            }

            if (!PcPickIsValid(ref picks[1]))// if he could not find a pair->so he is uding Random
            {
                picks[1] = SingleRandomGuess();
                Console.WriteLine("using random method");
            }


        }

        public void UpdateScore()
        {
            if (picks[0].Letter != picks[1].Letter)
            {
                picks[0].Visible = false;
                picks[1].Visible = false;
                Score++;
                
                ResetValues();
                ResetProbByValue(picks[0].Letter);
            }

            
        }

        public Cell[] GetPicks()
        {
            return picks;
        }

        public void MakeGuessVisible(Cell cell)
        {
            cell.Visible = true;
        }

    }
}
