using System.Collections.Generic;

namespace SpartaDungeon_GLSK.Data
{

    public class ItemData
    {
        List<Item> list { get; }

        public ItemData()
        {
            list = new List<Item>();
            list.Capacity = 100;

            //                코드(중복X!!) 이름            타입
            list.Add(new Item(IC.Potion1,   "작은 포션",    IT.PotionHP));
            //아이템 추가
        }

        public Item GetItem(IC code)
        {
            return list.Find(i => i.code == code);
        }
    }

    public class Item
    {
        public IC code { get; }
        public string name { get; }
        public IT type { get; }

        //필드 추가

        public Item(IC _code, string _name, IT _type)
        {
            code = _code;
            name = _name;
            type = _type;

            //필드 추가
        }
    }

    //아이템 식별자
    public enum IC
    {
        Potion1,
        Potion2
    }

    //아이템 타입
    public enum IT 
    {
        PotionHP
    }
}
