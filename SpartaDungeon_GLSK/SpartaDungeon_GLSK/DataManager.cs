using SpartaDungeon_GLSK.Data;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SpartaDungeon_GLSK
{
    public class DataManager
    {
        //저장파일 경로는 실행파일과 같음
        //저장파일 이름은 SaveData1, SaveData2, SaveData3 세개
        public static bool SaveData(int saveSocket)
        {
            if (saveSocket < 1 || saveSocket > 3)
            {
                return false;
            }

            SaveData saveData = new SaveData();

            string filePath = $"SaveData{saveSocket}";

            //Player Data -> Save Data
            saveData.inventory = new Dictionary<int, int>();
            foreach (WorldItem i in Program.playerData.inventory) saveData.inventory.Add((int)i.item.code, i.stack);

            try
            {
                // 객체를 JSON으로 직렬화
                string jsonString = JsonSerializer.Serialize(saveData);
                File.WriteAllText(filePath, jsonString);
                Console.WriteLine($"\n - {saveSocket}번 데이터에 저장 완료! - \n");
                Console.SetCursorPosition(0, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n데이터 저장 중 오류가 발생했습니다: {ex.Message}\n");
                return false;
            }

            return true;
        }

        //저장파일 경로는 실행파일과 같음
        //저장파일 이름은 SaveData1, SaveData2, SaveData3 세개
        public static bool LoadData(int saveSocket, out SaveData SaveData)
        {
            SaveData = null;

            if (saveSocket < 1 || saveSocket > 3)
            {
                return false;
            }

            string filePath = $"SaveData{saveSocket}";
            try
            {
                if (File.Exists(filePath))
                {
                    // JSON 파일을 읽어서 객체로 역직렬화
                    string jsonString = File.ReadAllText(filePath);
                    SaveData = JsonSerializer.Deserialize<SaveData>(jsonString);
                    Console.WriteLine($"\n - {saveSocket}번 데이터 불러오기 완료! - \n");

                    //Save Data -> Player Data
                    if (SaveData.inventory != null)
                    {
                        foreach (KeyValuePair<int, int> pair in SaveData.inventory)
                        {
                            if (ItemData.GetItem((IC)pair.Key) != null)
                            {
                                Program.playerData.inventory.Add(new WorldItem((IC)pair.Key, pair.Value));
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"\n - {saveSocket}번 데이터 파일을 찾을 수 없습니다! - \n");
                    SaveData = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n데이터 불러오기 중 오류가 발생했습니다: {ex.Message}\n");
                SaveData = null;
                return false;
            }



            return true;
        }

    }

    public class SaveData
    {
        public Dictionary<int, int> inventory { get; set; }
    }
}
