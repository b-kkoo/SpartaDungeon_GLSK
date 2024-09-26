namespace SpartaDungeon_GLSK.Scene
{
    //스태틱 메서드만 만들 것!!
    public class InventoryScene
    {
        public static bool TestInventoryScene(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.WriteLine("현재 위치 InventoryScene");
            Console.WriteLine("3초 후 프로그램 종료.");

            Thread.Sleep(3000);

            next = Scenes.MainScene; //false를 반환하는 순간 next Scene은 중요치 않음
            return false;
        }
    }
}
