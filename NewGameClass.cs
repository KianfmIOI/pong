using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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
        aBall ball = new aBall();
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
        public directions RocketCollideBall()
        {
            if(ball.X == Rockets.LocationLeft && Rockets.Tip <= ball.Y && ball.Y <= Rockets.Tip+Rockets.Height )
            {
                return directions.right;
            }
            else if(ball.X == Rockets.LocationRight && Rockets.Tip <= ball.Y && ball.Y <= Rockets.Tip + Rockets.Height)
            {
                return directions.left;
            }

        }
    }
        
    class rockets
    {
        int height = 5;
        directions direction;
        int tip = Console.WindowHeight / 2;
        int locationLeft = Console.WindowWidth / 4 + 2;
        int locationRight = Console.WindowWidth * 3 / 4 - 2;
        public rockets() { }
        public int Tip { get;}
        public int Height { get; }
        public int LocationLeft { get; }
        public int LocationRight { get;}
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
        int top = Console.WindowHeight / 4;
        int bottom = Console.WindowHeight * 3 / 4;
        char face = '@';
        directions direction = directions.left;
        int x = Console.WindowWidth / 2, y = Console.WindowHeight / 2;
        public aBall() { }
        public int X { get; set; }
        public int Y { get; set; }
        public void printBall()
        {
            Console.SetCursorPosition(x,y);
            Console.Write(face);
        }
        public void moveaBall()
        {
            switch (direction)
            {
                case directions.left:
                    x--;
                    break;
                case directions.right:
                    x++;
                    break;
                
                default:
                    break;
            }
            if (y <= top)
                y++;
            else if (y >= bottom)
                y--;
            
        }
        public void switchBallDirection(directions direct)
        {
            this.direction = direct;
        }
        
    }
    enum directions
    {
        up,
        UpRight,
        right,
        DownRight,
        down,
        DownLeft,
        left,
        UpLeft,
        none
    }
}
