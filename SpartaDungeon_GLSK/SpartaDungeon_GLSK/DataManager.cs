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
            saveData.invenPotion = new Dictionary<int, int>();
            foreach (KeyValuePair<PotionCode, int> i in Program.playerData.invenPotion) saveData.invenPotion.Add((int)i.Key, i.Value);

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
                    if (saveData.invenPotion != null)
                    {
                        foreach (KeyValuePair<int, int> pair in saveData.invenPotion)
                        {
                            if (PotionDatabase.GetPotion((PotionCode)pair.Key) != null)
                            {
                                Program.playerData.invenPotion.Add(new KeyValuePair<PotionCode, int>((PotionCode)pair.Key, pair.Value));
                            }
                        }
                    }
                    /*for (saveData.team)
                    Program.playerData.Name = saveData.Name;
                    Program.playerData.PClass = (JobCode)saveData.Chad;
                    Program.playerData.PClassName = saveData.PClassName;
                    Program.playerData.Lv = saveData.Lv;
                    Program.playerData.Hp = saveData.Hp;
                    Program.playerData.CurrentHp = saveData.currentHp;
                    Program.playerData.Atk = saveData.Atk;
                    Program.playerData.Def = saveData.Def;
                    Program.playerData.CriRate = saveData.CriRate;*/
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
                    info = $"LV {saveData.team[0].Lv}  {saveData.team[0].Name}  {saveData.team[0].PClassName}, 최고주파 던전 : , 파티 {saveData.team.Length}명";
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
        public struct Unit
        {
            public string Name;
            public int PClass;
            public int Lv;
            public int Exp;
            public int ExpNextLevel;

            //직업 관련 스테이터스
            public string PClassName;
            public int Hp;
            public int Mp;
            public int Atk;
            public int MAtk;
            public int Def;
            public int Speed;
            public int CriRate;

            //전투 관련 스테이터스
            public bool IsAlive;
            public int CurrentHp;
            public int CurrentMp;

            //리스트 영역
            public int[] Equipment;
            public int[] SkillList;
        }

        public int Gold;

        //인벤토리
        public Dictionary<int, int> invenPotion;
        public int[] invenGear;

        //유닛
        public Unit[] team;
        public int[] entry;
    }
}
