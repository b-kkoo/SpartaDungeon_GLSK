using SpartaDungeon_GLSK.Data;
using System;

namespace SpartaDungeon_GLSK.Scene
{
    //플레이어 관련 메뉴 : 스테이터스, 인벤토리, 장비, 스킬
    public class PlayerMenuScene
    {
        private static int _statusTab;
        private static bool _entryNumDisplay;

        public static bool GameMenu(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 게임 메뉴 >");
            Console.WriteLine("기본적인 정보 확인을 비롯한 여러 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 파티 정보");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 장비관리");
            Console.WriteLine("4. 저장");
            Console.WriteLine("5. 메인화면으로");
            Console.WriteLine("                                               숫자 버튼을 눌러 선택!");
            Console.WriteLine("                                                             X : 뒤로");

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.X };
            while (true)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        _statusTab = 0;
                        TeamEntryView(keyController);
                        next = Scenes.PlayerMenu_Menu;
                        return true;

                    case ConsoleKey.D2:
                        _statusTab = 1;
                        TeamEntryView(keyController);
                        next = Scenes.PlayerMenu_Menu;
                        return true;

                    case ConsoleKey.D3:
                        _statusTab = 2;
                        TeamEntryView(keyController);
                        next = Scenes.PlayerMenu_Menu;
                        return true;

                    case ConsoleKey.D4:
                        next = Scenes.PlayerMenu_SaveData; //저장하기
                        return true;

