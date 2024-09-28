using System.Collections.Generic;
using System.Net.Security;
using System.Reflection.Emit;
using static SpartaDungeon_GLSK.Data.Monster;

namespace SpartaDungeon_GLSK.Data
{
    public class WorldMonster
    {
        public Monster monster { get; }
        public bool isAlive { get; set; }  //생존여부
        public int currentHp { get; set; } //현재 HP

        public WorldMonster(MonsterCode _code)
        {
            monster = MonsterData.GetMonster(_code);
            isAlive = true;
            currentHp = monster.hp;
        }
    }

    public static class MonsterData
    {
        private static List<Monster> monsters { get; set; }

        public static void Set()
        {
            monsters = new List<Monster>();
            monsters.Capacity = 100;

            //                                                                                           level   hp      attack
            monsters.Add(new Monster(MonsterCode.CommonMonster1,    "일반 몬스터1", MonsterType.Common,  1,      50,     5));
            monsters.Add(new Monster(MonsterCode.CommonMonster2,    "일반 몬스터2", MonsterType.Common,  1,      50,     5));
            monsters.Add(new Monster(MonsterCode.CommonMonster3,    "일반 몬스터3", MonsterType.Common,  1,  50, 5));
            monsters.Add(new Monster(MonsterCode.CommonMonster4,    "일반 몬스터4", MonsterType.Common,  1,  50, 5));
            monsters.Add(new Monster(MonsterCode.CommonMonster4,    "일반 몬스터5", MonsterType.Common, 1, 50, 5));
            monsters.Add(new Monster(MonsterCode.SpecialMonster1,   "특수 몬스터1", MonsterType.Special,  1,  50, 5));
            monsters.Add(new Monster(MonsterCode.SpecialMonster1,   "특수 몬스터2", MonsterType.Special,  1,  50, 5));
            monsters.Add(new Monster(MonsterCode.SpecialMonster1,   "특수 몬스터3", MonsterType.Special, 1, 50, 5));
            monsters.Add(new Monster(MonsterCode.BossMonster1,      "보스 몬스터1", MonsterType.Boss,  1,  50, 5));
            monsters.Add(new Monster(MonsterCode.BossMonster2,      "보스 몬스터2", MonsterType.Boss,  1,  50, 5));
            monsters.Add(new Monster(MonsterCode.BossMonster3,      "보스 몬스터3", MonsterType.Boss, 1, 50, 5));

        }

        public static Monster GetMonster(MonsterCode code)
        {
            return monsters.Find(i => i.code == code);
        }

    }









    public class Monster
    {
        public MonsterCode code { get; }
        public string name { get; }
        public MonsterType type { get; }

        public int level { get; }
        public int hp { get; }
        public int attack { get; }

        public Monster(MonsterCode _code, string _name, MonsterType _type, int _level, int _hp, int _attack)
        {
            code = _code;
            name = _name;
            type = _type;

            hp = _hp;
            level = _level;
            attack = _attack;
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
}
