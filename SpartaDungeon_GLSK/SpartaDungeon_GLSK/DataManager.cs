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

            string filePath = $"SaveData{saveSocket}";

            /////////////////////////////////////////////////////////////////////////
            // Player Data -> Save Data
            SaveData saveData = new SaveData();

            // Gold
            saveData.Gold = Program.playerData.Gold;

            // 인벤토리
            saveData.invenPotion = new Dictionary<int, int>();
            foreach (KeyValuePair<PotionCode, int> pair in Program.playerData.invenPotion)
            {
                saveData.invenPotion.Add((int)pair.Key, pair.Value);
            }
            saveData.invenGear = new SaveData.WorldGear[Program.playerData.invenGear.Count];
            for (int i = 0; i < Program.playerData.invenGear.Count; i++)
            {
                WorldGear worldGear = Program.playerData.invenGear[i];

                saveData.invenGear[i].gear = (int)worldGear.gear;
                saveData.invenGear[i].wearer = (int)worldGear.wearer;
            }

            // 유닛
            saveData.team = new SaveData.Unit[Program.playerData.team.Count];
            for (int i = 0; i < Program.playerData.team.Count; i++)
            {
                PlayerUnitData playerUnitData = Program.playerData.team[i];

                saveData.team[i].Name = playerUnitData.Name;
                saveData.team[i].PClass = (int)playerUnitData.PClass;
                saveData.team[i].Lv = playerUnitData.Lv;
                saveData.team[i].Exp = playerUnitData.Exp;
                saveData.team[i].ExpNextLevel = playerUnitData.ExpNextLevel;

                saveData.team[i].PClassName = playerUnitData.PClassName;
                saveData.team[i].Hp = playerUnitData.Hp;
                saveData.team[i].Mp = playerUnitData.Mp;
                saveData.team[i].Atk = playerUnitData.Atk;
                saveData.team[i].MAtk = playerUnitData.MAtk;
                saveData.team[i].Def = playerUnitData.Def;
                saveData.team[i].Speed = playerUnitData.Speed;
                saveData.team[i].CriRate = playerUnitData.CriRate;

                saveData.team[i].IsAlive = playerUnitData.IsAlive;
                saveData.team[i].CurrentHp = playerUnitData.CurrentHp;
                saveData.team[i].CurrentMp = playerUnitData.CurrentMp;

                saveData.team[i].Equipment = new int[(int)GearSlot.Max]; //3
                for (int j = 0; j < (int)GearSlot.Max; j++)
                {
                    saveData.team[i].Equipment[j] = (int)playerUnitData.Equipment[j];
                }
                saveData.team[i].SkillList = new int[playerUnitData.SkillList.Count];
                for (int j = 0; j < playerUnitData.SkillList.Count; j++)
                {
                    saveData.team[i].SkillList[j] = (int)playerUnitData.SkillList[j];
                }
            }

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

                    ///////////////////////////////////////////////////////////
                    // Save Data -> Player Data
                    Program.playerData = new PlayerData();

                    // Gold
                    Program.playerData.Gold = saveData.Gold;

                    // 인벤토리
                    if (saveData.invenPotion != null)
                    {
                        Program.playerData.invenPotion = new List<KeyValuePair<PotionCode, int>>();
                        foreach (KeyValuePair<int, int> pair in saveData.invenPotion)
                        {
                            if (PotionDatabase.GetPotion((PotionCode)pair.Key) != null)
                            {
                                Program.playerData.invenPotion.Add(new KeyValuePair<PotionCode, int>((PotionCode)pair.Key, pair.Value));
                            }
                        }
                    }
                    if (saveData.invenGear != null)
                    {
                        Program.playerData.invenGear = new List<WorldGear>();
                        for (int i = 0; i < saveData.invenGear.Length; i++)
                        {
                            Program.playerData.invenGear.Add(new WorldGear((GearCode)saveData.invenGear[i].gear, saveData.invenGear[i].wearer));
                        }
                    }

                    //유닛
                    if (saveData.team != null)
                    {
                        for (int i = 0; i < saveData.team.Length; i++)
                        {
                            PlayerUnitData playerUnitData = new PlayerUnitData();

                            playerUnitData.Name = saveData.team[i].Name;
                            playerUnitData.PClass = (JobCode)saveData.team[i].PClass;
                            playerUnitData.Lv = saveData.team[i].Lv;
                            playerUnitData.Exp = saveData.team[i].Exp;
                            playerUnitData.ExpNextLevel = saveData.team[i].ExpNextLevel;

                            playerUnitData.PClassName = saveData.team[i].PClassName;
                            playerUnitData.Hp = saveData.team[i].Hp;
                            playerUnitData.Mp = saveData.team[i].Mp;
                            playerUnitData.Atk = saveData.team[i].Atk;
                            playerUnitData.MAtk = saveData.team[i].MAtk;
                            playerUnitData.Def = saveData.team[i].Def;
                            playerUnitData.Speed = saveData.team[i].Speed;
                            playerUnitData.CriRate = saveData.team[i].CriRate;

                            playerUnitData.IsAlive = saveData.team[i].IsAlive;
                            playerUnitData.CurrentHp = saveData.team[i].CurrentHp;
                            playerUnitData.CurrentMp = saveData.team[i].CurrentMp;

                            playerUnitData.Equipment = new GearCode[(int)GearSlot.Max]; //3
                            for (int j = 0; j < (int)GearSlot.Max; j++)
                            {
                                playerUnitData.Equipment[j] = (GearCode)saveData.team[i].Equipment[j];
                            }
                            playerUnitData.SkillList = new List<PlayerSkillCode>();
                            for (int j = 0; j < saveData.team[i].SkillList.Length; j++)
                            {
                                playerUnitData.SkillList.Add((PlayerSkillCode)saveData.team[i].SkillList[j]);
                            }

                            Program.playerData.team.Add(playerUnitData);
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
        public struct WorldGear
        {
            public int gear;
            public int wearer;
        }

        public int Gold;

        //인벤토리
        public Dictionary<int, int> invenPotion;
        public WorldGear[] invenGear;

        //유닛
        public Unit[] team;
        public int[] entry;
    }
}
