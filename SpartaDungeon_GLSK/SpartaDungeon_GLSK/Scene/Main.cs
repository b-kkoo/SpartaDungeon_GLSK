namespace SpartaDungeon_GLSK.Scene
{
    //스태틱 메서드만 만들 것!!
    public class MainScene
    {
        public static bool TestMainScene(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.WriteLine("현재 위치 MainScene");
            Console.WriteLine("Z를 눌러 Inventory Scene으로 이동~");
            Console.WriteLine("X를 눌러 프로그램 종료!");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.Z:
                        next = Scenes.Test_Inventory;
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.Test_Start; //false를 반환하는 순간 next Scene은 중요치 않음
                        return false;
                }
            }
        }
    }
}
