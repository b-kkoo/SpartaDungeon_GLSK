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
        public static List<PotionCode> _sellingPotionList = new List<PotionCode>();
        public static List<GearCode> _sellingGearList = new List<GearCode>();

        public static void Set()
        {
            _sellingPotionList.Add(PotionCode.PotionHp1);
            _sellingPotionList.Add(PotionCode.PotionHp2);
            _sellingPotionList.Add(PotionCode.PotionMp1);
            _sellingPotionList.Add(PotionCode.PotionMp2);

            for (int g = 0; g < (int)GearCode.Max; g++)
            {
                _sellingGearList.Add((GearCode)g);
            }                
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
                keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.D1:
                        next = Scenes.Shop_BuyPotion; //포션 구매로 이동
                        return true;

                    case ConsoleKey.D2:
                        next = Scenes.Shop_SellPotion; //포션 판매로 이동
                        return true;

                    case ConsoleKey.D3:
                        next = Scenes.Shop_BuyGear; //장비 구매로 이동
                        return true;

                    case ConsoleKey.D4:
                        next = Scenes.Shop_SellGear; //장비 판매로 이동
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.Town_Default; //마을 이동
                        return true;
                }
            }
        }

        //포션 구매
        public static bool BuyPotion(out Scenes next, KeyController keyController) 
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 포션 상점 >");
            Console.WriteLine("\n전투에 필요한 포션을 구매할 수 있습니다.");

            int potionTab = 0;
            int selectedIdx = 0;

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                //포션 판매 리스트 디스플레이
                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"소지금 : {Program.playerData.Gold} Gold");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 4);
                for (int i = 0; i <= 14; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 0);
                Console.SetCursorPosition(0, 4);
                for (int i = potionTab; i < potionTab + 9 && i < _sellingPotionList.Count; i++)
                {
                    Potion potion = PotionDatabase.GetPotion(_sellingPotionList[i]);
                    Console.Write($" {i - potionTab + 1}. {potion.name}");
                    Console.SetCursorPosition(20, Console.GetCursorPosition().Top);
                    Console.WriteLine($"{(potion.type == PotionType.HP ? "HP" : "MP")}를 {potion.power}만큼 회복시킨다.\n");
                }
                if (_sellingPotionList.Count > 9) Console.WriteLine("\n                             (Tab : 다음)");

                bool loop2 = true;
                while (loop2)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                    switch (keyInput)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.D2:
                        case ConsoleKey.D3:
                        case ConsoleKey.D4:
                        case ConsoleKey.D5:
                        case ConsoleKey.D6:
                        case ConsoleKey.D7:
                        case ConsoleKey.D8:
                        case ConsoleKey.D9:
                            selectedIdx = keyInput - ConsoleKey.D1;
                            if (selectedIdx + potionTab < _sellingPotionList.Count)
                            {
                                Potion potion = PotionDatabase.GetPotion(_sellingPotionList[selectedIdx + potionTab]);
                                Console.SetCursorPosition(0, 4);
                                for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 0);
                                Console.SetCursorPosition(0, 4);
                                Console.WriteLine($"{potion.name} : {(potion.type == PotionType.HP ? "HP" : "MP")}를 {potion.power}만큼 회복시킨다.\n\n");

                                Console.WriteLine("구매할 포션 개수를 입력해주세요");

                                int quantity = 1;
                                Console.SetCursorPosition(35, 6);
                                Console.Write("↑");
                                Console.SetCursorPosition(35, 8);
                                Console.Write("↓");
                                Console.SetCursorPosition(33, 7);
                                Console.Write("←");
                                Console.SetCursorPosition(37, 7);
                                Console.Write("→");
                                if (GetQuantityInput(36, 7, 99, keyController, ref quantity))
                                {
                                    Console.SetCursorPosition(0, 4);
                                    for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                    Console.SetCursorPosition(0, 0);
                                    Console.SetCursorPosition(0, 4);

                                    if (Program.playerData.Gold >= quantity * potion.Price)
                                    {
                                        //골드 차감
                                        Program.playerData.Gold -= quantity * potion.Price;

                                        //인벤토리에 아이템 추가
                                        bool findInventory = false;
                                        for (int i = 0; i < Program.playerData.invenPotion.Count; i++)
                                        {
                                            if (Program.playerData.invenPotion[i].potion == _sellingPotionList[selectedIdx + potionTab])
                                            {
                                                findInventory = true;
                                                Program.playerData.invenPotion[i].stack += quantity;
                                                break;
                                            }
                                        }
                                        if (findInventory == false)
                                        {
                                            Program.playerData.invenPotion.Add(new WorldPotion(_sellingPotionList[selectedIdx + potionTab], quantity));
                                        }

                                        //구매 메시지
                                        Console.WriteLine($"{potion.name}을 {quantity}개 구매하였습니다");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                    else //골드 부족
                                    {
                                        Console.WriteLine("골드가 부족합니다");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                }
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.Tab:
                            if (_sellingPotionList.Count > 9)
                            {
                                if (potionTab + 9 >= _sellingPotionList.Count) potionTab = 0;
                                else potionTab += 9;
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.X:
                            loop2 = false;
                            loop = false;
                            break;
                    }
                }
            }

            next = Scenes.Shop_Default; //상점으로 되돌아가기
            return true;
        }

        //포션 판매
        public static bool SellPotion(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 포션 상점 >");
            Console.WriteLine("\n보유한 포션을 판매할 수 있습니다.");

            int potionTab = 0;
            int selectedIdx = 0;

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                //보유 포션 리스트 디스플레이
                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"소지금 : {Program.playerData.Gold} Gold");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 4);
                for (int i = 0; i <= 14; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 0);
                Console.SetCursorPosition(0, 4);
                for (int i = potionTab; i < potionTab + 9 && i < Program.playerData.invenPotion.Count; i++)
                {
                    Potion potion = PotionDatabase.GetPotion(Program.playerData.invenPotion[i].potion);
                    Console.Write($" {i - potionTab + 1}. {potion.name}");
                    Console.SetCursorPosition(20, Console.GetCursorPosition().Top);
                    Console.WriteLine($"{(potion.type == PotionType.HP ? "HP" : "MP")}를 {potion.power}만큼 회복시킨다.\n");
                }
                if (Program.playerData.invenPotion.Count > 9) Console.WriteLine("\n                             (Tab : 다음)");

                bool loop2 = true;
                while (loop2)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                    switch (keyInput)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.D2:
                        case ConsoleKey.D3:
                        case ConsoleKey.D4:
                        case ConsoleKey.D5:
                        case ConsoleKey.D6:
                        case ConsoleKey.D7:
                        case ConsoleKey.D8:
                        case ConsoleKey.D9:
                            selectedIdx = keyInput - ConsoleKey.D1;
                            if (selectedIdx + potionTab < Program.playerData.invenPotion.Count)
                            {
                                Potion potion = PotionDatabase.GetPotion(Program.playerData.invenPotion[selectedIdx + potionTab].potion);
                                Console.SetCursorPosition(0, 4);
                                for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 0);
                                Console.SetCursorPosition(0, 4);
                                Console.WriteLine($"{potion.name} X {Program.playerData.invenPotion[selectedIdx + potionTab].stack}.\n\n");

                                Console.WriteLine("판매할 포션 개수를 입력해주세요");

                                int quantity = 1;
                                Console.SetCursorPosition(35, 6);
                                Console.Write("↑");
                                Console.SetCursorPosition(35, 8);
                                Console.Write("↓");
                                Console.SetCursorPosition(33, 7);
                                Console.Write("←");
                                Console.SetCursorPosition(37, 7);
                                Console.Write("→");
                                if (GetQuantityInput(36, 7, 99, keyController, ref quantity))
                                {
                                    Console.SetCursorPosition(0, 4);
                                    for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                    Console.SetCursorPosition(0, 0);
                                    Console.SetCursorPosition(0, 4);

                                    if (Program.playerData.invenPotion[selectedIdx + potionTab].stack >= quantity)
                                    {
                                        //골드 수령
                                        Program.playerData.Gold += quantity * (int)(potion.Price * 0.8);

                                        //인벤토리에서 아이템 삭제
                                        bool findInventory;
                                        for (int i = 0; i < Program.playerData.invenPotion.Count; i++)
                                        {
                                            if (Program.playerData.invenPotion[i].stack > quantity )
                                            {
                                                Program.playerData.invenPotion[i].stack -= quantity;
                                                break;
                                            }
                                            else 
                                            {
                                                Program.playerData.invenPotion.RemoveAt(selectedIdx + potionTab);
                                                findInventory = false;
                                                break;
                                            }
                                        }

                                        //판매 메시지
                                        Console.WriteLine($"{potion.name}을 {quantity}개 판매매하였습니다");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                    else //보유 개수 부족
                                    {
                                        Console.WriteLine("보유한 포션이 판매할 수량보다 적습니다");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                }
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.Tab:
                            if (Program.playerData.invenPotion.Count > 9)
                            {
                                if (potionTab + 9 >= Program.playerData.invenPotion.Count) potionTab = 0;
                                else potionTab += 9;
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.X:
                            loop2 = false;
                            loop = false;
                            break;
                    }
                }
            }

            next = Scenes.Shop_Default; //상점으로 되돌아가기
            return true;
        }

        //장비 구매
        public static bool BuyGear(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 장비 상점 >");
            Console.WriteLine("\n전투에 필요한 장비를 구매할 수 있습니다.");

            int gearTab = 0;
            int selectedIdx = 0;

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                //장비 판매 리스트 디스플레이
                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"소지금 : {Program.playerData.Gold} Gold");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 4);
                for (int i = 0; i <= 14; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 0);
                Console.SetCursorPosition(0, 4);
                for (int i = gearTab; i < gearTab + 9 && i < _sellingGearList.Count; i++)
                {
                    Gear gear = GearDatabase.GetGear(_sellingGearList[i]);
                    Console.Write($" {i - gearTab + 1}. {gear.name}");
                    Console.SetCursorPosition(20, Console.GetCursorPosition().Top);
                    Console.WriteLine($"\n");
                }
                if (_sellingGearList.Count > 9) Console.WriteLine("\n                             (Tab : 다음)");

                bool loop2 = true;
                while (loop2)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                    switch (keyInput)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.D2:
                        case ConsoleKey.D3:
                        case ConsoleKey.D4:
                        case ConsoleKey.D5:
                        case ConsoleKey.D6:
                        case ConsoleKey.D7:
                        case ConsoleKey.D8:
                        case ConsoleKey.D9:
                            selectedIdx = keyInput - ConsoleKey.D1;
                            if (selectedIdx + gearTab < _sellingGearList.Count)
                            {
                                Gear gear = GearDatabase.GetGear(_sellingGearList[selectedIdx + gearTab]);
                                Console.SetCursorPosition(0, 4);
                                for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 0);
                                Console.SetCursorPosition(0, 4);
                                Console.WriteLine($"{gear.name} : \n\n");

                                Console.WriteLine("구매할 장비 개수를 입력해주세요");

                                int quantity = 1;
                                Console.SetCursorPosition(35, 6);
                                Console.Write("↑");
                                Console.SetCursorPosition(35, 8);
                                Console.Write("↓");
                                Console.SetCursorPosition(33, 7);
                                Console.Write("←");
                                Console.SetCursorPosition(37, 7);
                                Console.Write("→");
                                if (GetQuantityInput(36, 7, 99, keyController, ref quantity))
                                {
                                    Console.SetCursorPosition(0, 4);
                                    for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                    Console.SetCursorPosition(0, 0);
                                    Console.SetCursorPosition(0, 4);

                                    if (Program.playerData.Gold >= quantity * gear.Price)
                                    {
                                        //골드 차감
                                        Program.playerData.Gold -= quantity * gear.Price;

                                        //인벤토리에 아이템 추가
                                        for (int i = 0; i < quantity; i++)
                                        {
                                            Program.playerData.invenGear.Add(new WorldGear(_sellingGearList[selectedIdx + gearTab]));
                                        }

                                        //구매 메시지
                                        Console.WriteLine($"{gear.name}을 {quantity}개 구매하였습니다");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                    else //골드 부족
                                    {
                                        Console.WriteLine("골드가 부족합니다");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                }
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.Tab:
                            if (_sellingGearList.Count > 9)
                            {
                                if (gearTab + 9 >= _sellingGearList.Count) gearTab = 0;
                                else gearTab += 9;
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.X:
                            loop2 = false;
                            loop = false;
                            break;
                    }
                }
            }

            next = Scenes.Shop_Default; //상점으로 되돌아가기
            return true;
        }

        //장비 판매
        public static bool SellGear(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            Console.WriteLine("< 장비 상점 >");
            Console.WriteLine("\n보유한 장비를 판매할 수 있습니다.");

            int gearTab = 0;
            int selectedIdx = 0;

            keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3, ConsoleKey.D4, ConsoleKey.D5, ConsoleKey.D6, ConsoleKey.D7, ConsoleKey.D8, ConsoleKey.D9, ConsoleKey.Tab, ConsoleKey.X };
            bool loop = true;
            while (loop)
            {
                //장비 판매 리스트 디스플레이
                Console.SetCursorPosition(20, 0);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"소지금 : {Program.playerData.Gold} Gold");
                Console.ForegroundColor = ConsoleColor.White;
                Console.SetCursorPosition(0, 4);
                for (int i = 0; i <= 14; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                Console.SetCursorPosition(0, 0);
                Console.SetCursorPosition(0, 4);
                for (int i = gearTab; i < gearTab + 9 && i < Program.playerData.invenGear.Count; i++)
                {
                    Gear gear = GearDatabase.GetGear(Program.playerData.invenGear[i].gear);
                    Console.Write($" {i - gearTab + 1}. {gear.name}");
                    Console.SetCursorPosition(20, Console.GetCursorPosition().Top);
                    Console.WriteLine($"\n");
                }
                if (Program.playerData.invenGear.Count > 9) Console.WriteLine("\n                             (Tab : 다음)");

                bool loop2 = true;
                while (loop2)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                    switch (keyInput)
                    {
                        case ConsoleKey.D1:
                        case ConsoleKey.D2:
                        case ConsoleKey.D3:
                        case ConsoleKey.D4:
                        case ConsoleKey.D5:
                        case ConsoleKey.D6:
                        case ConsoleKey.D7:
                        case ConsoleKey.D8:
                        case ConsoleKey.D9:
                            selectedIdx = keyInput - ConsoleKey.D1;
                            if (selectedIdx + gearTab < Program.playerData.invenGear.Count)
                            {
                                Gear gear = GearDatabase.GetGear(Program.playerData.invenGear[selectedIdx + gearTab].gear);
                                Console.SetCursorPosition(0, 4);
                                for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                Console.SetCursorPosition(0, 0);
                                Console.SetCursorPosition(0, 4);
                                Console.WriteLine($"{gear.name} : \n\n");

                                Console.WriteLine("판매하시겠습니까?");

                                int quantity = 1;
                                Console.SetCursorPosition(35, 6);
                                Console.Write("↑");
                                Console.SetCursorPosition(35, 8);
                                Console.Write("↓");
                                Console.SetCursorPosition(33, 7);
                                Console.Write("←");
                                Console.SetCursorPosition(37, 7);
                                Console.Write("→");
                                if (GetQuantityInput(36, 7, 99, keyController, ref quantity))
                                {
                                    Console.SetCursorPosition(0, 4);
                                    for (int i = 0; i <= 25; i++) Console.WriteLine(new string(' ', Console.WindowWidth));
                                    Console.SetCursorPosition(0, 0);
                                    Console.SetCursorPosition(0, 4);

                                    if (quantity == 1)
                                    {
                                        //골드 수령
                                        Program.playerData.Gold += (int)(gear.Price * 0.8);

                                        //인벤토리서 아이템 삭제
                                        Program.playerData.invenGear.RemoveAt(selectedIdx + gearTab);


                                        //판매 메시지
                                        Console.WriteLine($"{gear.name}을 판매하였습니다");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                    else //보유량 부족
                                    {
                                        Console.WriteLine("1개씩 판매해주세요");
                                        Thread.Sleep(1000);
                                        keyController.GetUserInput(keyFilter, out cheatActivated);
                                    }
                                }
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.Tab:
                            if (_sellingGearList.Count > 9)
                            {
                                if (gearTab + 9 >= _sellingGearList.Count) gearTab = 0;
                                else gearTab += 9;
                                loop2 = false;
                            }
                            break;

                        case ConsoleKey.X:
                            loop2 = false;
                            loop = false;
                            break;
                    }
                }
            }

            next = Scenes.Shop_Default; //상점으로 되돌아가기
            return true;
        }


        private static bool GetQuantityInput(int screenLeft, int screenTop, int max, KeyController keyController, ref int quantity)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            keyController.GetUserInput(keyFilter, out cheatActivated);

            keyFilter = new ConsoleKey[] { ConsoleKey.UpArrow, ConsoleKey.DownArrow, ConsoleKey.LeftArrow, ConsoleKey.RightArrow, ConsoleKey.Z, ConsoleKey.X };
            while (true)
            {
                Console.SetCursorPosition(screenLeft, screenTop);
                Console.Write("  ");
                Console.SetCursorPosition(screenLeft, screenTop);
                Console.Write($"{quantity,2}");

                bool loop = true;
                while (loop)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out int cheatActivate);
                    switch (keyInput)
                    {
                        case ConsoleKey.UpArrow:
                            quantity += 10;
                            if (quantity > max) quantity = max;
                            loop = false;
                            break;
                        case ConsoleKey.DownArrow:
                            quantity -= 10;
                            if (quantity < 1) quantity = 1;
                            loop = false;
                            break;
                        case ConsoleKey.LeftArrow:
                            quantity--;
                            if (quantity < 1) quantity = 1;
                            loop = false;
                            break;
                        case ConsoleKey.RightArrow:
                            quantity++;
                            if (quantity > max) quantity = max;
                            loop = false;
                            break;
                        case ConsoleKey.Z:
                            return true;
                        case ConsoleKey.X:
                            return false;
                    }
                }
            }
        }
    }
}
