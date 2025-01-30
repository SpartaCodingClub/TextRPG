namespace TextRPG
{
    internal class Field_PlayerName : Field_Base
    {
        public Field_PlayerName()
        {
            title = Define.FIELD_PLAYER_NAME_TITLE;
            info = Define.FIELD_PLAYER_NAME_INFO;
        }

        public override int Update()
        {
            string? nickname;
            while (true)
            {
                Utils.WriteColor(Define.FIELD_CURSOR, ConsoleColor.DarkYellow);

                nickname = Console.ReadLine();
                if (nickname == null)
                {
                    continue;
                }

                nickname = nickname.Replace(" ", string.Empty);
                if (nickname.Length >= 2 && nickname.Length <= 8)
                {
                    break;
                }

                Console.WriteLine($" {Define.ERROR_MESSAGE_INPUT}");
            }

            Program.Nickname = nickname.ToUpper();
            Program.CurrentField = new Field_PlayerType();

            return 0;
        }
    }
}