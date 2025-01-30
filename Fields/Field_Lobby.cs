namespace TextRPG
{
    internal class Field_Lobby : Field_Base
    {
        public Field_Lobby()
        {
            title = Define.FIELD_LOBBY_TITLE;
            info = Define.FIELD_LOBBY_INFO;

            menu.Add("상태 보기");
            menu.Add("인벤토리");
            menu.Add("상점");
            menu.Add("던전 입장");
            menu.Add("여관 입장");
            menu.Add("게임 종료");
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentField = new Field_PlayerStats();
                    break;
                case 2:
                    Program.CurrentField = new Field_Inventory();
                    break;
                case 3:
                    Program.CurrentField = new Field_Shop();
                    break;
                case 4:
                    Program.CurrentField = new Field_Dungeon();
                    break;
                case 5:
                    Program.CurrentField = new Field_Inn();
                    break;
                case 6:
                    Console.WriteLine("게임이 종료되었습니다.");
                    return -1;
            }

            return 0;
        }
    }
}