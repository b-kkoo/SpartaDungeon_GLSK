using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon_GLSK.Data.JobDatabase;
using System.Xml.Linq;

namespace SpartaDungeon_GLSK.Data
{
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // PLAYER JOB

    public static class JobDatabase
    {
        private static readonly Dictionary<JobCode, Job> jobs;

        static JobDatabase()
        {
            jobs = new Dictionary<JobCode, Job>();

            //                                 name      hp      mp      atk     matk    def    speed  cri    //LvUp//    hp      mp      atk     matk    def    speed  cri
            jobs.Add(JobCode.Warrior, new Job( "전사",   100,    20,     10,     2,      7,     5,     20,                10,     2,      2,      1,      2,     1,     0    ));
            jobs.Add(JobCode.Archer,  new Job( "궁수",   80,     30,     8,      3,      4,     8,     30,                8,      3,      2,      2,      1,     3,     1    ));
            jobs.Add(JobCode.Mage,    new Job( "마법사", 70,     50,     5,      6,      3,     3,     20,                7,      5,      1,      3,      0,     1,     0    ));
        }

        public static Job GetJob(JobCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (jobs.TryGetValue(code, out Job job))
            {
                return job;
            }

            Console.WriteLine("해당 ID의 직업을 찾을 수 없습니다.");
            return null;
        }
    }


    public class Job
    {
        public string jobName { get; }

        //기본 스텟
        public int initialHp { get; }
        public int initialMp { get; }
        public int initialAtk { get; }
        public int initialMAtk { get; }
        public int initialDef { get; }
        public int initialSpeed { get; }
        public int initialCriRate {  get; }

        //레벨업 시 상승 스텟
        public int lvUpHp { get; }
        public int lvUpMp { get; }
        public int lvUpAtk { get; }
        public int lvUpMAtk { get; }
        public int lvUpDef { get; }
        public int lvUpSpeed { get; }
        public int lvUpCriRate { get; }

        public Job(string _name, int _hp, int _mp, int _atk, int _matk, int _def, int _speed, int _CriRate, int _hpUp, int _mpUp, int _atkUp, int _matkUp, int _defUp, int _speedUp, int _CriRateUp)
        {
            jobName = _name;

            initialHp = _hp;
            initialMp = _mp;
            initialAtk = _atk;
            initialMAtk = _matk;
            initialDef = _def;
            initialSpeed = _speed;
            initialCriRate = _CriRate;

            lvUpHp = _hpUp;
            lvUpMp = _mpUp;
            lvUpAtk = _atkUp;
            lvUpMAtk = _matkUp;
            lvUpDef = _defUp;
            lvUpSpeed = _speedUp;
            lvUpCriRate = _CriRateUp;
        }
    }

    public enum JobCode
    {
        Warrior,
        Archer,
        Mage
    }





    //////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // SKILL

    public static class PSkillDatabase
    {
        private static readonly Dictionary<PSkillCode, PSkill> skills;

        static PSkillDatabase()
        {
            skills = new Dictionary<PSkillCode, PSkill>();

            //                                                              info                  Warrior              level    a.ratio    m.ratio    mpConsum      splash     charging
            skills.Add(PSkillCode.W_Basic,   new PSkill("베기",             "전방의 적을 벤다.", JobCode.Warrior,   1,       1.0,       0.0,      0,            false,     false));

            //                                                              info                            Archer            level    a.ratio    m.ratio    mpConsum      splash   charging
            skills.Add(PSkillCode.A_Basic,   new PSkill("사격",             "적에게 화살을 발사한다.", JobCode.Archer,    1,       1.0,       0.0,      0,            false,     false));

            //                                                              info                     Mage              level    a.ratio    m.ratio    mpConsum      splash   charging
            skills.Add(PSkillCode.M_Basic,   new PSkill("지팡이 휘두르기",  "지팡이를 휘둘러 적을 공격한다.", JobCode.Mage,      1,       0.8,       0.0,      0,            false,     false));
            skills.Add(PSkillCode.M_Magic1,  new PSkill("파이어볼",         "불로 된 구체를 상대에게 날린다.",JobCode.Mage,      1,       0.0,       3.0,      10,           false,     false));
        }

        public static PSkill GetPSkill(PSkillCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (skills.TryGetValue(code, out PSkill skill))
            {
                return skill;
            }

            Console.WriteLine("해당 ID의 스킬을 찾을 수 없습니다.");
            return null;
        }
    }


    //플레이어 스킬
    public class PSkill
    {
        // 스킬 개요
        public string skillName {  get; }
        public string info { get; }
        public JobCode useClass { get; }
        public int unlockLv { get; }

        // 스킬 성능
        public double atkRatio { get; }
        public double matkRatio { get; }
        public int mpConsum { get; }
        public bool isSplash { get; } //전체공격 여부
        public bool needCharging { get; } //시전 집중이 필요한 스킬



        public PSkill(string _skillName, string _info, JobCode _useClass, int _unlockLv, double _atkRatio, double _matkRatio, int _mpConsum, bool _isSplash, bool _needCharging)
        {
            skillName = _skillName;
            info = _info;
            useClass = _useClass;
            unlockLv = _unlockLv;

            atkRatio = _atkRatio;
            matkRatio = _matkRatio;
            mpConsum = _mpConsum;
            isSplash = _isSplash;
            needCharging = _needCharging;
        }

        public double CalcDamage(PlayerData playerData)
        {
            return playerData.Atk * atkRatio + playerData.MAtk * matkRatio;
        }
    }

    public enum PSkillCode
    {
        W_Basic,

        A_Basic,

        M_Basic,
        M_Magic1
    }
}
       