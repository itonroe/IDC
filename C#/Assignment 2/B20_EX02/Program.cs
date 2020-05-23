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

            SetGameMode(m_Game.Player1_Name);

            if (m_Game.GameMode == 'M')
            {
                Console.WriteLine("\nEnter second player name:");
                m_Game.Player2_Name = Console.ReadLine();
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

        public static void SetSize()
        {
            string width, height;

            Console.WriteLine("\nPlese enter a bord width between 4-6");

            while (!m_Game.ValidBoardWidth(width = Console.ReadLine()))
            {
                Console.WriteLine("Bord width is not valid, please try again.");
            }

            m_Game.SetWidth(width);

            Console.WriteLine("Plese enter a bord height between 4-6");

            while (!m_Game.ValidBoardHeight(height = Console.ReadLine()))
            {
                Console.WriteLine("Bord height is not valid, please try again.");
            }

            m_Game.SetHeight(height);
        }

        public static void SetGameMode(string i_Player1Name)
        {
            Console.WriteLine($"\nHello {m_Game.Player1_Name}, lets play a memmory card game.\nS = Singal player, M = Mutiplayer");

            string userInput = Console.ReadLine();

            while (!userInput.Equals("S") && !userInput.Equals("M"))
            {
                Console.WriteLine("Please try again...\nS = Singal player, M = Mutiplayer");

                userInput = Console.ReadLine();
            }

            m_Game.GameMode = userInput[0];
        }

        public static void StartGame()
        {
            m_Game.StartGame();
        }

        public static void EndGame()
        {
            Ex02.ConsoleUtils.Screen.Clear();

            Console.WriteLine("- THE END - \n");

            Console.WriteLine(m_Game.GetScore());
            Console.WriteLine(m_Game.GetWinner());
        }
    }
}
