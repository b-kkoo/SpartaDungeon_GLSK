namespace SpartaDungeon_GLSK
{
    public class KeyController
    {
        //치트코드는 영문자와 스페이스만 받음
        int[] cheat1 = null;
        int[] cheat2 = null;
        int[] cheat3 = null;
        int cheat1Fill = 0;
        int cheat2Fill = 0;
        int cheat3Fill = 0;


        public ConsoleKey GetUserInput(ConsoleKey[] filter, out int cheatActive)
        {
            ConsoleKey keyReturn = 0;
            ConsoleKey keyInput;
            cheatActive = 0;

            while (Console.KeyAvailable)
            {
                keyInput = Console.ReadKey(true).Key;
                foreach (ConsoleKey k in filter)
                {
                    if (keyInput == k) keyReturn = keyInput;
                }

                //치트 검사
                if (cheat1 != null)
                {
                    if (cheat1Fill < cheat1.Length)
                    {
                        if (cheat1[cheat1Fill] == (int)keyInput)
                        {
                            cheat1Fill++;
                            if (cheat1Fill == cheat1.Length)
                            {
                                cheatActive = 1;
                                cheat1Fill = 0;
                            }
                        }
                        else
                        {
                            cheat1Fill = 0;
                        }
                    }
                }
                if (cheat2 != null)
                {
                    if (cheat2Fill < cheat2.Length)
                    {
                        if (cheat2[cheat2Fill] == (int)keyInput)
                        {
                            cheat2Fill++;
                            if (cheat2Fill == cheat2.Length)
                            {
                                cheatActive = 2;
                                cheat2Fill = 0;
                            }
                        }
                        else
                        {
                            cheat2Fill = 0;
                        }
                    }
                }
                if (cheat3 != null)
                {
                    if (cheat3Fill < cheat3.Length)
                    {
                        if (cheat3[cheat3Fill] == (int)keyInput)
                        {
                            cheat3Fill++;
                            if (cheat3Fill == cheat3.Length)
                            {
                                cheatActive = 3;
                                cheat3Fill = 0;
                            }
                        }
                        else
                        {
                            cheat3Fill = 0;
                        }
                    }
                }
            }

            return keyReturn;
        }

        public void ClearCheat()
        {
            cheat1 = null;
            cheat2 = null;
            cheat3 = null;
            cheat1Fill = 0;
            cheat2Fill = 0;
            cheat3Fill = 0;
        }
        public void SetCheat(int n, string s)
        {
            if (n < 1 || n > 3)
            {
                return;
            }

            List<int> cheatCodeInt = new List<int>();
            string cheatCodeStr = s.ToUpper();

            for (int i = 0; i < cheatCodeStr.Length; i++)
            {
                if (cheatCodeStr[i] == ' ' || ('A' <= cheatCodeStr[i] && cheatCodeStr[i] <= 'Z'))
                {
                    cheatCodeInt.Add(cheatCodeStr[i]);
                }
            }

            switch (n)
            {
                case 1: cheat1 = cheatCodeInt.ToArray(); break;
                case 2: cheat2 = cheatCodeInt.ToArray(); break;
                case 3: cheat3 = cheatCodeInt.ToArray(); break;
            }
        }
    }
}
