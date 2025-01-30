namespace TextRPG
{
    internal class ItemManager
    {
        public ItemManager()
        {
            ItemList.Add(new(ItemType.Armor, PlayerType.Common, "수련자의 갑옷\t", "수련에 도움을 주는 갑옷입니다.\t\t\t", 1000, def: 5));
            ItemList.Add(new(ItemType.Armor, PlayerType.Common, "무쇠 갑옷\t\t", "무쇠로 만들어져 튼튼한 갑옷입니다.\t\t\t", 1800, def: 9));
            ItemList.Add(new(ItemType.Armor, PlayerType.Common, "스파르타의 갑옷\t", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.\t", 3500, def: 15));
            ItemList.Add(new(ItemType.Armor, PlayerType.Shield, "판금 갑옷\t\t", "[방패병 전용] 판금으로 만들어져 튼튼한 갑옷입니다.\t", 20000, def: 30));

            ItemList.Add(new(ItemType.Weapon, PlayerType.Common, "낡은 검\t\t", "쉽게 볼 수 있는 낡은 검입니다.\t\t\t", 600, atk: 2));
            ItemList.Add(new(ItemType.Weapon, PlayerType.Common, "청동 도끼\t\t", "어디선가 사용됐던 거 같은 도끼입니다.\t\t", 1500, atk: 5));
            ItemList.Add(new(ItemType.Weapon, PlayerType.Common, "스파르타의 창\t", "스파르타의 전사들이 사용했다는 전설의 창입니다.\t", 2700, atk: 7));
            ItemList.Add(new(ItemType.Weapon, PlayerType.Mage, "강철 메이스\t\t", "[힘법사 전용] 공수에 모두 뛰어난 메이스입니다.\t", 20000, atk: 8, def: 8));

            ItemList.Add(new(ItemType.Jewelry, PlayerType.Common, "결혼 반지\t\t", "누군가의 결혼을 기념한 반지입니다.\t\t\t", 1004, def: 1, hp: 100));
            ItemList.Add(new(ItemType.Jewelry, PlayerType.Common, "이혼 반지\t\t", "누군가의 분노가 느껴지는 반지입니다.\t\t\t", 20000, atk: 4, hp: 100));
            ItemList.Add(new(ItemType.Jewelry, PlayerType.Common, "칠 반지\t\t", "Chill한 보호 주문이 깃든 반지입니다.\t\t\t", 77777, def: 7, hp: 777));
        }

        public static readonly ItemManager Instance = new();

        public List<Item> ItemList { get; private set; } = new();
    }
}