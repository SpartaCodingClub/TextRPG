namespace TextRPG
{
    internal class Field_Sale : Field_Base
    {
        public Field_Sale()
        {
            title = Define.FIELD_SALE_TITLE;
            info = Define.FIELD_SALE_INFO;
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
            int index = base.Update();
            switch (index)
            {
                case 0:
                    Program.CurrentField = new Field_Shop();
                    break;
                default:
                    Item item = inventory[index - 1];
                    item.Sale();
                    Program.Player.Inventory.Remove(item);
                    Program.Player.Equipment();
                    Update();
                    break;
            }

            return 0;
        }

        protected override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine(" [아이템 목록]");

            if (inventory.Count > 0)
            {
                for (int i = 0; i < inventory.Count; i++)
                {
                    inventory[i].ShowInfo(true, true, i + 1);
                }
            }
            else
            {
                Utils.WriteColorLine("  -  비어있음", ConsoleColor.DarkGray);
            }

            Console.WriteLine("\n [보유 골드]");
            Utils.WriteColorLine($" {Program.Player.Stats.Gold}G\n", ConsoleColor.Yellow);

            Console.WriteLine(" [0] 나가기");
        }
    }
}