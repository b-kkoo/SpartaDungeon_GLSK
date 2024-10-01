

using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using static SpartaDungeon_GLSK.MonsterData;
using static SpartaDungeon_GLSK.Scene.BattleScene;
using static System.Net.Mime.MediaTypeNames;

namespace SpartaDungeon_GLSK.Scene
{
    //전투 관련 씬
    public class BattleScene
    {
        //배틀 인덱스
        public static BattleStage battleStage;
        public enum BattleStage
        {
            Tutorial,
        }

        //배틀 진행 정보
        private static BattleTable battleTable;
        public class BattleTable
        {
            //아군 진영
            public PlayerData[] Ally;
            //적군 진영
            public WorldMonster[] Hostile;

            public BattleTable(PlayerData playerData, WorldMonster[] enemies)
            {
                Ally = new PlayerData[3];
                Ally[0] = playerData;
                Hostile = new WorldMonster[5];
                for (int i = 0; i < 5 && i < enemies.Length; i++)
                {
                    Hostile[i] = enemies[i];
                }
            }
        }

        public static bool LoadBattleScene(out Scenes next, KeyController keyController)
        {
            MonsterCode[] enemies = null;
            int screenTop = 0;
            bool victory = false;

            switch (battleStage)
            {
                case BattleStage.Tutorial:
                    enemies = new MonsterCode[2] { MonsterCode.TutoralMonster, MonsterCode.TutoralMonster };
                    Console.WriteLine("튜토리얼 전투");
                    screenTop = 2;
                    break;
            }

            victory = Battle(enemies, screenTop, keyController);

            EndBattleScene(out next, keyController);

            return true;
        }

        public static void EndBattleScene(out Scenes next, KeyController keyController)
        {
            switch (battleStage)
            {
                default:
                    next = Scenes.Town_Default;
                    break;
            }
        }

