namespace TextRPG
{
    internal class Field_PlayerStats : Field_Base
    {
        public Field_PlayerStats()
        {
            title = Define.FIELD_PLAYER_STATS_TITLE;
            info = Define.FIELD_PLAYER_STATS_INFO;

            menu.Add("나가기");
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentField = new Field_Lobby();
                    break;
            }

            return 0;
        }

        protected override void ShowInfo()
        {
            base.ShowInfo();

            Player player = Program.Player;
            CreatureStats stats = player.Stats;
            ItemStats itemStats = player.ItemStats;

            Utils.WriteColorLine($" Lv. {stats.LV:00}", ConsoleColor.White);

            Utils.WriteColor($" {stats.Name}", ConsoleColor.DarkYellow);
            Console.WriteLine($" ({Define.CLASS[(int)player.Type]})\n");

            ShowStats("공격력", stats.ATK, itemStats.ATK);
            ShowStats("방어력", stats.DEF, itemStats.DEF);
            ShowStats("체  력", stats.HP, itemStats.HP);

            Console.Write(" 소지금: ");
            Utils.WriteColorLine($"{stats.Gold}G\n", ConsoleColor.Yellow);
        }

        private void ShowStats(string label, int value, int itemValue)
        {
            Console.Write($" {label}: ");
            Utils.WriteColor(value, ConsoleColor.White);
            if (itemValue != 0)
            {
                Utils.WriteColor($" + {itemValue}", ConsoleColor.DarkGreen);
            }

            Console.WriteLine();
        }
    }
}