                    case ConsoleKey.D5:
                        next = Scenes.Main_Menu; //타이틀로 이동
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //마을로 이동
                        return true;
                }
            }
        }

        // < 배틀 엔트리 >
        //
        // 1. LV 00  일이삼사오륙칠  도적   2.
        //    EXP 000 / 000   파티번호 00
        // 
        //    HP 000 / 000
        //    MP 000 / 000
        //    ATK 000
        //  M.ATK 000
        //    DEF 000
        //    SPD 000
        //    CRI 000 %
        //
        //
        // <Q.파티 정보> <W.인벤토리> <E.장비 관리>
        //
        // 
        //
        //
        //
        //
        //
        //
        //

        //상태창
        public static bool TeamEntryView(KeyController keyController) 
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            List<PlayerUnitData> team = Program.playerData.team;
            PlayerUnitData[] entry = Program.playerData.entry;
            List<WorldPotion> invenPotion = Program.playerData.invenPotion;
            List<WorldGear> invenGear = Program.playerData.invenGear;

            int teamTab = 0;
            int potionTab = 0;
            int gearTab = 0;
            int selectedIdx = 0;

            bool loop = true;
            while (loop)
            {
                DrawEntry();

                //파티 정보
                if (_statusTab == 0)
                {
                    DrawTeam(14, teamTab);

                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.W, ConsoleKey.E, ConsoleKey.X };
                    bool loop2 = true;
                    while (loop2)
                    {
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        switch (keyInput)
                        {
                            // 유닛 선택
                            case ConsoleKey.D1:
                            case ConsoleKey.D2:
                            case ConsoleKey.D3:
                            case ConsoleKey.D4:
                            case ConsoleKey.D5:
                            case ConsoleKey.D6:
                            case ConsoleKey.D7:
                            case ConsoleKey.D8:
                            case ConsoleKey.D9:
                                selectedIdx = keyInput - ConsoleKey.D1;
                                if (selectedIdx + teamTab < team.Count)
                                {
                                    int entryNum = -1; //출전 넘버
                                    if (entry[0] == team[selectedIdx + teamTab]) entryNum = 0;
                                    if (entry[1] == team[selectedIdx + teamTab]) entryNum = 1;
                                    if (entry[2] == team[selectedIdx + teamTab]) entryNum = 2;
                                    if (entryNum >= 0) //출전하고 있을 경우 제외
                                    {
                                        if (entryNum == 0) { entry[0] = entry[1]; entry[1] = entry[2]; entry[2] = null; }
                                        else if (entryNum == 1) { entry[1] = entry[2]; entry[2] = null; }
                                        else { entry[2] = null; }
                                    }
                                    else //출전 안하고 있으면 빈자리 채워 넣음
                                    {
                                        if (entry[0] == null) entry[0] = team[selectedIdx + teamTab];
                                        else if (entry[1] == null) entry[1] = team[selectedIdx + teamTab];
                                        else if (entry[2] == null) entry[2] = team[selectedIdx + teamTab];
                                    }
                                    DrawEntry();
                                    DrawTeam(14, teamTab);
                                }
                                break;

                            // 다음 인덱스
                            case ConsoleKey.Tab:
                                if (team.Count > 9)
                                {
                                    if (teamTab + 9 >= team.Count) teamTab = 0;
                                    else teamTab += 9;
                                    loop2 = false;
                                }
                                break;

                            // 다른 창으로 이동
                            case ConsoleKey.W:
                                _statusTab = 1;
                                loop2 = false;
                                break;
                            case ConsoleKey.E:
                                _statusTab = 2;
                                loop2 = false;
                                break;

                            // 나가기
                            case ConsoleKey.X:
                                loop2 = false;
                                loop = false;
                                break;
                        }
                    }
                }

                //인벤토리
                else if (_statusTab == 1)
                {
                    DrawInvenPotion(14, teamTab);

                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.Q, ConsoleKey.E, ConsoleKey.X };
                    bool loop2 = true;
                    while (loop2)
                    {
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        switch (keyInput)
                        {
                            // 아이템 선택
                            case ConsoleKey.D1:
                            case ConsoleKey.D2:
                            case ConsoleKey.D3:
                            case ConsoleKey.D4:
                            case ConsoleKey.D5:
                            case ConsoleKey.D6:
                            case ConsoleKey.D7:
                            case ConsoleKey.D8:
                            case ConsoleKey.D9:
                                selectedIdx = keyInput - ConsoleKey.D1;
                                if (selectedIdx + potionTab < invenPotion.Count)
                                {
                                    Potion potion = PotionDatabase.GetPotion(invenPotion[selectedIdx + potionTab].potion);

                                    Console.SetCursorPosition(0, 16);
                                    for (int i = 0; i <= 12; i++)
                                    {
                                        Console.WriteLine(new string(' ', Console.WindowWidth));
                                    }
                                    Console.SetCursorPosition(0, 16);

                                    int entryNum = 0;//출전유닛 수
                                    for (int i = 0; i < 3; i++) if (entry[i] != null) entryNum++;

                                    if (entryNum == 0) //아이템 사용 대상이 없음
                                    {
                                        Console.WriteLine("출전중인 유닛에만 사용할 수 있습니다!");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                        loop2 = true;
                                        break;
                                    }
                                    else
                                    {
                                        _entryNumDisplay = true;
                                        DrawEntry();

                                        Console.SetCursorPosition(0, 16);
                                        Console.WriteLine($"{potion.name}을 누구에게 사용하시겠습니까?\n");
                                        for (int i = 0; i < entryNum; i++)
                                            Console.WriteLine($"{i + 1}. {entry[i].Name}");

                                        keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.X };
                                        bool loop3 = true;
                                        while (loop3)
                                        {
                                            keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                                            switch (keyInput)
                                            {
                                                case ConsoleKey.D1:
                                                case ConsoleKey.D2:
                                                case ConsoleKey.D3:
                                                    int selectedTarget = keyInput - ConsoleKey.D1;
                                                    if (selectedTarget < entryNum)
                                                    {
                                                        //아이템 효과 적용
                                                        if (potion.type == PotionType.HP)
                                                        {
                                                            entry[selectedTarget].CurrentHp += potion.power;
                                                            if (entry[selectedTarget].CurrentHp > entry[selectedTarget].Hp) entry[selectedTarget].CurrentHp = entry[selectedTarget].Hp;
                                                        }
                                                        else
                                                        {
                                                            entry[selectedTarget].CurrentMp += potion.power;
                                                            if (entry[selectedTarget].CurrentMp > entry[selectedTarget].Mp) entry[selectedTarget].CurrentMp = entry[selectedTarget].Mp;
                                                        }
                                                        //아이템 갯수 -1
                                                        invenPotion[selectedIdx].stack--;
                                                        if (invenPotion[selectedIdx].stack == 0)
                                                        {
                                                            invenPotion.RemoveAt(selectedIdx);
                                                        }

                                                        DrawEntry();
                                                        DrawInvenPotion(14, teamTab);
                                                        keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.Q, ConsoleKey.E, ConsoleKey.X };
                                                        loop3 = false;
                                                    }
                                                    break;

                                                case ConsoleKey.X:
                                                    DrawInvenPotion(14, teamTab);
                                                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.Q, ConsoleKey.E, ConsoleKey.X };
                                                    loop3 = false;
                                                    break;
                                            }
                                        }

                                        _entryNumDisplay = false;
                                        DrawEntry();
                                    }
                                }
                                break;

                            // 다음 인덱스
                            case ConsoleKey.Tab:
                                if (invenPotion.Count > 9)
                                {
                                    if (potionTab + 9 >= invenPotion.Count) potionTab = 0;
                                    else potionTab += 9;
                                    loop2 = false;
                                }
                                break;

                            // 다른 창으로 이동
                            case ConsoleKey.Q:
                                _statusTab = 0;
                                loop2 = false;
                                break;
                            case ConsoleKey.E:
                                _statusTab = 2;
                                loop2 = false;
                                break;

                            // 나가기
                            case ConsoleKey.X:
                                loop2 = false;
                                loop = false;
                                break;
                        }
                    }
                }



                //장비 관리
                else
                {
                    DrawInvenGear(14, teamTab);

                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.Q, ConsoleKey.W, ConsoleKey.X };
                    bool loop2 = true;
                    while (loop2)
                    {
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        switch (keyInput)
                        {
                            // 아이템 선택
                            case ConsoleKey.D1:
                            case ConsoleKey.D2:
                            case ConsoleKey.D3:
                            case ConsoleKey.D4:
                            case ConsoleKey.D5:
                            case ConsoleKey.D6:
                            case ConsoleKey.D7:
                            case ConsoleKey.D8:
                            case ConsoleKey.D9:
                                selectedIdx = keyInput - ConsoleKey.D1;
                                if (selectedIdx + gearTab < invenGear.Count)
                                {
                                    WorldGear worldGear = invenGear[selectedIdx];
                                    Gear gear = GearDatabase.GetGear(worldGear.gear);

                                    if (worldGear.wearer >= 0) //착용중인 장비 해제
                                    {
                                        GearSlot gs;
                                        switch (gear.type)
                                        {
                                            case GearType.WeaponS:
                                            case GearType.WeaponB:
                                            case GearType.WeaponW:
                                                gs = GearSlot.Weapon;
                                                break;
                                            case GearType.ArmorHA:
                                            case GearType.ArmorLA:
                                            case GearType.ArmorR:
                                                gs = GearSlot.Armor;
                                                break;
                                            default:
                                                gs = GearSlot.Ring;
                                                break;
                                        }
                                        team[worldGear.wearer].UnequipGear(worldGear.wearer, gs);
                                        worldGear.wearer = -1;

                                        DrawEntry();
                                        DrawInvenGear(14, teamTab);
                                    }
                                    else //착용되지 않은 장비 착용
                                    {
                                        Console.SetCursorPosition(0, 16);
                                        for (int i = 0; i <= 12; i++)
                                        {
                                            Console.WriteLine(new string(' ', Console.WindowWidth));
                                        }
                                        Console.SetCursorPosition(0, 16);

                                        int entryNum = 0;//출전유닛 수
                                        for (int i = 0; i < 3; i++) if (entry[i] != null) entryNum++;

                                        if (entryNum == 0) //아이템 사용 대상이 없음
                                        {
                                            Console.WriteLine("출전중인 유닛에만 착용할 수 있습니다!");
                                            Thread.Sleep(1000);
                                            keyController.GetUserInput(keyFilter, out cheatActivated);
                                            loop2 = false;
                                            break;
                                        }
                                        else
                                        {
                                            _entryNumDisplay = true;
                                            DrawEntry();

                                            Console.SetCursorPosition(0, 16);
                                            Console.WriteLine($"{gear.name}을 누구에게 착용하시겠습니까?\n");
                                            bool[] wearable = new bool[3];
                                            for (int i = 0; i < entryNum; i++)
                                            {
                                                switch (entry[i].PClass)
                                                {
                                                    case JobCode.Warrior:
                                                        if (gear.type == GearType.WeaponS || gear.type == GearType.ArmorHA || gear.type == GearType.Ring) wearable[i] = true;
                                                        else wearable[i] = false;
                                                        break;
                                                    case JobCode.Archer:
                                                        if (gear.type == GearType.WeaponB || gear.type == GearType.ArmorLA || gear.type == GearType.Ring) wearable[i] = true;
                                                        else wearable[i] = false;
                                                        break;
                                                    case JobCode.Mage:
                                                        if (gear.type == GearType.WeaponW || gear.type == GearType.ArmorR || gear.type == GearType.Ring) wearable[i] = true;
                                                        else wearable[i] = false;
                                                        break;
                                                }

                                                if (wearable[i] == true)
                                                {
                                                    Console.WriteLine($"{i + 1}. {entry[i].Name}");
                                                }
                                                else
                                                {
                                                    Console.ForegroundColor = ConsoleColor.DarkGray;
                                                    Console.WriteLine($"{i + 1}. {entry[i].Name} (착용 불가)");
                                                    Console.ForegroundColor = ConsoleColor.White;
                                                }
                                            }

                                            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.X };
                                            bool loop3 = true;
                                            while (loop3)
                                            {
                                                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                                                switch (keyInput)
                                                {
                                                    case ConsoleKey.D1:
                                                    case ConsoleKey.D2:
                                                    case ConsoleKey.D3:
                                                        int selectedTarget = keyInput - ConsoleKey.D1;
                                                        if (selectedTarget < entryNum)
                                                        {
                                                            if (wearable[selectedTarget] == true)
                                                            {
                                                                int selectedTeamTarget = 0;
                                                                for (int i = 0; i < team.Count; i++)
                                                                {
                                                                    if (entry[selectedTarget] == team[i])
                                                                    {
                                                                        selectedTeamTarget = i;
                                                                        break;
                                                                    }
                                                                }
                                                                //실질 착용
                                                                entry[selectedTarget].EquipGear(selectedTeamTarget, worldGear);

                                                                DrawEntry();
                                                                DrawInvenGear(14, teamTab);
                                                                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.Q, ConsoleKey.W, ConsoleKey.X };
                                                                loop3 = false;
                                                            }
                                                        }
                                                        break;

                                                    case ConsoleKey.X:
                                                        DrawInvenGear(14, teamTab);
                                                        keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.Q, ConsoleKey.W, ConsoleKey.X };
                                                        loop3 = false;
                                                        break;
                                                }
                                            }

                                            _entryNumDisplay = false;
                                            DrawEntry();
                                        }
                                    }
                                }
                                break;

                            // 다음 인덱스
                            case ConsoleKey.Tab:
                                if (invenGear.Count > 9)
                                {
                                    if (gearTab + 9 >= invenGear.Count) gearTab = 0;
                                    else gearTab += 9;
                                    loop2 = false;
                                }
                                break;

                            // 다른 창으로 이동
                            case ConsoleKey.Q:
                                _statusTab = 0;
                                loop2 = false;
                                break;
                            case ConsoleKey.W:
                                _statusTab = 1;
                                loop2 = false;
                                break;

                            // 나가기
                            case ConsoleKey.X:
                                loop2 = false;
                                loop = false;
                                break;
                        }
                    }
                }
            }

            return true;
        }

        //상단 엔트리 그리기
        private static void DrawEntry()
        {
            List<PlayerUnitData> team = Program.playerData.team;
            PlayerUnitData[] entry = Program.playerData.entry;

            Console.SetCursorPosition(0, 0);
            for (int i = 0; i <= 12; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(" < 출전 엔트리 >");
            if (entry[0] == null && entry[1] == null && entry[2] == null)
            {
                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("엔트리가 비어있습니다!");
                Console.ForegroundColor = ConsoleColor.White;
            }
            Console.SetCursorPosition(50, 0);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"소지금 : {Program.playerData.Gold} Gold");
            Console.ForegroundColor = ConsoleColor.White;

            for (int i = 0; i < 3; i++)
            {
                if (entry[i] != null)
                {
                    int teamIdx = -1;
                    for (int j = 0; j < team.Count; j++) if (entry[i] == team[j]) { teamIdx = j; break; }

                    if (entry[i].IsAlive == false) Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.SetCursorPosition(i * 40, 2); Console.Write($"{(_entryNumDisplay ? $" {i + 1}. " : "")}");
                    Console.SetCursorPosition(i * 40 + 4, 2); Console.Write($"LV {entry[i].Lv,2}  {entry[i].Name}{(entry[i].IsAlive == false ? "(기절)" : "")}");
                    Console.SetCursorPosition(i * 40 + 30, 2); Console.Write($"{entry[i].PClassName}");
                    Console.SetCursorPosition(i * 40, 3); Console.Write($"    EXP {entry[i].Exp} / {entry[i].ExpNextLevel}   파티번호 {teamIdx,2}");
                    Console.SetCursorPosition(i * 40, 4); Console.Write($"    HP {entry[i].CurrentHp,3} / {entry[i].Hp,3}");
                    Console.SetCursorPosition(i * 40, 5); Console.Write($"    MP {entry[i].CurrentMp,3} / {entry[i].Mp,3}");
                    Console.SetCursorPosition(i * 40, 6); Console.Write($"    ATK {entry[i].Atk,3}");
                    Console.SetCursorPosition(i * 40, 7); Console.Write($"  M.ATK {entry[i].MAtk,3}");
                    Console.SetCursorPosition(i * 40, 8); Console.Write($"    DEF {entry[i].Def,3}");
                    Console.SetCursorPosition(i * 40, 9); Console.Write($"    SPD {entry[i].Speed,3}");
                    Console.SetCursorPosition(i * 40,10); Console.Write($"    CRI {entry[i].CriRate,3} %");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
        //파티 정보 그리기
        private static void DrawTeam(int screenTop, int teamTab)
        {
            List<PlayerUnitData> team = Program.playerData.team;
            PlayerUnitData[] entry = Program.playerData.entry;

            Console.SetCursorPosition(0, screenTop);
            for (int i = 0; i <= 12; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, screenTop);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(" <Q.파티 정보> ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" W.인벤토리   E.장비 관리 ");
            Console.ForegroundColor= ConsoleColor.DarkGray;
            Console.WriteLine("출전중인 파티원을 제외하거나 출전하지 않은 파티원을 출전시킬 수 있습니다");
            Console.ForegroundColor = ConsoleColor.White;

            //9개씩 디스플레이
            for (int i = teamTab; i < teamTab + 9 && i < team.Count; i++)
            {
                int entryNum = -1; //출전 넘버
                if (entry[0] == team[i]) entryNum = 0;
                if (entry[1] == team[i]) entryNum = 1;
                if (entry[2] == team[i]) entryNum = 2;
                Console.Write($" {i + 1}. LV {team[i].Lv,2}  {team[i].Name}");
                Console.SetCursorPosition(30, Console.GetCursorPosition().Top);
                Console.WriteLine($"{team[i].PClassName}{(entryNum >= 0 ? $"   출전-{entryNum + 1}" : "")}");
            }

            if (team.Count > 9) Console.WriteLine("\n                             (Tab : 다음)");
        }
        //인벤토리 그리기
        private static void DrawInvenPotion(int screenTop, int potionTab)
        {
            List<WorldPotion> invenPotion = Program.playerData.invenPotion;

            Console.SetCursorPosition(0, screenTop);
            for (int i = 0; i <= 12; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, screenTop);
            Console.Write("  Q.파티 정보  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("<W.인벤토리> ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(" E.장비 관리 ");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("보유중인 아이템을 확인하고 출전중인 파티원에게 사용할 수 있습니다");
            Console.ForegroundColor = ConsoleColor.White;

            //9개씩 디스플레이
            for (int i = potionTab; i < potionTab + 9 && i < invenPotion.Count; i++)
            {
                Potion potion = PotionDatabase.GetPotion(invenPotion[i].potion);
                Console.Write($" {i - potionTab + 1}. {potion.name} X {invenPotion[i].stack}");
                Console.SetCursorPosition(50, Console.GetCursorPosition().Top);
                Console.WriteLine($"{(potion.type == PotionType.HP ? "HP" : "MP")}를 {potion.power}만큼 회복시킨다.");
            }

            if (invenPotion.Count > 9) Console.WriteLine("\n                             (Tab : 다음)");
        }
        //장비 관리 그리기
        private static void DrawInvenGear(int screenTop, int gearTab)
        {
            List<WorldGear> invenGear = Program.playerData.invenGear;
            List<PlayerUnitData> team = Program.playerData.team;
            PlayerUnitData[] entry = Program.playerData.entry;

            Console.SetCursorPosition(0, screenTop);
            for (int i = 0; i <= 12; i++)
            {
                Console.WriteLine(new string(' ', Console.WindowWidth));
            }
            Console.SetCursorPosition(0, screenTop);
            Console.Write("  Q.파티 정보   W.인벤토리  ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("<E.장비 관리>");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("착용중인 장비를 해제하거나 출전중인 파티원에게 착용할 수 있습니다");
            Console.ForegroundColor = ConsoleColor.White;

            //9개씩 디스플레이
            for (int i = gearTab; i < gearTab + 9 && i < invenGear.Count; i++)
            {
                Gear gear = GearDatabase.GetGear(invenGear[i].gear);
                int wearer = invenGear[i].wearer;
                Console.Write($" {i - gearTab + 1}. {gear.name}");

                // 착용중인 장비 표시, 출전중인 유닛이 착용 중일 시 노란색으로 표시
                if (wearer >= 0)
                {
                    int entryNum = -1;
                    if (entry[0] == team[wearer]) entryNum = 0;
                    if (entry[1] == team[wearer]) entryNum = 1;
                    if (entry[2] == team[wearer]) entryNum = 2;
                    if (entryNum >= 0) Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($" ({wearer + 1}. {team[wearer].Name}이 착용 중)");
                    if (entryNum >= 0) Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("");
                }
            }

            if (invenGear.Count > 9) Console.WriteLine("\n                             (Tab : 다음)");
        }

        //스킬
        public static bool Skill(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 게임 메뉴 >");
            Console.WriteLine("스킬 목록과 스킬 정보를 확인할 수 있습니다.\n");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.X:
                        next = Scenes.PlayerMenu_Menu; //게임 메뉴로 이동
                        return true;
                }
            }
        }




        //저장 화면
        public static bool DataSaveScene(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            int selectedIdx = 0;
            bool confirm = false;

            bool loop = true;
            while (loop)
            {
                Console.Clear();
                Console.WriteLine("< 게임 저장 >\n");
                bool savefileExist1 = DataManager.GetSavefileStatus(1, out string savefileInfo1);
                bool savefileExist2 = DataManager.GetSavefileStatus(2, out string savefileInfo2);
                bool savefileExist3 = DataManager.GetSavefileStatus(3, out string savefileInfo3);
                Console.WriteLine($"1. {savefileInfo1}");
                Console.WriteLine($"2. {savefileInfo2}");
                Console.WriteLine($"3. {savefileInfo3}");
                Console.WriteLine("\n                     (1 ~ 3 : 선택,  X : 뒤로)");

                if (confirm == false)
                {
                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.X };

                    bool loop2 = true;
                    while (loop2)
                    {
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        switch (keyInput)
                        {
                            case ConsoleKey.D1:
                                if (savefileExist1 == false)
                                {
                                    DataManager.SaveDatafile(1);
                                }
                                else
                                {
                                    selectedIdx = 0;
                                    confirm = true;
                                }
                                loop2 = false;
                                break;
                            case ConsoleKey.D2:
                                if (savefileExist2 == false)
                                {
                                    DataManager.SaveDatafile(2);
                                }
                                else
                                {
                                    selectedIdx = 1;
                                    confirm = true;
                                }
                                loop2 = false;
                                break;
                            case ConsoleKey.D3:
                                if (savefileExist3 == false)
                                {
                                    DataManager.SaveDatafile(3);
                                }
                                else
                                {
                                    selectedIdx = 2;
                                    confirm = true;
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
                else
                {
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                    Console.WriteLine(new string(' ', Console.WindowWidth));
                    Console.SetCursorPosition(0, Console.GetCursorPosition().Top - 1);
                    Console.WriteLine($"{selectedIdx + 1}번 파일을 덮어 씌우시겠습니까?\n");
                    Console.WriteLine("                                         (Z : 예,  X : 아니오)");

                    keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };

                    bool loop2 = true;
                    while (loop2)
                    {
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        switch (keyInput)
                        {
                            case ConsoleKey.Z:
                                DataManager.SaveDatafile(selectedIdx + 1);
                                loop2 = false;
                                break;
                            case ConsoleKey.X:
                                loop2 = false;
                                break;
                        }
                    }
                    confirm = false;
                }
            }

            next = Scenes.PlayerMenu_Menu;
            return true;
        }
    }
}
