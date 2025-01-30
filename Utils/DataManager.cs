using System.Text;
using Newtonsoft.Json;

namespace TextRPG
{
    internal class DataManager
    {
        private readonly string FILE_NAME = "test";

        public static readonly DataManager Instance = new();

        public void SaveData()
        {
            Player player = Program.Player;
            FileStream fileStream = new($"{FILE_NAME}.json", FileMode.Create);

            StringBuilder stringBuilder = new();
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Type)}\n");
            stringBuilder.Append($"{JsonConvert.SerializeObject(player.Stats)}\n");
            foreach (var item in player.Inventory)
            {
                stringBuilder.Append($"{JsonConvert.SerializeObject(item.Stats)}\n");
                stringBuilder.Append($"{JsonConvert.SerializeObject(item.Status)}\n");
            }

            string jsonData = stringBuilder.ToString();
            byte[] data = Encoding.UTF8.GetBytes(jsonData);
            fileStream.Write(data, 0, data.Length);
            fileStream.Close();
        }

        public bool LoadData()
        {
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

            foreach (var playerItem in player.Inventory)
            {
                foreach (var item in ItemManager.Instance.ItemList)
                {
                    if (item.Status.Name != playerItem.Status.Name)
                    {
                        continue;
                    }

                    // 플레이어가 보유한 아이템이라면 상점에서 판매 중인 해당 아이템을 구매 완료로 변경하기
                    ItemStatus newStatus = item.Status;
                    newStatus.IsPurchased = true;
                    item.Status = newStatus;
                }
            }

            player.Equipment();
            player.Stats = stats;

            return true;
        }
    }
}