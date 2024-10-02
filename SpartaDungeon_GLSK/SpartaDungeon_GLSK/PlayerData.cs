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
    // 공유 데이터
    public class PlayerData
    {
        public int Gold { get; set; }

        //인벤토리
        public List<KeyValuePair<PotionCode, int>> invenPotion; //인덱스로 접근할 수 있게 List로 선언함
        public List<WorldGear> invenGear;

        //유닛
        public List<PlayerUnitData> team;
        public PlayerUnitData[] entry;

        public PlayerData()
        {
            Gold = 0;

            invenPotion = new List<KeyValuePair<PotionCode, int>>();
            invenGear = new List<WorldGear>();

            team = new List<PlayerUnitData>();
            entry = new PlayerUnitData[3];
        }
    }

    public class PlayerUnitData
    {
        //캐릭터 기본 스테이터스
        public string Name { get; set; }
        public JobCode PClass { get; set; }
        public int Lv { get; set; }
        public int Exp { get; set; }
        public int ExpNextLevel { get; set; }

        //직업 관련 스테이터스
        public string PClassName { get; set; }
        public int Hp { get; set; }
        public int Mp { get; set; }
        public int Atk { get; set; }
        public int MAtk { get; set; }
        public int Def { get; set; }
        public int Speed { get; set; }
        public int CriRate { get; set; }

        //전투 관련 스테이터스
        public bool IsAlive { get; set; }
        public int CurrentHp { get; set; }
        public int CurrentMp { get; set; }
        //전투 시작 시 초기화
        public double AP { get; set; } // 행동력 (100이 돼야 턴이 돌아옴)
        public bool Concentrating { get; set; } // 시전 집중 상태
        public int ReservedSkill { get; set; } // 시전 준비중인 스킬 인덱스
        public int ReservedTarget { get; set; } // 시전 준비중인 스킬 대상

        //리스트 영역
        public GearCode[] Equipment = new GearCode[(int)GearSlot.Max]; //3
        public List<PlayerSkillCode> SkillList = new List<PlayerSkillCode>();



        //이 함수가 호출되기 전에 플레이어의 직업은 정해져 있어야 됨
        public void SetLv1() //초기 스테이터스
        {
            //캐릭터 기본 스테이터스(이름, 직업은 정해져 있음)
            Lv = 1;
            Exp = 0;
            ExpNextLevel = 10;

            //직업 관련 스테이터스
            Job playerClass = JobDatabase.GetJob(PClass);
            PClassName = playerClass.jobName;
            Hp = playerClass.initialHp;
            Mp = playerClass.initialMp;
            Atk = playerClass.initialAtk;
            MAtk = playerClass.initialMAtk;
            Def = playerClass.initialDef;
            Speed = playerClass.initialSpeed;
            CriRate = playerClass.initialCriRate;

            //전투 관련 스테이터스
            IsAlive = true;
            CurrentHp = playerClass.initialHp;
            CurrentMp = playerClass.initialMp;

            //스킬셋
            if (PClass == JobCode.Warrior)
            {
                SkillList.Add(PlayerSkillCode.W_Basic);
            }
            else if (PClass == JobCode.Archer)
            {
                SkillList.Add(PlayerSkillCode.A_Basic);
            }
            else if (PClass == JobCode.Mage)
            {
                SkillList.Add(PlayerSkillCode.M_Basic);
                SkillList.Add(PlayerSkillCode.M_Magic1);
            }
        }

        public void LvUp() //레벨업 시 스텟 증가
        {
            Lv++;
            Exp -= ExpNextLevel;

            Job playerClass = JobDatabase.GetJob(PClass);
            Hp += playerClass.lvUpHp;
            Mp += playerClass.lvUpMp;
            Atk += playerClass.lvUpAtk;
            MAtk += playerClass.lvUpMAtk;
            Def += playerClass.lvUpDef;
            Speed += playerClass.lvUpSpeed;
            CriRate += playerClass.lvUpCriRate;
        }



    }
}
