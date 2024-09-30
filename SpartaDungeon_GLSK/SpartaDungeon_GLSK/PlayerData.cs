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
        //스테이터스 영역
        public string Name { get; set; }
        public JobCode Chad { get; set; }
        public string ChadName { get; set; }
        public int Lv { get; set; }
        public int Hp { get; set; }
        public int currentHp { get; set; }
        public int Atk { get; set; }
        public int MAtk { get; set; }
        public int Def { get; set; }
        public int Speed { get; set; }
        public int CriRate { get; set; }

        //이 함수가 호출되기 전에 플레이어의 직업은 정해져 있어야 됨
        public void SetLv1() //set lv1함수
        {
            Lv = 1;
            Job playerClass = JobDatabase.GetJob(Chad);
            Hp = playerClass.initialHp;
            currentHp = playerClass.initialHp;
            Atk = playerClass.initialAtk;
            MAtk = playerClass.initialMAtk;
            Def = playerClass.initialDef;
            Speed = playerClass.initialSpeed;
            CriRate = playerClass.initialCriRate;

            if      (Chad == JobCode.Warrior)  skillList.Add(PSkillCode.W_Basic);
            else if (Chad == JobCode.Archer)   skillList.Add(PSkillCode.A_Basic);
            else if (Chad == JobCode.Mage)     skillList.Add(PSkillCode.M_Basic);

        }

        public void LvUp() //lv up함수
        {

        }


        //리스트 영역
        public Dictionary<PotionCode, int> invenPotion = new Dictionary<PotionCode, int>();
        public Dictionary<GearCode, bool> invenGear = new Dictionary<GearCode, bool>(); //현재 장비 아이템은 중복으로 소지 못하게 함(Dictionary)
        public List<PSkillCode> skillList = new List<PSkillCode>();

    }
}
