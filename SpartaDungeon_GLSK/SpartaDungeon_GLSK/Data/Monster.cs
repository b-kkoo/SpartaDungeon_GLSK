using System.Diagnostics;

namespace SpartaDungeon_GLSK.Data
{
    //정적(static) 클래스 : 일반적인 클래스가 객체를 만들어 사용하는 것과 달리, 정적 클래스는 객체를 만들 수 없다.
    //                      static 클래스는 그 안의 필드와 메서드도 전부 static 키워드를 써야 한다.
    //                      데이터베이스를 정적 클래스로 만든 이유는 그렇게 하면 데이터는 프로그램 전반에서 공유되며, 메모리에 한 번만 로드되고 내용이 변경되지 않도록 관리할 수 있기 때문.
    //                      또한 정적 클래스의 정적 생성자는 아무 멤버가 참조되기 전에 자동으로 호출되기 때문에 초기화하지 않은 객체의 멤버에 접근하려는 NullReferenceException 에러를 방지할 수 있다.
    public static class MonsterDatabase
    {
        // 모든 몬스터 데이터를 저장하는 읽기 전용 Dictionary
        private static readonly Dictionary<MonsterCode, Monster> monsters;

        //정적 생성자에는 public, private와 같은 액세스 한정자를 사용할 수 없음
        static MonsterDatabase()
        {
            monsters = new Dictionary<MonsterCode, Monster>();

            List<MonsterSkillCode> skillSet = new List<MonsterSkillCode>();

            //                                                             name              type                level   exp      hp       attack  mattack   def      speed    criRate     skilllist
            monsters.Add(MonsterCode.Comm_Slime,        new Monster(  "슬라임",       MonsterType.Common,          1,     5,      10,        1,       0,      0,        1,        5/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Slime })); // 튜토리얼 몬스터

