namespace TextRPG
{
    internal class Utils
    {
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