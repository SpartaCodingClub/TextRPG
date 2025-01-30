namespace TextRPG
{
    internal class Field_Battle : Field_Base
    {
        private readonly string[] MESSAGES_EXPLORATION =
        {
            "낡은 지도를 살펴보는 중",
            "누군가의 시선을 느끼는 중",
            "던전을 탐색하는 중",
            "두근거리는 마음을 진정시키는 중",
            "뒤를 돌아보는 중",
            "문을 조심스럽게 여는 중",
            "미로 속을 헤매는 중",
            "발자국 소리가 늘어나는 중",
            "보물이 있을 것 같은 기분",
            "빛 한 줄기 따라가는 중",
            "숨겨진 문을 찾는 중",
            "숨죽이며 기다리는 중",
            "어둠 속을 헤쳐 나가는 중",
            "위험한 기운이 감도는 중",
            "이상한 소리가 들리는 중",
            "정처없이 걷는 중",
            "차가운 공기가 스며드는 중",
            "출구가 점점 멀어지는 중",
            "함정이 작동하는 소리가 들리는 중",
            "Chill 해지는 중"
        };

        private readonly string[] MESSAGES_BATTLE =
        {
            "검을 뽑아드는 중",
            "도망갈 기회를 보는 중",
            "반격할 기회를 노리는 중",
            "방패를 들어올리는 중",
            "승패가 갈리는 순간이 다가오는 중",
            "아드레날린이 솟구치는 중",
            "일격을 날리는 중",
            "적이 위협적인 기술을 준비하는 중",
            "적의 공격을 막아내는 중",
            "적의 약점을 파악하는 중",
            "적의 움직임을 읽는 중",
            "적의 빈틈을 노리는 중",
            "전장의 긴장감이 감도는 중",
            "전장의 먼지가 가라않는 중",
            "최후의 승자를 가리는 순간이 다가오는 중",
            "최후의 일격을 가하는 중",
            "최후의 한 방을 날리는 중",
            "치명적인 일격을 준비하는 중",
            "포션을 급히 마시는 중",
            "피할 수 없는 공격이 다가오는 중"
        };

        private readonly int CURSOR_HP_LEFT = 9;
        private readonly int CURSOR_HP_TOP = 9;

        private readonly int MIN_DAMAGE = 20;
        private readonly int MAX_DAMAGE = 35;

        private readonly double PERCENT_FAILURE = 0.4;

        public Field_Battle(DungeonType type)
        {
            title = Define.FIELD_DUNGEON_TITLE;

            dungeon = Define.DUNGEONS[(int)type];
            info = $" {dungeon.Name.Replace("\t", string.Empty)} | 방어력 {dungeon.DEF} 이상 권장";

            this.type = type;
        }

        private readonly DungeonType type;
        private readonly Dungeon dungeon;

        private readonly Player player = Program.Player;
        private readonly Random random = new();

        public override int Update()
        {
            // 플레이어 정보 출력
            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ShowPlayerInfo();

            // 던전 탐색
            do
            {
                Progress(MESSAGES_EXPLORATION);
            }
            while (random.Next(0, 2) == 0);

            Utils.WriteColorLine("\n [!] 몬스터 발견! 곧 전투가 시작됩니다.", ConsoleColor.Red);
            Thread.Sleep(2000);
            ConsoleClear();

            // 전투 시작
            do
            {
                Progress(MESSAGES_BATTLE);
            }
            while (random.Next(0, 2) == 0);

            // 전투 결과 (승리/패배)
            (int damage, int gold) = Result();
            Thread.Sleep(2000);
            ConsoleClear();

            // 피해량 계산
            Damage(damage);
            Thread.Sleep(2000);

            // 플레이어가 사망했다면
            if (player.Stats.HP <= 0)
            {
                Utils.WriteColorLine(" [!] GAME OVER! 게임이 종료되었습니다.", ConsoleColor.Red);
                return -1;
            }

            // 전투에서 승리했다면 (보상이 있다면)
            if (gold > 0)
            {
                Program.CurrentField = new Field_Reward(type, gold);
                return 0;
            }

            // 던전 로비로 돌아가기
            Program.CurrentField = new Field_Dungeon();
            return 0;
        }

        private void ConsoleClear()
        {
            Console.Clear();
            Start();

            Console.SetCursorPosition(0, Console.CursorTop - 1);
            ShowPlayerInfo();
        }

        private void Progress(string[] messages)
        {
            Utils.WriteColor(Define.FIELD_CURSOR, ConsoleColor.DarkYellow);

            int index = random.Next(0, messages.Length);
            Console.Write(messages[index]);

            Thread.Sleep(200);

            for (int i = 0; i < 3; i++)
            {
                Console.Write(".");
                Thread.Sleep(200);
            }

            Console.WriteLine();
        }

        private (int, int) Result()
        {
            CreatureStats stats = player.Stats;
            if (stats.DEF < dungeon.DEF)
            {
                if (random.NextDouble() <= PERCENT_FAILURE)
                {
                    Utils.WriteColorLine("\n [!] 전투에서 패배했습니다! ", ConsoleColor.Red);
                    return ((int)((player.Stats.MaxHP + player.ItemStats.MaxHP) * 0.5f), 0);
                }
            }

            Utils.WriteColorLine("\n [!] 전투에서 승리했습니다! ", ConsoleColor.DarkGreen);
            int def = dungeon.DEF - (stats.DEF + player.ItemStats.DEF);
            int minDamage = Math.Max(MIN_DAMAGE + def, 1);
            int maxDamage = Math.Max(MAX_DAMAGE + def, 1) + 1;
            int damage = random.Next(minDamage, maxDamage);

            int atk = stats.ATK + player.ItemStats.ATK;
            int minGold = (int)(dungeon.Gold * atk * 0.01f);
            int maxGold = minGold * 2 + 1;
            int gold = random.Next(minGold, maxGold);

            return (damage, dungeon.Gold + gold);
        }

        private void Damage(int damage)
        {
            Utils.WriteColorLine($" [!] {damage}의 피해를 받았습니다.", ConsoleColor.Red);

            CreatureStats stats = player.Stats;
            int currentHP = stats.HP;
            (int left, int top) = Console.GetCursorPosition();

            while (stats.HP > 0 && damage > 0)
            {
                stats.HP--;
                damage--;

                Utils.ClearLine(CURSOR_HP_LEFT, CURSOR_HP_TOP);
                Console.SetCursorPosition(CURSOR_HP_LEFT, CURSOR_HP_TOP);
                Utils.WriteColor(currentHP, ConsoleColor.White);
                Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);
                Utils.WriteColor(stats.HP, ConsoleColor.DarkRed);
                Thread.Sleep(20);
            }

            Program.Player.Stats = stats;
            Console.SetCursorPosition(left, top);
        }
    }
}