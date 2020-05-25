using System;

namespace B20_EX02
{
    public class Game
    {
        public PcPlayer pcPlayer = new PcPlayer();

        private const int k_MinWidth = 4;
        private const int k_MaxWidth = 6;
        private const int k_MinHeight = 4;
        private const int k_MaxHeight = 6;

        private Player m_PlayerTurn;

        public Board m_Board;// changed to public
        private Player m_P1;
        private Player m_P2;
        private char m_GameMode;
        private bool m_GameIsOn;

        public Game()
        {
            m_Board = new Board();
            m_P1 = new Player();
            m_P2 = new Player();
            m_GameIsOn = false;
        }

        public void BuildGame()
        {
            m_Board.Build();

            if (m_GameMode == 'S')
            {
                pcPlayer.InitGame(m_Board);
            }

            ChangeTurn();
        }

        public void SetWidth(string i_Width)
        {
            m_Board.Width = int.Parse(i_Width);
        }

        public void SetHeight(string i_Height)
        {
            m_Board.Height = int.Parse(i_Height);
        }

        public bool ValidWidth(string i_Width)
        {
            return int.TryParse(i_Width, out _) && int.Parse(i_Width) <= k_MaxWidth && int.Parse(i_Width) >= k_MinWidth;
        }

        public bool ValidHeight(string i_Height)
        {
            return int.TryParse(i_Height, out _) && (int.Parse(i_Height) <= k_MaxHeight && int.Parse(i_Height) >= k_MinHeight) && (int.Parse(i_Height) * m_Board.Width % 2 == 0);
        }

        public void OpenCard(string i_Card)
        {
            m_Board.ShowCell(i_Card);

            if (m_GameMode == 'S')
            {
                pcPlayer.RefreshPcMemory(m_Board.GetCellByString(i_Card));
            }
        }

        public void ChangeTurn()
        {
            if (m_P1.Turn)
            {
                m_P1.Turn = false;
                m_P2.Turn = true;
                m_PlayerTurn = m_P2;
            }
            else
            {
                m_P1.Turn = true;
                m_P2.Turn = false;
                m_PlayerTurn = m_P1;
            }
        }

        public bool ValidCard(string i_InputCard)
        {
            return m_Board.ValidCell(i_InputCard);
        }

        public bool MatchingCards(string i_Card1, string i_Card2)
        {
            bool match = m_Board.GetCellByString(i_Card1).Letter == m_Board.GetCellByString(i_Card2).Letter;

            if (match)
            {
                m_PlayerTurn.Score++;
            }
            else
            {
                HideCards(i_Card1, i_Card2);
            }

            if (m_GameMode == 'S' && match)
                pcPlayer.ResetProbByValue(m_Board.GetCellByString(i_Card1).Letter);

            return match;

        }


        public void HideCards(string i_Card1, string i_Card2)
        {
            m_Board.HideCell(i_Card1);
            m_Board.HideCell(i_Card2);
        }

        public string GetWinnerName()
        {
            string winner = m_P1.Name;

            if (m_P1.Score < m_P2.Score)
            {
                winner = m_P2.Name;
            }
            else
            {
                winner = "no one... it's a tie.";
            }

            return winner;
        }

        public bool IsEnded()
        {
            bool isEnded = true;

            if (m_Board.BouardIsFull())
            {
                m_GameIsOn = false;
            }
            else
            {
                isEnded = false;
            }

            return isEnded;
        }

        public override string ToString()
        {
            return m_Board.ToString();
        }

        public string PlayerTurn_Name
        {
            get { return m_PlayerTurn.Name; }
        }
        public string Player1_Name
        {
            get
            {
                return m_P1.Name;
            }
            set
            {
                m_P1.Name = value;
            }
        }
        public string Player2_Name
        {
            get
            {
                return m_P2.Name;
            }
            set
            {
                m_P2.Name = value;
            }
        }
        public int Player1_Score
        {
            get { return m_P1.Score; }
        }
        public int Player2_Score
        {
            get { return m_P2.Score; }
        }
        public char GameMode
        {
            get
            {
                return m_GameMode;
            }
            set
            {
                m_GameMode = value;
            }
        }
        public bool GameIsOn
        {
            get 
            { 
                return m_GameIsOn; 
            }
            set 
            { 
                m_GameIsOn = value; 
            }
        }
    }
}
