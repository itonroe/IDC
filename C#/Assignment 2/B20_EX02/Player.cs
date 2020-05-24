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
        //private bool humenOrPc;

        public Player()
        {
            this.m_Name = "";
            this.m_Score = 0;
            //this.humenOrPc = true;
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

        /*public bool HumenOrPc
        {
            get { return humenOrPc; }
            set { humenOrPc = value; }
        }*/

        public bool Turn
        {
            get { return m_Turn; }
            set { m_Turn = value; }
        }

        public bool CompareTo(Player i_Player)
        {
            bool A_isBigger = true;

            if (m_Score < i_Player.Score)
                A_isBigger = false;

            return A_isBigger;
        }
    }
}
