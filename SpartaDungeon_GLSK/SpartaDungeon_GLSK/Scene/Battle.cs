

using SpartaDungeon_GLSK.Data;
using static SpartaDungeon_GLSK.Scene.BattleScene;
using static System.Net.Mime.MediaTypeNames;

namespace SpartaDungeon_GLSK.Scene
{
    //전투 관련 씬
    public class BattleScene
    {
        //배틀 진행 정보
        public class BattleTable
        {
            //아군 진영
            public PlayerData[] Ally;
            //적군 진영
            public WorldMonster[] Hostile;

            public BattleTable(PlayerData playerData, WorldMonster[] enemies)
            {
                Ally = new PlayerData[1];
                Ally[0] = playerData;
                Hostile = enemies;
            }
        }
        private static BattleTable battleTable;
             
        public static bool TutorialBattle(out Scenes next, KeyController keyController)
        {
            MonsterCode[] enemies = new MonsterCode[] { MonsterCode.CommonMonster1, MonsterCode.CommonMonster2 };
            battleTable = new BattleTable(Program.playerData/*나중에 다대다 전투 구현 가능하면 PlayerData에 함수 추가*/, MonsterData.GetWorldMonsters(enemies));

            ConsoleKey keyInput;
            int cheatActivated;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            int screenTop = Console.GetCursorPosition().Top; //전투가 이루어지는 화면 가장 꼭대기
            bool loop = true;
            while (loop)
            {   
                // LV 00  몬스터
                // HP 000 / 000
                // AP 000 %
                //
                //
                // LV 00  플레이어
                // HP 000 / 000
                // AP 000 %
                //
                // ㅇㅇㅇ의 턴!
                // 
                // 1. 전투 스킬
                // 2. 아이템
                
                if (battleTable.Hostile[0].isAlive)
                {
                    Console.SetCursorPosition(0, screenTop);
                    Console.WriteLine($"LV {battleTable.Hostile[0].monster.level}   {battleTable.Hostile[0].monster.name}");
                    Console.SetCursorPosition(0, screenTop + 1);
                    Console.WriteLine($"HP {battleTable.Hostile[0].currentHp} / {battleTable.Hostile[0].monster.hp}");
                }
                if (battleTable.Hostile[1].isAlive)
                {
                    Console.SetCursorPosition(50, screenTop);
                    Console.WriteLine($"LV {battleTable.Hostile[1].monster.level}   {battleTable.Hostile[1].monster.name}");
                    Console.SetCursorPosition(50, screenTop + 1);
                    Console.WriteLine($"{battleTable.Hostile[1].currentHp} / {battleTable.Hostile[1].monster.hp}");
                }
                Console.SetCursorPosition(0, screenTop + 5);
                Console.WriteLine($"LV {battleTable.Ally[0].Lv}   {battleTable.Ally[0].Name}");
                Console.SetCursorPosition(0, screenTop + 6);
                Console.WriteLine($"{battleTable.Ally[0].CurrentHp} / {battleTable.Ally[0].Hp}");

                Console.SetCursorPosition(0, screenTop + 50);
                Console.SetCursorPosition(0, screenTop);
                Console.SetCursorPosition(0, screenTop + 9);

                GetPlayerOrder(battleTable.Ally[0], screenTop, keyController, out int a, out int b, out int c);

                bool loop2 = true;
                while (loop2)
                {
                    keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                    switch (keyInput)
                    {
                        case ConsoleKey.Z:
                            battleTable.Hostile[0].currentHp -= 10;
                            if (battleTable.Hostile[0].currentHp <= 0) battleTable.Hostile[0].isAlive = false;
                            battleTable.Hostile[1].currentHp -= 10;
                            if (battleTable.Hostile[1].currentHp <= 0) battleTable.Hostile[1].isAlive = false;
                            if (battleTable.Hostile[0].isAlive == false && battleTable.Hostile[1].isAlive == false)
                            {
                                next = Scenes.Start_TutoEnd;
                                return true;
                            }
                            loop2 = false;
                            break;
                    }
                }
            }

            next = Scenes.Start_TutoEnd;
            return true;
        }


