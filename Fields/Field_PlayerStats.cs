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
            ShowPlayerInfo();
        }
    }
}