        //승패 여부 리턴
        private static bool Battle(MonsterCode[] enemies, int screenTop, KeyController keyController)
        {
            battleTable = new BattleTable(Program.playerData/*나중에 다대다 전투 구현 가능하면 PlayerData에 함수 추가*/, MonsterData.GetWorldMonsters(enemies));

            ConsoleKey keyInput;
            int cheatActivated;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            Queue<int> actionTurn = new Queue<int>(); //AP 100% 행동턴을 획듯한 유닛. (0~2 : 플레이어, 3~7 : 적)
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
                // MP 000 / 000
                // AP 000 %
                //
                // ㅇㅇㅇ의 턴!
                // 
                // 1. 전투 스킬
                // 2. 아이템

                DrawBattleField(screenTop);

                //대기 : AP 증가
                for (int i = 0; i < battleTable.Ally.Length; i++)
                {
                    if (battleTable.Ally[i] != null && battleTable.Ally[i].IsAlive)
                    {
                        battleTable.Ally[i].AP += battleTable.Ally[i].Speed * 0.01;
                        if (battleTable.Ally[i].AP >= 100)
                        {
                            battleTable.Ally[i].AP = 100;
                            actionTurn.Enqueue(i);
                        }
                    }
                }
                for (int i = 0; i < battleTable.Hostile.Length; i++)
                {
                    if (battleTable.Hostile[i] != null && battleTable.Hostile[i].isAlive)
                    {
                        battleTable.Hostile[i].AP += battleTable.Hostile[i].monster.speed * 0.01;
                        if (battleTable.Hostile[i].AP >= 100)
                        {
                            battleTable.Hostile[i].AP = 100;
                            actionTurn.Enqueue(3 + i);
                        }
                    }
                }

                //턴을 가진 유닛 행동 수행
                while (actionTurn.Count > 0)
                {
                    DrawBattleField(screenTop);

                    int actionUnit = actionTurn.Dequeue();
                    // 플레이어 턴
                    if (actionUnit < battleTable.Ally.Length)
                    {
                        if (battleTable.Ally[actionUnit] != null && battleTable.Ally[actionUnit].IsAlive)
                        {
                            //9 ~ 16줄 Clear
                            Console.SetCursorPosition(0, screenTop + 9);
                            for (int i = 9; i <= 16; i++)
                            {
                                Console.WriteLine(new string(' ', Console.WindowWidth));
                            }

                            // 차징한 스킬이 있을 경우
                            if (battleTable.Ally[actionUnit].Concentrating)
                            {
                                DoPlayerAction(battleTable.Ally[actionUnit], screenTop + 1, keyController, 0, battleTable.Ally[actionUnit].ReservedSkill, battleTable.Ally[actionUnit].ReservedTarget);
                            }
                            // 새로운 행동
                            else
                            {
                                GetPlayerOrder(battleTable.Ally[actionUnit], screenTop + 1, keyController, out int selectedAct, out int selectedIdx, out int selectedTarget);
                                DoPlayerAction(battleTable.Ally[actionUnit], screenTop + 1, keyController, selectedAct, selectedIdx, selectedTarget);
                            }

                            //0 ~ 16줄 Clear
                            Console.SetCursorPosition(0, screenTop);
                            for (int i = 0; i <= 16; i++)
                            {
                                Console.WriteLine(new string(' ', Console.WindowWidth));
                            }
                            DrawBattleField(screenTop);

                            // 게임 승리 여부 확인
                            bool victory = true;
                            for (int i = 0; i < battleTable.Hostile.Length; i++)
                            {
                                if (battleTable.Hostile[i] != null && battleTable.Hostile[i].isAlive)
                                {
                                    victory = false;
                                    break;
                                }
                            }
                            if (victory)
                            {
                                return true;
                            }

                            battleTable.Ally[actionUnit].AP = 0;
                        }
                    }
                    // 적 턴
                    else
                    {
                        actionUnit -= battleTable.Ally.Length;
                        if (battleTable.Hostile[actionUnit] != null && battleTable.Hostile[actionUnit].isAlive)
                        {
                            //9 ~ 16줄 Clear
                            Console.SetCursorPosition(0, screenTop + 9);
                            for (int i = 9; i <= 16; i++)
                            {
                                Console.WriteLine(new string(' ', Console.WindowWidth));
                            }

                            // 차징한 스킬이 있을 경우
                            if (battleTable.Hostile[actionUnit].concentrating)
                            {
                                DoHostileAction(battleTable.Hostile[actionUnit], screenTop + 1, keyController, battleTable.Hostile[actionUnit].reservedSkill, battleTable.Hostile[actionUnit].reservedTarget);
                            }
                            // 새로운 행동
                            else
                            {
                                GetHostileOrder(battleTable.Hostile[actionUnit], out int selectedIdx, out int selectedTarget);
                                DoHostileAction(battleTable.Hostile[actionUnit], screenTop + 1, keyController, selectedIdx, selectedTarget);
                            }

                            //0 ~ 16줄 Clear
                            Console.SetCursorPosition(0, screenTop);
                            for (int i = 0; i <= 16; i++)
                            {
                                Console.WriteLine(new string(' ', Console.WindowWidth));
                            }
                            DrawBattleField(screenTop);

                            // 게임 패배 여부 확인
                            bool defeat = true;
                            for (int i = 0; i < battleTable.Ally.Length; i++)
                            {
                                if (battleTable.Ally[i] != null && battleTable.Ally[i].IsAlive)
                                {
                                    defeat = false;
                                    break;
                                }
                            }
                            if (defeat)
                            {
                                return false;
                            }

                            battleTable.Hostile[actionUnit].AP = 0;
                        }
                    }
                }
            }

            return false;
        }

