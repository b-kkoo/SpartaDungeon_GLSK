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
