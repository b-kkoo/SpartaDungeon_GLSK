using System;
namespace SpartaDungeon_GLSK.Scene
{
    internal class GameoverScene
	{
        public static bool GAMEOVER(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("YOU DIED");
            Console.WriteLine("당신은 사망하셨습니다\n");
            Console.WriteLine("1. 마을로");
            Console.WriteLine("2. 나가기");
            Console.WriteLine("                                               숫자 버튼을 눌러 선택!");
            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2};
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.Town_Default; //마을로 이동
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Main_Menu; //메인메뉴로 이동
                        return true;
                }
            }
        }
    }
}


