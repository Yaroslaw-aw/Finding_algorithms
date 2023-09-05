using System.Diagnostics.SymbolStore;
using System.Runtime.ExceptionServices;

namespace Finding
{

    struct Coord
    {
        public int x; public int y;
        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    internal class Paint
    {
        int[,] map;
        int w, h;
        string symbols = " #+ox";
        public ConsoleColor[] colors =
        {
            ConsoleColor.White,
            ConsoleColor.DarkBlue,
            ConsoleColor.Yellow,
            ConsoleColor.Green,
            ConsoleColor.Cyan
        };

        public Paint(int w, int h)
        {           
            this.w = w;
            this.h = h;
            this.map = new int[w, h];
        }

        void SetMap(int x, int y, int number)
        {
            if (x < 0 || x >= w) return;
            if (y < 0 || y >= h) return;
            map[x, y] = number;
            PrintAt(x, y);
        }

        void PrintAt(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = colors[map[x, y]];
            Console.Write(symbols[map[x, y]]);
            Console.SetCursorPosition(0, 0);
        }

        public void PutRandomNumbers(int count)
        {
            //Console.SetWindowSize(150 , 50 );
            Random random = new Random();
            for (int j = 0; j < count; j++)
                SetMap(random.Next(w), random.Next(h), 1);
        }
        
        public void FillFor(int px, int py)
        {
            SetMap(px, py, 2);
            while (true)
            {
                bool stop = true;
                Thread.Sleep(100);
                for (int x = 0; x < w; x++)
                    for (int y = 0; y < h; y++)
                        if (map[x, y] == 2)
                        {
                            SetMap(x, y, 4);
                            stop = false;
                            if (isEmpty(x - 1, y)) SetMap(x - 1, y, 3);
                            if (isEmpty(x + 1, y)) SetMap(x + 1, y, 3);
                            if (isEmpty(x, y - 1)) SetMap(x, y - 1, 3);
                            if (isEmpty(x, y + 1)) SetMap(x, y + 1, 3);
                        }
                if (stop) break;    
                for (int x = 0; x < w; x++)
                    for (int y = 0; y < h; y++)
                        if (map[x, y] == 3)
                            SetMap(x, y, 2);
            }
        }

        public void FillQueue(int px, int py)
        {
            SetMap(px, py, 2);
            Queue<Coord> queue = new Queue<Coord>();
            queue.Enqueue(new Coord(px, py));
            while (queue.Count > 0)
            {
                Coord coord = queue.Dequeue();
                int x = coord.x;
                int y = coord.y;
                SetMap(x, y, 4);
                if (isEmpty(x - 1, y)) { queue.Enqueue(new Coord(x - 1, y)); SetMap(x - 1, y, 2); }
                if (isEmpty(x + 1, y)) { queue.Enqueue(new Coord(x + 1, y)); SetMap(x + 1, y, 2); }
                if (isEmpty(x, y + 1)) { queue.Enqueue(new Coord(x, y + 1)); SetMap(x, y + 1, 2); }
                if (isEmpty(x, y - 1)) { queue.Enqueue(new Coord(x, y - 1)); SetMap(x, y - 1, 2); }
                Thread.Sleep(10);
            };                     
        }

        public void FillStack(int px, int py)
        {
            SetMap(px, py, 2);
            Stack<Coord> stack = new Stack<Coord>();
            stack.Push(new Coord(px, py));
            while (stack.Count > 0)
            {
                Coord coord = stack.Pop();
                int x = coord.x;
                int y = coord.y;
                SetMap(x, y, 4);
                if (isEmpty(x - 1, y)) { stack.Push(new Coord(x - 1, y)); SetMap(x - 1, y, 2); }
                if (isEmpty(x + 1, y)) { stack.Push(new Coord(x + 1, y)); SetMap(x + 1, y, 2); }
                if (isEmpty(x, y + 1)) { stack.Push(new Coord(x, y + 1)); SetMap(x, y + 1, 2); }
                if (isEmpty(x, y - 1)) { stack.Push(new Coord(x, y - 1)); SetMap(x, y - 1, 2); }
                Thread.Sleep(10);
            };
        }

        public void FillDepth(int x, int y)
        {
            Thread.Sleep(1);
            if (!isEmpty(x, y)) return;
            SetMap(x, y, 2);
            FillDepth(x - 1, y);
            FillDepth(x + 1, y);
            FillDepth(x, y - 1);
            FillDepth(x, y + 1);
            SetMap(x, y, 4);
            
        }

        public void ClearField()
        {
            for (int i = 0; i < w; i++)
                for (int j = 0; j < h; j++)
                    SetMap(i, j, 0);
        }

        bool isEmpty(int x, int y)
        {
            if (x < 0 || x >= w) return false;
            if (y < 0 || y >= h) return false;
            return map[x, y] == 0;

        }
    }
}
