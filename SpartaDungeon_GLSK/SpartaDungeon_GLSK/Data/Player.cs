using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon_GLSK.Data.JobData;
using System.Xml.Linq;

namespace SpartaDungeon_GLSK.Data
{
    public class JobData
    {
        public static Job Warrior = new Job(Chad.Warrior, 100, 5, 10, 10, 1, 3);

        public static Job Archer = new Job(Chad.Archer, 80, 7, 7, 5, 2, 2);

        public static Job Mage = new Job(Chad.Mage, 70, 10, 5, 3, 3, 1);
    }

    public class Job
    {
        public Chad Chad { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int Hp_lvup { get; set; }
        public int Atk_lvup { get; set; }
        public int Def_lvup { get; set; }       

        public Job(Chad _chad, int _hp, int _atk, int _def, int _HP_lvup, int _Atk_lvup, int _Def_lvup)
        {
            Chad = _chad;
            Hp = _hp;
            Atk = _atk;
            Def = _def;
            Hp_lvup = _HP_lvup;
            Atk_lvup = _Atk_lvup;
            Def_lvup = _Def_lvup;
        }       
    }

    public enum Chad
    {
        Warrior,
        Archer,
        Mage
    }
}
       