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

        private int cursorTop;

        public override void Start()
        {
            menu.Clear();
            base.Start();

            for (int i = 0; i < inventory.Count; i++)
            {
                menu.Add(string.Empty);
            }
        }

        public override int Update(bool hasZero = false)
        {
            while (true)
            {
                int index = base.Update(true);
                switch (index)
                {
                    case 0:
                        Program.CurrentField = new Field_Shop();
                        return 0;
                    default:
                        // 아이템 판매
                        Item item = inventory[index - 1];
                        if (item.Sale() == false) continue;

                        // 아이템 스탯 재적용
                        Program.Player.Equipment();

                        // 보유금 콘솔 상태 변경
                        (int left, int top) = Console.GetCursorPosition();
                        Console.SetCursorPosition(0, cursorTop);
                        ShowGoldInfo();
                        Console.SetCursorPosition(left, top);

                        // 해당 필드를 떠나지 않기
                        Update();

                        // 인벤토리에서 아이템 제거
                        Program.Player.Inventory.Remove(item);
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
                    inventory[i].ShowInfo(true, true, i + 1);
                }
            }
            else
            {
                Utils.WriteColorLine("  -  비어있음", ConsoleColor.DarkGray);
            }

            cursorTop = Console.CursorTop;
            ShowGoldInfo();
            Console.WriteLine(" [0] 나가기");
        }
    }
}