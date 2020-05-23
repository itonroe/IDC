using System;

namespace B20_EX02
{
    public class Game
    {
        private const int k_MinWidth = 2;
        private const int k_MaxWidth = 6;
        private const int k_MinHeight = 2;
        private const int k_MaxHeight = 6;

        public PcPlayer pcPlayer = new PcPlayer();

        private Board m_Board;
        private Player m_P1;
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
        private Player m_P2;
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
        private char m_GameMode;
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
        private bool m_GameIsOn;

        public Game()
        {
            m_Board = new Board();
            m_P1 = new Player();
            m_P2 = new Player();
            m_GameIsOn = false;
        }

        public void StartGame()
        {
            m_Board.Build();

            if (m_GameMode == 'S')
            {
                pcPlayer.InitGame(m_Board);
            }

            m_GameIsOn = true;

            while (m_GameIsOn)
            {
                Round();

                if (m_P1.Score + m_P2.Score == m_Board.NumOfCards())
                {
                    m_GameIsOn = false;
                    return;
                }
            }
        }

        public void SetWidth(string i_Width)
        {
            m_Board.Width = int.Parse(i_Width);
        }

        public void SetHeight(string i_Height)
        {
            m_Board.Height = int.Parse(i_Height);
        }

        public bool ValidBoardWidth(string i_Width)
        {
            return int.TryParse(i_Width, out _) && int.Parse(i_Width) <= k_MaxWidth && int.Parse(i_Width) >= k_MinWidth;
        }

        public bool ValidBoardHeight(string i_Height)
        {
            return int.TryParse(i_Height, out _) && (int.Parse(i_Height) <= k_MaxHeight && int.Parse(i_Height) >= k_MinHeight) && (int.Parse(i_Height) * m_Board.Width % 2 == 0);
        }

        public void Round()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Turn(m_P1);

            if (m_P1.Score + m_P2.Score == m_Board.NumOfCards())
            {
                m_GameIsOn = false;
                return;
            }   

            Ex02.ConsoleUtils.Screen.Clear();

            if (m_GameMode == 'M')
            {
                Turn(m_P2);
            }
            else
            {
                TurnPc();
            }
        }

        public void Turn(Player i_Player)
        {
            string card1 = FirstPick(i_Player.Name);
            string card2 = SecondPick(i_Player.Name);

            if (MatchingCards(card1, card2))
            {
                m_Board.ShowCell(card2);
                i_Player.Score++;
            }
            else
            {
                System.Threading.Thread.Sleep(2000);
                m_Board.HideCell(card1);
                m_Board.HideCell(card2);
            }

            Ex02.ConsoleUtils.Screen.Clear();
            m_Board.Print();
        }

        public void TurnPc()
        {
            pcPlayer.Turn(this);
        }

        public void PcGuess(Point[] i_Points)
        {
            System.Threading.Thread.Sleep(2000);
            Cell pick1 = m_Board.GetCellByPoint(i_Points[0].X, i_Points[0].Y);
            pick1.Visible = true;
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("PCGUESS");
            m_Board.Print();
            pcPlayer.RefreshPcMemory(pick1);

            System.Threading.Thread.Sleep(2000);
            Cell pick2 = m_Board.GetCellByPoint(i_Points[1].X, i_Points[1].Y);
            pick2.Visible = true;
            Ex02.ConsoleUtils.Screen.Clear();
            m_Board.Print();
            pcPlayer.RefreshPcMemory(pick2);

            System.Threading.Thread.Sleep(2000);

            if (!MatchingCards(pick1, pick2))
            {
                pick1.Visible = false;
                pick2.Visible = false;
            }
            else
            {
                pcPlayer.prob[i_Points[0].X, i_Points[0].Y] = 0;
                pcPlayer.prob[i_Points[1].X, i_Points[1].Y] = 0;
            }
        }

        public void PcRandomGuess()
        {
            System.Threading.Thread.Sleep(2000);
            Point pick1 = m_Board.ShowRandomCell();
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("PCRANDOM");
            m_Board.Print();

            pcPlayer.RefreshPcMemory(m_Board.GetCellByPoint(pick1.X, pick1.Y));

            System.Threading.Thread.Sleep(2000);
            Point pick2 = m_Board.ShowRandomCell();
            Ex02.ConsoleUtils.Screen.Clear();
            m_Board.Print();

            pcPlayer.RefreshPcMemory(m_Board.GetCellByPoint(pick2.X, pick2.Y));
            System.Threading.Thread.Sleep(2000);

            if (!MatchingCards(m_Board.GetCellByPoint(pick1.X, pick1.Y), m_Board.GetCellByPoint(pick2.X, pick2.Y)))
            {
                m_Board.GetCellByPoint(pick1.X, pick1.Y).Visible = false;
                m_Board.GetCellByPoint(pick2.X, pick2.Y).Visible = false;
            }
            else
            {
                pcPlayer.prob[pick1.X, pick1.Y] = 0;
                pcPlayer.prob[pick2.X, pick2.Y] = 0;
            }
        }

        public string ChooseCard(string i_CardNumber)
        {
            Console.WriteLine($"Choose {i_CardNumber} card:");

            string card;

            while (!ValidCard(card = Console.ReadLine()))
            {
                Console.WriteLine($"Choose {i_CardNumber} card again:");
            }

            m_Board.ShowCell(card);

            if (m_GameMode == 'S')
            {
                pcPlayer.RefreshPcMemory(m_Board.GetCell(card));
            }

            return card;
        }

        public bool ValidCard(string i_InputCard)
        {
            bool validcard = true;

            if (i_InputCard.Equals("Q"))
                ExitGame();
            else
            {
                if (i_InputCard.Length != 2 || !(i_InputCard[0] <= 'Z' && i_InputCard[0] >= 'A') || !(i_InputCard[1] >= '0'  && i_InputCard[1] <= '9'))
                {
                    validcard = false;
                }
                else
                {
                    validcard = m_Board.ValidCell(i_InputCard);
                }
            }

            return validcard;
        }

        private string FirstPick(string i_PlayerName)
        {
            Console.WriteLine($"Turn: {i_PlayerName}, first pick\n");
            m_Board.Print();

            string card = ChooseCard("first");

            Ex02.ConsoleUtils.Screen.Clear();

            return card;
        }

        private string SecondPick(string i_PlayerName)
        {
            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            m_Board.Print();

            string card = ChooseCard("second");

            Ex02.ConsoleUtils.Screen.Clear();

            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            m_Board.Print();

            return card;
        }

        public bool MatchingCards(string i_Card1, string i_Card2)
        {
            return m_Board.GetCell(i_Card1).Letter == m_Board.GetCell(i_Card2).Letter;
        }

        public bool MatchingCards(Cell pick1, Cell pick2)
        {
            return pick1.Letter == pick2.Letter;
        }

        public string GetScore()
       {
           return $"The Score is:\n    {m_P1.Name} - {m_P1.Score}\n    {m_P2.Name} - {m_P2.Score}";
       }

        public string GetWinner()
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

            return $"The winner is {winner}";
        }

        public void ExitGame()
        {
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("{player.Name}, thank you for playing memory game");
            m_GameIsOn = false;
        }
    }
}
