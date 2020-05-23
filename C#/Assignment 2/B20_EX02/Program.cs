using System;
using System.Collections.Generic;
using System.Text;

namespace B20_EX02
{
    class Program
    {

        public static void Main(string[] args)
        {
            int width = int.Parse(Console.ReadLine());
            int height = int.Parse(Console.ReadLine());

            Board board = new Board();
            board.SetSize(width, height);
            board.Build();
            board.Print();


            Console.ReadLine();
        }

    }
}
