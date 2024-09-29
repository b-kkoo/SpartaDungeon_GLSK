using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon_GLSK.Data.JobDatabase;
using System.Xml.Linq;

namespace SpartaDungeon_GLSK.Data
{

    public static class JobDatabase
    {
        public static Job Warrior = new Job("전사", Chad.Warrior, 100, 5, 10, 30);

        public static Job Archer = new Job("궁수", Chad.Archer, 80, 7, 7, 40);

        public static Job Mage = new Job("마법사", Chad.Mage, 70, 10, 5, 30);
    }

    public class Job
    {
        public string Name { get; set; }
        public Chad Chad { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int CriRate {  get; set; }   

        public Job(string _name, Chad _chad, int _hp, int _atk, int _def, int _CriRate)
        {
            Name = _name;
            Chad = _chad;
            Hp = _hp;
            Atk = _atk;
            Def = _def;
            CriRate = _CriRate;
        }
    }

    public enum Chad
    {
        Warrior,
        Archer,
        Mage
    }
}
       