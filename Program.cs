namespace TextRPG
{
    internal class Program
    {
        public static Field_Base CurrentField = new Field_Title();
        public static Player Player = new();

        public static string Nickname = string.Empty;

        static void Main()
        {
            Console.CursorVisible = false;

            while (true)
            {
                CurrentField.Start();
                if (CurrentField.Update() < 0)
                {
                    break;
                }
            }
        }
    }
}