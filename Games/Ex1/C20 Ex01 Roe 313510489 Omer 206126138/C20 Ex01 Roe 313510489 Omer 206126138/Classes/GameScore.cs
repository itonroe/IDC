using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    class GameScore
    {
        private int m_score;

        public GameScore()
        {
            m_score = 0;
        }

        public int Score { get { return m_score; } set { m_score = value; } }

        public void AddSCore(int i_ScoreToAdd)
        {
            m_score += i_ScoreToAdd;
            if (m_score < 0)
                m_score = 0;
        }
    }
}
