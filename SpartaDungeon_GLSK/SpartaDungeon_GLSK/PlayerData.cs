using SpartaDungeon_GLSK.Data;
using SpartaDungeon_GLSK.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeon_GLSK
{
    internal class PlayerData
    {
        public string Name { get; set; }
        public Chad Chad { get; set; }
        public int Lv { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int CriRate { get; set; }

        public void SetLv1() //set lv1함수
        {
            
        }

        public void LvUp() //lv up함수
        {
            if (Chad == Chad.Warrior)
            {
                Lv++;
                Hp += 10;
                Atk += 1;
                Def += 3;
            }
            else if (Chad == Chad.Archer)
            {
                Lv++;
                Hp += 5;
                Atk += 2;
                Def += 2;
                CriRate++;
            }
            else
            {
                Lv++;
                Hp += 5;
                Atk += 3;
                Def += 1;
            }

        }
    }
}
