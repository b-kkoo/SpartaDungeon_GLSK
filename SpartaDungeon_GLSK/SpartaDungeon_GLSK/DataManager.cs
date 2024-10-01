using SpartaDungeon_GLSK.Data;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SpartaDungeon_GLSK
{
    public class DataManager
    {
        //저장파일 경로는 실행파일과 같음
        //저장파일 이름은 SaveData1, SaveData2, SaveData3 세개
        public static bool SaveDatafile(int saveSocket)
        {
            if (saveSocket < 1 || saveSocket > 3)
            {
                return false;
            }

            SaveData saveData = new SaveData();

            string filePath = $"SaveData{saveSocket}";

            //Player Data -> Save Data
            saveData.inventory = new Dictionary<int, int>();
            foreach (KeyValuePair<PotionCode, int> i in Program.playerData.invenPotion) saveData.inventory.Add((int)i.Key, i.Value);

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
        public static bool LoadDatafile(int saveSocket)
        {
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
                    SaveData saveData = JsonSerializer.Deserialize<SaveData>(jsonString);
                    Console.WriteLine($"\n - {saveSocket}번 데이터 불러오기 완료! - \n");

                    //Save Data -> Player Data
                    Program.playerData.Name = saveData.Name;
                    Program.playerData.PClass = (JobCode)saveData.Chad;
                    Program.playerData.PClassName = saveData.PClassName;
                    Program.playerData.Lv = saveData.Lv;
                    Program.playerData.Hp = saveData.Hp;
                    Program.playerData.CurrentHp = saveData.currentHp;
                    Program.playerData.Atk = saveData.Atk;
                    Program.playerData.Def = saveData.Def;
                    Program.playerData.CriRate = saveData.CriRate;
                    if (saveData.inventory != null)
                    {
                        foreach (KeyValuePair<int, int> pair in saveData.inventory)
                        {
                            if (PotionDatabase.GetPotion((PotionCode)pair.Key) != null)
                            {
                                Program.playerData.invenPotion.Add(new KeyValuePair<PotionCode, int>((PotionCode)pair.Key, pair.Value));
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"\n - {saveSocket}번 데이터 파일을 찾을 수 없습니다! - \n");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n데이터 불러오기 중 오류가 발생했습니다: {ex.Message}\n");
                return false;
            }



            return true;
        }


        //세이브 파일의 대략적인 정보를 반환
        public static bool GetSavefileStatus(int saveSocket, out string info)
        {
            if (saveSocket < 1 || saveSocket > 3)
            {
                info = "매개변수가 잘못되었습니다";
                return false;
            }

            string filePath = $"SaveData{saveSocket}";
            try
            {
                if (File.Exists(filePath))
                {
                    // JSON 파일을 읽어서 객체로 역직렬화
                    string jsonString = File.ReadAllText(filePath);
                    SaveData saveData = JsonSerializer.Deserialize<SaveData>(jsonString);
                    info = $"LV {saveData.Lv}  {saveData.Name}  {saveData.PClassName}";
                }
                else
                {
                    info = "빈 데이터";
                    return false;
                }
            }
            catch (Exception ex)
            {
                info = "데이터 오류";
                return false;
            }

            return true;
        }
    }

    //인게임 데이터를 json 형식으로 저장할 수 있도록 변환한 클래스
    public class SaveData
    {
        public string Name { get; set; }
        public int Chad { get; set; }
        public string PClassName { get; set; }
        public int Lv { get; set; }
        public int Hp { get; set; }
        public int currentHp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int CriRate { get; set; }
        public Dictionary<int, int> inventory { get; set; }
    }
}
