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
            foreach (WorldPotion worldPotion in Program.playerData.invenPotion)
            {
                saveData.invenPotion.Add((int)worldPotion.potion, worldPotion.stack);
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
            //엔트리
            saveData.entry = new int[3];
            for (int i = 0; i < 3; i++)
            {
                int entryNum = -1;
                if (Program.playerData.entry[i] != null)
                {
                    for (int j = 0; j < Program.playerData.team.Count; j++)
                    {
                        if (Program.playerData.entry[i] == Program.playerData.team[j])
                        {
                            entryNum = j;
                            break;
                        }
                    }
                }
                saveData.entry[i] = entryNum;
            }

            ///////////////////////////////////////////////////////////
            // Save Data -> Ingame Data
            saveData.DefeatHighestDungeonStage = Program.ingameData.DefeatHighestDungeonStage;
            saveData.DungeonUnlock = Program.ingameData.DungeonUnlock;
            saveData.QuestFlag = Program.ingameData.QuestFlag;

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
                        Program.playerData.invenPotion = new List<WorldPotion>();
                        foreach (KeyValuePair<int, int> pair in saveData.invenPotion)
                        {
                            if (PotionDatabase.GetPotion((PotionCode)pair.Key) != null)
                            {
                                Program.playerData.invenPotion.Add(new WorldPotion((PotionCode)pair.Key, pair.Value));
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
                    //엔트리
                    if (saveData.entry != null)
                    {
                        Program.playerData.entry = new PlayerUnitData[3];
                        for (int i = 0; i < 3; i++)
                        {
                            if (saveData.entry[i] < 0)
                            {
                                Program.playerData.entry[i] = null;
                            }
                            else
                            {
                                Program.playerData.entry[i] = Program.playerData.team[saveData.entry[i]];
                            }
                        }
                    }



                    ///////////////////////////////////////////////////////////
                    // Save Data -> Ingame Data
                    Program.ingameData.DefeatHighestDungeonStage = saveData.DefeatHighestDungeonStage;
                    Program.ingameData.DungeonUnlock = saveData.DungeonUnlock;
                    if (saveData.QuestFlag != null)
                    {
                        Program.ingameData.QuestFlag = saveData.QuestFlag;
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
        ///////////////////////////////////////////////////////////
        // Player Data
        public struct Unit
        {
            public string Name {  get; set; }
            public int PClass { get; set; }
            public int Lv { get; set; }
            public int Exp { get; set; }
            public int ExpNextLevel { get; set; }

            //직업 관련 스테이터스
            public string PClassName { get; set; }
            public int Hp { get; set; }
            public int Mp { get; set; }
            public int Atk { get; set; }
            public int MAtk { get; set; }
            public int Def { get; set; }
            public int Speed { get; set; }
            public int CriRate { get; set; }

            //전투 관련 스테이터스
            public bool IsAlive { get; set; }
            public int CurrentHp { get; set; }
            public int CurrentMp { get; set; }

            //리스트 영역
            public int[] Equipment { get; set; }
            public int[] SkillList { get; set; }
        }
        public struct WorldGear
        {
            public int gear { get; set; }
            public int wearer { get; set; }
        }

        public int Gold { get; set; }

        //인벤토리
        public Dictionary<int, int> invenPotion { get; set; }
        public WorldGear[] invenGear { get; set; }

        //유닛
        public Unit[] team { get; set; }
        public int[] entry { get; set; }



        ///////////////////////////////////////////////////////////
        // Ingame Data
        public int DefeatHighestDungeonStage { get; set; }

        public int DungeonUnlock { get; set; }

        public int[] QuestFlag { get; set; }
    }
}
