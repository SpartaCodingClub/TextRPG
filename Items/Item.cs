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
        private static readonly int CURSOR_LEFT = 99;
        private static readonly int CURSOR_STATS_LEFT = 24;
        private static readonly int CURSOR_STATS_RIGHT = 40;

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

            // 착용 불가 아이템 검사(공용 아이템이 아니고, 자신의 직업 아이템도 아닐 때)
            if (Status.RequiredType != PlayerType.Common && Status.RequiredType != player.Type)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_REQUIRED);
                return false;
            }

            // 장착하려는 아이템과 동일 부위의 장착 아이템이 있다면, 장착 해제
            foreach (var item in player.Inventory)
            {
                // 자기 자신이라면 검사하지 않음
                if (item == this)
                {
                    continue;
                }

                // 동일 부위 아이템이 아니라면 검사하지 않음
                if (item.Status.Type != Status.Type)
                {
                    continue;
                }

                // 장착한 아이템이 있다면, 장착 해제
                if (item.Status.IsEquipped)
                {
                    item.Equipment();
                }
            }

            // 아이템 장착
            ItemStatus newStatus = Status;
            newStatus.IsEquipped = !newStatus.IsEquipped;
            Status = newStatus;

            return true;
        }

        public bool Purchase()
        {
            // 이미 구매한 아이템인지 검사
            if (Status.IsPurchased)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_PURCHASED);
                return false;
            }

            // 보유한 골드가 충분한지 검사
            CreatureStats stats = Program.Player.Stats;
            if (Status.Gold > stats.Gold)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_PURCHASE);
                return false;
            }

            // 아이템 구매
            ItemStatus newStatus = Status;
            newStatus.IsPurchased = true;
            Status = newStatus;

            // 플레이어 골드 감소
            stats.Gold -= Status.Gold;
            Program.Player.Stats = stats;

            // 콘솔 출력
            Console.Write("구매를 완료했습니다! ");
            Utils.WriteColorLine($"-{Status.Gold:N0}G", ConsoleColor.DarkRed);
            (int left, int top) = Console.GetCursorPosition();

            // 아이템 콘솔 상태 변경
            Console.SetCursorPosition(CURSOR_LEFT, cursorTop);
            Utils.WriteColor("구매완료", ConsoleColor.DarkGray);
            Console.SetCursorPosition(left, top);

            return true;
        }

        public bool Sale()
        {
            foreach (var item in ItemManager.Instance.ItemList)
            {
                // 판매하려는 아이템과 일치하는 상점 아이템 검사
                if (item.Status.Name != Status.Name)
                {
                    continue;
                }

                // 이미 판매된 아이템이라면, 판매 불가
                if (item.Status.IsPurchased == false)
                {
                    Console.WriteLine(Define.ERROR_MESSAGE_SOLD);
                    return false;
                }

                // 아이템 판매
                ItemStatus newStatus = item.Status;
                newStatus.IsPurchased = false;
                item.Status = newStatus;
            }

            // 플레이어 골드 증가
            CreatureStats newStats = Program.Player.Stats;
            int price = (int)(Status.Gold * 0.85f);
            newStats.Gold += price;
            Program.Player.Stats = newStats;

            // 콘솔 출력
            Console.Write("판매를 완료했습니다! ");
            Utils.WriteColorLine($"+{price:N0}G", ConsoleColor.Yellow);
            (int left, int top) = Console.GetCursorPosition();

            // 아이템 콘솔 상태 변경
            Console.SetCursorPosition(CURSOR_LEFT, cursorTop);
            Utils.WriteColor("판매완료", ConsoleColor.DarkGray);
            Console.SetCursorPosition(left, top);

            return true;
        }

        public void ShowInfo(bool showEquip, bool showGold, int index = 0)
        {
            // 해당 아이템의 콘솔 위치 저장
            cursorTop = Console.CursorTop;
            offset = 0;

            // 콘솔 출력 - 인덱스 번호
            if (index > 0)
            {
                Utils.WriteColor($" [{index}] ", ConsoleColor.DarkYellow);
            }
            else
            {
                Utils.WriteColor("  -  ", ConsoleColor.DarkYellow);
            }

            // 콘솔 출력 - 장착 여부
            if (showEquip && Status.IsEquipped)
            {
                Utils.WriteColor("[E]", ConsoleColor.DarkGray);
            }

            // 콘솔 출력 - 아이템 이름
            Console.Write(Status.Name);

            // 콘솔 출력 - 아이템 스탯
            if (Stats.ATK > 0) ShowStatsInfo("공격력", Stats.ATK);
            if (Stats.DEF > 0) ShowStatsInfo("방어력", Stats.DEF);
            if (Stats.MaxHP > 0) ShowStatsInfo("체  력", Stats.MaxHP);

            // 콘솔 출력 - 전용 아이템 여부
            Console.SetCursorPosition(CURSOR_STATS_RIGHT, cursorTop);
            Utils.WriteColor(" | ", ConsoleColor.DarkYellow);
            if (Status.RequiredType != PlayerType.Common)
            {
                Utils.WriteColor($"[{Define.CLASS[(int)Status.RequiredType]} 전용] ", ConsoleColor.Blue);
            }

            // 콘솔 출력 - 아이템 설명
            Console.Write(Status.Description);

            // 콘솔 출력 - 아이템 가격
            if (showGold)
            {
                Utils.WriteColor(" | ", ConsoleColor.DarkYellow);

                if (Status.IsPurchased)
                {
                    Utils.WriteColor("구매완료", ConsoleColor.DarkGray);
                }
                else
                {
                    Utils.WriteColor($"{Status.Gold:N0}G", ConsoleColor.Yellow);
                }
            }

            // 콘솔 커서 다음 줄로 이동
            Console.SetCursorPosition(0, cursorTop + offset);
        }

        private void ShowStatsInfo(string label, int value)
        {
            Console.SetCursorPosition(CURSOR_STATS_LEFT, cursorTop + offset);
            Utils.WriteColor(" | ", ConsoleColor.DarkYellow);
            Console.Write($"{label} ");
            Utils.WriteColor($"+{value}\t", ConsoleColor.DarkGreen);

            offset++;
        }
    }
}