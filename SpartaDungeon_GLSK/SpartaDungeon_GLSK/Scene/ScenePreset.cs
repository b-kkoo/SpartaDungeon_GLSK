using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Scene
{
    public class ScenePreset
    {

        //Z키만으로 대화 진행
        public static void Conversation(string[] conversationPreset, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            keyController.GetUserInput(keyFilter, out int cheatActivate);

            ConsoleKey keyInput;
            keyFilter = new ConsoleKey[] { ConsoleKey.Z };
            for (int i = 0; i < conversationPreset.Length; i++)
            {
                Console.WriteLine(conversationPreset[i]);
                Console.WriteLine("                                                          Z-확인");
                while (true)
                {
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivate);
                    if (keyInput == ConsoleKey.Z) break;
                }
            }
        }
    }
}
