namespace TextRPG
{
    public enum PlayerType
    {
        Sword,
        Shield,
        Mage,
        Common
    }

    internal class Player : Creature
    {
        public Player() { }

        public Player(PlayerType type, CreatureStats stats) : base(stats) { Type = type; }

        public PlayerType Type { get; private set; }

        public ItemStats ItemStats { get; private set; }

        public List<Item> Inventory { get; private set; } = new(9);

        public void Equipment()
        {
            ItemStats = new();

            foreach (var item in Inventory)
            {
                if (item.Stats.IsEquipped == false)
                {
                    continue;
                }

                ItemStats += item.Stats;
            }
        }
    }
}