        //플레이어 턴에 행동명령 받기   selectedAct : 0-스킬사용 1-아이템사용   selectedIdx : 사용항목인덱스   selectedTarget : 사용대상
        private static void GetPlayerOrder(PlayerData playerData, int screenTop, KeyController keyController, out int selectedAct, out int selectedIdx, out int selectedTarget)
        {
            selectedAct = 0;
            selectedIdx = 0;
            selectedTarget = 0;

            ConsoleKey keyInput;
            int cheatActivated;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            int orderState = 0; // 0:행동 선택, 1:전투 스킬 선택, 2:스킬 대상 선택, 3:사용 아이템 선택, 4:아이템 대상 선택
            int skillTab = 0; //리스트를 최대 5개씩만 띄워 줄거기 때문에 현재 탭 위치를 알고 있어야 함
            int skillNum = playerData.skillList.Count;
            int potionTab = 0;
            int potionNum = playerData.invenPotion.Count;

            bool loop = true;
            while (loop)
            {
                Console.SetCursorPosition(0, screenTop + 9);
                Console.WriteLine($"{playerData.Name}의 턴!");

                //10 ~ 16줄 Clear
                for (int i = 10; i <= 16; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }


                //행동 선택
                if (orderState == 0)
                {
                    Console.SetCursorPosition(0, screenTop + 10);
                    Console.WriteLine("");
                    Console.WriteLine($"1. 전투 스킬");
                    Console.WriteLine($"2. 아이템");

                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2 };
                    bool loop2 = true;
                    while (loop2)
                    {
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                        switch (keyInput)
                        {
                            case ConsoleKey.D1:
                                //스킬 선택으로 이동
                                orderState = 1;
                                loop2 = false;
                                break;
                            case ConsoleKey.D2:
                                //아이템 선택으로 이동
                                orderState = 3;
                                loop2 = false;
                                break;
                        }
                    }
                }

                //전투 스킬 선택
                else if (orderState == 1)
                {
                    Console.SetCursorPosition(0, screenTop + 10);
                    Console.WriteLine("스킬 선택");

                    bool tabActivate = (skillNum > 5);

                    int dispSkillNum = skillNum - skillTab;
                    if (dispSkillNum > 5) dispSkillNum = 5; //한번에 표시할 스킬 수 5개로 제한
                    for (int i = 0; i < dispSkillNum; i++)
                    {
                        Console.WriteLine($"{i + 1}. {PSkillDatabase.GetPSkill(playerData.skillList[skillTab + i]).skillName}");
                    }
                    if (dispSkillNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispSkillNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Tab, ConsoleKey.X };
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
                                selectedIdx = skillTab + (keyInput - ConsoleKey.D1); //선택 스킬 인덱스
                                if (selectedIdx < playerData.skillList.Count)
                                {
                                    PSkill seletedSkill = PSkillDatabase.GetPSkill(playerData.skillList[selectedIdx]);
                                    if (seletedSkill.isSplash == true)
                                    {
                                        //선택 확정
                                        selectedAct = 0;
                                        selectedTarget = 0;
                                        loop2 = false;
                                        loop = false;
                                    }
                                    else
                                    {
                                        //대상 선택으로 이동
                                        orderState = 2;
                                        loop2 = false;
                                    }
                                }
                                break;
                            case ConsoleKey.Tab:
                                if (tabActivate == true)
                                {
                                    //다음 리스트 보기
                                    if (skillTab + 5 >= skillNum) skillTab = 0;
                                    else skillTab += 5;
                                    loop2 = false;
                                }
                                break;
                            case ConsoleKey.X:
                                //행동 선택으로 되돌아가기
                                orderState = 0;
                                loop2 = false;
                                break;
                        }
                    }
                }

