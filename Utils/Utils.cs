namespace TextRPG
{
    internal class Utils
    {
        public static void ClearBuffer()
        {
            Thread.Sleep(100);
            while (Console.KeyAvailable) Console.ReadKey(true);
        }
        
        public static void ClearLine(int left, int top)
        {
            int currentTop = Console.CursorTop;
            Console.SetCursorPosition(left, top);
            Console.Write(new string(' ', Console.WindowWidth - left));
            Console.SetCursorPosition(0, currentTop);
        }

        public static void WriteColor(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.Write(value);
            Console.ResetColor();
        }

        public static void WriteColorLine(object value, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(value);
            Console.ResetColor();
        }
    }
}