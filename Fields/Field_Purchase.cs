namespace TextRPG
{
    internal class Field_Purchase : Field_Base
    {
        public Field_Purchase()
        {
            title = Define.FIELD_SHOP_TITLE;
            info = Define.FIELD_SHOP_INFO;
        }

        private readonly List<Item> itemList = ItemManager.Instance.ItemList;

        public override void Start()
        {
            menu.Clear();
            base.Start();

            for (int i = 0; i < itemList.Count; i++)
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
                        Program.CurrentField = new Field_Shop();
                        return 0;
                    default:
                        Item item = itemList[index - 1];
                        if (item.Purchase() == false) continue;
                        Program.Player.Inventory.Add(item);
                        return 0;
                }
            }
        }

        protected override void ShowInfo()
        {
            base.ShowInfo();
            Console.WriteLine(" [아이템 목록]");

            if (itemList.Count > 0)
            {
                for (int i = 0; i < itemList.Count; i++)
                {
                    itemList[i].ShowInfo(false, true, i + 1);
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