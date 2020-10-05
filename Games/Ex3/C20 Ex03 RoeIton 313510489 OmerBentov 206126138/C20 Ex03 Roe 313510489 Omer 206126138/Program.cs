using System;

namespace C20_Ex03_Roe_313510489_Omer_206126138
{
    public static class Program
    {
        [STAThread]
        public static void Main()
        {
            using (var game = new GameWithScreens())
            {
                game.Run();
            }
        }
    }
}
