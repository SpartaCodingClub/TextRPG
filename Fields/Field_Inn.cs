namespace TextRPG
{
    internal class Field_Inn : Field_Base
    {
        private readonly int PRICE = 500;
        private readonly int CURSOR_LEFT = 9;
        private readonly int CURSOR_TOP = 9;

        public Field_Inn()
        {
            title = Define.FIELD_INN_TITLE;
            info = Define.FIELD_INN_INFO;

            menu.Add("휴식하기");
            menu.Add("나가기");
        }

        public override int Update(bool hasZero = false)
        {
            while (true)
            {
                switch (base.Update())
                {
                    case 1:
                        if (Heal() == false) continue;
                        return 0;
                    case 2:
                        Program.CurrentField = new Field_Lobby();
                        return 0;
                }
            }
        }

        protected override void ShowInfo()
        {
            Utils.WriteColor($" {PRICE}G", ConsoleColor.Yellow);
            base.ShowInfo();
            ShowPlayerInfo();
        }

        private bool Heal()
        {
            // 보유한 골드가 충분한지 검사
            CreatureStats stats = Program.Player.Stats;
            if (stats.Gold < PRICE)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_PURCHASE);
                return false;
            }

            // 이미 체력이 가득찼는지 검사
            ItemStats itemStats = Program.Player.ItemStats;
            int maxHP = stats.MaxHP + itemStats.MaxHP;
            if (stats.HP == maxHP)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_FULLHP);
                return false;
            }

            // 체력 회복 로직
            int currentHP = stats.HP;
            (int left, int top) = Console.GetCursorPosition();
            while (stats.HP < maxHP)
            {
                int amount = maxHP - stats.HP;
                if (amount > 10)
                {
                    stats.HP += 10;
                }
                else
                {
                    stats.HP++;
                }

                Utils.ClearLine(CURSOR_LEFT, CURSOR_TOP);
                Console.SetCursorPosition(CURSOR_LEFT, CURSOR_TOP);
                Utils.WriteColor(currentHP, ConsoleColor.White);
                Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);
                Utils.WriteColor(stats.HP, ConsoleColor.Green);
                Thread.Sleep(10);
            }

            // 플레이어 골드 감소
            stats.Gold -= PRICE;
            Program.Player.Stats = stats;

            // 콘솔 출력
            Console.SetCursorPosition(left, top);
            Console.Write("체력이 회복되었습니다! ");
            Utils.WriteColorLine($"-{PRICE:N0}G", ConsoleColor.DarkRed);

            // 보유금 콘솔 상태 변경
            (left, top) = Console.GetCursorPosition();
            Utils.ClearLine(CURSOR_LEFT, CURSOR_TOP + 1);
            Console.SetCursorPosition(CURSOR_LEFT, CURSOR_TOP + 1);
            Utils.WriteColorLine($"{stats.Gold:N0}G\n", ConsoleColor.Yellow);
            Console.SetCursorPosition(left, top);

            // 해당 필드를 떠나지 않기
            Update();
            return true;
        }
    }
}