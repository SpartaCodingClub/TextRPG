using System.Text;
using Newtonsoft.Json;

namespace TextRPG
{
    internal class DataManager
    {
        private readonly string FILE_NAME = "data";

        public static readonly DataManager Instance = new();

        public void SaveData()
        {
            Player player = Program.Player;
            FileStream fileStream = new($"{FILE_NAME}.json", FileMode.Create);
            StringBuilder stringBuilder = new();

            // 플레이어 정보 저장하기
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Type)}\n");
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Stats)}\n");

            // 아이템 정보 저장하기
            foreach (var item in player.Inventory)
            {
                stringBuilder.Append($"{JsonConvert.SerializeObject(item.Stats)}\n");
                stringBuilder.Append($"{JsonConvert.SerializeObject(item.Status)}\n");
            }

            // data.json 파일에 데이터 쓰기
            string jsonData = stringBuilder.ToString();
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }

        public bool LoadData()
        {
            // 저장된 데이터가 있는지 확인
            FileStream fileStream;
            try
            {
                fileStream = new($"{FILE_NAME}.json", FileMode.Open);
            }
            catch
            {
                Console.WriteLine(Define.ERROR_MESSAGE_DATA);
                return false;
            }

            // data.json 파일의 데이터 읽어오기
            byte[] data = new byte[fileStream.Length];
            fileStream.Read(data, 0, data.Length);
            fileStream.Close();

            // 플레이어 정보 불러오기
            string[] jsonData = Encoding.UTF8.GetString(data).Split('\n');
            PlayerType type = JsonConvert.DeserializeObject<PlayerType>(jsonData[0]);
            CreatureStats stats = JsonConvert.DeserializeObject<CreatureStats>(jsonData[1]);
            Player player = Program.Player = new(type, stats);

            // 아이템 정보 불러오기
            for (int i = 2; i < jsonData.Length - 1; i++)
            {
                if (i % 2 != 0)
                {
                    continue;
                }

                ItemStats itemStats = JsonConvert.DeserializeObject<ItemStats>(jsonData[i]);
                ItemStatus itemStatus = JsonConvert.DeserializeObject<ItemStatus>(jsonData[i + 1]);
                Item item = new() { Stats = itemStats, Status = itemStatus };
                player.Inventory.Add(item);
            }

            // 상점 아이템 정보 동기화
            foreach (var playerItem in player.Inventory)
            {
                foreach (var item in ItemManager.Instance.ItemList)
                {
                    if (item.Status.Name != playerItem.Status.Name)
                    {
                        continue;
                    }

                    // 상점 아이템 중 플레이어가 보유한 아이템은 판매된 아이템으로 변경하기
                    ItemStatus newStatus = item.Status;
                    newStatus.IsPurchased = true;
                    item.Status = newStatus;
                }
            }

            // 착용 중인 아이템 스탯 반영하기
            player.Equipment();

            // 체력이 증가하는 아이템을 착용하고 저장했을 경우, 체력이 중복으로 증가하는 것을 방지하기
            player.Stats = stats;

            return true;
        }
    }
}