            monsters.Add(MonsterCode.Comm_Goblin,       new Monster(  "고블린",       MonsterType.Common,          3,    10,      20,        2,       0,      0,        3,        6/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Goblin })); // 초기 몬스터
            monsters.Add(MonsterCode.Comm_Hobgoblin,    new Monster(  "홉 고블린",    MonsterType.Common,          4,    15,      25,        3,       0,      1,        4,        8/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Goblin, MonsterSkillCode.Melee_Goblin })); // 초기 몬스터
            monsters.Add(MonsterCode.Spec_GoblinMage,   new Monster(  "고블린 법사",  MonsterType.Special,         5,    20,      15,        1,       8,      0,        3,       10/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_GoblinMage, MonsterSkillCode.Magic_GoblinMage })); // 초기 특수 몬스터
            monsters.Add(MonsterCode.Boss_GoblinLord,   new Monster(  "고블린 로드",  MonsterType.Boss,           10,    30,      50,        5,       5,      2,        4,       10/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_GoblinLord, MonsterSkillCode.Magic_GoblinLord })); // 초기 보스 몬스터
                                                                                                                                                                                                                    
            monsters.Add(MonsterCode.Comm_Skeleton,     new Monster(  "스켈레톤",     MonsterType.Common,          7,    15,      30,        4,       0,      1,        2,       12/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Skeleton, MonsterSkillCode.Melee_Skeleton })); // 중반부 몬스터
            monsters.Add(MonsterCode.Comm_Ghoul,        new Monster(  "구울",         MonsterType.Common,          9,    20,      40,        5,       0,      1,        3,       13/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Ghoul, MonsterSkillCode.Melee_Ghoul })); // 중반부 몬스터
            monsters.Add(MonsterCode.Comm_DeathKnight,  new Monster(  "데스나이트",   MonsterType.Common,         10,    30,      50,        5,       0,      1,        2,       13/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_DeathKnight, MonsterSkillCode.Melee_DeathKnight })); // 중반부 몬스터
            monsters.Add(MonsterCode.Spec_Lich,         new Monster(  "리치",         MonsterType.Special,        13,    50,      30,        2,      17,      2,        5,       15/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Lich, MonsterSkillCode.Magic_Lich, MonsterSkillCode.Magic_Lich2 })); // 중반부 특수 몬스터
            monsters.Add(MonsterCode.Boss_Necromancer,  new Monster(  "네크로맨서",   MonsterType.Boss,           20,   100,     100,        9,      10,      3,        7,       20/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Necromancer, MonsterSkillCode.Melee_Necromancer, MonsterSkillCode.Magic_Necromancer, MonsterSkillCode.Magic_Necromancer2, MonsterSkillCode.Magic_Necromancer3 })); // 중반부 보스 몬스터

            monsters.Add(MonsterCode.Comm_Hatchling,    new Monster(  "해츨링",       MonsterType.Common,         15,    70,      70,       11,       0,      2,       10,       20/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Hatchling, MonsterSkillCode.Melee_Hatchling })); // 후반부 몬스터
            monsters.Add(MonsterCode.Comm_Wyvern,       new Monster(  "성룡",         MonsterType.Common,         20,   100,      90,        8,       8,      3,       12,       20/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Wyvern, MonsterSkillCode.Melee_Wyvern, MonsterSkillCode.Magic_Wyvern })); // 후반부 몬스터
            monsters.Add(MonsterCode.Spec_Dragon,       new Monster(  "고룡",         MonsterType.Common,         22,   120,     110,       10,      15,      3,       15,       20/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_Dragon, MonsterSkillCode.Melee_Dragon, MonsterSkillCode.Magic_Dragon, MonsterSkillCode.Magic_Dragon2 })); // 후반부 특수 몬스터
            monsters.Add(MonsterCode.Boss_AncientDragon,new Monster("에인션트 드래곤",MonsterType.Common,         30,   250,     300,       15,      17,      4,       20,       30/*%*/,  new MonsterSkillCode[] { MonsterSkillCode.Basic_AncientDragon, MonsterSkillCode.Melee_AncientDragon, MonsterSkillCode.Magic_AncientDragon, MonsterSkillCode.Magic_AncientDragon2, MonsterSkillCode.Magic_AncientDragon3 })); // 후반부 특수 몬스터

            monsters.Add(MonsterCode.Comm_HellHound,    new Monster(  "헬 하운드",    MonsterType.Common,         30,   200,     220,       12,      12,      4,       15,       30/*%*/,  new MonsterSkillCode[] { })); // 최종 일반 몬스터
            monsters.Add(MonsterCode.Comm_Demon1,       new Monster(  "하급 마족",    MonsterType.Common,         32,   250,     300,       15,      15,      5,       20,       30/*%*/,  new MonsterSkillCode[] { })); // 최종 일반 몬스터
            monsters.Add(MonsterCode.Comm_Demon2,       new Monster(  "중급 마족",    MonsterType.Common,         35,   300,     330,       19,      18,      5,       20,       30/*%*/,  new MonsterSkillCode[] { })); // 최종 일반 몬스터
            monsters.Add(MonsterCode.Comm_Demon3,       new Monster(  "상급 마족",    MonsterType.Common,         40,   350,     370,       24,      26,      6,       20,       30/*%*/,  new MonsterSkillCode[] { })); // 최종 일반 몬스터
            monsters.Add(MonsterCode.Spec_Cerberus,     new Monster(  "켈베로스",     MonsterType.Special,        45,   400,     500,       30,      33,      8,       20,       35/*%*/,  new MonsterSkillCode[] { })); // 최종 특수 몬스터
            monsters.Add(MonsterCode.Boss_Diablo,       new Monster(  "마왕 ",        MonsterType.Boss,           50,  1000,    1000,       50,      60,     10,       25,       40/*%*/,  new MonsterSkillCode[] { })); // 최종 보스 몬스터


        }

        public static Monster GetMonster(MonsterCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (monsters.TryGetValue(code, out Monster monster))
            {
                return monster;
            }

            Console.WriteLine("해당 ID의 몬스터를 찾을 수 없습니다.");
            return null;
        }

    }


    public class Monster
    {
        // 몬스터 개요
        public string name { get; }
        public MonsterType type { get; }
        public int level { get; }
        public int exp { get; } // 처치 시 경험치

        // 몬스터 스텟
        public int hp { get; }
        public int attack { get; }
        public int Mattack { get; }
        public int def { get; }
        public int speed { get; }
        public int criRate { get; }
        public List<MonsterSkillCode> skillList { get; } // 앞선 인덱스 부터 시전 우선순위를 가짐


        public Monster(string _name, MonsterType _type, int _level, int _exp, int _hp, int _attack, int _Mattack, int _def, int _speed, int _criRate, MonsterSkillCode[] _skillList)
        {
            name = _name;
            type = _type;
            level = _level;
            exp = _exp;

            hp = _hp;
            attack = _attack;
            Mattack = _Mattack;
            def = _def;
            speed = _speed;
            criRate = _criRate;
            skillList = _skillList.ToList();
        }

    }

    public enum MonsterCode
    {
        Comm_Slime,

        Comm_Goblin,
        Comm_Hobgoblin,
        Comm_Skeleton,
        Comm_Ghoul,
        Comm_DeathKnight,
        Comm_Hatchling,
        Comm_Wyvern,
        Comm_HellHound,
        Comm_Demon1,
        Comm_Demon2,
        Comm_Demon3,

        Spec_GoblinMage,
        Spec_Lich,
        Spec_Dragon,
        Spec_Cerberus,

        Boss_GoblinLord,
        Boss_Necromancer,
        Boss_AncientDragon,
        Boss_Diablo
       
    }

    public enum MonsterType
    {
        Common, // 일반 몬스터
        Special, // 특수 몬스터
        Boss // 보스 몬스터
    }





    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // MONSTER SKILL

    public static class MonsterSkillDatabase
    {
        private static readonly Dictionary<MonsterSkillCode, MonsterSkill> skills;

        static MonsterSkillDatabase()
        {
            skills = new Dictionary<MonsterSkillCode, MonsterSkill>();

            //                                                                Name               Att    M.Att   Consum    Splash    Charging
            skills.Add(MonsterSkillCode.Basic_Slime,       new MonsterSkill("몸통박치기",         1,      0,      0,      false,    false));

            skills.Add(MonsterSkillCode.Basic_Goblin,      new MonsterSkill("몽둥이 휘두르기",    1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_Goblin,      new MonsterSkill("내려찍기",         1.5,      0,     15,      false,    false));
            skills.Add(MonsterSkillCode.Basic_GoblinMage,  new MonsterSkill("토템 휘두르기",      1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Magic_GoblinMage,  new MonsterSkill("다크 볼",            0,    1.5,     15,      false,    false));
            skills.Add(MonsterSkillCode.Basic_GoblinLord,  new MonsterSkill("마구 찍기",          1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_GoblinLord,  new MonsterSkill("휩쓸기",           1.5,      0,      0,       true,    false));
            skills.Add(MonsterSkillCode.Magic_GoblinLord,  new MonsterSkill("파이어볼",           0,      2,      0,       true,    false));

            skills.Add(MonsterSkillCode.Basic_Skeleton,    new MonsterSkill("휘두르기",           1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_Skeleton,    new MonsterSkill("찌르기",           1.5,      0,     15,      false,    false));
            skills.Add(MonsterSkillCode.Basic_Ghoul,       new MonsterSkill("할퀴기",             1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_Ghoul,       new MonsterSkill("물어뜯기",         1.5,      0,     15,      false,    false));
            skills.Add(MonsterSkillCode.Basic_DeathKnight, new MonsterSkill("참격",               1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_DeathKnight, new MonsterSkill("일검격",           1.5,      0,     15,      false,    false));
            skills.Add(MonsterSkillCode.Basic_Lich,        new MonsterSkill("저주의 손",          1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Magic_Lich,        new MonsterSkill("데스 샤워",          0,    1.5,     10,       true,    false));
            skills.Add(MonsterSkillCode.Magic_Lich2,       new MonsterSkill("영혼 수확",          0,    2.0,     20,      false,    false));
            skills.Add(MonsterSkillCode.Basic_Necromancer, new MonsterSkill("죽음의 낫",          1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_Necromancer, new MonsterSkill("공간 절기",        1.5,      0,     10,       true,    false));
            skills.Add(MonsterSkillCode.Magic_Necromancer, new MonsterSkill("강령술",             0,    1.2,     10,       true,    false));
            skills.Add(MonsterSkillCode.Magic_Necromancer2,new MonsterSkill("망자소환",           0,    2.5,     15,      false,    false));
            skills.Add(MonsterSkillCode.Magic_Necromancer3,new MonsterSkill("망자의 절규",        0,    2.5,     20,       true,     true));

            skills.Add(MonsterSkillCode.Basic_Hatchling,   new MonsterSkill("용의 발톱",          1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_Hatchling,   new MonsterSkill("비행 박치기",      1.5,      0,     15,      true,     false));
            skills.Add(MonsterSkillCode.Basic_Wyvern,      new MonsterSkill("내려 찍기",          1,      0,      0,      false,    false));
            skills.Add(MonsterSkillCode.Melee_Wyvern,      new MonsterSkill("비행 돌진",        1.5,      0,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Wyvern,      new MonsterSkill("와이번 브레스",      0,      3,     30,      true,      true));
            skills.Add(MonsterSkillCode.Basic_Dragon,      new MonsterSkill("드래곤 패기",        1,      0,      0,     false,     false));
            skills.Add(MonsterSkillCode.Melee_Dragon,      new MonsterSkill("칼날 바람",          0,    1.5,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Dragon,      new MonsterSkill("용의 숨결",          0,      2,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Dragon2,     new MonsterSkill("드래곤 피어",        0,    3.5,     30,      true,      true));
            skills.Add(MonsterSkillCode.Basic_AncientDragon, new MonsterSkill("충격파",           1,      0,      0,      true,     false));
            skills.Add(MonsterSkillCode.Melee_AncientDragon, new MonsterSkill("대지 가르기",    1.5,      0,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_AncientDragon, new MonsterSkill("용언",             0,      2,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_AncientDragon2,new MonsterSkill("메테오",           0,      3,     20,      true,      true));
            skills.Add(MonsterSkillCode.Magic_AncientDragon3,new MonsterSkill("인페르노 브레스",  0,      4,     30,      true,      true));

            skills.Add(MonsterSkillCode.Basic_HellHound,   new MonsterSkill("짐승의 손갈퀴",      1,      0,      0,     false,     false));
            skills.Add(MonsterSkillCode.Melee_HellHound,   new MonsterSkill("지반 강타",        1.5,      0,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_HellHound,   new MonsterSkill("지옥불",             0,      2,     30,      true,      true));
            skills.Add(MonsterSkillCode.Basic_Demon1,      new MonsterSkill("암흑 타격",          1,      0,      0,     false,     false));
            skills.Add(MonsterSkillCode.Melee_Demon1,      new MonsterSkill("암흑 강타",        1.5,      0,     15,     false,     false));
            skills.Add(MonsterSkillCode.Magic_Demon1,      new MonsterSkill("생기 갈취",          0,      4,     30,     false,     false));
            skills.Add(MonsterSkillCode.Basic_Demon2,      new MonsterSkill("암흑 베기",          1,      0,      0,     false,     false));
            skills.Add(MonsterSkillCode.Melee_Demon2,      new MonsterSkill("암흑 절기",          2,      0,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Demon2,      new MonsterSkill("영혼 폭발",          0,      4,     30,      true,      true));
            skills.Add(MonsterSkillCode.Basic_Demon3,      new MonsterSkill("블러드 소드",        1,      0,      0,     false,     false));
            skills.Add(MonsterSkillCode.Melee_Demon3,      new MonsterSkill("피의 연격",          2,      0,     15,     false,     false));
            skills.Add(MonsterSkillCode.Magic_Demon3,      new MonsterSkill("망자의 노래",        0,      3,     20,      true,     false));
            skills.Add(MonsterSkillCode.Magic2_Demon3,     new MonsterSkill("베놈 서클",          0,      4,     30,      true,      true));
            skills.Add(MonsterSkillCode.Basic_Cerberus,    new MonsterSkill("마권",               1,      0,      0,     false,     false));
            skills.Add(MonsterSkillCode.Melee_Cerberus,    new MonsterSkill("진귀참",           1.5,      0,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Cerberus,    new MonsterSkill("불벼락",             0,    2.5,     30,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Cerberus2,   new MonsterSkill("피의 욕망",          0,      3,     30,      true,      true));
            skills.Add(MonsterSkillCode.Magic_Cerberus3,   new MonsterSkill("대죄 심판",          0,      5,     30,      true,      true));
            skills.Add(MonsterSkillCode.Basic_Diablo,      new MonsterSkill("마기참",             1,      0,      0,     false,     false));
            skills.Add(MonsterSkillCode.Melee_Diablo,      new MonsterSkill("데스사이즈",       1.5,      0,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Diablo,      new MonsterSkill("지옥문",             0,      3,     15,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Diablo2,     new MonsterSkill("마왕 패기",          0,      3,     20,      true,     false));
            skills.Add(MonsterSkillCode.Magic_Diablo3,     new MonsterSkill("즉결 처형",          0,      4,     20,      true,      true));
            skills.Add(MonsterSkillCode.Magic_Diablo4,     new MonsterSkill("영겁의 굴레",        4,      4,     30,      true,      true));

        }

        public static MonsterSkill GetMSkill(MonsterSkillCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (skills.TryGetValue(code, out MonsterSkill skill))
            {
                return skill;
            }

            Console.WriteLine("해당 ID의 스킬을 찾을 수 없습니다.");
            return null;
        }
    }


    //몬스터 특수공격
    public class MonsterSkill
    {
        // 스킬 개요
        public string skillName { get; }

        // 스킬 성능
        public double atkRatio { get; }
        public double matkRatio { get; }
        public int angerConsum { get; }
        public bool isSplash { get; } //전체공격 여부
        public bool needCharging { get; } //시전 집중이 필요한 스킬



        public MonsterSkill(string _skillName, double _atkRatio, double _matkRatio, int _angerConsum, bool _isSplash, bool _needCharging)
        {
            skillName = _skillName;

            atkRatio = _atkRatio;
            matkRatio = _matkRatio;
            angerConsum = _angerConsum;
            isSplash = _isSplash;
            needCharging = _needCharging;
        }

        public double CalcDamage(Monster monster)
        {
            return monster.attack * atkRatio + monster.Mattack * matkRatio;
        }
    }

    public enum MonsterSkillCode //Basic : 기본공격, Melee : 물리공격, Magic : 마법공격, Mixed : 혼합
    {
        Basic_Slime,

        Basic_Goblin,
        Melee_Goblin,
        Basic_GoblinMage,
        Magic_GoblinMage,
        Basic_GoblinLord,
        Melee_GoblinLord,
        Magic_GoblinLord,

        Basic_Skeleton,
        Melee_Skeleton,
        Basic_Ghoul,
        Melee_Ghoul,
        Basic_DeathKnight,
        Melee_DeathKnight,
        Basic_Lich,
        Magic_Lich,
        Magic_Lich2,
        Basic_Necromancer,
        Melee_Necromancer,
        Magic_Necromancer,
        Magic_Necromancer2,
        Magic_Necromancer3,

        Basic_Hatchling,
        Melee_Hatchling,
        Basic_Wyvern,
        Melee_Wyvern,
        Magic_Wyvern,
        Basic_Dragon,
        Melee_Dragon,
        Magic_Dragon,
        Magic_Dragon2,
        Basic_AncientDragon,
        Melee_AncientDragon,
        Magic_AncientDragon,
        Magic_AncientDragon2,
        Magic_AncientDragon3,

        Basic_HellHound,
        Melee_HellHound,
        Magic_HellHound,
        Basic_Demon1,
        Melee_Demon1,
        Magic_Demon1,
        Basic_Demon2,
        Melee_Demon2,
        Magic_Demon2,
        Basic_Demon3,
        Melee_Demon3,
        Magic_Demon3,
        Magic2_Demon3,
        Basic_Cerberus,
        Melee_Cerberus,
        Magic_Cerberus,
        Magic_Cerberus2,
        Magic_Cerberus3,
        Basic_Diablo,
        Melee_Diablo,
        Magic_Diablo,
        Magic_Diablo2,
        Magic_Diablo3,
        Magic_Diablo4,


    }

}
