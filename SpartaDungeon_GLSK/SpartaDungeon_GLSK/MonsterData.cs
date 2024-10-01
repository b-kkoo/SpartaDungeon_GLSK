using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK
{
    public class MonsterData
    {

        //필드에 구현된 몬스터
        public class WorldMonster
        {
            public Monster monster { get; }
            public bool isAlive { get; set; }
            public int currentHp { get; set; }
            public double AP { get; set; } // 행동력 (100이 돼야 턴이 돌아옴)
            public int anger { get; set; } // 분노 게이지. 공격하거나 피격당할 때 쌓이며, 스킬을 쓸 때 소모함
            public bool concentrating { get; set; } // 시전 집중 상태
            public int reservedSkill { get; set; } // 시전 준비중인 스킬 인덱스
            public int reservedTarget { get; set; } // 시전 준비중인 스킬 대상

            public WorldMonster(MonsterCode code)
            {
                monster = MonsterDatabase.GetMonster(code);
                isAlive = true;
                currentHp = monster.hp;
                AP = 0;
                anger = 0;
                concentrating = false;
            }
        }



        //Battle Scene에 넘겨 줄 필드 몬스터 
        public static WorldMonster[] GetWorldMonsters(MonsterCode[] monsterCodes)
        {
            WorldMonster[] worldMonsters = new WorldMonster[monsterCodes.Length];
            for (int i = 0; i < worldMonsters.Length; i++)
            {
                worldMonsters[i] = new WorldMonster(monsterCodes[i]);
            }
            return worldMonsters;
        }
    }
}
