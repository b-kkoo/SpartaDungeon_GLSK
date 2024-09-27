using SpartaDungeon_GLSK.Data;
using SpartaDungeon_GLSK.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK
{
    internal class PlayerData
    {
        public ItemData itemData = new ItemData();

        Dictionary<int, int> invetory = new Dictionary<int, int>();

        public void UseItem() //소비아이템 사용
        {
            Item item = itemData.GetItem(IC.Potion1);

            string hp = item.name;
        }

        public void StoreItem(IC code) //인벤토리에 아이템 목록 추가
        {
            invetory.Add((int)code, 1);
        }
    }
}
