using System.Collections.Generic;
using System.Net.Security;
using System.Reflection.Emit;
using static SpartaDungeon_GLSK.Data.Monster;

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

            //                                                                                           level   hp      attack
            monsters.Add(MonsterCode.CommonMonster1,    new Monster(  "일반 몬스터1", MonsterType.Common,  1,      50,     5));
            monsters.Add(MonsterCode.CommonMonster2,    new Monster(  "일반 몬스터2", MonsterType.Common,  3,      50,     5));
            monsters.Add(MonsterCode.CommonMonster3,    new Monster(  "일반 몬스터3", MonsterType.Common,  10,  50, 5));
            monsters.Add(MonsterCode.CommonMonster4,    new Monster(  "일반 몬스터4", MonsterType.Common,  1,  50, 5));
            monsters.Add(MonsterCode.CommonMonster5,    new Monster(  "일반 몬스터5", MonsterType.Common, 1, 50, 5));
            monsters.Add(MonsterCode.SpecialMonster1,   new Monster(  "특수 몬스터1", MonsterType.Special,  1,  50, 5));
            monsters.Add(MonsterCode.SpecialMonster2,   new Monster(  "특수 몬스터2", MonsterType.Special,  1,  50, 5));
            monsters.Add(MonsterCode.SpecialMonster3,   new Monster(  "특수 몬스터3", MonsterType.Special, 1, 50, 5));
            monsters.Add(MonsterCode.BossMonster1,      new Monster(  "보스 몬스터1", MonsterType.Boss,  1,  50, 5));
            monsters.Add(MonsterCode.BossMonster2,      new Monster(  "보스 몬스터2", MonsterType.Boss,  1,  50, 5));
            monsters.Add(MonsterCode.BossMonster3,      new Monster(  "보스 몬스터3", MonsterType.Boss, 1, 50, 5));

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
        public List<MSkillCode> skillList { get; } // 앞선 인덱스 부터 시전 우선순위를 가짐


        public Monster(string _name, MonsterType _type, int _level, int _exp, int _hp, int _attack, int _Mattack, int _def)
        {
            name = _name;
            type = _type;
            level = _level;
            exp = _exp;

            hp = _hp;
            attack = _attack;
            Mattack = _Mattack;
            def = _def;
        }

    }

    public enum MonsterCode
    {
        CommonMonster1,
        CommonMonster2,
        CommonMonster3,
        CommonMonster4,
        CommonMonster5,

        SpecialMonster1,
        SpecialMonster2,
        SpecialMonster3,

        BossMonster1,
        BossMonster2,
        BossMonster3
    }

    public enum MonsterType
    {
        Common, // 일반 몬스터
        Special, // 특수 몬스터
        Boss // 보스 몬스터
    }





    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // MONSTER SKILL

    public static class MSkillDatabase
    {
        private static readonly Dictionary<MSkillCode, MSkill> skills;

        static MSkillDatabase()
        {
            skills = new Dictionary<MSkillCode, MSkill>();
        }

        public static MSkill GetMSkill(MSkillCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (skills.TryGetValue(code, out MSkill skill))
            {
                return skill;
            }

            Console.WriteLine("해당 ID의 스킬을 찾을 수 없습니다.");
            return null;
        }
    }


    //몬스터 특수공격
    public class MSkill
    {
        // 스킬 개요
        public string skillName { get; }

        // 스킬 성능
        public double atkRatio { get; }
        public double matkRatio { get; }
        public int angerConsum { get; }
        public bool isSplash { get; } //전체공격 여부
        public bool needCharging { get; } //시전 집중이 필요한 스킬



        public MSkill(string _skillName, double _atkRatio, double _matkRatio, int _angerConsum, bool _isSplash, bool _needCharging)
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

    public enum MSkillCode
    {
        
    }

}
