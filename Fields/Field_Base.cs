namespace TextRPG
{
    internal abstract class Field_Base
    {
        protected string title = string.Empty;
        protected string info = string.Empty;

        protected readonly List<string> menu = new();

        public void Start()
        {
            Console.Clear();
            Utils.WriteColorLine($"\n{title}", ConsoleColor.Red);

            ShowInfo();

            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($" [{i + 1}] {menu[i]}");
            }

            Console.WriteLine();
        }

        public virtual int Update()
        {
            while (true)
            {
                Utils.WriteColor(Define.FIELD_CURSOR, ConsoleColor.DarkYellow);

                // 정수 검사
                char input = Console.ReadKey(true).KeyChar;
                if (char.IsDigit(input) == false)
                {
                    Console.WriteLine(Define.ERROR_INPUT_MESSAGE);
                    continue;
                }

                // 인덱스 검사
                int index = input - '0';
                if (index > menu.Count)
                {
                    Console.WriteLine(Define.ERROR_INPUT_MESSAGE);
                    continue;
                }

                return index;
            }
        }

        protected virtual void ShowInfo()
        {
            Console.WriteLine($"{info}\n");
        }
    }
}