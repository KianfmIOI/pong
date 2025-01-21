using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace pong
{
    internal class NewGameClass
    {
        int top = Console.WindowHeight / 4;
        int bottom = Console.WindowHeight * 3 / 4;
        int left = Console.WindowWidth / 4;
        int right = Console.WindowWidth * 3 / 4;
        rockets Rockets = new rockets();
        public directions detectKey()
        {
            ConsoleKeyInfo info = Console.ReadKey();
            switch (info.Key)
            {
                case ConsoleKey.W:
                    return directions.up;
                case ConsoleKey.S:
                    return directions.down;
                default:
                    return directions.none;

            }
        }
        public void makeWall()
        {
            for (int i = bottom; i >= top; i--)
            {
                Console.SetCursorPosition(left,i);
                Console.Write('|');
                Console.SetCursorPosition(right, i);
                Console.Write('|');
            }
            for (int i = left; i <= right; i++)
            {
                Console.SetCursorPosition(i,top); Console.Write('-');
                Console.SetCursorPosition(i,bottom);Console.Write('-');
            }
        }
        public void run()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    directions direction = detectKey();
                    if (direction != directions.none)
                        Rockets.switchDirection(direction);
                }
                makeWall();
                Rockets.printRockets();
                Rockets.moveRockets();
                Thread.Sleep(20);
                Console.Clear();
            }
        }
    }
    enum directions
    {
        up,
        down,
        left,
        right,
        none
    }
    class rockets
    {
        int height = 5;
        directions direction;
        int tip = Console.WindowHeight / 2;
        int locationLeft = Console.WindowWidth / 4 + 2;
        int locationRight = Console.WindowWidth * 3 / 4 - 2;
        public rockets() { }
        public void printRockets()
        {
            for(int i = tip; i <= tip + height; i++)
            {
                Console.SetCursorPosition(locationLeft,i);  Console.Write('|');
                Console.SetCursorPosition(locationRight,i); Console.Write('|');
            }
        }
        public void switchDirection(directions direct)
        {
            this.direction = direct;
        }
        public void moveRockets()
        {
            int top = Console.WindowHeight / 4;
            int bottom = Console.WindowHeight * 3 / 4;
            if (direction == directions.up)
                tip--;
            else if (direction == directions.down)
                tip++;
            if(tip-1<top)
                direction = directions.down;
            else if (tip+height+1>bottom) 
                direction = directions.up;
             
                
        }
    }
    class aBall
    {
        char face = '@';
        directions direction = directions.left;
        int x = Console.WindowWidth / 2, y = Console.WindowHeight / 2;
        public aBall() { }
        public void printBall()
        {
            Console.SetCursorPosition(x,y);
            Console.Write(face);
        }
        public void switchDirection()
        {
            if(this.direction == directions.left)
                this.direction = directions.right;
            else if (this.direction == directions.right)
                this.direction = directions.left;
        }
        public void moveBall()
        {
        }
    }
}
