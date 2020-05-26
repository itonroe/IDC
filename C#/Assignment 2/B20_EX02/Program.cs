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

            if (m_Game.GameMode == 'S')
            {
                SetGameDifficulty();
            }
        }

        public static void SetGameDifficulty()
        {
            Console.WriteLine("\nChoose game level:\n1 - Easy\n2 - Medium\n3 - Hard");

            string userDifficulty = Console.ReadLine();

            while (!ValidDifficulty(userDifficulty))
            {
                Console.WriteLine("Please try again...\nChoose game level :\n1 - Easy\n2 - Medium\n3 - Hard");
                userDifficulty = Console.ReadLine();
            }

            m_Game.SetDifficulty(userDifficulty);
        }

        public static bool ValidDifficulty(string i_Difficulty)
        {
            return int.TryParse(i_Difficulty, out _) && int.Parse(i_Difficulty) >= 1 && int.Parse(i_Difficulty) <= 3;
        }

        public static void SetSize()
        {
            string width, height;

            Console.WriteLine("\nPlese enter a board width between 4-6");

            while (!m_Game.ValidWidth(width = Console.ReadLine()))
            {
                Console.WriteLine("Baord width is not valid, please try again.");
            }

            m_Game.SetWidth(width);

            Console.WriteLine("Plese enter a baord height between 4-6");

            while (!m_Game.ValidHeight(height = Console.ReadLine()))
            {
                Console.WriteLine("Baord height is not valid, please try again.");
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
            HumanTurn();

            if (m_Game.IsEnded() || !m_Game.GameIsOn)
            {
                return;
            }

            m_Game.ChangeTurn();

            if (m_Game.GameMode == 'M')
            {
                HumanTurn();
            }
            else
            {
                PcTurn();
            }

            if (m_Game.IsEnded() || !m_Game.GameIsOn)
            {
                return;
            }

            m_Game.ChangeTurn();
        }

        public static void HumanTurn()
        {
            Ex02.ConsoleUtils.Screen.Clear();

            string playerName = m_Game.PlayerTurn_Name;

            Pick(playerName);
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
            
            m_Game.OpenCard(card1);
            Ex02.ConsoleUtils.Screen.Clear();

            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);

            string card2 = ChooseCard("second");

            if (card2.Equals("Q"))
            {
                ExitGame();
                return;
            }

            m_Game.OpenCard(card2);
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);
            System.Threading.Thread.Sleep(2000);

            m_Game.MatchingCards(card1, card2);
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
            System.Threading.Thread.Sleep(2000);

            Environment.Exit(0);
        }

        public static void PcTurn()
        {
            Ex02.ConsoleUtils.Screen.Clear();

            //computer is looking for pair
            m_Game.PcSelectCards();

            PcPick(m_Game.PlayerTurn_Name);
        }

        public static void PcPick(string i_PlayerName)
        {
            Console.WriteLine($"Turn: {i_PlayerName}, first pick\n");
            Console.WriteLine(m_Game);

            Console.WriteLine("Pc is thinking...");
            System.Threading.Thread.Sleep(2000);

            m_Game.PcOpenCard(0);
            
            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);

            Console.WriteLine("Pc is thinking...");
            System.Threading.Thread.Sleep(2000);

            m_Game.PcOpenCard(1);

            Ex02.ConsoleUtils.Screen.Clear();
            Console.WriteLine($"Turn: {i_PlayerName}, second pick\n");
            Console.WriteLine(m_Game);
            System.Threading.Thread.Sleep(2000);

            m_Game.PcMatchingCards();
        }
    }
}
