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
                case Scenes.Start_PrologEnd:
                    loop = StartScene.PrologEnd(out next, keyController);
                    break;

                //Battle Scene : 튜토리얼 배틀, 배틀 프리셋
                case Scenes.Battle_Tutorial:
                    loop = BattleScene.LoadTutorialBattle(out next, keyController);
                    break;

                //Town Scene : 마을, 장비 상점, 소모품 상점
                case Scenes.Town_Default:
                    loop = Town.Default(out next, keyController);
                    break;

                //Shop Scene
                case Scenes.Shop_Default:
                    loop = ShopScene.Shop(out next, keyController);
                    break;
                case Scenes.Shop_BuyPotion:
                    loop = ShopScene.BuyPotion(out next, keyController);
                    break;

                //Guild Scene
                case Scenes.Guild_Hall:
                    loop = GuildScene.GuildHall(out next, keyController);
                    break;
                case Scenes.Guild_Quest:
                    loop = GuildScene.GuildQuest(out next, keyController);
                    break;

                //PlayerMenu Scene : 상태창, 인벤토리, 장비, 스킬, 저장
                case Scenes.PlayerMenu_Menu:
                    loop = PlayerMenuScene.GameMenu(out next, keyController);
                    break;
                case Scenes.PlayerMenu_SaveData:
                    loop = PlayerMenuScene.DataSaveScene(out next, keyController);
                    break;

                //Dungeon Scene : 던전 스테이지 분기
                case Scenes.Dungeon_Default:
                    loop = DungeonScene.ChallengeToDungeon(out next, keyController);
                    break;

                //Game Over Scene
                case Scenes.Gameover_Default:
                    //loop = GameoverScene.GAMEOVER(out next, keyController);
                    break;


                //Test
                case Scenes.Test_Default:
                    loop = _Test_jsj2518.TestMain(out next, keyController);
                    break;
                case Scenes.Test_altkzs:
                    loop = _Test_altkzs.Test(out next, keyController);
                    break;
                case Scenes.Test_bkkoo:
                    loop = _Test_bkkoo.Test(out next, keyController);
                    break;
                case Scenes.Test_jsj2518:
                    loop = _Test_jsj2518.Test(out next, keyController);
                    break;
                case Scenes.Test_leecoading:
                    loop = _Test_leecoading.Test(out next, keyController);
                    break;
                case Scenes.Test1_kkoo:
                    loop = _Test_bkkoo.Test1(out next, keyController);
                    break;
                case Scenes.TestTab_kkoo:
                    loop = _Test_bkkoo.TestTab(out next, keyController);
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
        Start_PrologEnd,

        //Battle Scene : 튜토리얼 배틀, 배틀 프리셋
        Battle_Tutorial,

        //Town Scene : 마을
        Town_Default,

        //Shop Scene
        Shop_Default,
        Shop_BuyPotion,
        Shop_SellPotion,
        Shop_BuyGear,
        Shop_SellGear,

        //Guild Scene
        Guild_Hall,
        Guild_Quest,
        Guild_Mercenary,
        Guild_Inn,

        //게임 메뉴
        PlayerMenu_Menu,
        PlayerMenu_SaveData,

        //던전
        Dungeon_Default,

        //게임오버
        Gameover_Default,


        //Test Menu
        Test_Default,
        Test_altkzs,
        Test_bkkoo,
        Test_jsj2518,
        Test_leecoading,
        Test1_kkoo,
        TestTab_kkoo
    }
}

