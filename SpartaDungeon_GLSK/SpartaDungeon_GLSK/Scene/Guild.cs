using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Scene
{
    internal class GuildScene
    {
        //길드
        public static bool GuildHall(out Scenes next, KeyController keyController) 
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine(" < < 길드 > >");
            Console.WriteLine("퀘스트를 받거나 완료하고, 모험가를 모집하고 휴식을 취하게 할 수 있습니다.\n");
            Console.WriteLine("1. 퀘스트 받기");
            Console.WriteLine("2. 모험가 모집");
            Console.WriteLine("3. 파티원 휴식");
            Console.WriteLine("                                               숫자 버튼을 눌러 선택!");
            Console.WriteLine("                                                             X : 뒤로");

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.X };
            while (true)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.Guild_Quest;
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Guild_Mercenary;
                        return true;

                    case ConsoleKey.D3:
                        next = Scenes.Guild_Inn;
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //마을로 이동
                        return true;
                }
            }
        }
        
        //퀘스트
        public static bool GuildQuest(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            bool loop = true;
            while (loop)
            {
                Console.Clear();

                List<int> acceptableQuest = new List<int>();
                List<int> processingQuest = new List<int>();
                List<int> completedQuest = new List<int>();

                int[] questFlag = Program.ingameData.QuestFlag;
                for (int i = 0; i < questFlag.Length; i++)
                {
                    if (questFlag[i] == 0) acceptableQuest.Add(i);
                    else if (questFlag[i] == 1) processingQuest.Add(i);
                    else if (questFlag[i] == 2) completedQuest.Add(i);
                }

                Console.WriteLine(" < < 길드 > >\n");
                Console.WriteLine(" - 퀘스트 접수 창구\n\n");

                if (acceptableQuest.Count == 0 && processingQuest.Count == 0 && completedQuest.Count == 0)
                {
                    string[] comment = new string[] { "안내인 : 지금은 수행할 수 있는 퀘스트가 없네요.", "안내인 : 다음에 다시 오시겠어요?" };
                    ScenePreset.Conversation(comment, keyController);
                    next = Scenes.Guild_Hall;
                    return true;
                }

                //퀘스트 디스플레이
                List<int> allQuest = new List<int>();
                foreach (int q in completedQuest)
                {
                    allQuest.Add(q);

                    Quest quest = QuestDatabase.GetQuest((QuestCode)q);
                    Console.Write($"{allQuest.Count}. ( ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("!");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" ) {quest.name}");
                }
                foreach (int q in acceptableQuest)
                {
                    allQuest.Add(q);

                    Quest quest = QuestDatabase.GetQuest((QuestCode)q);
                    Console.Write($"{allQuest.Count}. ( ");
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("?");
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.WriteLine($" ) {quest.name}");
                }
                foreach (int q in processingQuest)
                {
                    allQuest.Add(q);

                    Quest quest = QuestDatabase.GetQuest((QuestCode)q);
                    Console.WriteLine($"{allQuest.Count}. (...) {quest.name}");
                }

                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.X };
                bool loop2 = true;
                while (loop2)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                    switch (keyInput)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.D2:
                        case ConsoleKey.D3:
                        case ConsoleKey.D4:
                        case ConsoleKey.D5:
                            int selectedIdx = keyInput - ConsoleKey.D1;
                            if (selectedIdx < allQuest.Count)
                            {
                                Console.SetCursorPosition(0, 5);
                                for (int i = 0; i < 20; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 5);

                                Quest quest = QuestDatabase.GetQuest((QuestCode)allQuest[selectedIdx]);
                                if (questFlag[allQuest[selectedIdx]] == 0) //퀘스트 받기
                                {
                                    ScenePreset.Conversation(quest.initialScript.ToArray(), keyController);
                                    questFlag[allQuest[selectedIdx]] = 1;
                                }
                                else if (questFlag[allQuest[selectedIdx]] == 1) //퀘스트 완료 못할 때
                                {
                                    ScenePreset.Conversation(quest.processingScript, keyController);
                                }
                                else if (questFlag[allQuest[selectedIdx]] == 2) //퀘스트 완료
                                {
                                    ScenePreset.Conversation(quest.completScript, keyController);
                                    questFlag[allQuest[selectedIdx]] = 3;

                                    // 보상 지급
                                    Program.playerData.Gold += quest.goldReward;
                                    for (int i = 0; i < quest.gearReward.Length; i++)
                                    {
                                        Program.playerData.invenGear.Add(new WorldGear(quest.gearReward[i]));
                                    }
                                    for (int i = 0; i < quest.potionReward.Length; i++)
                                    {
                                        bool find = false;
                                        for (int j = 0; j < Program.playerData.invenPotion.Count; j++)
                                        {
                                            if (Program.playerData.invenPotion[j].potion == quest.potionReward[i].potion)
                                            {
                                                find = true;
                                                Program.playerData.invenPotion[j].stack += quest.potionReward[i].stack;
                                                break;
                                            }
                                        }
                                        if (find == false) Program.playerData.invenPotion.Add(quest.potionReward[i]);
                                    }
                                }
                            }
                            loop2 = false;
                            break;

                        case ConsoleKey.X:
                            loop2 = false;
                            loop = false;
                            break;
                    }
                }
            }

            next = Scenes.Guild_Hall;
            return true;
        }

        //모험가 모집
        public static void GuildMercenary()
        {

        }
    }
}
