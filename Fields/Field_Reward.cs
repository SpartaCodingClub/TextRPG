namespace TextRPG
{
    internal class Field_Reward : Field_Base
    {
        private readonly int CURSOR_LV_LEFT = 4;
        private readonly int CURSOR_EXP_LEFT = 8;
        private readonly int CURSOR_LV_TOP = 5;

        private readonly int CURSOR_ATK_LEFT = 9;
        private readonly int CURSOR_ATK_TOP = 8;

        private readonly int CURSOR_DEF_LEFT = 9;
        private readonly int CURSOR_DEF_TOP = 9;

        private readonly int CURSOR_HP_TOP = 10;

        private readonly int CURSOR_GOLD_LEFT = 9;
        private readonly int CURSOR_GOLD_TOP = 11;

        public Field_Reward(DungeonType type, int gold)
        {
            title = Define.FIELD_DUNGEON_TITLE;

            dungeon = Define.DUNGEONS[(int)type];
            info = $" 축하합니다!!\n {dungeon.Name.Replace("\t", string.Empty)}을 클리어 하였습니다.";

            CreatureStats stats = player.Stats;
            currentLV = stats.LV;
            currentATK = stats.ATK;
            currentDEF = stats.DEF;

            this.gold = gold;
        }

        private readonly Dungeon dungeon;

        private readonly Player player = Program.Player;

        private readonly int currentLV;
        private readonly int currentATK;
        private readonly int currentDEF;
        private readonly int gold;

        public override int Update()
        {
            // 플레이어 정보 출력
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ShowPlayerInfo();

            // 리워드 (경험치, 골드 증가)
            Reward(gold);
            Thread.Sleep(2000);

            // 레벨이 올랐다면
            while (player.Stats.EXP >= player.Stats.MaxEXP && player.Stats.MaxEXP > 0)
            {
                LevelUp();
            }

            if (player.Stats.LV > currentLV)
            {
                Utils.WriteColorLine(" [!] LEVEL UP! 레벨이 증가했습니다.", ConsoleColor.Green);
                Thread.Sleep(2000);
            }

            menu.Add(string.Empty);
            Console.WriteLine("\n [1] 나가기\n");
            if (base.Update() == 1)
            {
                Program.CurrentField = new Field_Dungeon();
            }

            return 0;
        }

        private void Reward(int gold)
        {
            Utils.WriteColor(" [!] ", ConsoleColor.Green);

            CreatureStats stats = player.Stats;
            bool isMaxLevel = stats.MaxEXP == 0;

            if (!isMaxLevel)
            {
                Utils.WriteColor($"{dungeon.EXP}의 경험치와 ", ConsoleColor.Green);
            }

            Utils.WriteColorLine($"{gold:N0}G의 골드를 획득했습니다.", ConsoleColor.Green);
            (int left, int top) = Console.GetCursorPosition();

            // 경험치 증가
            if (!isMaxLevel)
            {
                Utils.ClearLine(CURSOR_EXP_LEFT, CURSOR_LV_TOP);
                Console.SetCursorPosition(CURSOR_EXP_LEFT, CURSOR_LV_TOP);
                Console.Write(stats.EXP);
                Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);

                stats.EXP += dungeon.EXP;
                Utils.WriteColor(stats.EXP, ConsoleColor.Green);
                Console.Write(")");
                Thread.Sleep(500);
            }

            // 골드 증가
            int currentGold = stats.Gold;
            while (gold > 0)
            {
                if (gold > 20)
                {
                    stats.Gold += 20;
                    gold -= 20;
                }
                else if (gold > 10)
                {
                    stats.Gold += 10;
                    gold -= 10;
                }
                else
                {
                    stats.Gold++;
                    gold--;
                }

                Utils.ClearLine(CURSOR_GOLD_LEFT, CURSOR_GOLD_TOP);
                Console.SetCursorPosition(CURSOR_GOLD_LEFT, CURSOR_GOLD_TOP);
                Utils.WriteColor($"{currentGold:N0}G", ConsoleColor.Yellow);
                Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);
                Utils.WriteColor($"{stats.Gold:N0}G", ConsoleColor.Green);
                Thread.Sleep(20);
            }

            Program.Player.Stats = stats;
            Console.SetCursorPosition(left, top);
        }

        private void LevelUp()
        {
            CreatureStats stats = player.Stats;
            (int left, int top) = Console.GetCursorPosition();

            // 체력 회복
            int maxHP = stats.MaxHP + player.ItemStats.MaxHP;
            if (stats.HP != maxHP)
            {
                stats.HP = maxHP;
                Utils.ClearLine(0, CURSOR_HP_TOP);
                Console.SetCursorPosition(0, CURSOR_HP_TOP);
                ShowStatsInfo(" 체  력: ", stats.HP);

                ItemStats itemStats = player.ItemStats;
                ShowStatsInfo("/", stats.MaxHP + itemStats.MaxHP, itemStats.MaxHP);
            }

            // 레벨 증가
            Utils.ClearLine(CURSOR_LV_LEFT, CURSOR_LV_TOP);
            Console.SetCursorPosition(CURSOR_LV_LEFT, CURSOR_LV_TOP);
            Console.Write(currentLV);
            Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);
            Utils.WriteColor(++stats.LV, ConsoleColor.Green);

            // 공격력 0.5 증가
            if ((stats.LV - 1) % 2 == 0) // 레벨이 2 증가했다면
            {
                // 공격력이 1 증가
                Utils.ClearLine(CURSOR_ATK_LEFT, CURSOR_ATK_TOP);
                Console.SetCursorPosition(CURSOR_ATK_LEFT, CURSOR_ATK_TOP);
                Utils.WriteColor(currentATK, ConsoleColor.White);
                Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);
                Utils.WriteColor(++stats.ATK, ConsoleColor.Green);
            }

            // 방어력 1 증가
            Utils.ClearLine(CURSOR_DEF_LEFT, CURSOR_DEF_TOP);
            Console.SetCursorPosition(CURSOR_DEF_LEFT, CURSOR_DEF_TOP);
            Utils.WriteColor(currentDEF, ConsoleColor.White);
            Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);
            Utils.WriteColor(++stats.DEF, ConsoleColor.Green);

            // 경험치 테이블
            if (stats.LV == Define.MAX_EXP.Length + 1)
            {
                stats.EXP = 0;
                stats.MaxEXP = 0;
            }
            else
            {
                stats.EXP -= stats.MaxEXP;
                stats.MaxEXP = Define.MAX_EXP[stats.LV - 1];
            }

            player.Stats = stats;
            Console.SetCursorPosition(left, top);
        }
    }
}