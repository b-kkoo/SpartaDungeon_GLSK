using SpartaDungeon_GLSK.Data;
using SpartaDungeon_GLSK.Scene;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SpartaDungeon_GLSK
{
    public class PlayerData
    {
        public string Name { get; set; }
        public Chad Chad { get; set; }
        public int Lv { get; set; }
        public int Hp { get; set; }
        public int Atk { get; set; }
        public int Def { get; set; }
        public int CriticalRate { get; set; }

        public void SetLv1() //set lv1함수
        {
            //if (player가 Warrior 선택 시)
            //{
            //    Warrior 정보 불러오기
            //}
            //else if (player가 Archer 선택 시)
            //{
            //    Archer 정보 불러오기
            //}
            //else //player가 Mage 선택 시
            //{
            //    Mage 정보 불러오기
            //}
        }

        public void LvUp() //lv up함수
        {
            //Warrior의 경우
            if (Chad == Chad.Warrior)
            {
                Lv++;
                Hp+=10;
                Atk+=1;
                Def+=3;
            }
            //Archer의 경우
            else if (Chad == Chad.Archer)
            {
                Lv++;
                Hp+=5;
                Atk+=2;
                Def+=2;
                CriticalRate++;
            }
            //Mage의 경우
            else
            {
                Lv++;
                Hp += 5;
                Atk += 3;
                Def += 1;
            }
        }
    }
}
