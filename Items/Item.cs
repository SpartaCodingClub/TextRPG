namespace TextRPG
{
    public enum ItemType
    {
        Weapon,
        Armor,
        Jewelry
    }

    public struct ItemStats
    {
        public ItemStats(ItemStats stats)
        {
            RequiredType = stats.RequiredType;

            IsEquipped = stats.IsEquipped;
            IsPurchased = stats.IsPurchased;

            Name = stats.Name;
            Description = stats.Description;

            ATK = stats.ATK;
            DEF = stats.DEF;
            MaxHP = stats.MaxHP;
            Gold = stats.Gold;
        }

        public static ItemStats operator +(ItemStats a, ItemStats b)
        {
            return new() { ATK = a.ATK + b.ATK, DEF = a.DEF + b.DEF, MaxHP = a.MaxHP + b.MaxHP };
        }

        public PlayerType RequiredType;

        public bool IsEquipped;
        public bool IsPurchased;

        public string Name;
        public string Description;

        public int ATK;
        public int DEF;
        public int MaxHP;
        public int Gold;
    }

    internal class Item
    {
        private static readonly int CURSOR_LEFT = 24;
        private static readonly int CURSOR_RIGHT = 40;

        public ItemType Type { get; set; }

        public Item() { }

        public Item(ItemType type, PlayerType requiredType, string name, string description, int gold, int atk = 0, int def = 0, int hp = 0)
        {
            Stats = new()
            {
                RequiredType = requiredType,
                Name = name,
                Description = description,
                ATK = atk,
                DEF = def,
                MaxHP = hp,
                Gold = gold
            };

            Type = type;
        }

        public ItemStats Stats { get; set; }

        private int cursorTop;
        private int offset;

        public bool Equipment()
        {
            Player player = Program.Player;
            if (Stats.RequiredType != player.Type && Stats.RequiredType != PlayerType.Common)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_REQUIRED);
                return false;
            }

            foreach (var item in player.Inventory)
            {
                if (item == this)
                {
                    continue;
                }

                if (item.Type != Type)
                {
                    continue;
                }

                if (item.Stats.IsEquipped)
                {
                    item.Equipment();
                }
            }

            ItemStats newStats = Stats;
            newStats.IsEquipped = !newStats.IsEquipped;
            Stats = newStats;

            return true;
        }

        public bool Purchase()
        {
            if (Stats.IsPurchased)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_PURCHASED);
                return false;
            }

            CreatureStats stats = Program.Player.Stats;
            if (Stats.Gold > stats.Gold)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_PURCHASE);
                return false;
            }

            List<Item> inventory = Program.Player.Inventory;
            if (inventory.Count == inventory.Capacity)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_CAPACITY);
                return false;
            }

            stats.Gold -= Stats.Gold;
            Program.Player.Stats = stats;

            ItemStats newItemStats = Stats;
            newItemStats.IsPurchased = !newItemStats.IsPurchased;
            Stats = newItemStats;

            Console.Write("구매를 완료했습니다! ");
            Utils.WriteColorLine($"-{Stats.Gold}G", ConsoleColor.DarkRed);
            Console.ReadKey(true);

            return true;
        }

        public void Sale()
        {
            int price = (int)(Stats.Gold * 0.85f);

            CreatureStats newStats = Program.Player.Stats;
            newStats.Gold += price;
            Program.Player.Stats = newStats;

            Console.Write("판매를 완료했습니다! ");
            Utils.WriteColorLine($"+{price}G", ConsoleColor.Yellow);
            Console.ReadKey(true);
        }

        public void ShowInfo(bool showEquip, bool showGold, int index = 0)
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

            if (showEquip && Stats.IsEquipped)
            {
                Utils.WriteColor("[E]", ConsoleColor.DarkGray);
            }

            Console.Write(Stats.Name);

            if (Stats.ATK > 0)
            {
                ShowStatsInfo("공격력", Stats.ATK);
            }

            if (Stats.DEF > 0)
            {
                ShowStatsInfo("방어력", Stats.DEF);
            }

            if (Stats.MaxHP > 0)
            {
                ShowStatsInfo("체  력", Stats.MaxHP);
            }

            Console.SetCursorPosition(CURSOR_RIGHT, cursorTop);
            Utils.WriteColor(" | ", ConsoleColor.DarkYellow);
            Console.Write(Stats.Description);

            if (showGold)
            {
                Utils.WriteColor(" | ", ConsoleColor.DarkYellow);

                if (Stats.IsPurchased)
                {
                    Utils.WriteColor($"구매완료", ConsoleColor.DarkGray);
                }
                else
                {
                    Utils.WriteColor($"{Stats.Gold:N0}G", ConsoleColor.Yellow);
                }
            }

            Console.SetCursorPosition(0, cursorTop + offset);
        }

        private void ShowStatsInfo(string label, int value)
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