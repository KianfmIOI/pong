using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace pong
{
    internal class game
    {
        rocket rockets = new rocket();
        ball ball = new ball();
        public void Collide()
        {

            int rocketL = Console.WindowWidth / 4 + 2;
            if (ball.X <= rocketL+1 && (ball.Y >= rockets.Tip && ball.Y <= rockets.Tip + rockets.Height))
                ball.Direction = Direction.right;
            int rocketR = Console.WindowWidth * 3 / 4 - 2;
            if (ball.X >= rocketR - 1 && (ball.Y >= rockets.Tip && ball.Y <= rockets.Tip + rockets.Height))
                ball.Direction = Direction.left;
        }

        public game()
        {
            makeWall();
            ball.moveBall();
            rockets.printRockets();
        }

        public void makeWall()
        {
            int left = Console.WindowWidth / 4;
            int right = Console.WindowWidth * 3 / 4;
            int top = Console.WindowHeight / 4;
            int bottom = Console.WindowHeight * 3 / 4;
            for (int i = left; i <= right; i++)
            {
                Console.SetCursorPosition(i, top);
                Console.Write('-');
                Console.SetCursorPosition(i, bottom);
                Console.Write('-');
            }
            for (int j = top; j <= bottom; j++)
            {
                Console.SetCursorPosition(left, j);
                Console.Write('|');
                Console.SetCursorPosition(right, j);
                Console.Write('|');
            }
        }
        public Direction detectKey()
        {
            ConsoleKeyInfo info = Console.ReadKey();
            switch (info.Key)
            {
                case ConsoleKey.W:
                    return Direction.up;
                case ConsoleKey.S:
                    return Direction.down;
                default:
                    return Direction.none;
            }
        }
        public void run()
        {
            while (true)
            {
                if (Console.KeyAvailable)
                {
                    Direction direction = detectKey();
                    if (direction != Direction.none)
                        rockets.changeDirect(direction);
                }
                Collide();
                ball.moveBall();
                ball.printBall();
                rockets.moveRocket();
                rockets.printRockets();
                Thread.Sleep(100);
                Console.Clear();
                makeWall();
            }
        }

    }
    enum Direction
    {
        up,
        down,
        left,
        right,
        none
    }
    class rocket
    {
        int height;
        Direction direction;
        int tip = Console.WindowHeight / 4 + 5;
        public rocket(int height = 5)
        {
            this.height = height;
        }
        public rocket(int height, Direction direction, int tip)
        {
            this.direction = direction;
            this.tip = tip;
            this.height = height;
        }

        public int Height
        {
            get { return height; }
            set { height = value; }
        }
        public Direction Direction { get; set; }
        public int Tip
        {
            get { return tip; }
            set { tip = value; }
        }
        public void printRockets()
        {
            int top = Console.WindowHeight / 4;
            int bottom = Console.WindowHeight * 3 / 4;
            for (int i = tip; i <= tip + height; i++)
            {
                Console.SetCursorPosition(Console.WindowWidth / 4 + 2, i);
                Console.Write("|");
                Console.SetCursorPosition(Console.WindowWidth * 3 / 4 - 2, i);
                Console.Write("|");
            }
        }
        public void changeDirect(Direction newDirect)
        {
            this.direction = newDirect;
        }
        public void moveRocket()
        {
            if (direction == Direction.up)
                tip--;
            else if(direction == Direction.down)
                tip++;
            int top = Console.WindowHeight / 4;
            int bottom = Console.WindowHeight * 3 / 4;
            if (tip <= top)
                direction = Direction.down;
            else if (tip >= (bottom-height))
                direction = Direction.up;
        }
        

    }
    class ball
    {
        int x = Console.WindowWidth / 2, y = Console.WindowHeight / 2;
        Direction direction = Direction.left;
        public ball() { }
        public ball(int x, int y, Direction direction)
        {
            this.x = x;
            this.y = y;
            this.direction = direction;
        }
        public int X
        {
            get { return x; }
            set { x = value; }
        }
        public int Y
        {
            get { return y; }
            set { y = value; }
        }
        public Direction Direction { get; set; }
        public void changeDirect(Direction newDirect)
        {
            this.direction = newDirect;
        }

        public void moveBall()
        {
            if (direction == Direction.left)
                x--;
            else if (direction == Direction.right)
                x++;
        }
        public void printBall()
        {
            Console.SetCursorPosition(x,y);
            Console.Write('*');
        }
    }
}
