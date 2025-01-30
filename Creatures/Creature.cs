namespace TextRPG
{
    public struct CreatureStats
    {
        public CreatureStats(CreatureStats stats)
        {
            LV = stats.LV;
            Name = stats.Name;

            ATK = stats.ATK;
            DEF = stats.DEF;

            HP = stats.HP;
            MaxHP = stats.MaxHP;

            EXP = stats.EXP;
            MaxEXP = stats.MaxEXP;

            Gold = stats.Gold;
        }

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
            stats.MaxEXP = Define.MAX_EXP[stats.LV - 1];

            Stats = stats;
        }

        public CreatureStats Stats { get; set; }
    }
}