namespace TextRPG
{
    internal class Field_Equipment : Field_Base
    {
        public Field_Equipment()
        {
            title = Define.FIELD_EQUIPMENT_TITLE;
            info = Define.FIELD_INVENTORY_INFO;
        }

        public override int Update()
        {
            switch (base.Update())
            {
                case 0:
                    Program.CurrentField = new Field_Inventory();
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
                for (int i = 0; i < inventory.Count; i++)
                {
                    inventory[i].ShowInfo(i + 1);
                }
            }
            else
            {
                Utils.WriteColorLine("  -  비어있음", ConsoleColor.DarkGray);
            }

            Console.WriteLine("\n [0] 나가기");
        }
    }
}