using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK
{
    public class IngameData
    {
        //다양한 이벤트 플래그를 관리할 멤버가 필요















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
