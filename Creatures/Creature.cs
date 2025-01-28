namespace TextRPG
{
    public struct CreatureStats
    {
        public int LV;
        public string Name;

        public int ATK;
        public int DEF;

        public int HP;
        public int MaxHP;

        public int EXP;
        public int MaxEXP;

        public int Gold;
    }

    internal abstract class Creature
    {
        public Creature() { }

        public Creature(CreatureStats stats)
        {
            stats.LV = Math.Max(stats.LV, 1);
            stats.Name ??= Program.Nickname;
            stats.MaxHP = stats.HP;

            Stats = stats;
        }

        public CreatureStats Stats { get; protected set; }
    }
}