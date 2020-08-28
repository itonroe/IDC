using System;
using System.Collections.Generic;
using System.Text;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    public class GameScore
    {
        private int m_Score;

        public GameScore()
        {
            m_Score = 0;
        }

        public int Score 
        { 
            get 
            { 
                return m_Score; 
            } 
            
            set 
            { 
                m_Score = value; 
            } 
        }

        public void AddSCore(int i_ScoreToAdd)
        {
            m_Score += i_ScoreToAdd;
            if (m_Score < 0)
            {
                m_Score = 0;
            }
        }
    }
}
