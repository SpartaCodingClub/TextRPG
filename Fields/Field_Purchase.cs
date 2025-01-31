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

        private int cursorTop;

        public override void Start()
        {
            menu.Clear();
            base.Start();

            for (int i = 0; i < itemList.Count; i++)
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
                        // 아이템 구매
                        Item item = itemList[index - 1];
                        if (item.Purchase() == false) continue;

                        // 플레이어의 아이템은 가격이 노출될 수 있도록 판매되지 않음 처리
                        ItemStatus status = item.Status;
                        status.IsPurchased = false;
                        Item newItem = new() { Stats = item.Stats, Status = status };

                        // 인벤토리에 아이템 추가
                        Program.Player.Inventory.Add(newItem);

                        // 보유금 콘솔 상태 변경
                        (int left, int top) = Console.GetCursorPosition();
                        Console.SetCursorPosition(0, cursorTop);
                        ShowGoldInfo();
                        Console.SetCursorPosition(left, top);

                        // 해당 필드를 떠나지 않기
                        Update();
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

            cursorTop = Console.CursorTop;
            ShowGoldInfo();
            Console.WriteLine(" [0] 나가기");
        }
    }
}