using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Data
{
    
    internal class InitializedInfo
    {
        List<Player> list { get; }

        public InitializedInfo()
        {
            list = new List<Player>();
            list.Capacity = 3;

            list.Add(new Warrior("이름", Chad.Warrior, 1, 100, 5, 10));
            list.Add(new Archer("이름", Chad.Archer, 1, 100, 7, 7));
            list.Add(new Mage("이름", Chad.Mage, 1, 100, 10, 5));
        }
        
    }

    public class Player
    {
        public string Name { get; set; }
        public Chad Chad { get; set; }
        public int Lv { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }                
    }

    public class Warrior : Player
    {
        public Warrior(string _name, Chad _chad, int _lv, int _hp, int _atk, int _def) 
        {
            Name = _name;
            Chad = _chad;
            Lv = _lv;
            Hp = _hp;
            Atk = _atk;
            Def = _def;
        }
        public void LevelUpW()
        {
            Lv ++;
            Hp += 10;
            Atk += 1;
            Def += 3;
        }
    }

    public class Archer : Player 
    {
        public Archer(string _name, Chad _chad, int _lv, int _hp, int _atk, int _def)
        {
            Name = _name;
            Chad = _chad;
            Lv = _lv;
            Hp = _hp;
            Atk = _atk;
            Def = _def;
        }
        public void LevelUpA()
        {
            Lv++;
            Hp += 5;
            Atk += 2;
            Def += 2;
        }
    }

    public class Mage : Player
    {
        public Mage(string _name, Chad _chad, int _lv, int _hp, int _atk, int _def)
        {
            Name = _name;
            Chad = _chad;
            Lv = _lv;
            Hp = _hp;
            Atk = _atk;
            Def = _def;
        }
        public void LevelUpM()
        {
            Lv++;
            Hp += 3;
            Atk += 3;
            Def += 1;
        }
    }

    public enum Chad
    {
        Warrior,
        Archer,
        Mage
    }
}
       