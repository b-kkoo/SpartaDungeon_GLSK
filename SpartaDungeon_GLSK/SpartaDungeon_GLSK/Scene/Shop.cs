using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Scene
{
    internal class ShopScene
    {
        //상점
        public static bool Shop(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 상점 >");
            Console.WriteLine("전투에 필요한 장비나 포션을 구매할 수 있습니다.\n");
            Console.WriteLine("1. 포션 구매");
            Console.WriteLine("2. 포션 판매");
            Console.WriteLine("3. 장비 구매");
            Console.WriteLine("4. 장비 판매");
            Console.WriteLine("                                               숫자 버튼을 눌러 선택!");
            Console.WriteLine("                                                             X : 뒤로");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.Main_Menu; //포션 구매로 이동
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Main_Menu; //포션 판매로 이동
                        return true;

                    case ConsoleKey.D3:
                        next = Scenes.Main_Menu; //장비 구매로 이동
                        return true;

                    case ConsoleKey.D4:
                        next = Scenes.Main_Menu; //장비 판매로 이동
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.PlayerMenu_Menu; //게임 메뉴 이동
                        return true;
                }
            }
        }

        public static bool BuyPotion(out Scenes next, KeyController keyController, out int selectedIdx)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            int potionNum = Program.playerData.invenPotion.Count;
            int potionTab = 0;
            selectedIdx = 0;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 포션 상점 >");
            Console.WriteLine("전투에 필요한 포션을 구매할 수 있습니다.\n");

            bool tabActivate = (potionNum > 5);
            int dispPotionNum = potionNum - potionTab;

            for (int i = 0; i < dispPotionNum; i++)
            {
                Console.WriteLine($"{i + 1}. {PotionDatabase.GetPotion(Program.playerData.invenPotion[potionTab + i].Key).name} X {Program.playerData.invenPotion[potionTab + i].Value}");
            }
            if (dispPotionNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
            else Console.WriteLine($"(1 ~ {dispPotionNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                        selectedIdx = potionTab + (keyInput - ConsoleKey.D1);
                        if (selectedIdx < Program.playerData.invenPotion[])
                        {

                        }
                        break;

                    case ConsoleKey.Tab:
                        if (tabActivate == true)
                        {
                            //다음 리스트 보기
                            if (potionTab + 5 >= potionNum) potionTab = 0;
                            else potionTab += 5;
                            loop = false;
                        }
                        break;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //상점으로 되돌아가기
                        return true;
                }
            }
        }

        public static bool SellPotion(out Scenes next, KeyController keyController, out int selectedIdx)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            int potionNum = Program.playerData.invenPotion.Count;
            int potionTab = 0;
            selectedIdx = 0;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 포션 상점 >");
            Console.WriteLine("전투에 필요한 포션을 구매할 수 있습니다.\n");

            bool tabActivate = (potionNum > 5);
            int dispPotionNum = potionNum - potionTab;

            for (int i = 0; i < dispPotionNum; i++)
            {
                Console.WriteLine($"{i + 1}. {PotionDatabase.GetPotion(Program.playerData.invenPotion[potionTab + i].Key).name} X {Program.playerData.invenPotion[potionTab + i].Value}");
            }
            if (dispPotionNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
            else Console.WriteLine($"(1 ~ {dispPotionNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                        selectedIdx = potionTab + (keyInput - ConsoleKey.D1);
                        if (selectedIdx < Program.playerData.invenPotion[])
                        {

                        }
                        break;

                    case ConsoleKey.Tab:
                        if (tabActivate == true)
                        {
                            //다음 리스트 보기
                            if (potionTab + 5 >= potionNum) potionTab = 0;
                            else potionTab += 5;
                            loop = false;
                        }
                        break;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //상점으로 되돌아가기
                        return true;
                }
            }
        }

        public static bool BuyGear(out Scenes next, KeyController keyController, out int selectedIdx)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            int gearNum = Program.playerData.invenGear.Count;
            int gearTab = 0;
            selectedIdx = 0;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 장비 상점 >");
            Console.WriteLine("전투에 필요한 장비를 구매할 수 있습니다.\n");

            bool tabActivate = (gearNum > 5);
            int dispGearNum = gearNum - gearTab;

            for (int i = 0; i < dispGearNum; i++)
            {
                Console.WriteLine($"{i + 1}. {Program.playerData.invenGear[i]}");
            }
            if (dispGearNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
            else Console.WriteLine($"(1 ~ {dispGearNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                        selectedIdx = gearTab + (keyInput - ConsoleKey.D1);
                        if (selectedIdx < Program.playerData.invenPotion[])
                        {

                        }
                        break;

                    case ConsoleKey.Tab:
                        if (tabActivate == true)
                        {
                            //다음 리스트 보기
                            if (gearTab + 5 >= gearNum) gearTab = 0;
                            else gearTab += 5;
                            loop = false;
                        }
                        break;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //상점으로 되돌아가기
                        return true;
                }
            }
        }

        public static bool SellGear(out Scenes next, KeyController keyController, out int selectedIdx)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            int gearNum = Program.playerData.invenGear.Count;
            int gearTab = 0;
            selectedIdx = 0;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 장비 상점 >");
            Console.WriteLine("전투에 필요한 장비를 구매할 수 있습니다.\n");

            bool tabActivate = (gearNum > 5);
            int dispGearNum = gearNum - gearTab;

            for (int i = 0; i < dispGearNum; i++)
            {
                Console.WriteLine($"{i + 1}. {Program.playerData.invenGear[i]}");
            }
            if (dispGearNum == 1) Console.WriteLine($"(1 : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");
            else Console.WriteLine($"(1 ~ {dispGearNum} : 선택, {(tabActivate ? "Tab : 다음, " : "")}X : 취소)");

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                    case ConsoleKey.D2:
                    case ConsoleKey.D3:
                    case ConsoleKey.D4:
                    case ConsoleKey.D5:
                        selectedIdx = gearTab + (keyInput - ConsoleKey.D1);
                        if (selectedIdx < Program.playerData.invenPotion[])
                        {

                        }
                        break;

                    case ConsoleKey.Tab:
                        if (tabActivate == true)
                        {
                            //다음 리스트 보기
                            if (gearTab + 5 >= gearNum) gearTab = 0;
                            else gearTab += 5;
                            loop = false;
                        }
                        break;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //상점으로 되돌아가기
                        return true;
                }
            }
        }
    }
}