        private static void DrawBattleField(int screenTop)
        {
            for (int i = 0; i < 5; i++)
            {
                if (battleTable.Hostile[i] != null && battleTable.Hostile[i].isAlive)
                {
                    Console.SetCursorPosition(i * 30, screenTop);
                    Console.WriteLine($"LV {battleTable.Hostile[i].monster.level}   {battleTable.Hostile[i].monster.name}");
                    Console.SetCursorPosition(i * 30, screenTop + 1);
                    Console.WriteLine($"HP {battleTable.Hostile[i].currentHp} / {battleTable.Hostile[i].monster.hp}");
                    Console.SetCursorPosition(i * 30, screenTop + 2);
                    Console.WriteLine($"AP {Math.Floor(battleTable.Hostile[i].AP)} %{(battleTable.Hostile[i].concentrating ? " (집중)" : "")}");
                }
            }
            for (int i = 0; i < 3; i++)
            {
                if (battleTable.Ally[i] != null && battleTable.Ally[i].IsAlive)
                {
                    Console.SetCursorPosition(i * 50, screenTop + 5);
                    Console.WriteLine($"LV {battleTable.Ally[i].Lv}   {battleTable.Ally[i].Name}");
                    Console.SetCursorPosition(i * 50, screenTop + 6);
                    Console.WriteLine($"HP {battleTable.Ally[i].CurrentHp} / {battleTable.Ally[i].Hp}");
                    Console.SetCursorPosition(i * 50, screenTop + 7);
                    Console.WriteLine($"MP {battleTable.Ally[i].CurrentMp} / {battleTable.Ally[i].Mp}");
                    Console.SetCursorPosition(i * 50, screenTop + 8);
                    Console.WriteLine($"AP {Math.Floor(battleTable.Ally[i].AP)} %{(battleTable.Ally[i].Concentrating ? " (집중)" : "")}");
                }
            }
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
                //9 ~ 16줄 Clear
                Console.SetCursorPosition(0, screenTop + 9);
                for (int i = 9; i <= 16; i++)
                {
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                }

                Console.SetCursorPosition(0, screenTop + 9);
                Console.WriteLine($"{playerData.Name}의 턴!");

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

                    //스킬목록 디스플레이
                    int dispSkillNum = skillNum - skillTab;
                    if (dispSkillNum > 5) dispSkillNum = 5; //한번에 표시할 스킬 수 5개로 제한
                    for (int i = 0; i < dispSkillNum; i++)
                    {
                        Console.WriteLine($"{i + 1}. {PSkillDatabase.GetPSkill(playerData.skillList[skillTab + i]).skillName}");
                    }
                    if (dispSkillNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispSkillNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

                    //입력 처리
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
                                    if (playerData.CurrentMp < seletedSkill.mpConsum) //스킬시전에 필요한 마나가 부족한 경우
                                    {
                                        Console.SetCursorPosition(0, screenTop + 10);
                                        for (int i = 10; i <= 16; i++)
                                        {
                                            Console.WriteLine(new string(' ', Console.WindowWidth));
                                        }
                                        Console.WriteLine("MP가 부족합니다!");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                        loop2 = false;
                                    }
                                    else
                                    {
                                        if (seletedSkill.isSplash == true) //스킬이 전체공격인 경우
                                        {
                                            //선택 확정
                                            if (seletedSkill.isSplash) //차징 스킬인 경우
                                            {
                                                playerData.Concentrating = true;
                                                playerData.ReservedSkill = selectedIdx;
                                                playerData.ReservedTarget = 0;
                                            }
                                            selectedAct = 0;
                                            selectedTarget = 0;
                                            loop2 = false;
                                            loop = false;
                                        }
                                        else //스킬이 단일공격인 경우
                                        {
                                            //대상 선택으로 이동
                                            orderState = 2;
                                            loop2 = false;
                                        }
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
                    PSkill selectedSkill = PSkillDatabase.GetPSkill(playerData.skillList[selectedIdx]);
                    Console.WriteLine($"스킬 대상 선택 - {selectedSkill.skillName}");

                    //스킬 대상 목록 디스플레이
                    int dispTargetNum = 0;
                    Dictionary<int, int> aliveMonsterIdx = new Dictionary<int, int>(); //살아있는 몬스터의 실제 인덱스<표시된 인덱스, Hostile 내 인덱스>
                    for (int i = 0; i < battleTable.Hostile.Length; i++)
                    {
                        if (battleTable.Hostile[i] != null && battleTable.Hostile[i].isAlive == true)
                        {
                            aliveMonsterIdx.Add(dispTargetNum, i);
                            Console.WriteLine($"{++dispTargetNum}. {battleTable.Hostile[i].monster.name}");
                        }
                    }
                    if (dispTargetNum == 1) Console.WriteLine($"(1 : 선택, X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispTargetNum} : 선택, X : 취소)");

                    //입력 처리
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
                                    if (selectedSkill.isSplash) //차징 스킬인 경우
                                    {
                                        playerData.Concentrating = true;
                                        playerData.ReservedSkill = selectedIdx;
                                        playerData.ReservedTarget = selectedTarget;
                                    }
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

                    //아이템 목록 디스플레이
                    int dispPotionNum = potionNum - potionTab;
                    if (dispPotionNum > 5) dispPotionNum = 5; //한번에 표시할 아이템 수 5개로 제한
                    for (int i = 0; i < dispPotionNum; i++)
                    {
                        Console.WriteLine($"{i + 1}. {PotionDatabase.GetPotion(playerData.invenPotion[potionTab + i].Key).name} X {playerData.invenPotion[potionTab + i].Value}");
                    }
                    if (dispPotionNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispPotionNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

                    //입력 처리
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

                    //아이템 사용 대상 목록 디스플레이
                    int dispTargetNum = 0;
                    Dictionary<int, int> aliveAllyIdx = new Dictionary<int, int>(); //살아있는 아군의 실제 인덱스<표시된 인덱스, Ally 내 인덱스>
                    for (int i = 0; i < battleTable.Ally.Length; i++)
                    {
                        if (battleTable.Ally[i] != null && battleTable.Ally[i].IsAlive == true)
                        {
                            aliveAllyIdx.Add(dispTargetNum, i);
                            Console.WriteLine($"{++dispTargetNum}. {battleTable.Ally[i].Name}");
                        }
                    }
                    if (dispTargetNum == 1) Console.WriteLine($"(1 : 선택, X : 취소)");
                    else Console.WriteLine($"(1 ~ {dispTargetNum} : 선택, X : 취소)");

                    //입력 처리
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

        //플레이어 캐릭터 행동 수행   selectedAct : 0-스킬사용 1-아이템사용   selectedIdx : 사용항목인덱스   selectedTarget : 사용대상
        private static void DoPlayerAction(PlayerData playerData, int screenTop, KeyController keyController, int selectedAct, int selectedIdx, int selectedTarget)
        {
            ConsoleKey keyInput;
            int cheatActivated;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            //10 ~ 16줄 Clear
            Console.SetCursorPosition(0, screenTop + 10);
            for (int i = 10; i <= 16; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }

            // 스킬 사용
            if (selectedAct == 0)
            {
                PSkill selectedSkill = PSkillDatabase.GetPSkill(playerData.skillList[selectedIdx]);

                //차징 스킬을 사용한 경우
                if (selectedSkill.needCharging && playerData.Concentrating == false)
                {
                    Console.SetCursorPosition(0, screenTop + 11);
                    Console.WriteLine(new string($"{playerData.Name}이 {selectedSkill.skillName}의 시전 준비에 돌입!"));
                    Console.WriteLine(new string("                                      (Z : 확인)"));
                    playerData.Concentrating = true;
                    playerData.ReservedSkill = selectedIdx;
                    return;
                }
                else if (playerData.Concentrating == true)
                {
                    //시전집중 중에 대상이 죽었을 경우 
                    if (selectedSkill.isSplash == false)
                    {
                        WorldMonster victim = battleTable.Hostile[selectedTarget];
                        if (victim != null && victim.isAlive == false) 
                        { 
                            for (int i = 0; i < battleTable.Hostile.Length; i++)
                            {
                                if (battleTable.Hostile[i] != null && battleTable.Hostile[i].isAlive)
                                {
                                    selectedTarget = i;
                                    break;
                                }
                            }
                        }
                    }

                    playerData.Concentrating = false;
                }

                Random random = new Random();
                int rand = random.Next(100);
                bool critcalHit = (rand < playerData.CriRate);

                Console.SetCursorPosition(0, screenTop + 11);
                Console.WriteLine(new string($"{playerData.Name}의 {selectedSkill.skillName}!"));
                if (critcalHit) Console.WriteLine("크리티컬로 적중!");
                Console.WriteLine(new string("                                      (Z : 확인)"));

                keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                bool loop = true;
                while (loop)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                    if (keyInput == ConsoleKey.Z) loop = false;
                }

                // 피격 구현
                double damage = selectedSkill.CalcDamage(playerData);
                if (critcalHit) damage *= 1.5;  //크리티컬 적중 시 1.5배의 데미지
                if (selectedSkill.isSplash) //전체공격
                {
                    for (int i = 0; i < battleTable.Hostile.Length; i++)
                    {
                        if (battleTable.Hostile[i] != null && battleTable.Hostile[i].isAlive)
                        {
                            WorldMonster victim = battleTable.Hostile[i];
                            int finalDamage = (int)(damage - victim.monster.def);
                            if (finalDamage < 0) finalDamage = 0;
                            victim.currentHp -= finalDamage;
                            //몬스터 사망
                            if (victim.currentHp <= 0)
                            {
                                victim.isAlive = false;
                            }
                            //몬스터 생존
                            else
                            {
                                victim.anger += finalDamage; //분노게이지 맞은만큼 상승
                                victim.concentrating = false; // 시전집중 끊기
                            }
                        }
                    }
                }
                else //단일공격
                {
                    WorldMonster victim = battleTable.Hostile[selectedTarget];
                    int finalDamage = (int)(damage - victim.monster.def);
                    if (finalDamage < 0) finalDamage = 0;
                    victim.currentHp -= finalDamage;
                    //몬스터 사망
                    if (victim.currentHp <= 0)
                    {
                        victim.isAlive = false;
                    }
                    //몬스터 생존
                    else
                    {
                        victim.anger += finalDamage; //분노게이지 맞은만큼 상승
                        victim.concentrating = false; // 시전집중 끊기
                    }
                }
            }

            // 아이템 사용
            else
            {
                Potion selectedPotion = PotionDatabase.GetPotion(playerData.invenPotion[selectedIdx].Key);

                Console.SetCursorPosition(0, screenTop + 11);
                Console.WriteLine(new string($"{playerData.Name}가 {selectedPotion.name} 사용!"));
                Console.WriteLine(new string("                                      (Z : 확인)"));

                keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                bool loop = true;
                while (loop)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                    if (keyInput == ConsoleKey.Z) loop = false;
                }

                // 아이템 효과 구현
                switch (selectedPotion.type)
                {
                    case PotionType.HP:
                        battleTable.Ally[selectedTarget].CurrentHp += selectedPotion.power;
                        if (battleTable.Ally[selectedTarget].CurrentHp > playerData.Hp) battleTable.Ally[selectedTarget].CurrentHp = playerData.Hp;
                        break;
                    case PotionType.MP:
                        battleTable.Ally[selectedTarget].CurrentMp += selectedPotion.power;
                        if (battleTable.Ally[selectedTarget].CurrentMp > playerData.Mp) battleTable.Ally[selectedTarget].CurrentMp = playerData.Mp;
                        break;
                }
            }
        }


        //적 턴 행동명령 받기
        private static void GetHostileOrder(WorldMonster worldMonster, out int selectedIdx, out int selectedTarget)
        {
            selectedIdx = 0; //기본공격

            Random rand = new Random();
            int randResult;

            // 특수 스킬 선택
            int anger = worldMonster.anger;
            MSkill mSkill;
            int angerConsum;
            for (int i = 1; i < worldMonster.monster.skillList.Count; i++)
            {
                mSkill = MSkillDatabase.GetMSkill(worldMonster.monster.skillList[i]);
                angerConsum = mSkill.angerConsum;

                if (anger < angerConsum) continue;
                else
                {
                    // 현재 분노게이지가 높을 수록 시전 확률 높아짐
                    int chance = 40 + (anger - angerConsum) * 20 / angerConsum;
                    randResult = rand.Next(100);
                    if (randResult < chance)
                    {
                        selectedIdx = i;
                        break;
                    }
                }
            }

            // 공격 대상 선택
            Dictionary<int, int> aliveAllyIdx = new Dictionary<int, int>(); //살아있는 플레이어 진영의 실제 인덱스<표시된 인덱스, Ally 내 인덱스>
            int targetNum = 0;
            for (int i = 0; i < battleTable.Ally.Length; i++)
            {
                if (battleTable.Ally[i] != null && battleTable.Ally[i].IsAlive == true)
                {
                    aliveAllyIdx.Add(targetNum++, i);
                }
            }
            randResult = rand.Next(targetNum);
            aliveAllyIdx.TryGetValue(randResult, out selectedTarget); //선택된 대상 인덱스
        }

        // 적 턴 행동 수행
        private static void DoHostileAction(WorldMonster worldMonster, int screenTop, KeyController keyController, int selectedIdx, int selectedTarget)
        {
            ConsoleKey keyInput;
            int cheatActivated;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            //9 ~ 16줄 Clear
            Console.SetCursorPosition(0, screenTop + 9);
            for (int i = 9; i <= 16; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }

            Console.SetCursorPosition(0, screenTop + 9);
            Console.WriteLine($"{worldMonster.monster.name}의 턴!");

            MSkill selectedSkill = MSkillDatabase.GetMSkill(worldMonster.monster.skillList[selectedIdx]);

            //차징 스킬을 사용한 경우
            if (selectedSkill.needCharging && worldMonster.concentrating == false)
            {
                Console.SetCursorPosition(0, 11);
                Console.WriteLine(new string($"{worldMonster.monster.name}이 {selectedSkill.skillName}의 시전 준비에 돌입!"));
                Console.WriteLine(new string("                                      (Z : 확인)"));
                worldMonster.concentrating = true;
                worldMonster.reservedSkill = selectedIdx;
                worldMonster.reservedTarget = selectedTarget;
                return;
            }
            else if (worldMonster.concentrating == true)
            {
                //시전집중 중에 대상이 죽었을 경우 
                if (selectedSkill.isSplash == false)
                {
                    PlayerData victim = battleTable.Ally[selectedTarget];
                    if (victim.IsAlive == false)
                    {
                        for (int i = 0; i < battleTable.Ally.Length; i++)
                        {
                            if (battleTable.Ally[i] != null && battleTable.Ally[i].IsAlive)
                            {
                                selectedTarget = i;
                                break;
                            }
                        }
                    }
                }

                worldMonster.concentrating = false;
            }

            Random random = new Random();
            int rand = random.Next(100);
            bool critcalHit = (rand < worldMonster.monster.criRate);

            Console.SetCursorPosition(0, screenTop + 11);
            Console.WriteLine(new string($"{worldMonster.monster.name}의 {selectedSkill.skillName}!"));
            if (critcalHit) Console.WriteLine("크리티컬로 적중!");
            Console.WriteLine(new string("                                      (Z : 확인)"));

            keyFilter = new ConsoleKey[] { ConsoleKey.Z };
            bool loop = true;
            while (loop)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                if (keyInput == ConsoleKey.Z) loop = false;
            }

            // 피격 구현
            double damage = selectedSkill.CalcDamage(worldMonster.monster);
            if (critcalHit) damage *= 1.5;  //크리티컬 적중 시 1.5배의 데미지
            if (selectedSkill.isSplash) //전체공격
            {
                for (int i = 0; i < battleTable.Ally.Length; i++)
                {
                    if (battleTable.Ally[i] != null && battleTable.Ally[i].IsAlive)
                    {
                        PlayerData victim = battleTable.Ally[i];
                        int finalDamage = (int)(damage - victim.Def);
                        if (finalDamage < 0) finalDamage = 0;
                        victim.CurrentHp -= finalDamage;
                        //몬스터 사망
                        if (victim.CurrentHp <= 0)
                        {
                            victim.IsAlive = false;
                        }
                        //몬스터 생존
                        else
                        {
                            victim.Concentrating = false; // 시전집중 끊기
                        }
                        worldMonster.anger += finalDamage; //분노게이지 때린만큼 상승
                    }
                }
            }
            else //단일공격
            {
                PlayerData victim = battleTable.Ally[selectedTarget];
                int finalDamage = (int)(damage - victim.Def);
                if (finalDamage < 0) finalDamage = 0;
                victim.CurrentHp -= finalDamage;
                //몬스터 사망
                if (victim.CurrentHp <= 0)
                {
                    victim.IsAlive = false;
                }
                //몬스터 생존
                else
                {
                    victim.Concentrating = false; // 시전집중 끊기
                }
            }
        }
    }
}
