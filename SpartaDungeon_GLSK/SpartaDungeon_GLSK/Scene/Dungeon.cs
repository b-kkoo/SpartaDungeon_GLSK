using SpartaDungeon_GLSK.Data;
using static SpartaDungeon_GLSK.Scene.BattleScene;

namespace SpartaDungeon_GLSK.Scene
{
    internal class DungeonScene
    {
        

        public static bool ChallengeToDungeon(out Scenes next, KeyController keyController)
        {
            Console.WriteLine("< < 던  전 > >\n");
            string[] stageSignBoard = new string[5];// 배열 수는 대단위 스테이지 수를 나타냄
            stageSignBoard[0] = "< 고블린 소굴 >";
            stageSignBoard[1] = "< 타락한 자들의 땅 >";
            stageSignBoard[2] = "< 용의 둥지 >";
            stageSignBoard[3] = "< 마계의 틈새 >";
            stageSignBoard[4] = "< ? ? ? >";

            string[][] stageSign = new string[5][];
            stageSign[0] = new string[3]; // 배열 수는 소단위 스테이지 수를 나타냄
            stageSign[0][0] = "1. 소굴 입구";
            stageSign[0][1] = "2. 지하 공동";
            stageSign[0][2] = "3. 우두머리의 거처";

            //스테이지 계속 추가 (소단위 최대 5개)
            stageSign[1] = new string[4]; // 배열 수는 소단위 스테이지 수를 나타냄
            stageSign[1][0] = "1. 타락한 자들의 땅 - 입구";
            stageSign[1][1] = "2. 타락한 자들의 땅 - ";
            stageSign[1][2] = "3. 타락한 자들의 땅 - 최심부";
            stageSign[1][3] = "4. 타락한 자들의 땅 - 네크로멘서의 연구실";

            stageSign[2] = new string[4]; // 배열 수는 소단위 스테이지 수를 나타냄
            stageSign[2][0] = "1. 용의 둥지 입구";
            stageSign[2][1] = "2. 성룡의 레어";
            stageSign[2][2] = "3. 고룡의 레어";
            stageSign[2][3] = "4. 드래곤 로드의 레어";

            stageSign[3] = new string[5]; // 배열 수는 소단위 스테이지 수를 나타냄
            stageSign[3][0] = "1. 마계의 틈새 입구";
            stageSign[3][1] = "2. 마의 계곡";
            stageSign[3][2] = "3. 마왕성 - 입구";
            stageSign[3][3] = "4. 마왕성 - 연회장";
            stageSign[3][4] = "5. 마왕성 - 마왕의 방";

            stageSign[4] = new string[1]; // 배열 수는 소단위 스테이지 수를 나타냄
            stageSign[4][0] = "< ? ? ? >";


            ConsoleKey keyInput;
            int cheatActivated;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            int stageUnlock = Program.ingameData.DungeonUnlock;
            int stageTab = 0;
            int selectedIdx = 0;
            int selectedStage = 0;
            bool confirm = false;

            bool loop = true;
            while (loop)
            {
                Console.SetCursorPosition(0, 2);
                for (int i = 9; i <= 20; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }
                Console.SetCursorPosition(0, 2);
                Console.WriteLine(stageSignBoard[stageTab]);

                Console.SetCursorPosition(0, 4);
                for (int i = 0; i < stageSign[stageTab].Length; i++)
                {
                    Console.WriteLine(stageSign[stageTab][i]);
                }
                string infoSelect = $"1 ~ {stageSign[stageTab].Length} : 선택";
                Console.WriteLine($"\n                      ({infoSelect}{(stageUnlock == 0 ? "" : ", Tab : 다음 스테이지")})");

                if (confirm == false)
                {
                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.X, ConsoleKey.Tab };

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
                                selectedIdx = keyInput - ConsoleKey.D1;
                                if (selectedIdx < stageSign[stageTab].Length)
                                {
                                    //스테이지 선택
                                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                                    Console.WriteLine(new string(' ', Console.WindowWidth));
                                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                                    Console.WriteLine($"{stageSignBoard[stageTab]} - {stageSign[stageTab][selectedIdx]} 에 도전하시겠습니까?");
                                    Console.WriteLine("\n                        (Z : 확인, X : 취소)");
                                    confirm = true;
                                    loop2 = false;
                                }
                                break;

                            case ConsoleKey.X:
                                next = Scenes.Town_Default;
                                return true;

                            case ConsoleKey.Tab:
                                if (stageUnlock > 0)
                                {
                                    if (stageTab >= stageUnlock) stageTab = 0;
                                    else stageTab++;
                                    loop2 = false;
                                }
                                break;
                        }
                    }
                }
                if (confirm == true)
                {
                    keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X};

