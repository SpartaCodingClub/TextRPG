namespace TextRPG
{
    internal class Define
    {
        #region Class
        public static readonly string[] CLASS = { "검병", "방패병", "힘법사" };
        public static readonly CreatureStats[] STATS =
        {
            new() { ATK = 10, DEF = 5, HP = 100, Gold = 1500 },
            new() { ATK = 5, DEF = 10, HP = 100, Gold = 1500 },
            new() { ATK = 8, DEF = 8, HP = 80, Gold = 1500 }
        };
        #endregion
        #region Fields
        public static readonly string FIELD_TITLE_TITLE = " 스파르타 전사 키우기";
        public static readonly string FIELD_TITLE_INFO = " 스파르타 캠프에 오신 것을 환영합니다.";

        public static readonly string FIELD_PLAYER_NAME_TITLE = " 닉네임 설정";
        public static readonly string FIELD_PLAYER_NAME_INFO = " 닉네임은 2~8자로 만들어 주세요.";

        public static readonly string FIELD_PLAYER_TYPE_TITLE = " 클래스 선택";
        public static readonly string FIELD_PLAYER_TYPE_INFO = " 화려한 마법 클래스를 원하시나요? 여긴 스파르타입니다.";

        public static readonly string FIELD_LOBBY_TITLE = " 스파르타 마을";
        public static readonly string FIELD_LOBBY_INFO = " 이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.";

        public static readonly string FIELD_PLAYER_STATS_TITLE = " 상태 보기";
        public static readonly string FIELD_PLAYER_STATS_INFO = " 캐틱터의 정보가 표시됩니다.";

        public static readonly string FIELD_INVENTORY_TITLE = " 인벤토리";
        public static readonly string FIELD_EQUIPMENT_TITLE = " 인벤토리 - 장착 관리";
        public static readonly string FIELD_INVENTORY_INFO = " 보유 중인 아이템을 관리할 수 있습니다.";


        public static readonly string FIELD_SHOP_TITLE = " 스파르타 던전";
        public static readonly string FIELD_SHOP_INFO = "";

        public static readonly string FIELD_CURSOR = " >> ";
        #endregion
        #region Errors
        public static readonly string ERROR_INPUT_MESSAGE = "[!] 잘못된 입력입니다.";
        #endregion
    }
}