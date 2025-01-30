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

        public override int Update()
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
            CreatureStats stats = Program.Player.Stats;
            if (stats.Gold < PRICE)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_PURCHASE);
                return false;
            }

            ItemStats itemStats = Program.Player.ItemStats;
            int maxHP = stats.MaxHP + itemStats.MaxHP;
            if (stats.HP == maxHP)
            {
                Console.WriteLine(Define.ERROR_MESSAGE_FULLHP);
                return false;
            }

            int currentHP = stats.HP;
            var (left, top) = Console.GetCursorPosition();
            while (stats.HP < maxHP)
            {
                stats.HP++;

                Utils.ClearLine(CURSOR_LEFT, CURSOR_TOP);
                Console.SetCursorPosition(CURSOR_LEFT, CURSOR_TOP);
                Utils.WriteColor(currentHP, ConsoleColor.White);
                Utils.WriteColor(" -> ", ConsoleColor.DarkYellow);
                Utils.WriteColor(stats.HP, ConsoleColor.Green);
                Thread.Sleep(10);
            }

            stats.Gold -= PRICE;
            Program.Player.Stats = stats;

            Console.SetCursorPosition(left, top);
            Console.Write("체력이 회복되었습니다! ");
            Utils.WriteColorLine($"-{PRICE}G", ConsoleColor.DarkRed);
            Update();

            return true;
        }
    }
}