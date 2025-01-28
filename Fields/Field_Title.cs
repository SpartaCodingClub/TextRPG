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
            switch (base.Update())
            {
                case 1:
                    Program.CurrentField = new Field_PlayerName();
                    break;
                case 2:
                    // TODO: 추가 과제 - 데이터 로드 구현하기
                    break;
            }

            return 0;
        }
    }
}