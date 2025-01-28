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
                    // TODO: 상점 구현하기
                    break;
                case 4:
                    return -1;
            }

            return 0;
        }
    }
}