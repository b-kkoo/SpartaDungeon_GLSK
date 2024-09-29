using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Scene
{
    internal class _Test_leecoading
    {
        public static bool Test(out Scenes next, KeyController keyController)
        {
            next = Scenes.Main_Menu;
            return true;
        }
    }
}
