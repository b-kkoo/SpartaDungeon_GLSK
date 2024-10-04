using SpartaDungeon_GLSK.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Scene
{
    public class GuildScene
    {
        private static List<Mercenary> mercenaries;
        //고용 모험가
        private class Mercenary
        {
            public bool Purchased { get; set; }

            public string Name { get; }
            public JobCode PClass { get; }
            public string PClassName { get; }
            public int Lv { get; }

            public int Price { get; }

            public Mercenary(string name, JobCode pClass, string pClassName, int lv, int price)
            {
                Purchased = false;

                Name = name;
                PClass = pClass;
                PClassName = pClassName;

                Lv = lv;

                Price = price;
            }
        }


        public static void Set()
        {
            mercenaries = new List<Mercenary>();

            mercenaries.Add(new Mercenary("도도", JobCode.Warrior, "전사", 1, 0));
            mercenaries.Add(new Mercenary("바우", JobCode.Archer, "궁수", 1, 0));
            mercenaries.Add(new Mercenary("델리키", JobCode.Mage, "마법사", 1, 0));
            mercenaries.Add(new Mercenary("듀크", JobCode.Warrior, "전사", 5, 300));
            mercenaries.Add(new Mercenary("파칼", JobCode.Archer, "궁수", 5, 300));
            mercenaries.Add(new Mercenary("콜라비", JobCode.Mage, "마법사", 5, 300));
        }

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

                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.X };
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
                        case ConsoleKey.D6:
                        case ConsoleKey.D7:
                        case ConsoleKey.D8:
                        case ConsoleKey.D9:
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
        public static bool GuildMercenary(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            bool loop = true;
            while (loop)
            {
                Console.Clear();

                List<int> waitingMercenary = new List<int>();
                for (int i = 0; i < mercenaries.Count; i++)
                {
                    mercenaries[i].Purchased = Program.ingameData.MercenaryPurchased[i];
                    if (Program.ingameData.MercenaryPurchased[i] == false) waitingMercenary.Add(i);
                }

                Console.WriteLine(" < < 길드 > >\n");
                Console.WriteLine(" - 모험가 모집\n\n");

                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"소지금 : {Program.playerData.Gold} Gold");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 5);

                if (waitingMercenary.Count == 0)
                {
                    string[] comment = new string[] { "영입할 수 있는 모험가가 더이상 없습니다." };
                    ScenePreset.Conversation(comment, keyController);
                    next = Scenes.Guild_Hall;
                    return true;
                }

                //모험가 구매 목록 디스플레이
                int waitingNum = 0;
                for (int i = 0; i < waitingMercenary.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {mercenaries[waitingMercenary[i]].Name}, {mercenaries[waitingMercenary[i]].PClassName}, LV {mercenaries[waitingMercenary[i]].Lv} : {mercenaries[waitingMercenary[i]].Price} Gold");
                }

                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.X };
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
                        case ConsoleKey.D6:
                        case ConsoleKey.D7:
                        case ConsoleKey.D8:
                        case ConsoleKey.D9:
                            int selectedIdx = keyInput - ConsoleKey.D1;
                            if (selectedIdx < waitingMercenary.Count)
                            {
                                Mercenary mercenary = mercenaries[waitingMercenary[selectedIdx]];

                                Console.SetCursorPosition(0, 5);
                                for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 0);
                                Console.SetCursorPosition(0, 5);
                                Console.WriteLine($"{selectedIdx + 1}. {mercenary.Name}, {mercenary.PClassName}, LV {mercenary.Lv} : {mercenary.Price} Gold");

                                Console.WriteLine("\n해당 모험가를 영입하시겠습니까?");
                                Console.WriteLine("\n                                 (Z : 예,  X : 아니오)");

                                keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                                bool loop3 = true;
                                while (loop3)
                                {
                                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                                    switch (keyInput)
                                    {
                                        case ConsoleKey.Z:
                                            Console.SetCursorPosition(0, 5);
                                            for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                            Console.SetCursorPosition(0, 0);
                                            Console.SetCursorPosition(0, 5);

                                            if (Program.playerData.Gold >= mercenary.Price) //구매 완
                                            {
                                                //골드 차감
                                                Program.playerData.Gold -= mercenary.Price;
                                                //정보 갱신
                                                Program.ingameData.MercenaryPurchased[waitingMercenary[selectedIdx]] = true;

                                                //팀에 모험가 추가
                                                PlayerUnitData playerUnitData = new PlayerUnitData();
                                                playerUnitData.Name = mercenary.Name;
                                                playerUnitData.PClass = mercenary.PClass;
                                                playerUnitData.SetLv1();
                                                for (int i = 1; i < mercenary.Lv; i++) playerUnitData.LvUp();
                                                playerUnitData.Exp = 0;

                                                Program.playerData.team.Add(playerUnitData);

                                                //구매 메시지
                                                Console.WriteLine($"{mercenary.Name}을 영입하였습니다!");
                                                Console.WriteLine($"\n파티 정보에서 엔트리에 추가해 주세요.");
                                                Thread.Sleep(1000);
                                                keyController.GetUserInput(keyFilter, out cheatActivated);
                                            }
                                            else //골드 소지량 부족
                                            {
                                                Console.WriteLine("영입에 필요한 골드가 부족합니다.");
                                                Thread.Sleep(1000);
                                                keyController.GetUserInput(keyFilter, out cheatActivated);
                                            }
                                            loop3 = false;
                                            break;

                                        case ConsoleKey.X:
                                            loop3 = false;
                                            break;
                                    }
                                }
                                loop2 = false;
                            }
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


        //퀘스트
        public static bool GuildInn(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            bool loop = true;
            while (loop)
            {
                Console.Clear();

                Console.WriteLine(" < < 길드 > >\n");
                Console.WriteLine(" - 모험가 숙소\n\n");

                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"소지금 : {Program.playerData.Gold} Gold");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 5);

                Console.WriteLine("길드 숙소 관리인 : 휴식을 취하고 싶은 모험가분 있으신가요?");
                Console.WriteLine("                   50 Gold에 방을 내어드리겠습니다~");

                List<PlayerUnitData> team = Program.playerData.team;
                //팀원 목록 디스플레이
                Console.SetCursorPosition(0, 8);
                for (int i = 0; i < team.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {team[i].Name}, {team[i].CurrentHp,3} / {team[i].Hp,3}{(team[i].IsAlive == false ? "(기절)" : "")}");
                }

                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.X };
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
                        case ConsoleKey.D6:
                        case ConsoleKey.D7:
                        case ConsoleKey.D8:
                        case ConsoleKey.D9:
                            int selectedIdx = keyInput - ConsoleKey.D1;
                            if (selectedIdx < team.Count)
                            {
                                PlayerUnitData playerUnit = team[selectedIdx];

                                Console.SetCursorPosition(0, 8);
                                for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 0);
                                Console.SetCursorPosition(0, 8);
                                Console.WriteLine($"    {team[selectedIdx].Name}, {team[selectedIdx].CurrentHp,3} / {team[selectedIdx].Hp,3}{(team[selectedIdx].IsAlive == false ? "(기절)" : "")}");

                                Console.WriteLine("\n해당 모험가를 휴식 시키시겠습니까?");
                                Console.WriteLine("\n                                 (Z : 예,  X : 아니오)");

                                keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                                bool loop3 = true;
                                while (loop3)
                                {
                                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                                    switch (keyInput)
                                    {
                                        case ConsoleKey.Z:
                                            Console.SetCursorPosition(0, 5);
                                            for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                            Console.SetCursorPosition(0, 0);
                                            Console.SetCursorPosition(0, 5);

                                            if (Program.playerData.Gold >= 50) //구매 완
                                            {

                                                //골드 차감
                                                Program.playerData.Gold -= 50;

                                                //휴식 효과
                                                playerUnit.IsAlive = true;
                                                playerUnit.CurrentHp = playerUnit.Hp;

                                                //구매 메시지
                                                Console.WriteLine($"길드 숙소 관리인 : {playerUnit.Name}님, 편히 쉬세요~");
                                                Thread.Sleep(1000);
                                                keyController.GetUserInput(keyFilter, out cheatActivated);
                                            }
                                            else //골드 소지량 부족
                                            {
                                                Console.WriteLine($"길드 숙소 관리인 : 돈이 부족한 것 같아요~");
                                                Thread.Sleep(1000);
                                                keyController.GetUserInput(keyFilter, out cheatActivated);
                                            }
                                            loop3 = false;
                                            break;

                                        case ConsoleKey.X:
                                            loop3 = false;
                                            break;
                                    }
                                }
                                loop2 = false;
                            }
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
    }
}
