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

            quests.Add(QuestCode.dungeonStage0_2, new Quest("고블린 로드 퇴치",
                                                    new string[] { "안내인 : 고블린 소굴에대해 새로운 정보가 공개됐어요.",
                                                                   "안내인 : 그곳에 고블린 로드가 발생되었다는 정보를 입수했어요.",
                                                                   "안내인 : 고블린 로드가 발생됨에 따라 던전 브레이크가 발생될수있으니.",
                                                                   "안내인 : 고블린 로드 퇴치를 의뢰 할까합니다 의뢰를 해결하시면 상당한 보상을 드릴게요."},
                                                       new string[] { "안내인 : 고블린 로드 퇴치는 아직인가요?",
                                                                      "안내인 : 고블린 로드 퇴치를 할때 무리를 하지않는게 좋을 거예요."},
                                                          new string[] { "안내인 : 고블린 로드 퇴치를 하고 오셨군요!",
                                                                         "안내인 : 고블린 로드 퇴치에 대해 걱정이 많았엇는데 다행이군요!",
                                                                         "안내인 : 보상금 여기있습니다!"},
                                                                                                                               200, new GearCode[0], new WorldPotion[0]));

            quests.Add(QuestCode.dungeonStage1_1, new Quest("타락한 자들의 땅",
                                                    new string[] { "안내인 : 최근에 모험가님이 타락한 자들의 땅을 다녀오신 적 있으셨다고 들었어요.",
                                                                   "안내인 : 그곳에서 언데드가 점점 많아지고 있다는 정보가 있어요",
                                                                   "안내인 : 성왕교에서 이를 이상하게 여기고 조사의뢰를 했어요",
                                                                   "안내인 : 타락한 자들의 땅에서 무슨일이 있엇는지 조사를 부탁드립니다."},
                                                       new string[] { "안내인 : 타락한 자들의 땅 조사는 아직인가요?",
                                                                      "안내인 : 무리하지않고 조금해서 조사하시는게 좋을 거예요."},
                                                          new string[] { "안내인 : 타락한 자들의 땅조사를 하고 오셨군요!",
                                                                         "안내인 : 언데드가 대량 발생을 했다고요? 좋은 정보 감사합니다!",
                                                                         "안내인 : 보상금 여기있습니다!"},
                                                                                                                               220, new GearCode[0], new WorldPotion[0]));

            quests.Add(QuestCode.dungeonStage1_2, new Quest("언데드 사냥",
                                                    new string[] { "안내인 : 타락한 자들의 땅에대해 새로운 정보가 들어왔어요.",
                                                                   "안내인 : 그곳에 언데드들이 대량 발생했다는 정보가 있어요 .",
                                                                   "안내인 : 성왕교에서 언데드 정화를 위해 퇴치를 의뢰 했어요",
                                                                   "안내인 : 언데드들을 사냥해 오시면 상당한 보상을 드릴게요."},
                                                       new string[] { "안내인 : 사냥은 아직인가요?",
                                                                      "안내인 : 샤냥이 오래걸리더라도 천천히 해보시는게 안전에 좋을 거예요."},
                                                          new string[] { "안내인 : 사냥을 다하고 오셨군요!",
                                                                         "안내인 : 마지막방에 네크로 멘서가 있엇다고요? 위험하셨을텐데 좋은정보 감사합니다!",
                                                                         "안내인 : 보상금 여기있습니다!"},
                                                                                                                               250, new GearCode[0], new WorldPotion[0]));
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
        dungeonStage0_1,
        dungeonStage0_2,
        dungeonStage1_1,
        dungeonStage1_2

    }
}
