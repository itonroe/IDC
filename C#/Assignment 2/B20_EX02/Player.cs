using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class Player
    {
        private string m_Name;
        private int m_Score;
        private bool m_Turn;

        public Player()
        {
            this.m_Name = string.Empty;
            this.m_Score = 0;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }

        public int Score
        {
            get { return m_Score; }
            set { m_Score = value; }
        }

        public bool Turn
        {
            get { return m_Turn; }
            set { m_Turn = value; }
        }

        public bool CompareTo(Player i_Player)
        {
            bool A_isBigger = true;

            if (m_Score < i_Player.Score)
            {
                A_isBigger = false;
            }

            return A_isBigger;
        }
    }
}
