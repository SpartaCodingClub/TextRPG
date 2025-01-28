namespace TextRPG
{
    public struct ItemStats
    {
        public bool IsEquipment;

        public string Name;
        public string Description;

        public int ATK;
        public int DEF;
        public int HP;
        public int Gold;

        public static ItemStats operator +(ItemStats a, ItemStats b)
        {
            return new() { ATK = a.ATK + b.ATK, DEF = a.DEF + b.DEF, HP = a.HP + b.HP };
        }
    }

    internal class Item
    {
        private static readonly int CURSOR_LEFT = 24;
        private static readonly int CURSOR_RIGHT = 40;

        public Item(string name, string description, int gold, int atk = 0, int def = 0, int hp = 0)
        {
            Stats = new()
            {
                Name = name,
                Description = description,
                ATK = atk,
                DEF = def,
                HP = hp,
                Gold = gold
            };
        }

        public ItemStats Stats { get; private set; }

        private int cursorTop;
        private int offset;

        public void Equipment()
        {
            // TODO: 여기부터
        }

        public void ShowInfo(int index = 0, bool showGold = false)
        {
            cursorTop = int.MaxValue;
            offset = 0;

            if (index > 0)
            {
                Utils.WriteColor($" [{index}] ", ConsoleColor.DarkYellow);
            }
            else
            {
                Utils.WriteColor("  -  ", ConsoleColor.DarkYellow);
            }

            if (Stats.IsEquipment)
            {
                Console.Write("[E] ");
            }

            Console.Write(Stats.Name);

            if (Stats.ATK > 0)
            {
                ShowStats("공격력", Stats.ATK);
            }

            if (Stats.DEF > 0)
            {
                ShowStats("방어력", Stats.DEF);
            }

            if (Stats.HP > 0)
            {
                ShowStats("체  력", Stats.DEF);
            }

            Console.SetCursorPosition(CURSOR_RIGHT, cursorTop);
            Utils.WriteColor(" | ", ConsoleColor.DarkYellow);
            Console.Write(Stats.Description);

            if (showGold)
            {
                Utils.WriteColor(" | ", ConsoleColor.DarkYellow);
                Utils.WriteColor($"{Stats.Gold}G", ConsoleColor.Yellow);
            }

            Console.SetCursorPosition(0, cursorTop + offset);
        }

        private void ShowStats(string label, int value)
        {
            cursorTop = Math.Min(Console.CursorTop, cursorTop);

            Console.SetCursorPosition(CURSOR_LEFT, cursorTop + offset);
            Utils.WriteColor(" | ", ConsoleColor.DarkYellow);
            Console.Write($"{label} ");
            Utils.WriteColor($"+{value}\t", ConsoleColor.DarkGreen);

            offset++;
        }
    }
}