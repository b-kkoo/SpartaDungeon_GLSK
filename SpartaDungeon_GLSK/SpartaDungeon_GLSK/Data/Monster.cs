using System.Collections.Generic;
using System.Net.Security;
using System.Reflection.Emit;
using static SpartaDungeon_GLSK.Data.Monster;

namespace SpartaDungeon_GLSK.Data
{
    internal class WorldMonster
    {
        public Monster Monster { get; }
        public int currenthp { get; set; }

        public WorldMonster(MonsterCode _code)
        {
           
            
        }

        
    }
    internal class MonsterData
        {
            List<Monster> list { get; }

            public MonsterData()
            {

                list = new List<Monster>();
                list.Capacity = 100;

                list.Add(new Monster(MonsterCode.CommonMonster1, "일반 몬스터1", MonsterType.Common));
                list.Add(new Monster(MonsterCode.CommonMonster2, "일반 몬스터2", MonsterType.Common));
                list.Add(new Monster(MonsterCode.CommonMonster3, "일반 몬스터3", MonsterType.Common));
                list.Add(new Monster(MonsterCode.CommonMonster4, "일반 몬스터4", MonsterType.Common));
                list.Add(new Monster(MonsterCode.CommonMonster4, "일반 몬스터5", MonsterType.Common));
                list.Add(new Monster(MonsterCode.SpecialMonster1, "특수 몬스터1", MonsterType.Special));
                list.Add(new Monster(MonsterCode.SpecialMonster1, "특수 몬스터2", MonsterType.Special));
                list.Add(new Monster(MonsterCode.SpecialMonster1, "특수 몬스터3", MonsterType.Special));
                list.Add(new Monster(MonsterCode.BossMonster1, "보스 몬스터1", MonsterType.Boss));
                list.Add(new Monster(MonsterCode.BossMonster2, "보스 몬스터2", MonsterType.Boss));
                list.Add(new Monster(MonsterCode.BossMonster3, "보스 몬스터3", MonsterType.Boss));

            }

            public Monster GetMonster(MonsterCode cd)
            {

                return list.Find(i => i.cod == cd);

            }

        }   

       
    

    




    internal class Monster
    {
        public MonsterCode cod { get; }
        public string name { get; }
        public MonsterType type { get; }


        public Monster(MonsterCode _code, string _name, MonsterType _type)
        {
            cod = _code;
            name = _name;
            type = _type;
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
