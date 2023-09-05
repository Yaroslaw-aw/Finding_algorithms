namespace Finding
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "СПАСИБО, ДЕНИС САПРЫКИН!";
            Paint paint = new Paint(40, 20);
            paint.PutRandomNumbers(300);
            Thread.Sleep(500);

            paint.FillFor(20, 10);
            Console.Clear();

            paint.ClearField();
            paint.PutRandomNumbers(300);
            Thread.Sleep(500);

            paint.FillQueue(20, 10);
            Console.Clear();

            paint.ClearField();
            paint.PutRandomNumbers(300);
            Thread.Sleep(500);

            paint.FillStack(20, 10);
            Console.Clear();

            paint.ClearField();
            paint.PutRandomNumbers(300);
            Thread.Sleep(500);

            paint.FillDepth(20, 10);
            Console.ReadKey();            
        }
    }
}