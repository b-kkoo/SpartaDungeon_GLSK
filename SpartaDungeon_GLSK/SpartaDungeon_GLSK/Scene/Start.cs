﻿
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

            //초기 데이터 설정
            Program.playerData = new PlayerData();
            PlayerUnitData newUnit = new PlayerUnitData();
            Program.playerData.team.Add(newUnit);
            Program.playerData.entry[0] = newUnit;

            int cheatActivated;

            string strInput;
            int i, intVal;
            string[] tConversation;

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            //playerData 초기화 필요
            tConversation = new string[4];
            tConversation[0] = "어서 일어나게. 지금 잘 시간이 아니야!";
            tConversation[1] = "마을에 던전의 몬스터가 나타났어!";
            tConversation[2] = "마을의 수비대가 자리를 비운 상태라 자네도 전투에 참여해야하네.";
            tConversation[3] = "자네의 이름은 무엇인가?";
            ScenePreset.Conversation(tConversation, keyController);

            Console.CursorVisible = true;
            while (true)
            {
                Console.Clear();
                Console.WriteLine("이름을 입력해주세요(최대 14자, 한글은 글자당 2자)");
                Console.Write("");
                strInput = Console.ReadLine();
                strInput = strInput.Trim();
                intVal = 0;
                for (i = 0; i < strInput.Length; i++)
                {
                    if ((int)strInput[i] > 128)
                    {
                        intVal += 2;
                    }
                    else
                    {
                        intVal += 1;
                    }
                }
                if (intVal > 14)
                {
                    Console.WriteLine("14 글자가 넘습니다");
                    Thread.Sleep(1000);
                    keyController.GetUserInput(keyFilter, out cheatActivated);
                }
                else if (intVal == 0)
                {
                    Console.WriteLine("이름을 공백으로 설정할 수 없습니다.");
                    Thread.Sleep(1000);
                    keyController.GetUserInput(keyFilter, out cheatActivated);
                }
                else
                {
                    Console.WriteLine($"\n\"{strInput}\"으로 정하시겠습니까?");                    
                    Console.WriteLine("                                                  Z : 예  X : 아니오");
                    Console.WriteLine("한글로 입력하셨으면 한영키를 눌러주세요");
                    while (true)
                    {
                        keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        if (keyInput == ConsoleKey.Z || keyInput == ConsoleKey.X) break;
                    }
                    if (keyInput == ConsoleKey.Z) //이름 확정
                    {
                        Console.Clear();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("다시 입력해주세요");
                        Thread.Sleep(1000);
                        keyController.GetUserInput(keyFilter, out cheatActivated);
                    }
                }
            }
            Console.CursorVisible = false;
            newUnit.Name = strInput;

            tConversation = new string[2];
            tConversation[0] = $"\"{newUnit.Name}\"(이)라고 하는군";
            tConversation[1] = "자네가 사용하던 무기는 무엇인가?";
            ScenePreset.Conversation(tConversation, keyController);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("사용하던 무기를 선택해주세요");
                Console.WriteLine("1. 검\n2. 활\n3. 지팡이");
                Console.WriteLine("                                              숫자 버튼을 눌러 선택!");
                while (true)
                {
                    keyFilter = new ConsoleKey[] { ConsoleKey.D1, ConsoleKey.D2, ConsoleKey.D3 };
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                    if (keyInput == ConsoleKey.D1 || keyInput == ConsoleKey.D2 || keyInput == ConsoleKey.D3) break;
                }
                if (keyInput == ConsoleKey.D1)
                {
                    Console.WriteLine($"\n\"전사\"로 정하시겠습니까?");
                    Console.WriteLine("                                                  Z : 예  X : 아니오");
                    while (true)
                    {
                        keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        if (keyInput == ConsoleKey.Z || keyInput == ConsoleKey.X) break;
                    }
                    if (keyInput == ConsoleKey.Z) //직업 확정
                    {
                        Console.Clear();
                        Console.WriteLine("자네는 전사로군. 어서 이 검을 들고 전투에 참여해주게.");
                        Console.WriteLine("                                                          (Z : 확인)");
                        newUnit.PClass = Data.JobCode.Warrior;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("다시 입력해주세요");
                        Thread.Sleep(1000);
                        keyController.GetUserInput(keyFilter, out cheatActivated);
                    }
                }
                else if (keyInput == ConsoleKey.D2)
                {
                    Console.WriteLine($"\n\"궁수\"로 정하시겠습니까?");
                    Console.WriteLine("                                                  Z : 예  X : 아니오");
                    while (true)
                    {
                        keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        if (keyInput == ConsoleKey.Z || keyInput == ConsoleKey.X) break;
                    }
                    if (keyInput == ConsoleKey.Z) //직업 확정
                    {
                        Console.Clear();
                        Console.WriteLine("자네는 궁수로군. 어서 이 활을 들고 전투에 참여해주게.");
                        Console.WriteLine("                                                          (Z : 확인)");
                        newUnit.PClass = Data.JobCode.Archer;
                        break;
                    }
                    else
                    {
                        Console.WriteLine("다시 입력해주세요");
                        Thread.Sleep(1000);
                        keyController.GetUserInput(keyFilter, out cheatActivated);
                    }                    
                }
                else
                {
                    Console.WriteLine($"\n\"마법사\"로 정하시겠습니까?");
                    Console.WriteLine("                                                  Z : 예  X : 아니오");                    
                    while (true)
                    {
                        keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                        keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);
                        if (keyInput == ConsoleKey.Z || keyInput == ConsoleKey.X) break;
                    }
                    if (keyInput == ConsoleKey.Z) //직업 확정
                    {
                        Console.Clear();
                        Console.WriteLine("자네는 마법사로군. 어서 이 지팡이를 들고 전투에 참여해주게");
                        Console.WriteLine("                                                          (Z : 확인)");
                        newUnit.PClass = Data.JobCode.Mage;
                        newUnit.PClassName = "마법사";
                        break;
                    }
                    else
                    {
                        Console.WriteLine("다시 입력해주세요");
                        Thread.Sleep(1000);
                        keyController.GetUserInput(keyFilter, out cheatActivated);
                    }                    
                }                
            }
            newUnit.SetLv1();
                        
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

        public static bool PrologEnd(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            string[] tConversation;

            keyController.GetUserInput(keyFilter, out cheatActivated);

            tConversation = new string[3];
            tConversation[0] = "자네가 이렇게 잘 싸우는지 몰랐군";
            tConversation[1] = "자네도 앞으로는 직접 던전으로 가서 싸워도 되겠어";
            tConversation[2] = $"앞으로 잘 부탁하네 \"{Program.playerData.entry[0].Name}\"";
            ScenePreset.Conversation(tConversation, keyController);

            Console.WriteLine("마을로 이동합니다");

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.Z:
                        next = Scenes.Town_Default;
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