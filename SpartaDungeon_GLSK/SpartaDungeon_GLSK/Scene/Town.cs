namespace SpartaDungeon_GLSK.Scene
{
    //마을 메뉴, 장비 상점, 포션 상점
    internal class Town
    {
        public static bool Default(out Scenes next, KeyController keyController)
        {
            next = Scenes.Main_Menu;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            keyController.SetCheat(1, "level up");
            keyController.SetCheat(2, "show me the money");

            Console.WriteLine("마을에 오신 것을 환영합니다.");
            Console.WriteLine("마을에서는 원하는 곳으로 이동할 수 있습니다.\n");
            Console.WriteLine($"1. 게임 메뉴");
            Console.WriteLine($"2. 상점");
            Console.WriteLine($"3. 길드");
            Console.WriteLine($"4. 던전");
            Console.WriteLine("                                               숫자 버튼을 눌러 선택!");

            bool loop = true;
            while (loop)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4};
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                if (cheatActivated == 1) //Player team[0] 레벨업 치트
                {
                    Program.playerData.team[0].LvUp();
                    Program.playerData.team[0].Exp = 0;
                }
                else if (cheatActivated == 2) //돈치트
                {
                    Program.playerData.Gold += 10000;
                }

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.PlayerMenu_Menu; //게임메뉴로 이동
                        loop = false;
                        break;

                    case ConsoleKey.D2:
                        next = Scenes.Main_Menu; //상점으로 이동
                        loop = false;
                        break;

                    case ConsoleKey.D3:
                        next = Scenes.Guild_Hall; //길드로 이동
                        loop = false;
                        break;

                    case ConsoleKey.D4:
                        next = Scenes.Dungeon_Default; //던전으로 이동
                        loop = false;
                        break;
                }
            }

            keyController.ClearCheat();
            return true;
        }
    }
}
