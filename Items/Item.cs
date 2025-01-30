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
        public static ItemStats operator +(ItemStats a, ItemStats b)
        {
            return new() { ATK = a.ATK + b.ATK, DEF = a.DEF + b.DEF, MaxHP = a.MaxHP + b.MaxHP };
        }

        public int ATK;
        public int DEF;
        public int MaxHP;
    }

    public struct ItemStatus
    {
        public ItemType Type;
        public PlayerType RequiredType;

        public bool IsEquipped;
        public bool IsPurchased;

        public string Name;
        public string Description;

        public int Gold;
    }

    internal class Item
    {
        private static readonly int CURSOR_LEFT = 24;
        private static readonly int CURSOR_RIGHT = 40;

        public Item() { }

        public Item(ItemType type, PlayerType requiredType, string name, string description, int gold, int atk = 0, int def = 0, int hp = 0)
        {
            Stats = new()
            {
                ATK = atk,
                DEF = def,
                MaxHP = hp,
            };

            Status = new()
            {
                Type = type,
                RequiredType = requiredType,

                Name = name,
                Description = description,

                Gold = gold
            };
        }

        public ItemStats Stats { get; set; }
        public ItemStatus Status { get; set; }

        private int cursorTop;
        private int offset;

        public bool Equipment()
        {
            Player player = Program.Player;
            if (Status.RequiredType != player.Type && Status.RequiredType != PlayerType.Common)
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

                if (item.Status.Type != Status.Type)
                {
                    continue;
                }

                if (item.Status.IsEquipped)
                {
                    item.Equipment();
                }
            }

            ItemStatus newStatus = Status;
            newStatus.IsEquipped = !newStatus.IsEquipped;
            Status = newStatus;

            return true;
        }

        public bool Purchase()
        {
            if (Status.IsPurchased)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_PURCHASED);
                return false;
            }

            CreatureStats stats = Program.Player.Stats;
            if (Status.Gold > stats.Gold)
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

            stats.Gold -= Status.Gold;
            Program.Player.Stats = stats;

            Console.Write("구매를 완료했습니다! ");
            Utils.WriteColorLine($"-{Status.Gold}G", ConsoleColor.DarkRed);

            ItemStatus newStatus = Status;
            newStatus.IsPurchased = true;
            Status = newStatus;

            return true;
        }

        public void Sale()
        {
            int price = (int)(Status.Gold * 0.85f);

            CreatureStats newStats = Program.Player.Stats;
            newStats.Gold += price;
            Program.Player.Stats = newStats;

            foreach (var item in ItemManager.Instance.ItemList)
            {
                if (item.Status.Name != Status.Name)
                {
                    continue;
                }

                ItemStatus newStatus = item.Status;
                newStatus.IsPurchased = false;
                item.Status = newStatus;
            }

            Console.Write("판매를 완료했습니다! ");
            Utils.WriteColorLine($"+{price}G", ConsoleColor.Yellow);
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

            if (showEquip && Status.IsEquipped)
            {
                Utils.WriteColor("[E]", ConsoleColor.DarkGray);
            }

            Console.Write(Status.Name);

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
            Console.Write(Status.Description);

            if (showGold)
            {
                Utils.WriteColor(" | ", ConsoleColor.DarkYellow);

                if (Status.IsPurchased)
                {
                    Utils.WriteColor($"구매완료", ConsoleColor.DarkGray);
                }
                else
                {
                    Utils.WriteColor($"{Status.Gold:N0}G", ConsoleColor.Yellow);
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