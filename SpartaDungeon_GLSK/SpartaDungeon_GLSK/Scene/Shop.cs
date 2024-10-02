using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Scene
{
    public class ShopScene
    {
        public static List<PotionCode> sellingPotionList = new List<PotionCode>();

        public static void Set()
        {
            sellingPotionList.Add(PotionCode.Potion1);
            sellingPotionList.Add(PotionCode.Potion2);
            sellingPotionList.Add(PotionCode.Potion3);
            sellingPotionList.Add(PotionCode.Potion4);
        }

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

        public static bool BuyPotion(out Scenes next, KeyController keyController, out int selectedIdx) //포션 구매
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int intInput;

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
                Console.WriteLine($"{i + 1}. {PotionDatabase.GetPotion(sellingPotionList[potionTab + i]).name}");
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
                        if (selectedIdx < sellingPotionList.Count)
                        {
                            //구매할 개수 입력
                            Console.WriteLine("구매할 포션 개수를 입력해주세요");
                            Console.Write("");
                            intInput = int.Parse(Console.ReadLine());

                            if (intInput * 가격 <= Program.playerData.Gold)
                            {
                                //구매
                                Console.WriteLine($"포션을 {intInput}개 구매하였습니다");
                                //인벤토리에 아이템 추가, 개수 더하기
                                //골드 차감
                                Program.playerData.Gold -= intInput * 가격;
                            }
                            else if (intInput * 가격 > Program.playerData.Gold)
                            {
                                Console.WriteLine("골드가 부족합니다");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
                            else
                            {
                                Console.WriteLine("다시 입력해주세요");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
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

        public static bool SellPotion(out Scenes next, KeyController keyController, out int selectedIdx) //포션 판매
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int intInput;

            int cheatActivated;
            int potionNum = Program.playerData.invenPotion.Count;
            int potionTab = 0;
            selectedIdx = 0;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 포션 상점 >");
            Console.WriteLine("보유한 포션을 판매할 수 있습니다\n");

            bool tabActivate = (potionNum > 5);
            int dispPotionNum = potionNum - potionTab;

            for (int i = 0; i < dispPotionNum; i++)
            {
                Console.WriteLine($"{i + 1}. {PotionDatabase.GetPotion(Program.playerData.invenPotion[potionTab + i].potion).name} X {Program.playerData.invenPotion[potionTab + i].stack}");
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
                        if (selectedIdx < Program.playerData.invenPotion.Count)
                        {
                            //판매할 개수 입력
                            Console.WriteLine("판매할 포션 개수를 입력해주세요");
                            Console.Write("");
                            intInput = int.Parse(Console.ReadLine());

                            if (intInput <= Program.playerData.invenPotion[selectedIdx + potionTab].stack)
                            {
                                //판매
                                Console.WriteLine($"포션을 {intInput}개 판매하였습니다");
                                Program.playerData.invenPotion[selectedIdx + potionTab].stack -= intInput;
                                //돈 받기
                                Program.playerData.Gold += intInput * (int)(PotionDatabase.GetPotion(Program.playerData.invenPotion[selectedIdx + potionTab].potion).Price * 0.8);
                            }
                            else if (intInput > Program.playerData.invenPotion[selectedIdx + potionTab].stack)
                            {
                                Console.WriteLine("판매할 포션 개수가 보유한 포션 개수보다 많습니다");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
                            else
                            {
                                Console.WriteLine("다시 입력해주세요");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
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

        public static bool BuyGear(out Scenes next, KeyController keyController, out int selectedIdx) //장비 구매
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int intInput;

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
                        if (selectedIdx < Program.playerData.invenGear.Count)
                        {
                            Console.WriteLine("장비를 구매하시겠습니까?");
                            Console.WriteLine("                                       1.예 2.아니오 ");
                            Console.Write("");
                            intInput = int.Parse(Console.ReadLine());

                            if (intInput == 1)
                            {
                                //판매
                                Console.WriteLine("장비를 구매하였습니다");
                                //인벤토리에서 장비 추가
                                //돈 차감
                                Program.playerData.Gold -= 가격;
                            }
                            else if (intInput == 2)
                            {
                                Console.WriteLine("다시 입력해주세요");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
                            else
                            {
                                Console.WriteLine("다시 입력해주세요");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
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

        public static bool SellGear(out Scenes next, KeyController keyController, out int selectedIdx) //장비 판매
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int intInput;

            int cheatActivated;
            int gearNum = Program.playerData.invenGear.Count;
            int gearTab = 0;
            selectedIdx = 0;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 장비 상점 >");
            Console.WriteLine("보유한 장비를 판매할 수 있습니다.\n");

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
                        if (selectedIdx < Program.playerData.invenGear.Count)
                        {
                            Console.WriteLine("장비를 판매하시겠습니까?");
                            Console.WriteLine("                                       1.예 2.아니오 ");
                            Console.Write("");
                            intInput = int.Parse(Console.ReadLine());

                            if (intInput == 1)
                            {
                                //장착중인 장비의 경우 장착 해제
                                //판매
                                Console.WriteLine("장비를 판매하였습니다");
                                //인벤토리에서 장비 삭제
                                //돈 받기
                                Program.playerData.Gold += (int)가격 * 0.8;
                            }
                            else if (intInput == 2)
                            {
                                Console.WriteLine("다시 입력해주세요");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
                            else
                            {
                                Console.WriteLine("다시 입력해주세요");
                                Thread.Sleep(1000);
                                keyController.GetUserInput(keyFilter, out cheatActivated);
                            }
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
