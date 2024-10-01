namespace SpartaDungeon_GLSK.Scene
{
    //마을 메뉴, 장비 상점, 포션 상점
    internal class Town
    {
        public static bool Default(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            ////이름 입력, 직업 선택, SetLv1 잘 되었는지 확인
            //Console.WriteLine($"{Program.playerData.Name}"); //이름
            //Console.WriteLine($"{Program.playerData.PClassName}"); //직업
            //Console.WriteLine($"{Program.playerData.Lv}"); //레벨
            //Console.WriteLine($"{Program.playerData.Hp}"); //HP
            //Console.WriteLine($"{Program.playerData.Atk}"); //Atk
            //Console.WriteLine($"{Program.playerData.MAtk}"); //MAtk
            //Console.WriteLine($"{Program.playerData.Def}"); //Def
            //Console.WriteLine($"{Program.playerData.Speed}"); //Speed
            //Console.WriteLine($"{Program.playerData.CriRate}"); //CriRate

            keyController.SetCheat(1, "show me the money");
            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8 };
            while (true)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                if( cheatActivated == 1)
                {
                    Program.playerData.Lv++;
                    Console.WriteLine($"{Program.playerData.Lv}");
                }
            }
            

            




            Console.WriteLine("마을에 오신 것을 환영합니다.");
            Console.WriteLine("마을에서는 원하는 곳으로 이동할 수 있습니다.\n");
            Console.WriteLine($"1. 게임 메뉴");
            Console.WriteLine($"2. 상점");
            Console.WriteLine($"3. 길드");
            Console.WriteLine($"4. 던전");
            Console.WriteLine("                                               숫자 버튼을 눌러 선택!");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4};
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.PlayerMenu_Menu; //게임메뉴로 이동
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Main_Menu; //상점으로 이동
                        return true;

                    case ConsoleKey.D3:
                        next = Scenes.Main_Menu; //길드로 이동
                        return true;

                    case ConsoleKey.D4:
                        next = Scenes.Main_Menu; //던전으로 이동
                        return true;
                }
            }
        }
    }
}
