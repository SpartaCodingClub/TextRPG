namespace TextRPG
{
    internal class Field_PlayerType : Field_Base
    {
        public Field_PlayerType()
        {
            title = Define.FIELD_PLAYER_TYPE_TITLE;
            info = Define.FIELD_PLAYER_TYPE_INFO;

            for (int i = 0; i < Define.CLASS.Length; i++)
            {
                menu.Add(Define.CLASS[i]);
            }
        }

        public override int Update(bool hasZero = false)
        {
            int index = base.Update() - 1;

            Program.Player = new((PlayerType)index, Define.STATS[index]);
            Program.CurrentField = new Field_Lobby();

            return 0;
        }

        protected override void ShowInfo()
        {
            Utils.WriteColor($" {Program.Nickname},", ConsoleColor.DarkYellow);
            base.ShowInfo();
        }
    }
}