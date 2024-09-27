
namespace SpartaDungeon_GLSK.Scene
{
    internal class PrologEnd
    {
        internal static bool PrologBattleEnd(out Scenes next, KeyController keyController)
        {

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.WriteLine("프롤로그 끝");


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


/*
OO마을 문지기:자네 정말 잘싸우는군 근데 이름이 어떻게 되나

OO마을 문지기:$$$라고 하는군!
OO마을 문지기:그래 다시한번 말하지 모험이 시작되는 OOO마을에 온걸 환영하네
OO마을 문지기:앞으로 자네의 앞길에 꽃이 피길!

 */