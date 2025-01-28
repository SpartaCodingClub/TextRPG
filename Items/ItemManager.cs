namespace TextRPG
{
    internal class ItemManager
    {
        public ItemManager()
        {
            ItemList.Add(new("수련자의 갑옷\t", "수련에 도움을 주는 갑옷입니다.\t\t\t", 1000, def: 5));
            ItemList.Add(new("무쇠 갑옷\t\t", "무쇠로 만들어져 튼튼한 갑옷입니다.\t\t\t", 1800, def: 9));
            ItemList.Add(new("스파르타의 갑옷\t", "스파르타의 전사들이 사용했다는 전설의 갑옷입니다.\t", 3500, def: 15));
            ItemList.Add(new("낡은 검\t\t", "쉽게 볼 수 있는 낡은 검입니다.\t\t\t", 600, atk: 2));
            ItemList.Add(new("청동 도끼\t\t", "어디선가 사용됐던 거 같은 도끼입니다.\t\t", 1500, atk: 5));
            ItemList.Add(new("스파르타의 창\t", "스파르타의 전사들이 사용했다는 전설의 창입니다.\t", 2700, atk: 7));
        }

        public static readonly ItemManager Instance = new();

        public List<Item> ItemList { get; private set; } = new();
    }
}