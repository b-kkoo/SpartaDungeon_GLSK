

namespace SpartaDungeon_GLSK.Scene
{
    //스태틱 메서드만 만들 것!!
    public class StartScene
    {
        public static bool TestStartScene(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.WriteLine("현재 위치 StartScene");
            Console.WriteLine("Z를 눌러 Main Scene으로 이동~");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.Z:
                        next = Scenes.Test_Main;
                        return true;
                }
            }
        }
    }
}
