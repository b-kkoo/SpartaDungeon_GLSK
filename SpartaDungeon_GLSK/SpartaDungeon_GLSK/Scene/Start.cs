
using Microsoft.VisualBasic;

namespace SpartaDungeon_GLSK.Scene
{
    //새로 시작 씬 : 프롤로그, 튜토리얼 전투 이후
    public class StartScene
    {
        

        public static bool Prolog(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻


            //playerData 초기화 필요




            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.Z:
                        next = Scenes.Battle_Tutorial;
                        return true;
                }
            }
        }
    }
}







/*프롤로그
OO마을 문지기:어서오게 전사여
OO마을 문지기:OO마을에 온걸 환영하네
OO마을 문지기:던전과 모험을 좋아하는 젋은이여 환영하네!
OO마을 문지기:그래서 출입할려면 돈을 줘야하는데 가지고있는거 맞지?
???:돈? 그런거없는데!?
OO마을 문지기:뭐 없다고? 흐음,, 그럼 못들어가는데...
OO마을 문지기:나쁜 사람은 아닌거 같은데 그리고 그리 쌔보이지 않고말이야...
OO마을 문지기:좋아 저기 있는 몹을 잡아오면 특별히 출입을 가능하게 해주지
???:좋아 내실력을 보여주지!
 */








/*
OO마을 문지기:자네 정말 잘싸우는군 근데 이름이 어떻게 되나

OO마을 문지기:$$$라고 하는군!
OO마을 문지기:그래 다시한번 말하지 모험이 시작되는 OOO마을에 온걸 환영하네
OO마을 문지기:앞으로 자네의 앞길에 꽃이 피길!

 */