using System.Collections.Generic;

namespace SpartaDungeon_GLSK.Data
{
    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // GEAR - 장비 아이템

    //정적(static) 클래스 : 일반적인 클래스가 객체를 만들어 사용하는 것과 달리, 정적 클래스는 객체를 만들 수 없다.
    //                      static 클래스는 그 안의 필드와 메서드도 전부 static 키워드를 써야 한다.
    //                      데이터베이스를 정적 클래스로 만든 이유는 그렇게 하면 데이터는 프로그램 전반에서 공유되며, 메모리에 한 번만 로드되고 내용이 변경되지 않도록 관리할 수 있기 때문.
    //                      또한 정적 클래스의 정적 생성자는 아무 멤버가 참조되기 전에 자동으로 호출되기 때문에 초기화하지 않은 객체의 멤버에 접근하려는 NullReferenceException 에러를 방지할 수 있다.
    public static class GearDatabase
    {
        // 모든 아이템 데이터를 저장하는 읽기 전용 Dictionary
        private static readonly Dictionary<GearCode, Gear> Gears;

        //정적 생성자에는 public, private와 같은 액세스 한정자를 사용할 수 없음
        static GearDatabase()
        {
            Gears = new Dictionary<GearCode, Gear>();

            //        코드(중복X!!)                     이름            타입                   Atk    Def
            Gears.Add(GearCode.Sword1,      new Gear(   "검1",          GearType.WeaponS,      5,     0));
            Gears.Add(GearCode.Sword2,      new Gear(   "검2",          GearType.WeaponS,      7,     0));
            Gears.Add(GearCode.Bow1,        new Gear(   "활1",          GearType.WeaponB,      6,     0));
            Gears.Add(GearCode.Bow2,        new Gear(   "활2",          GearType.WeaponB,      8,     0));
            Gears.Add(GearCode.Wand1,       new Gear(   "지팡이1",      GearType.WeaponW,      7,     0));
            Gears.Add(GearCode.Wand2,       new Gear(   "지팡이2",      GearType.WeaponW,      9,     0));
            Gears.Add(GearCode.HeavyArmor1, new Gear(   "중갑옷1",      GearType.ArmorHA,      0,     7));
            Gears.Add(GearCode.HeavyArmor2, new Gear(   "중갑옷2",      GearType.ArmorHA,      0,     9));
            Gears.Add(GearCode.LightArmor1, new Gear(   "경갑옷1",      GearType.ArmorLA,      0,     6));
            Gears.Add(GearCode.LightArmor2, new Gear(   "경갑옷2",      GearType.ArmorLA,      0,     8));
            Gears.Add(GearCode.Robe1,       new Gear(   "로브1",        GearType.ArmorR,       0,     5));
            Gears.Add(GearCode.Robe2,       new Gear(   "로브2",        GearType.ArmorR,       0,     7));

            //아이템을 계속 추가해 보자
        }

        public static Gear GetGear(GearCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (Gears.TryGetValue(code, out Gear gear))
            {
                return gear;
            }

            Console.WriteLine("해당 ID의 아이템을 찾을 수 없습니다.");
            return null;
        }
    }

    public class Gear
    {
        public string name { get; }
        public string info { get; }
        public GearType type { get; }

        //장비 옵션
        public int hp { get; }
        public int mp { get; }
        public int atk { get; }
        public int mAtk { get; }
        public int def { get; }
        public int speed { get; }
        public int criRate { get; }


        public Gear(string _name, GearType _type, int _atk, int _def)
        {
            name = _name;
            type = _type;
            atk = _atk;
            def = _def;
        }
    }

    //아이템 식별자
    public enum GearCode
    {
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
    public enum GearType
    {
        WeaponS, // Sword
        WeaponB, // Bow
        WeaponW, // Wand
        ArmorHA, // Heavy Armor
        ArmorLA, // Light Armor
        ArmorR,  // Robe
        Ring     // Ring
    }




    ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // POTION - 소모성 아이템


    public static class PotionDatabase
    {
        // 모든 아이템 데이터를 저장하는 읽기 전용 Dictionary
        private static readonly Dictionary<PotionCode, Potion> Potions;

        //정적 생성자에는 public, private와 같은 액세스 한정자를 사용할 수 없음
        static PotionDatabase()
        {
            Potions = new Dictionary<PotionCode, Potion>();

            //          코드(중복X!!)                   이름          타입                    power
            Potions.Add(PotionCode.Potion1, new Potion("포션1",       PotionType.HP,    20));
            Potions.Add(PotionCode.Potion2, new Potion("포션2",       PotionType.HP,    50));

            //아이템을 계속 추가해 보자
        }

        public static Potion GetPotion(PotionCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (Potions.TryGetValue(code, out Potion item))
            {
                return item;
            }

            Console.WriteLine("해당 ID의 아이템을 찾을 수 없습니다.");
            return null;
        }
    }

    public class Potion
    {
        public string name { get; }
        public PotionType type { get; }
        public int power { get; }

        //필드를 계속 추가해 보자

        public Potion(string _name, PotionType _type, int _atk)
        {
            name = _name;
            type = _type;
            power = _atk;
        }
    }

    //아이템 식별자
    public enum PotionCode
    {
        Potion1,
        Potion2,
    }

    //아이템 타입
    public enum PotionType
    {
        HP,
        MP
    }
}