                //스킬 대상 선택(적대 몬스터는 무조건 5개 이하임)
                else if (orderState == 2)
                {
                    Console.SetCursorPosition(0, screenTop + 10);
                    Console.WriteLine($"스킬 대상 선택 - {PSkillDatabase.GetPSkill(playerData.skillList[selectedIdx]).skillName}");

                    int dispTargetNum = 0;
                    Dictionary<int, int> aliveMonsterIdx = new Dictionary<int, int>(); //살아있는 몬스터의 실제 인덱스<표시된 인덱스, Hostile 내 인덱스>
                    for (int i = 0; i < battleTable.Hostile.Length; i++)
                    {
                        if (battleTable.Hostile[i].isAlive == true)
                        {
                            aliveMonsterIdx.Add(dispTargetNum, i);
                            Console.WriteLine($"{++dispTargetNum}. {battleTable.Hostile[i].monster.name}");
                        }
                    }
                    if (dispTargetNum == 1) Console.WriteLine($"(1 : 선택, X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispTargetNum} : 선택, X : 취소)");

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
                                if (keyInput - ConsoleKey.D1 < dispTargetNum)
                                {
                                    aliveMonsterIdx.TryGetValue(keyInput - ConsoleKey.D1, out selectedTarget); //선택된 대상 인덱스
                                    //선택 확정
                                    selectedAct = 0;
                                    loop2 = false;
                                    loop = false;
                                }
                                break;
                            case ConsoleKey.X:
                                //스킬 선택으로 되돌아가기
                                orderState = 1;
                                loop2 = false;
                                break;
                        }
                    }
                }

                //아이템 선택
                else if (orderState == 3)
                {
                    Console.SetCursorPosition(0, screenTop + 10);
                    
                    if (potionNum == 0) //아이템이 하나도 없는 경우(취소하여 행동 선택으로 돌아가기)
                    {
                        Console.WriteLine("사용할 수 있는 아이템이 없습니다!");
                        Console.WriteLine("(X : 취소)");

                        keyFilter = new ConsoleKey[] { ConsoleKey.X };
                        while (true)
                        {
                            keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                            if (keyInput == ConsoleKey.X)
                            {
                                break;
                            }
                        }
                        orderState = 0;
                        continue;
                    }
                    
                    Console.WriteLine("아이템 선택");

                    bool tabActivate = (potionNum > 5);

                    int dispPotionNum = potionNum - potionTab;
                    if (dispPotionNum > 5) dispPotionNum = 5; //한번에 표시할 아이템 수 5개로 제한
                    for (int i = 0; i < dispPotionNum; i++)
                    {
                        Console.WriteLine($"{i + 1}. {PotionDatabase.GetPotion(playerData.invenPotion[potionTab + i].Key).name} X {playerData.invenPotion[potionTab + i].Value}");
                    }
                    if (dispPotionNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispPotionNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Tab, ConsoleKey.X };
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
                                selectedIdx = potionTab + (keyInput - ConsoleKey.D1); //선택 아이템 인덱스
                                if (selectedIdx < playerData.invenPotion.Count)
                                {
                                    //대상 선택으로 이동
                                    orderState = 4;
                                    loop2 = false;
                                }
                                break;
                            case ConsoleKey.Tab:
                                if (tabActivate == true)
                                {
                                    //다음 리스트 보기
                                    if (potionTab + 5 >= potionNum) potionTab = 0;
                                    else potionTab += 5;
                                    loop2 = false;
                                }
                                break;
                            case ConsoleKey.X:
                                //행동 선택으로 되돌아가기
                                orderState = 0;
                                loop2 = false;
                                break;
                        }
                    }
                }

                //아이템 대상 선택(아군 유닛은 무조건 5개 이하임)
                else
                {
                    Console.SetCursorPosition(0, screenTop + 10);
                    Console.WriteLine($"아이템 대상 선택 - {PotionDatabase.GetPotion(playerData.invenPotion[selectedIdx].Key).name}");

                    int dispTargetNum = 0;
                    Dictionary<int, int> aliveAllyIdx = new Dictionary<int, int>(); //살아있는 아군의 실제 인덱스<표시된 인덱스, Ally 내 인덱스>
                    for (int i = 0; i < battleTable.Ally.Length; i++)
                    {
                        if (battleTable.Ally[i].IsAlive == true)
                        {
                            aliveAllyIdx.Add(dispTargetNum, i);
                            Console.WriteLine($"{++dispTargetNum}. {battleTable.Ally[i].Name}");
                        }
                    }
                    if (dispTargetNum == 1) Console.WriteLine($"(1 : 선택, X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispTargetNum} : 선택, X : 취소)");

                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Z };
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
                                if (keyInput - ConsoleKey.D1 < dispTargetNum)
                                {
                                    aliveAllyIdx.TryGetValue(keyInput - ConsoleKey.D1, out selectedTarget); //선택된 대상 인덱스
                                    //선택 확정
                                    selectedAct = 1;
                                    loop2 = false;
                                    loop = false;
                                }
                                break;
                            case ConsoleKey.X:
                                //스킬 선택으로 되돌아가기
                                orderState = 1;
                                loop2 = false;
                                break;
                        }
                    }
                }
            }
        }

        //플레이어 캐릭터 액션 수행   selectedAct : 0-스킬사용 1-아이템사용   selectedIdx : 사용항목인덱스   selectedTarget : 사용대상
        private static void DoPlayerAction(PlayerData playerData, int screenTop, KeyController keyController, int selectedAct, int selectedIdx, int selectedTarget)
        {
            ConsoleKey keyInput;
            int cheatActivated;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            //10 ~ 16줄 Clear
            for (int i = 10; i <= 16; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }


            // 스킬 사용
            if (selectedAct == 0)
            {
                Console.SetCursorPosition(0,0);
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
        }
    }
}
