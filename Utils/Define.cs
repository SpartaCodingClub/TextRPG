namespace TextRPG
{
    internal class Define
    {
        #region Player
        public static readonly string[] CLASS = { "검병", "방패병", "힘법사" };
        public static readonly CreatureStats[] STATS =
        {
            new() { ATK = 10, DEF = 5, HP = 100, Gold = 1500 },
            new() { ATK = 5, DEF = 10, HP = 120, Gold = 1500 },
            new() { ATK = 8, DEF = 8, HP = 80, Gold = 1500 }
        };
        public static readonly int[] MAX_EXP = { 1, 2, 3, 5, 8, 13, 21, 34, 55 };
        #endregion
        #region Fields
        public static readonly string FIELD_TITLE_TITLE = " 스파르타 전사 키우기";
        public static readonly string FIELD_TITLE_INFO = " 스파르타 캠프에 오신 것을 환영합니다.";

        public static readonly string FIELD_PLAYER_NAME_TITLE = " 닉네임 설정";
        public static readonly string FIELD_PLAYER_NAME_INFO = " 닉네임은 2~8자로 만들어 주세요.";

        public static readonly string FIELD_PLAYER_TYPE_TITLE = " 클래스 선택";
        public static readonly string FIELD_PLAYER_TYPE_INFO = " 화려한 마법 클래스를 원하시나요? 여긴 스파르타입니다.";

        public static readonly string FIELD_LOBBY_TITLE = " 스파르타 마을";
        public static readonly string FIELD_DUNGEON_TITLE = " 던전 입장";
        public static readonly string FIELD_LOBBY_INFO = " 이곳에서 던전으로 들어가기 전 활동을 할 수 있습니다.";

        public static readonly string FIELD_PLAYER_STATS_TITLE = " 상태 보기";
        public static readonly string FIELD_PLAYER_STATS_INFO = " 캐틱터의 정보가 표시됩니다.";

        public static readonly string FIELD_INVENTORY_TITLE = " 인벤토리";
        public static readonly string FIELD_EQUIPMENT_TITLE = " 인벤토리 - 장착 관리";
        public static readonly string FIELD_INVENTORY_INFO = " 보유 중인 아이템을 관리할 수 있습니다.";

        public static readonly string FIELD_SHOP_TITLE = " 상점";
        public static readonly string FIELD_SHOP_INFO = " 필요한 아이템을 얻을 수 있는 상점입니다.";

        public static readonly string FIELD_SALE_TITLE = " 상점 - 아이템 판매";
        public static readonly string FIELD_SALE_INFO = " 판매 시 구매 가격의 85% 가격에 판매합니다. 장착하고 있는 아이템이었다면 해제됩니다.";

        public static readonly string FIELD_INN_TITLE = " 휴식하기";
        public static readonly string FIELD_INN_INFO = "를 내면 체력을 회복할 수 있습니다.";

        public static readonly string FIELD_CURSOR = " >> ";
        #endregion
        #region Dungeons
        public static readonly Dungeon[] DUNGEONS =
        {
            new() { Name = "쉬운 던전\t", DEF = 5, EXP = 1, Gold = 1000 },
            new() { Name = "보통 던전\t", DEF = 11, EXP = 2, Gold = 1700 },
            new() { Name = "어려운 던전", DEF = 17, EXP = 3, Gold = 2500 },
            new() { Name = "지옥 던전\t", DEF = 44, EXP = 6, Gold = 666 }
        };
        #endregion
        #region Errors
        public static readonly string ERROR_MESSAGE_INPUT = "[!] 잘못된 입력입니다.";
        public static readonly string ERROR_MESSAGE_DATA = "[!] 저장된 데이터가 없습니다.";
        public static readonly string ERROR_MESSAGE_CAPACITY = "[!] 인벤토리가 가득 찼습니다.";
        public static readonly string ERROR_MESSAGE_PURCHASE = "[!] 골드가 부족합니다.";
        public static readonly string ERROR_MESSAGE_PURCHASED = "[!] 이미 구매한 아이템입니다.";
        public static readonly string ERROR_MESSAGE_REQUIRED = "[!] 착용할 수 없는 아이템입니다.";
        public static readonly string ERROR_MESSAGE_FULLHP = "[!] 이미 체력이 가득 찼습니다.";
        #endregion
    }
}