                    bool loop2 = true;
                    while (loop2)
                    {
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        switch (keyInput)
                        {
                            case ConsoleKey.Z:
                                selectedStage = stageTab * 10 + selectedIdx; //십의 자리는 대단위 스테이지, 일의 자리는 소단위 스테이지
                                loop2 = false;
                                loop = false;
                                break;

                            case ConsoleKey.X:
                                confirm = false;
                                loop2 = false;
                                break;
                        }
                    }
                }
            }

            switch (selectedStage) //십의 자리는 대단위 스테이지, 일의 자리는 소단위 스테이지
            {
                case 00: // < 고블린 소굴 > - 1. 소굴 입구
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Goblin, MonsterCode.Comm_Hobgoblin, MonsterCode.Comm_Goblin };
                    _battleComment = new string[] { "고블린 무리와 마주쳤다!" };
                    _goldReward = 75;
                    break;

                case 01: // < 고블린 소굴 > - 2. 지하 공동
                    if (Program.ingameData.QuestFlag[(int)QuestCode.dungeonStage0_1] != 1)
                    {
                        _enemies = new MonsterCode[] { MonsterCode.Comm_Goblin, MonsterCode.Comm_Hobgoblin, MonsterCode.Spec_GoblinMage };
                        _battleComment = new string[] { "고블린 무리와 마주쳤다!" };
                        _goldReward = 90;
                    }
                    else //만드라고라 퀘스트
                    {
                        _enemies = new MonsterCode[] { MonsterCode.Spec_GoblinMage, MonsterCode.Comm_Hobgoblin, MonsterCode.Spec_GoblinMage };
                        _battleComment = new string[] { "만드라고라를 재배 중이던 고블린 메이지와 마주쳤다!", "고블린 메이지는 싸움을 걸어왔다!" };
                        _goldReward = 110;
                    }
                    break;

                case 02: // < 고블린 소굴 > - 3. 우두머리의 거처
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Goblin, MonsterCode.Comm_Hobgoblin, MonsterCode.Boss_GoblinLord, MonsterCode.Spec_GoblinMage };
                    _battleComment = new string[] { "우두머리 고블린과 마주쳤다!" };
                    _goldReward = 105;
                    break;

                case 10: // < 타락한 자들의 땅 > - 1. 입구
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Skeleton, MonsterCode.Comm_Ghoul, MonsterCode.Comm_Skeleton };
                    _battleComment = new string[] { "언데드 무리와 마주쳤다!" };
                    _goldReward = 95;
                    break;

                case 11: // < 타락한 자들의 땅 > - 2. 중심부
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Skeleton, MonsterCode.Comm_Ghoul, MonsterCode.Comm_DeathKnight, MonsterCode.Comm_Skeleton };
                    _battleComment = new string[] { "언데드 무리와 마주쳤다!" };
                    _goldReward = 105;
                    break;

                case 12: // < 타락한 자들의 땅 > - 3. 최심부
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Ghoul, MonsterCode.Spec_Lich, MonsterCode.Comm_DeathKnight, MonsterCode.Comm_Skeleton };
                    _battleComment = new string[] { "언데드 무리와 마주쳤다!" };
                    _goldReward = 120;
                    break;

                case 13: // < 타락한 자들의 땅 > - 4. 네크로멘서의 연구실
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Ghoul, MonsterCode.Spec_Lich, MonsterCode.Boss_Necromancer, MonsterCode.Comm_DeathKnight, MonsterCode.Comm_Skeleton };
                    _battleComment = new string[] { "네크로멘서와 마주쳤다!" };
                    _goldReward = 135;
                    break;

                case 20: // < 용의 둥지 > - 1. 용의 둥지 입구
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Hatchling, MonsterCode.Comm_Hatchling, MonsterCode.Comm_Hatchling };
                    _battleComment = new string[] { "드래곤 무리와 마주쳤다!" };
                    _goldReward = 120;
                    break;

                case 21: // < 용의 둥지 > - 2. 성룡의 레어
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Hatchling, MonsterCode.Comm_Wyvern, MonsterCode.Comm_Hatchling };
                    _battleComment = new string[] { "드래곤 무리와 마주쳤다!" };
                    _goldReward = 135;
                    break;

                case 22: // < 용의 둥지 > - 3. 고룡의 레어
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Wyvern, MonsterCode.Spec_Dragon, MonsterCode.Comm_Wyvern, MonsterCode.Comm_Hatchling };
                    _battleComment = new string[] { "드래곤 무리와 마주쳤다!" };
                    _goldReward = 150;
                    break;

                case 23: // < 용의 둥지 > - 4. 드래곤 로드의 레어
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Wyvern, MonsterCode.Spec_Dragon, MonsterCode.Boss_AncientDragon, MonsterCode.Comm_Wyvern, MonsterCode.Comm_Hatchling };
                    _battleComment = new string[] { "에인션트 드래곤과 마주쳤다!" };
                    _goldReward = 165;
                    break;

                case 30: // < 마계의 틈새 > - 1. 틈새 입구
                    _enemies = new MonsterCode[] { MonsterCode.Comm_HellHound, MonsterCode.Comm_Demon1, MonsterCode.Comm_HellHound, MonsterCode.Comm_HellHound };
                    _battleComment = new string[] { "마족 무리와 마주쳤다!" };
                    _goldReward = 150;
                    break;

                case 31: // < 마계의 틈새 > - 2. 마의 계곡
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Demon1, MonsterCode.Comm_Demon1, MonsterCode.Comm_Demon2, MonsterCode.Comm_HellHound };
                    _battleComment = new string[] { "마족 무리와 마주쳤다!" };
                    _goldReward = 165;
                    break;

                case 32: // < 마계의 틈새 > - 3. 마왕성 - 입구
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Demon1, MonsterCode.Comm_Demon1, MonsterCode.Comm_Demon3, MonsterCode.Comm_Demon2, MonsterCode.Comm_HellHound };
                    _battleComment = new string[] { "마족 무리와 마주쳤다!" };
                    _goldReward = 180;
                    break;

                case 33: // < 마계의 틈새 > - 4. 마왕성 - 연회장
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Demon1, MonsterCode.Spec_Cerberus, MonsterCode.Comm_Demon3, MonsterCode.Comm_Demon2, MonsterCode.Comm_Demon1 };
                    _battleComment = new string[] { "마족 무리와 마주쳤다!" };
                    _goldReward = 195;
                    break;

                case 34: // < 마계의 틈새 > - 5. 마왕성 - 마왕의 방
                    _enemies = new MonsterCode[] { MonsterCode.Comm_Demon2, MonsterCode.Boss_Diablo, MonsterCode.Comm_Demon3, MonsterCode.Comm_Demon3, MonsterCode.Comm_Demon2 };
                    _battleComment = new string[] { "마왕과 마주쳤다!" };
                    _goldReward = 210;
                    break;

                case 40: // < ? ? ? > - 1. ? ? ?
                    _enemies = new MonsterCode[] { MonsterCode.Boss_AncientDragon, MonsterCode.Boss_GoblinLord, MonsterCode.Boss_Diablo, MonsterCode.Boss_Necromancer};
                    _battleComment = new string[] { "? ? ?" };
                    _goldReward = 300;
                    break;


                    //스테이지 계속 추가
            }

            Console.Clear();
            // 전투 개시
            bool victory = LoadBattleScene(keyController);

            if (victory == false)
            {
                next = Scenes.Gameover_Default;
                return true;
            }

            //최고 주파던전 갱신
            if (Program.ingameData.DefeatHighestDungeonStage < selectedStage)
            {
                Program.ingameData.DefeatHighestDungeonStage = selectedStage;
            }

            //특수 스테이지 클리어 시 분기
            switch (selectedStage)
            {
                case 01: // < 고블린 소굴 > - 2. 지하 공동 (만드라고라 퀘스트)
                    if (Program.ingameData.QuestFlag[(int)QuestCode.dungeonStage0_1] == -1)
                    {
                        Program.ingameData.QuestFlag[(int)QuestCode.dungeonStage0_1] = 0;
                    }
                    else if (Program.ingameData.QuestFlag[(int)QuestCode.dungeonStage0_1] == 1)
                    {
                        Program.ingameData.QuestFlag[(int)QuestCode.dungeonStage0_1] = 2;
                    }
                    break;
                case 02: // < 고블린 소굴 > - 3. 우두머리의 거처 (스테이지 해금)
                    if (stageUnlock == 0)
                    {
                        Program.ingameData.DungeonUnlock = 1;
                    }
                    break;
            }

            next = Scenes.Town_Default;
            return true;
        }
    }
}
