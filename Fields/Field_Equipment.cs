namespace TextRPG
{
    internal class Field_Equipment : Field_Base
    {
        public Field_Equipment()
        {
            title = Define.FIELD_EQUIPMENT_TITLE;
            info = Define.FIELD_INVENTORY_INFO;
        }

        private readonly List<Item> inventory = Program.Player.Inventory;

        public override void Start()
        {
            menu.Clear();
            base.Start();

            for (int i = 0; i < inventory.Count; i++)
            {
                menu.Add(string.Empty);
            }
        }

        public override int Update()
        {
            while (true)
            {
                int index = base.Update();
                switch (index)
                {
                    case 0:
                        Program.CurrentField = new Field_Inventory();
                        return 0;
                    default:
                        if (inventory[index - 1].Equipment() == false) continue;
                        Program.Player.Equipment();
                        return 0;
                }
            }
        }

        protected override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine(" [아이템 목록]");

            if (inventory.Count > 0)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    inventory[i].ShowInfo(true, false, i + 1);
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