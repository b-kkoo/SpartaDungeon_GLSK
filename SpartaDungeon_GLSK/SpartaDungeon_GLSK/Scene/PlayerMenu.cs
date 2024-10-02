using SpartaDungeon_GLSK.Data;

namespace SpartaDungeon_GLSK.Scene
{
    //플레이어 관련 메뉴 : 스테이터스, 인벤토리, 장비, 스킬
    public class PlayerMenuScene
    {
        public static bool GameMenu(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 게임 메뉴 >");
            Console.WriteLine("기본적인 정보 확인을 비롯한 여러 활동을 할 수 있습니다.\n");
            Console.WriteLine("1. 상태창");
            Console.WriteLine("2. 인벤토리");
            Console.WriteLine("3. 장비관리");
            Console.WriteLine("4. 스킬관리");
            Console.WriteLine("5. 저장");
            Console.WriteLine("6. 타이틀");
            Console.WriteLine("                                               숫자 버튼을 눌러 선택!");
            Console.WriteLine("                                                             X : 뒤로");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.Main_Menu; //상태창으로 이동
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Main_Menu; //인벤토리로 이동
                        return true;

                    case ConsoleKey.D3:
                        next = Scenes.Main_Menu; //장비관리로 이동
                        return true;

                    case ConsoleKey.D4:
                        next = Scenes.Main_Menu; //스킬관리로 이동
                        return true;

                    case ConsoleKey.D5:
                        next = Scenes.Main_Menu; //저장하기
                        return true;

                    case ConsoleKey.D6:
                        next = Scenes.Main_Menu; //타이틀로 이동
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //마을로 이동
                        return true;
                }
            }
        }

        //상태창
        public static bool Status(out Scenes next, KeyController keyController) 
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 상태창 >");
            Console.WriteLine("플레이어의 정보를 확인할 수 있습니다.\n");
            Console.WriteLine("- 이름    :");
            Console.WriteLine("- 직업    :");
            Console.WriteLine("- Lv      :");
            Console.WriteLine("- Exp     :");
            Console.WriteLine("- Hp      :");
            Console.WriteLine("- Mp      :");
            Console.WriteLine("- Atk     :");
            Console.WriteLine("- MAtk    :");
            Console.WriteLine("- Def     :");
            Console.WriteLine("- Speed   :");
            Console.WriteLine("- CriRate :");
            Console.WriteLine("                                                             X : 뒤로");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.X:
                        next = Scenes.PlayerMenu_Menu; //게임 메뉴로 이동
                        return true;
                }
            }
        }

        //인벤토리
        public static bool Inventory(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 인벤토리 >");
            Console.WriteLine("소모 아이템 목록을 확인하고 소모 아이템을 사용할 수 있습니다.\n");

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

        //장비
        public static bool Equipment(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 게임 메뉴 >");
            Console.WriteLine("장비 아이템 목록을 확인하고 장착/해제할 수 있습니다.\n");

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
    }
}
