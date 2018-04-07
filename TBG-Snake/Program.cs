using System;
using System.Threading;

namespace TBG_Snake
{
    class Program
    {
        static Thread InputThread = new Thread(Input);
        static Snake Snake = new Snake();
        static Food food = new Food();
        static int width = 20;
        static int height = 20;
        static int xBorder = width + 1;
        static int score = 0;
        static bool gameOver = false;
        static Random rand = new Random();


        static void Setup()
        {
            InputThread.Start();
            Snake.x = rand.Next(1, width);
            Snake.y = rand.Next(1, height);

            food.x = rand.Next(1, width);
            food.y = rand.Next(1, height);
        }

        static void Draw()
        {
            Console.Clear();
            Console.Title = $"Score: {score} | {Snake.dir} | {Snake.x % width} | {Snake.y}";
            Console.Write("  ");
            for (int i = 0; i < xBorder; i++) {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("█");
                Console.ResetColor();
            }
            Console.WriteLine();


            for (int i = 0; i < height; i++) {
                Console.Write("  ");
                for (int j = 0; j < xBorder; j++) {
                    if (j == 0 || j == xBorder-1) {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.Write("█");
                        Console.ResetColor();
                    } else if (Snake.x == j && Snake.y == i) {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("█");
                        Console.ResetColor();
                    } else if (food.x == j && food.y == i) {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write("█");
                        Console.ResetColor();
                    } else {
                        bool tailed = false;
                        foreach(var tail in Snake.tail) {
                            if (tail.x == j && tail.y == i) {
                                Console.ForegroundColor = ConsoleColor.DarkGray;
                                Console.Write("█");
                                Console.ResetColor();
                                tailed = true;
                            } 
                        }
                        if (!tailed) Console.Write(" ");
                    }
                }
                Console.WriteLine();
            }

            Console.Write("  ");
            for (int i = 0; i < xBorder; i++) {
                Console.ForegroundColor = ConsoleColor.DarkCyan;
                Console.Write("█");
                Console.ResetColor();
            }
            Console.WriteLine();
        }

        static void Input()
        {
            var keyInfo = new ConsoleKeyInfo();
            do {
                keyInfo = Console.ReadKey(true);
                switch (keyInfo.Key) {
                    case (ConsoleKey.Z):
                        if (Snake.dir != Direction.DOWN) Snake.dir = Direction.UP;
                        break;
                    case (ConsoleKey.Q):
                        if (Snake.dir != Direction.RIGHT) Snake.dir = Direction.LEFT;
                        break;
                    case (ConsoleKey.S):
                        if (Snake.dir != Direction.UP) Snake.dir = Direction.DOWN;
                        break;
                    case (ConsoleKey.D):
                        if (Snake.dir != Direction.LEFT) Snake.dir = Direction.RIGHT;
                        break;
                }
            } while (keyInfo.Key != ConsoleKey.Escape);
        }

        static void Logic()
        {

            int prevX = Snake.tail[0].x;
            int prevY = Snake.tail[0].y;
            int prev2X, prev2Y;
            Snake.tail[0].x = Snake.x;
            Snake.tail[0].y = Snake.y;
            for (int i = 1; i < Snake.tailNum; i++) {
                prev2X = Snake.tail[i].x;
                prev2Y = Snake.tail[i].y;
                Snake.tail[i].x = prevX;
                Snake.tail[i].y = prevY;
                prevX = prev2X;
                prevY = prev2Y;
            }


            //Snake.x++;
            switch (Snake.dir) {
                case (Direction.UP):
                    Snake.y--;
                    break;
                case (Direction.LEFT):
                    Snake.x--;
                    break;
                case (Direction.DOWN):
                    Snake.y++;
                    break;
                case (Direction.RIGHT):
                    Snake.x++;
                    break;
            }

            if (Snake.x > width - 1) Snake.x = 1;
            if (Snake.y > height) Snake.y = 0;
            if (Snake.x < 1) Snake.x = width - 1;
            if (Snake.y < 0) Snake.y = height;


            if (Snake.x == food.x && Snake.y == food.y) {
                score += 10;
                food.x = rand.Next(1, width);
                food.y = rand.Next(1, height);
                Snake.tailNum++;
            }
        }


        static void Main(string[] args)
        {
            Setup();
            while(!gameOver) {
                Draw();
                Logic();
                Thread.Sleep(100);
            }

            Console.WriteLine($"Game over. Final score: {score}");
            //Console.ReadKey();
        }
    }
}
