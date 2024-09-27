using System.Collections.Generic;

namespace SpartaDungeon_GLSK.Data
{

    internal class ItemData
    {
        List<Item> list { get; }

        public ItemData()
        {
            list = new List<Item>();
            list.Capacity = 100;

            //                코드(중복X!!)     이름            타입            Atk    Def
            list.Add(new Item(IC.Potion1,      "포션1",        IT.PotionHP,     0,     0));
            list.Add(new Item(IC.Potion2,      "포션2",        IT.PotionHP,     0,     0));
            list.Add(new Item(IC.Sword1,       "검1",          IT.WeaponS,      5,     0));
            list.Add(new Item(IC.Sword2,       "검2",          IT.WeaponS,      7,     0));
            list.Add(new Item(IC.Bow1,         "활1",          IT.WeaponB,      6,     0));
            list.Add(new Item(IC.Bow2,         "활2",          IT.WeaponB,      8,     0));
            list.Add(new Item(IC.Wand1,        "지팡이1",      IT.WeaponW,      7,     0));
            list.Add(new Item(IC.Wand2,        "지팡이2",      IT.WeaponW,      9,     0));
            list.Add(new Item(IC.HeavyArmor1,  "중갑옷1",      IT.ArmorHA,      0,     7));
            list.Add(new Item(IC.HeavyArmor2,  "중갑옷2",      IT.ArmorHA,      0,     9));
            list.Add(new Item(IC.LightArmor1,  "경갑옷1",      IT.ArmorLA,      0,     6));
            list.Add(new Item(IC.LightArmor2,  "경갑옷2",      IT.ArmorLA,      0,     8));
            list.Add(new Item(IC.Robe1,        "로브1",        IT.ArmorR,       0,     5));
            list.Add(new Item(IC.Robe2,        "로브2",        IT.ArmorR,       0,     7));

            //아이템 추가
        }

        public Item GetItem(IC code)
        {
            return list.Find(i => i.code == code);
        }
    }

    public class Item
    {
        public IC code { get; }
        public string name { get; }
        public IT type { get; }
        public int atk { get; }
        public int def {  get; }


        //필드 추가

        public Item(IC _code, string _name, IT _type, int _atk, int _def)
        {
            code = _code;
            name = _name;
            type = _type;
            atk = _atk;
            def = _def;

            //필드 추가
        }
    }

    //아이템 식별자
    public enum IC
    {
        Potion1,
        Potion2,
        Sword1,
        Sword2,
        Bow1,
        Bow2,
        Wand1,
        Wand2,
        HeavyArmor1,
        HeavyArmor2,
        LightArmor1,
        LightArmor2,
        Robe1,
        Robe2
    }

    //아이템 타입
    public enum IT
    {
        PotionHP,
        WeaponS,
        WeaponB,
        WeaponW,
        ArmorHA,
        ArmorLA,
        ArmorR
    }
}
