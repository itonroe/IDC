using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    public class Player
    {
        private string name;
        private int score;
        private bool humenOrPc;

        public Player()
        {
            this.name = "";
            this.score = 0;
            this.humenOrPc = true;
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public int Score
        {
            get { return score; }
            set { score = value; }
        }

        public bool HumenOrPc
        {
            get { return humenOrPc; }
            set { humenOrPc = value; }
        }

        public bool CompareTo(Player b)
        {
            bool AisBigger = false;

            if (this.score > b.score)
                AisBigger = true;

            return AisBigger;
        }
    }
}
