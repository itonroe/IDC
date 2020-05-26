using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class PcPlayer
    {
        private Board m_Board;
        private double[,] m_Probs;
        private double[] m_Values;
        private Cell[] m_Picks;

        private double m_Difficulty = 0.9;//(EASY FOR USER TO WIN) [0.7 ,0.9] (HARD FOR USER TO WIN)

        public void BuildPlayer(Board i_Board)
        {
            m_Board = i_Board;
            m_Probs = new double[i_Board.Height, i_Board.Width];
            m_Values = new double[i_Board.Width * i_Board.Height / 2];
        }

        public void TryToRemember()
        {
            Cell[] tempPicks = new Cell[2];

            if(GetHighestProb() / 2 > 52)
                tempPicks = Thinking();

            m_Picks = SortCellArray(tempPicks);
        }

        public double GetHighestProb()
        {
            double maxProb = 0;

            for (int i = 0; i < m_Values.GetLength(0); i++)
            {
                if (m_Values[i] > maxProb)
                    maxProb = m_Values[i];
            }

            return maxProb;
        }

        public Cell[] SortCellArray(Cell[] i_Picks)
        {
            if (i_Picks[0] != null && i_Picks[1] != null)
            {
                if (m_Probs[i_Picks[0].Point.X, i_Picks[0].Point.Y] < m_Probs[i_Picks[1].Point.X, i_Picks[1].Point.Y])
                {
                    Cell TempForSwap = i_Picks[0];
                    i_Picks[0] = i_Picks[1];
                    i_Picks[1] = TempForSwap;
                }
            }

            return i_Picks;
        }

        public Cell[] Thinking()
        {
            ResetValues();
            SumProb();
            return FindPairFromMemoryByValue(GetHighestValueByProb());
        }

        public void SetGameDifficulty(int i_Difficulty)
        {
            switch (i_Difficulty)
            {
                case 1:
                    m_Difficulty = 0.7;
                    break;
                case 2:
                    m_Difficulty = 0.8;
                    break;
                case 3:
                    m_Difficulty = 0.9;
                    break;
                default:
                    m_Difficulty = 0.5;
                    break;
            }
        }

        public void ResetValues()
        {
            for (int i = 0; i < m_Values.GetLength(0); i++)
            {
                m_Values[i] = 0;
            }
        }

        public void SumProb()
        {
            ResetValues();

            for (int i = 0; i < m_Probs.GetLength(0); i++)
            {
                for (int j = 0; j < m_Probs.GetLength(1); j++)
                {
                    if (m_Probs[i, j] > 0)
                    {
                        m_Values[m_Board.GetCell(new Point(i, j)).Letter - 'A'] += m_Probs[i, j];
                    }
                }
            }
        }

        public Cell[] FindPairFromMemoryByValue(char i_Letter)
        {
            Cell[] picks = new Cell[2];
            bool firstPick = true;

            for (int i = 0; i < m_Probs.GetLength(0); i++)
            {
                for (int j = 0; j < m_Probs.GetLength(1); j++)
                {
                    if (m_Board.GetCell(new Point(i, j)).Letter == i_Letter)
                    {
                        if (firstPick)
                        {
                            picks[0] = m_Board.GetCell(new Point(i, j));
                            firstPick = false;
                        }
                        else
                            picks[1] = m_Board.GetCell(new Point(i, j));
                    }
                }
            }

            return picks;
        }

        public char GetHighestValueByProb()
        {
            double maxProb = 0;
            int offset = 0;

            for(int i=0; i< m_Values.GetLength(0); i++)
            {
                if (m_Values[i] > maxProb)
                {
                    maxProb = m_Values[i];
                    offset = i;
                }
            }

            return (char)('A' + offset);
        }

        public void RefreshPcMemory(Cell i_Cell)
        {
            for (int i = 0; i < m_Probs.GetLength(0); i++)
            {
                for (int j = 0; j < m_Probs.GetLength(1); j++)
                {
                    m_Probs[i, j] *= m_Difficulty;
                }
            }

            m_Probs[i_Cell.Point.X, i_Cell.Point.Y] = 100;

            ResetValues();
            SumProb();
        }

        public void ResetProbByValue(char i_Letter)
        {
            for (int i = 0; i < m_Probs.GetLength(0); i++)
            {
                for (int j = 0; j < m_Probs.GetLength(1); j++)
                {
                    if (m_Board.GetCell(new Point(i, j)).Letter == i_Letter)
                    {
                        m_Probs[i, j] = 0;
                    }
                }
            }

            ResetValues();
            SumProb();
        }

        public Cell SingleRandomGuess()
        {
            return m_Board.GetRandomCell();
        }

        public void SetCorrectPcGuesses()
        {
            if (m_Picks[0] == null)// if he could not find a pair -> so he is uding Random
            {
                m_Picks[0] = SingleRandomGuess();
            }

            if (m_Picks[1] == null)// if he could not find a pair -> so he is uding Random
            {
                m_Picks[1] = SingleRandomGuess();

                while (m_Picks[0].Point.X == m_Picks[1].Point.X && m_Picks[0].Point.Y == m_Picks[1].Point.Y)
                {
                    m_Picks[1] = SingleRandomGuess();
                }
            }
        }

        public Cell[] Picks
        {
            get
            {
                return m_Picks;
            }
        }
    }
}
