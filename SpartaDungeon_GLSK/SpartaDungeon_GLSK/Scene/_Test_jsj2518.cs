namespace SpartaDungeon_GLSK.Scene
{
    internal class _Test_jsj2518
    {
        public static bool TestMain(out Scenes next, KeyController keyController)
        {

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.WriteLine("테스트 옵션");
            Console.WriteLine("1. altkzs");
            Console.WriteLine("2. b-kkoo");
            Console.WriteLine("3. jsj2518");
            Console.WriteLine("4. leecoading");
            Console.WriteLine("                     X : 뒤로");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.Test_altkzs;
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Test_bkkoo;
                        return true;

                    case ConsoleKey.D3:
                        next = Scenes.Test_jsj2518;
                        return true;

                    case ConsoleKey.D4:
                        next = Scenes.Test_leecoading;
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.Main_Menu;
                        return true;
                }
            }
        }


        public static bool Test(out Scenes next, KeyController keyController)
        {
            next = Scenes.Main_Menu;
            return true;
        }
    }
}
