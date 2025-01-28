namespace TextRPG
{
    public enum PlayerType
    {
        Sword,
        Shield,
        Mage
    }

    internal class Player : Creature
    {
        public Player() { }

        public Player(PlayerType type, CreatureStats stats) : base(stats)
        {
            Type = type;
            
            // TODO: 한줄쓰기로 바꿔야함
            // TEST CODE
            foreach (var item in ItemManager.Instance.ItemList)
            {
                Inventory.Add(item);
            }
        }

        public PlayerType Type { get; private set; }

        public ItemStats ItemStats { get; private set; }

        public List<Item> Inventory { get; private set; } = new();

        public void Equipment()
        {
            ItemStats = new();

            foreach (var item in Inventory)
            {
                if (item.Stats.IsEquipment == false)
                {
                    continue;
                }

                ItemStats += item.Stats;
            }
        }
    }
}