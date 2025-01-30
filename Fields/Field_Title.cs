namespace TextRPG
{
    internal class Field_Title : Field_Base
    {
        public Field_Title()
        {
            title = Define.FIELD_TITLE_TITLE;
            info = Define.FIELD_TITLE_INFO;

            menu.Add("새로 하기");
            menu.Add("이어 하기");
        }

        public override int Update()
        {
            while (true)
            {
                switch (base.Update())
                {
                    case 1:
                        Program.CurrentField = new Field_PlayerName();
                        return 0;
                    case 2:
                        if (DataManager.Instance.LoadData() == false) continue;
                        Program.CurrentField = new Field_Lobby();
                        return 0;
                }
            }
        }
    }
}