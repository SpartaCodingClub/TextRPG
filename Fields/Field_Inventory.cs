namespace TextRPG
{
    internal class Field_Inventory : Field_Base
    {
        public Field_Inventory()
        {
            title = Define.FIELD_INVENTORY_TITLE;
            info = Define.FIELD_INVENTORY_INFO;

            menu.Add("장착 관리");
            menu.Add("나가기");
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 1:
                    Program.CurrentField = new Field_Equipment();
                    break;
                case 2:
                    Program.CurrentField = new Field_Lobby();
                    break;
            }

            return 0;
        }

        protected override void ShowInfo()
        {
            base.ShowInfo();

            Console.WriteLine(" [아이템 목록]");

            var inventory = Program.Player.Inventory;
            if (inventory.Count > 0)
            {
                foreach (var item in inventory)
                {
                    item.ShowInfo();
                }
            }
            else
            {
                Utils.WriteColorLine("  -  비어있음", ConsoleColor.DarkGray);
            }

            Console.WriteLine();
        }
    }
}