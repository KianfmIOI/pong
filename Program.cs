using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pong
{
    internal class Program
    {
        static void Main(string[] args)
        {
            NewGameClass game = new NewGameClass();
            Console.CursorVisible = false;
            game.run();
            Console.ReadKey();
        }
    }
}
