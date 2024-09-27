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
        public static Job Warrior = new Job(Chad.Warrior, 100, 5, 10, 30);

        public static Job Archer = new Job(Chad.Archer, 80, 7, 7, 30);

        public static Job Mage = new Job(Chad.Mage, 70, 10, 5, 30);
    }

    public class Job
    {
        public Chad Chad { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int CriticalRate { get; set; }

        public Job(Chad _chad, int _hp, int _atk, int _def, int _criticalRate)
        {
            Chad = _chad;
            Hp = _hp;
            Atk = _atk;
            Def = _def;
            CriticalRate = _criticalRate;
        }
    }

    public enum Chad
    {
        Warrior,
        Archer,
        Mage
    }
}
       