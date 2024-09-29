using SpartaDungeon_GLSK.Scene;

namespace SpartaDungeon_GLSK
{

    public class SceneManager
    {
        Scenes currentScene;
        Scenes nextScene;

        KeyController keyController = new KeyController();
        
        public void Start()
        {
            //초기 씬 설정
            currentScene = Scenes.Main_Menu;

            bool loop = true;
            //게임 루프
            while (loop)
            {
                Console.Clear();

                loop = Execute(out nextScene);
            }

            //종료 시퀀스
        }

        //Start 함수에 의해 씬을 새로 불러올 때마다 호출됨
        private bool Execute(out Scenes next)
        {
            next = Scenes.Main_Menu; //디폴트값 지정

            bool loop = false;

            switch (currentScene)
            {
                //Main Scene : 메인 메뉴
                case Scenes.Main_Menu:
                    loop = MainScene.MainMenu(out next, keyController);
                    break;
                case Scenes.Main_Load:
                    loop = MainScene.MainLoad(out next, keyController);
                    break;

                //Start Scene : 프롤로그, 튜토리얼 후
                case Scenes.Start_Prolog:
                    loop = StartScene.Prolog(out next, keyController);
                    break;
                case Scenes.Start_TutoEnd:
                    //loop = StartScene.
                    break;

                //Battle Scene : 튜토리얼 배틀, 배틀 프리셋
                case Scenes.Battle_Tutorial:
                    loop = BattleScene.TutorialBattle(out next, keyController);
                    break;

                //Town Scene : 마을, 장비 상점, 소모품 상점
                case Scenes.Town_Default:
                    //loop = Town.
                    break;




                //Test
                case Scenes.Test_Default:
                    _Test_jsj2518.TestMain(out next, keyController);
                    break;
                case Scenes.Test_altkzs:
                    _Test_altkzs.Test(out next, keyController);
                    break;
                case Scenes.Test_bkkoo:
                    _Test_bkkoo.Test(out next, keyController);
                    break;
                case Scenes.Test_jsj2518:
                    _Test_jsj2518.Test(out next, keyController);
                    break;
                case Scenes.Test_leecoading:
                    _Test_leecoading.Test(out next, keyController);
                    break;

                default:
                    return false; //유효하지 않은 씬
            }

            currentScene = next;
            return loop;
        }
    }



    public enum Scenes
    {
        //Main Scene : 메인 메뉴
        Main_Menu,
        Main_Load,

        //Start Scene : 프롤로그, 튜토리얼 후
        Start_Prolog,
        Start_TutoEnd,

        //Battle Scene : 튜토리얼 배틀, 배틀 프리셋
        Battle_Tutorial,

        //Town Scene : 마을, 장비 상점, 소모품 상점
        Town_Default,




        //Test Menu
        Test_Default,
        Test_altkzs,
        Test_bkkoo,
        Test_jsj2518,
        Test_leecoading
    }
}

