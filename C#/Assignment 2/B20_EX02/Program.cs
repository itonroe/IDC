using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    class Program
    {

        public static Game m_Game;

        public static void Main(string[] args)
        {
            m_Game = new Game();

            Console.WriteLine("Enter your name:");
            m_Game.Player1_Name = Console.ReadLine();

            Console.WriteLine($"\nHello {m_Game.Player1_Name}, lets play a memmory card game.\nS = Singal player, M = Mutiplayer");
            SetGameMode();

            if (m_Game.GameMode == 'M')
            {
                Console.WriteLine("\nEnter second player name:");
                m_Game.Player2_Name = Console.ReadLine();
            }
            else
            {
                m_Game.Player2_Name = "PC";
            }

            bool keepPlaying = true;

            while(keepPlaying)
            {
                SetSize();
                StartGame();
                EndGame();

                Console.WriteLine("Do you want to play again? (Y - Yes, N - No)");

                string answer = Console.ReadLine();

                while (!answer.Equals("N") && !answer.Equals("Y"))
                {
                    Console.WriteLine("Wrong input, please try again... Y - Yes, N - No");
                    answer = Console.ReadLine();
                }

                if (answer.Equals("N"))
                {
                    keepPlaying = false;
                }
            }
        }

        public static void SetGameMode()
        {
            string userInput = Console.ReadLine();

            while (!userInput.Equals("S") && !userInput.Equals("M"))
            {
                Console.WriteLine("Please try again...\nS = Singal player, M = Mutiplayer");

                userInput = Console.ReadLine();
            }

            m_Game.GameMode = userInput[0];
        }

        public static void SetSize()
        {
            string width, height;

            Console.WriteLine("\nPlese enter a bord width between 4-6");

            while (!m_Game.ValidWidth(width = Console.ReadLine()))
            {
                Console.WriteLine("Bord width is not valid, please try again.");
            }

            m_Game.SetWidth(width);

            Console.WriteLine("Plese enter a bord height between 4-6");

            while (!m_Game.ValidHeight(height = Console.ReadLine()))
            {
                Console.WriteLine("Bord height is not valid, please try again.");
            }

            m_Game.SetHeight(height);
        }

        public static void StartGame()
        {
            m_Game.BuildGame();
            m_Game.GameIsOn = true;

            while (m_Game.GameIsOn)
            {
                Round();
            }
        }

        public static void Round()
        {
            Turn();
            Turn();
        }

        public static void Turn()
        {
            if (m_Game.IsEnded() || !m_Game.GameIsOn)
            {
                return;
            }

            Ex02.ConsoleUtils.Screen.Clear();

            string playerName = m_Game.PlayerTurn_Name;

            if (m_Game.IsPcTurn())
            {
                PcPick(playerName);
            }
            else
            {
                Pick(playerName);
            }

            if (m_Game.GameIsOn) 
            { 
                System.Threading.Thread.Sleep(2000);

                Ex02.ConsoleUtils.Screen.Clear();
                Console.WriteLine(m_Game);
            }

            m_Game.ChangeTurn();
        }

        public static string ChooseCard(string i_CardNumber)
        {
            Console.WriteLine($"Choose {i_CardNumber} card:");

            string card = Console.ReadLine();

            while (!ValidCard(card))
            {
                Console.WriteLine($"Choose {i_CardNumber} card again:");
                card = Console.ReadLine();
            }

            return card;
        }

        public static void Pick(string i_PlayerName)
        {
            Console.WriteLine($"Turn: {i_PlayerName}, first pick\n");
            Console.WriteLine(m_Game);

            string card1 = ChooseCard("first");

            if (card1.Equals("Q"))
            {
                ExitGame();
                return;
            }
            else
            {
                m_Game.OpenCard(card1);
            }

            Ex02.ConsoleUtils.Screen.Clear();

            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);

            string card2 = ChooseCard("second");

            if (card2.Equals("Q"))
            {
                ExitGame();
                return;
            }
            else
            {
                m_Game.OpenCard(card2);
            }

            Ex02.ConsoleUtils.Screen.Clear();

            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);

            m_Game.MatchingCards(card1, card2);

            if (m_Game.GameMode == 'S')
            {
                m_Game.RefreshPcPlayerMemory(card1, card2);
            }
        }

        public static bool ValidCard(string i_InputCard)
        {
            bool validcard = true;

            if (i_InputCard.Length != 2 || !(i_InputCard[0] <= 'Z' && i_InputCard[0] >= 'A') || !(i_InputCard[1] >= '0' && i_InputCard[1] <= '9'))
            {
                if (!i_InputCard.Equals("Q"))
                {
                    validcard = false;
                }
            }
            else
            {
                validcard = m_Game.ValidCard(i_InputCard);
            }

            return validcard;
        }

        public static void EndGame()
        {
            if (m_Game.IsEnded())
            {
                Ex02.ConsoleUtils.Screen.Clear();
            }

            Console.WriteLine("- THE END - \n");

            Console.WriteLine($"The Score is:\n    {m_Game.Player1_Name} - {m_Game.Player1_Score}\n    {m_Game.Player2_Name} - {m_Game.Player2_Score}");
            Console.WriteLine($"The winner is {m_Game.GetWinnerName()}");
        }

        public static void ExitGame()
        {
            m_Game.GameIsOn = false;
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine("Thank you for playing our memory game");
        }

        public static void PcPick(string i_PlayerName)
        {
            m_Game.PcChooseCards();

            Console.WriteLine($"Turn: {i_PlayerName}, first pick\n");
            Console.WriteLine(m_Game);

            Console.WriteLine("Pc is thinking...");
            System.Threading.Thread.Sleep(2000);
            m_Game.PcOpenCard();

            System.Threading.Thread.Sleep(3000);
            Ex02.ConsoleUtils.Screen.Clear();

            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);

            Console.WriteLine("Pc is thinking...");
            System.Threading.Thread.Sleep(2000);
            m_Game.PcOpenCard();

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);

            m_Game.MatchingCardsPc();
        }
    }
}
