namespace TextRPG
{
    internal abstract class Field_Base
    {
        protected string title = string.Empty;
        protected string info = string.Empty;

        protected readonly List<string> menu = new();

        public virtual void Start()
        {
            Console.Clear();
            Utils.ClearBuffer();
            Utils.WriteColorLine($"\n{title}", ConsoleColor.Red);

            ShowInfo();

            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine($" [{i + 1}] {menu[i]}");
            }

            Console.WriteLine();
        }

        public virtual int Update(bool hasZero = false)
        {
            while (true)
            {
                Utils.WriteColor(Define.FIELD_CURSOR, ConsoleColor.DarkYellow);

                // 정수 검사
                char input = Console.ReadKey(true).KeyChar;
                if (char.IsDigit(input) == false)
                {
                    Console.WriteLine(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                // 인덱스 검사
                int index = input - '0';
                if (index > menu.Count)
                {
                    Console.WriteLine(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                // 0 허용 검사
                if (!hasZero && index == 0)
                {
                    Console.WriteLine(Define.ERROR_MESSAGE_INPUT);
                    continue;
                }

                return index;
            }
        }

        protected virtual void ShowInfo()
        {
            Console.WriteLine($"{info}\n");
        }

        protected static void ShowPlayerInfo()
        {
            Player player = Program.Player;
            CreatureStats stats = player.Stats;
            ItemStats itemStats = player.ItemStats;

            Utils.WriteColor($" Lv.{stats.LV:00}", ConsoleColor.White);
            Console.WriteLine($" ({stats.EXP}/{stats.MaxEXP})");

            Utils.WriteColor($" {stats.Name}", ConsoleColor.DarkYellow);
            Console.WriteLine($" ({Define.CLASS[(int)player.Type]})\n");

            ShowStatsInfo(" 공격력: ", stats.ATK + itemStats.ATK, itemStats.ATK);
            Console.WriteLine();

            ShowStatsInfo(" 방어력: ", stats.DEF + itemStats.DEF, itemStats.DEF);
            Console.WriteLine();

            ShowStatsInfo(" 체  력: ", stats.HP);
            ShowStatsInfo("/", stats.MaxHP + itemStats.MaxHP, itemStats.MaxHP);
            Console.WriteLine();

            Console.Write(" 보유금: ");
            Utils.WriteColorLine($"{stats.Gold:N0}G\n", ConsoleColor.Yellow);
        }

        protected static void ShowStatsInfo(string label, int value, int itemValue = 0)
        {
            Console.Write(label);
            Utils.WriteColor(value, ConsoleColor.White);
            if (itemValue != 0)
            {
                Utils.WriteColor($" (+{itemValue})", ConsoleColor.DarkGreen);
            }
        }

        protected static void ShowGoldInfo()
        {
            Console.WriteLine("\n [보유 골드]");
            Utils.WriteColorLine($" {Program.Player.Stats.Gold:N0}G\n", ConsoleColor.Yellow);
        }
    }
}