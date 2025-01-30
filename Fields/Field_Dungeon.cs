namespace TextRPG
{
    public enum DungeonType
    {
        Easy,
        Normal,
        Hard,
        Hell
    }

    public struct Dungeon
    {
        public string Name;
        public int DEF;
        public int EXP;
        public int Gold;
    };

    internal class Field_Dungeon : Field_Base
    {
        public Field_Dungeon()
        {
            title = Define.FIELD_DUNGEON_TITLE;
            info = Define.FIELD_LOBBY_INFO;

            foreach (var dungeon in Define.DUNGEONS)
            {
                menu.Add($"{dungeon.Name} | 방어력 {dungeon.DEF} 이상 권장");
            }

            menu.Add("나가기");

            DataManager.Instance.SaveData();
        }

        public override int Update()
        {
            int index = base.Update();
            Program.CurrentField = index switch
            {
                5 => new Field_Lobby(),
                _ => new Field_Battle((DungeonType)(index - 1))
            };

            return 0;
        }

        protected override void ShowInfo()
        {
            base.ShowInfo();
            ShowPlayerInfo();
        }
    }
}