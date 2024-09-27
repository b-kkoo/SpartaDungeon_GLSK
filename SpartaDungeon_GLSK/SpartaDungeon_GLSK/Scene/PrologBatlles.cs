namespace SpartaDungeon_GLSK.Scene
{
    internal class PrologBatlles
    {
        internal static bool PrologBattle(out Scenes next, KeyController keyController)
        {

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.WriteLine("배틀 프롤로그");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.Z:
                        next = Scenes.Prolog_End;
                        return true;
                }
            }
        }

    }
}