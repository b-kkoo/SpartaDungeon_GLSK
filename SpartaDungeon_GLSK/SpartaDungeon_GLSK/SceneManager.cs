using SpartaDungeon_GLSK.Scene;

namespace SpartaDungeon_GLSK
{

    internal class SceneManager
    {
        Scenes currentScene;
        Scenes nextScene;

        KeyController keyController = new KeyController();
        
        public void Start()
        {
            //초기 씬 설정
            currentScene = Scenes.Start_Prolog_Scene;

            bool loop = true;
            //게임 루프
            while (loop)
            {
                loop = Execute(out nextScene);
            }

            //종료 시퀀스
        }

        //Start 함수에 의해 씬을 새로 불러올 때마다 호출됨
        private bool Execute(out Scenes next)
        {
            next = Scenes.MainScene; //디폴트값 지정

            bool loop = false;

            switch (currentScene)
            {
                case Scenes.Start_Prolog_Scene:
                    loop = StartScene.StartPrologScene(out next, keyController);
                    break;

                case Scenes.Prolog_Battel:
                    loop = PrologBatlles.PrologBattle(out next, keyController);
                    break;

                case Scenes.Prolog_End:
                    loop = PrologEnd.PrologBattleEnd(out next, keyController);
                    break;

                case Scenes.Test_Main:
                    loop = MainScene.TestMainScene(out next, keyController);
                    break;

                case Scenes.Test_Inventory:
                    loop = InventoryScene.TestInventoryScene(out next, keyController);
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
        //테스트용 씬
        Test_Main,
        Test_Dungeon,
        Test_Inventory,
        Test_Shop,
        Test_Status,

        //
        Start_Prolog_Scene,
        Prolog_Battel,
        Prolog_End,
        MainScene
    }
}

