using System;
using System.Collections.Generic;
using System.Text;
using Invaders.Classes;

namespace C20_Ex01_Roe_313510489_Omer_206126138.Classes
{
    public class Player : Ship
    {
        // Score
        private GameScore m_Score;

        public GameScore Score {

            get
            {
                return m_Score;
            }
            set
            {
                m_Score = value;
            }
        }

        public Player() : base()
        {
            m_Score = new GameScore();
        }
    }
}
