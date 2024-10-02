namespace SpartaDungeon_GLSK.Scene
{
    //메인메뉴 씬 : 메인메뉴, 불러오기
    public class MainScene
    {
        //메인화면
        public static bool MainMenu(out Scenes next, KeyController keyController)
        {

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("< < 스파르타 던전 for GLSK > >\n");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("1. 새로하기");
            Console.WriteLine("2. 불러오기");
            Console.WriteLine("3. 게임종료");
            Console.WriteLine("                     숫자 버튼을 눌러 선택!");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.T };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.Start_Prolog;
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Main_Load;
                        return true;

                    case ConsoleKey.D3:
                        next = Scenes.Main_Load; //false를 반환하는 순간 next Scene은 중요치 않음
                        return true;

                    //히든키 T를 눌러 테스트 옵션으로 이동
                    case ConsoleKey.T:
                        next = Scenes.Test_Default;
                        return true;
                }
            }
        }

        //메인화면 - 불러오기
        public static bool MainLoad(out Scenes next, KeyController keyController)
        {
            next = Scenes.Main_Menu;

            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            Console.WriteLine("< < 불 러 오 기 > >\n");
            bool savefileExist1 = DataManager.GetSavefileStatus(1, out string savefileInfo1);
            bool savefileExist2 = DataManager.GetSavefileStatus(2, out string savefileInfo2);
            bool savefileExist3 = DataManager.GetSavefileStatus(3, out string savefileInfo3);
            Console.WriteLine($"1. {savefileInfo1}");
            Console.WriteLine($"2. {savefileInfo2}");
            Console.WriteLine($"3. {savefileInfo3}");
            Console.WriteLine("\n                     (1 ~ 3 : 선택,  X : 뒤로)");

            bool loop = true;
            while (loop)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        if (savefileExist1 == true)
                        {
                            DataManager.LoadDatafile(1);
                            next = Scenes.Town_Default;
                            loop = false;
                        }
                        break;

                    case ConsoleKey.D2:
                        if (savefileExist2 == true)
                        {
                            DataManager.LoadDatafile(2);
                            next = Scenes.Town_Default;
                            loop = false;
                        }
                        break;

                    case ConsoleKey.D3:
                        if (savefileExist3 == true)
                        {
                            DataManager.LoadDatafile(3);
                            next = Scenes.Town_Default;
                            loop = false;
                        }
                        break;

                    case ConsoleKey.X:
                        loop = false;
                        break;
                }
            }

            return true;
        }
    }
}



