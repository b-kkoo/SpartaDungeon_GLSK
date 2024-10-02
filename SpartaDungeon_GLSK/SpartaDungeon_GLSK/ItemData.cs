using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK
{
    public class ItemData
    {
    }

    public class WorldGear //인벤토리에 가지고 있는 장비 객체
    {
        public GearCode gear { get; }
        public int wearer { get; set; } //현재 착용자. -1이면 아무도 착용하지 않은 상태

        public WorldGear(GearCode code, int _wearer = -1)
        {
            gear = code;
            wearer = _wearer;
        }
    }


    public class WorldPotion
    {
        public PotionCode potion { get; }
        public int stack { get; set; }

        public WorldPotion(PotionCode code, int _stack)
        {
            potion = code;
            stack = _stack;
        }
    }
}
