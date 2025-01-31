namespace TextRPG
{
    internal class Field_Shop : Field_Base
    {
        public Field_Shop()
        {
            title = Define.FIELD_SHOP_TITLE;
            info = Define.FIELD_SHOP_INFO;

            menu.Add("아이템 구매");
            menu.Add("아이템 판매");
            menu.Add("나가기");
        }

        public override int Update(bool hasZero = false)
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentField = new Field_Purchase();
                    break;
                case 2:
                    Program.CurrentField = new Field_Sale();
                    break;
                case 3:
                    Program.CurrentField = new Field_Lobby();
                    break;
            }

            return 0;
        }

        protected override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine(" [아이템 목록]");

            var itemList = ItemManager.Instance.ItemList;
            if (itemList.Count > 0)
            {
                foreach (var item in itemList)
                {
                    item.ShowInfo(false, true);
                }
            }
            else
            {
                Utils.WriteColorLine("  -  비어있음", ConsoleColor.DarkGray);
            }

            ShowGoldInfo();
        }
    }
}