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

        public void UseItem()
        {
            Item item = itemData.GetItem(IC.Potion1);

            string hp = item.name;
        }

        public void StoreItem(IC code)
        {
            invetory.Add((int)code, 1);
        }
    }
}
