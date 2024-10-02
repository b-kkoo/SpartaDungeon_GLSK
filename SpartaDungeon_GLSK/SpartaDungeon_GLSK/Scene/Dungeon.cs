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
            stageSignBoard[3] = "< 이계의 틈새 >";
            stageSignBoard[4] = "< ? ? ? >";

            string[][] stageSign = new string[5][];
            stageSign[0] = new string[3]; // 배열 수는 소단위 스테이지 수를 나타냄
            stageSign[0][0] = "1. 소굴 입구";
            stageSign[0][1] = "2. 지하 공동";
            stageSign[0][2] = "3. 우두머리의 거처";

            //스테이지 계속 추가 (소단위 최대 5개)


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

            //특수 스테이지 클리어 시 분기
            switch (selectedStage)
            {
                case 02: // < 고블린 소굴 > - 3. 우두머리의 거처
                    if (stageUnlock == 0)
                    {
                        Program.ingameData.DungeonUnlock = 1;
                    }
                    next = Scenes.Town_Default;
                    break;

                default:
                    next = Scenes.Town_Default;
                    break;
            }

            return true;
        }
    }
}
