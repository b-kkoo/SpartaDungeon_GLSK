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

            //        코드(중복X!!)                     이름               타입                    hp     mp    Atk    mAtk    Def  speed criRate Price
            Gears.Add(GearCode.Sword1,      new Gear(   "나무 검",       GearType.WeaponS,     0,     0,      5,     3,     0,     5,      10,   30));
            Gears.Add(GearCode.Sword2,      new Gear(   "구리 검",       GearType.WeaponS,     0,     0,      8,     6,     0,     8,      25,   55));
            Gears.Add(GearCode.Sword3,      new Gear(   "철 검",         GearType.WeaponS,     0,     0,     11,     9,     0,     11,     40,  85));
            Gears.Add(GearCode.Sword4,      new Gear(   "엑스칼리버",      GearType.WeaponS,     0,     0,     28,    20,     0,    20,     60,  700));

            Gears.Add(GearCode.Bow1,        new Gear(   "숏 보우",        GearType.WeaponB,     0,     0,      6,     5,     0,     4,     20,   35));
            Gears.Add(GearCode.Bow2,        new Gear(   "롱 보우",        GearType.WeaponB,     0,     0,      8,     8,     0,     8,     30,   55));
            Gears.Add(GearCode.Bow3,        new Gear(   "사냥꾼의 활",     GearType.WeaponB,     0,     0,     14,    13,     0,    15,     40,   85));
            Gears.Add(GearCode.Bow4,        new Gear(   "아그니 간디바",    GearType.WeaponB,     0,     0,     28,    20,     0,    20,     60,  700));

            Gears.Add(GearCode.Wand1,       new Gear(   "우드 스태프",     GearType.WeaponW,     0,     0,      7,     5,     0,     4,      20,   35));
            Gears.Add(GearCode.Wand2,       new Gear(   "사파이어 스태프",  GearType.WeaponW,     0,     0,      9,     7,     0,     8,      30,   55));
            Gears.Add(GearCode.Wand3,       new Gear(   "에메랄드 스태프",  GearType.WeaponW,     0,     0,      7,     5,     0,     15,     40,   85));
            Gears.Add(GearCode.Wand4,       new Gear(   "레바테인",        GearType.WeaponW,    0,     0,     28,    20,     0,     20,     60,  700));

            Gears.Add(GearCode.HeavyArmor1, new Gear(   "철갑옷",         GearType.ArmorHA,     5,     0,      0,     1,      7,     0,     0,   45));
            Gears.Add(GearCode.HeavyArmor2, new Gear(   "강철갑옷",        GearType.ArmorHA,    10,     0,      0,     3,      9,     0,     0,   65));
            Gears.Add(GearCode.HeavyArmor3, new Gear(   "그림자갑옷",      GearType.ArmorHA,     15,     0,      0,    5,     12,     0,     0,   85));
            Gears.Add(GearCode.HeavyArmor4, new Gear(   "드래곤뼈갑옷",     GearType.ArmorHA,    24,     0,      0,     10,    24,     0,     0,  700));
             
            Gears.Add(GearCode.LightArmor1, new Gear(   "가죽갑옷",       GearType.ArmorLA,     0,     0,      0,     2,     6,      0,       0,   45));
            Gears.Add(GearCode.LightArmor2, new Gear(   "쇠사슬갑옷",      GearType.ArmorLA,     0,     0,      0,     4,     8,      0,      0,   65));
            Gears.Add(GearCode.LightArmor3, new Gear(   "와이번가죽갑옷",   GearType.ArmorLA,     0,     0,      0,     6,     12,     0,      0,   85));
            Gears.Add(GearCode.LightArmor4, new Gear(   "드래곤가죽갑옷",   GearType.ArmorLA,     0,     0,      0,    10,     18,     0,      0,  700));

            Gears.Add(GearCode.Robe1,       new Gear(   "견습로브",        GearType.ArmorR,     5,     0,      0,     5,      5,      0,     0,   45));
            Gears.Add(GearCode.Robe2,       new Gear(   "마법사로브",       GearType.ArmorR,     7,     0,      0,     7,      7,      0,     0,   65));
            Gears.Add(GearCode.Robe3,       new Gear(   "대마법사로브",     GearType.ArmorR,     9,     0,      0,     10,      9,     0,     0,   85));
            Gears.Add(GearCode.Robe4,       new Gear(   "현자로브",        GearType.ArmorR,    15,     0,      0,     20,     15,     0,     0,  700));
 
            Gears.Add(GearCode.Ring1,       new Gear(   "마나반지",        GearType.Ring,       0,     8,      0,     6,     4,     0,      0,   50));
            Gears.Add(GearCode.Ring2,       new Gear(   "활력반지",        GearType.Ring,       8,     0,      0,     6,     4,     0,      0,   65));
            Gears.Add(GearCode.Ring3,       new Gear(   "시작반지",        GearType.Ring,      10,    10,      0,     6,     4,     0,      0,   80));
            Gears.Add(GearCode.Ring4,       new Gear(   "만능반지",        GearType.Ring,      20,    20,      20,   20,    20,     0,      0,  700));
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
        public int Price { get; } 

        //장비 옵션
        public int hp { get; }
        public int mp { get; }
        public int atk { get; }
        public int mAtk { get; }
        public int def { get; }
        public int speed { get; }
        public int criRate { get; }


        public Gear(string _name, GearType _type, int _hp, int _mp, int _atk, int _mAtk, int _def, int _speed, int _criRate, int _Price)
        {
            name = _name;
            type = _type;
            hp = _hp;
            mp = _mp;
            atk = _atk;
            mAtk = _mAtk;
            def = _def;
            speed = _speed;
            criRate = _criRate;
            Price = _Price;


        }
    }

    //아이템 식별자
    public enum GearCode
    {
        NONE = -1,

        Sword1,
        Sword2,
        Sword3,
        Sword4,

        Bow1,
        Bow2,
        Bow3,
        Bow4,

        Wand1,
        Wand2,
        Wand3,
        Wand4,

        HeavyArmor1,
        HeavyArmor2,
        HeavyArmor3,
        HeavyArmor4,

        LightArmor1,
        LightArmor2,
        LightArmor3,
        LightArmor4,

        Robe1,
        Robe2,
        Robe3,
        Robe4,

        Ring1,
        Ring2,
        Ring3,
        Ring4
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

    //장착 타입
    public enum GearSlot
    {
        Weapon,
        Armor,
        Ring,
        Max
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

            //          코드(중복X!!)                   이름          타입               power  Price
            Potions.Add(PotionCode.PotionHp1, new Potion("체력알약",       PotionType.HP,    20,     10));
            Potions.Add(PotionCode.PotionHp2, new Potion("체력물약",       PotionType.HP,    50,     30));
            Potions.Add(PotionCode.PotionMp1, new Potion("마나알약",       PotionType.MP,    10,     10));
            Potions.Add(PotionCode.PotionMp2, new Potion("마나물약",       PotionType.MP,    30,     40));

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
        public int Price {get;}

        //필드를 계속 추가해 보자

        public Potion(string _name, PotionType _type, int _atk, int _Price)
        {
            name = _name;
            type = _type;
            power = _atk;
            Price = _Price;
        }
    }

    //아이템 식별자
    public enum PotionCode
    {
        PotionHp1,
        PotionHp2,
        PotionMp1,
        PotionMp2,
    }

    //아이템 타입
    public enum PotionType
    {
        HP,
        MP
    }
}
