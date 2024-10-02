using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Data
{
    public static class QuestDatabase
    {
        private static readonly Dictionary<QuestCode, Quest> quests;

        static QuestDatabase()
        {
            quests = new Dictionary<QuestCode, Quest>();
   
            quests.Add(QuestCode.dungeonStage0_1, new Quest("만드라고라 수집",
                                                    new string[] { "안내인 : 최근에 모험가님이 고블린 소굴에 다녀오신 적 있으셨다고 들었어요.",
                                                                   "안내인 : 그곳에서 귀중한 약재인 만드라고라가 발견됐다는 정보가 있어요.",
                                                                   "안내인 : 지하 공동의 덩굴이 무성한 곳에 서식하는 것 같다고 하더라구요.",
                                                                   "안내인 : 만드라고라 샘플을 가져 오시면 상당한 보상을 드릴게요."},
                                                       new string[] { "안내인 : 만드라고라 샘플은 아직인가요?",
                                                                      "안내인 : 지하 공동의 덩굴이 무성한 곳에서 찾으시는 게 좋을 거예요."},
                                                          new string[] { "안내인 : 만드라고라 샘플을 가져 오셨군요!",
                                                                         "안내인 : 고블린 메이지가 재배중이었다고요? 좋은 정보 감사합니다!",
                                                                         "안내인 : 보상금 여기있습니다!"},
                                                                                                                               135, new GearCode[0], new WorldPotion[0]));
        }

        public static Quest GetQuest(QuestCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (quests.TryGetValue(code, out Quest quest))
            {
                return quest;
            }

            Console.WriteLine("해당 ID의 퀘스트를 찾을 수 없습니다.");
            return null;
        }
    }

    public class Quest
    {
        public string name {  get; }
        public string[] initialScript {  get; }
        public string[] processingScript {  get; }
        public string[] completScript {  get; }

        public int goldReward { get; }
        public GearCode[] gearReward { get; }
        public WorldPotion[] potionReward { get; }

        public Quest(string _name, string[] _initialScript, string[] _processingScript, string[] _completScript, int _goldReward, GearCode[] _gearReward, WorldPotion[] _potionReward)
        {
            name = _name;
            initialScript = _initialScript;
            processingScript = _processingScript;
            completScript = _completScript;
            goldReward = _goldReward;
            gearReward = _gearReward;
            potionReward = _potionReward;
        }
    }

    public enum QuestCode
    {
        dungeonStage0_1 = 2

    }
}
