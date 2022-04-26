using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prog_2_cesta_krala
{
    struct Point2d  
    {   
        public int x;
        public int y;

        public Point2d(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class Program
    {
        static int bfs(Board board, Queue queue, Point2d end)
        {
            Point2d current_position;
            while (! queue.is_empty())
            {
                current_position = queue.dequeue();

                for (int i = current_position.x - 1; i <= current_position.x + 1; i++)
                {
                    for (int j = current_position.y -1; j <= current_position.y + 1; j++)
                    {
                        if (board.matrix[i,j] == -1)
                        {
                            board.matrix[i, j] = board.matrix[current_position.x, current_position.y] + 1;
                            queue.enqueue(new Point2d(i, j));
                        }
                        if ((i == end.x) && (j == end.y))
                            return board.matrix[current_position.x, current_position.y] + 1;
                    }
                }
            }
            return -1;
        }
        static void Main(string[] args)
        {
            Board board = new Board(10, 10);
            Input.input_obbs(board);
            Point2d start = Input.input_start_end();
            Point2d end = Input.input_start_end();
            board.matrix[start.x, start.y] = 0;
            Queue queue = new Queue(64);
            queue.enqueue(start);
            int result = bfs(board, queue, end);
            Console.WriteLine(result);
        }
    }
    class Board
    {
        public int[,] matrix;
        public Board(int width, int height)
        {
            this.matrix = new int[width, height];
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if ((j == 0) || (j == height - 1) || (i == 0) || (i == width - 1))
                        matrix[i, j] = -2;
                    else
                        matrix[i, j] = -1;
                }
            }
        }
    }
    class Queue
    {
        Point2d[] array;
        int array_lenght;
        int phead = 0;
        int ptail = 0;
        public Queue(int n)
        {
            this.array = new Point2d[n];
            this.array_lenght = n;
        }

        public void enqueue(Point2d position)
        {
                array[phead] = position;
                phead = (phead + 1) % array_lenght;
        }
        public Point2d dequeue()
        {
                Point2d position = array[ptail];
                ptail = (ptail + 1) % array_lenght;
                return position;
        }
        public bool is_empty()
        {
            if (ptail == phead)
                return true;
            else
                return false;
        }
    }
    class Input
    {
        static public void input_obbs(Board board)
        {
            int x;
            int y;
            int num_obbs = Citacka.read_int();
            for (int i = 0; i < num_obbs; i++)
            {
                x = Citacka.read_int();
                y = Citacka.read_int();
                board.matrix[x, y] = -2;
            }
    
        }
        static public Point2d input_start_end()
        {
            int x = Citacka.read_int();
            int y = Citacka.read_int();
            Point2d position = new Point2d(x, y);
            return position;
        }
    }
    class Citacka
    {
        static public int read_int()
        {
            int number = Console.Read();
            bool negative = false;
            while ((number < '0') || (number > '9'))
            {
                negative = false;
                if (number == '-')
                    negative = true;
                number = Console.Read();
            }
            int result = 0;
            while ((number >= '0') && (number <= '9'))
            {
                result = (result * 10) + (number - '0');
                number = Console.Read();
            }
            if (negative)
                return -result;
            return result;
        }
    }
}
