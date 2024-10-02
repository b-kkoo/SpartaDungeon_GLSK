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

    public static class PlayerSkillDatabase
    {
        private static readonly Dictionary<PlayerSkillCode, PlayerSkill> skills;

        static PlayerSkillDatabase()
        {
            skills = new Dictionary<PlayerSkillCode, PlayerSkill>();

            //                                                                            info                                                        Warrior       level    a.ratio    m.ratio    mpConsum      splash     charging
            skills.Add(PlayerSkillCode.W_Basic,   new PlayerSkill("베기",             "전방의 적을 벤다.",                                             JobCode.Warrior,    1,       1.0,       0.0,      0,            false,     false));
            skills.Add(PlayerSkillCode.W_Basic1,   new PlayerSkill("강타",            "전방의 적을 강하게 벤다.",                                       JobCode.Warrior,    2,       2.0,       0.0,      5,            false,     false));
            skills.Add(PlayerSkillCode.W_Basic2,   new PlayerSkill("검기",            "전방의 적에게 검기를 날린다.",                                    JobCode.Warrior,    3,       2.5,       0.0,      10,           false,     false));
            skills.Add(PlayerSkillCode.W_Basic3,   new PlayerSkill("발도",            "전방의 적을 검으로 빠르게 벤다.",                                  JobCode.Warrior,    4,       3.5,       0.0,      20,           false,     false));
            skills.Add(PlayerSkillCode.W_Basic4,   new PlayerSkill("회전베기",         "불꽃을 휘감아 회전하며 적들을 벤다.",                               JobCode.Warrior,    5,       4.5,       0.0,      25,            true,     false));
            skills.Add(PlayerSkillCode.W_Basic5,   new PlayerSkill("검기방출",         "전방의 적들에게 검기를 날려 적을 벤다.",                             JobCode.Warrior,    7,       6.5,       0.0,      40,            true,     false));

            //                                                                            info                                                        Archer        level    a.ratio    m.ratio    mpConsum       splash    charging
            skills.Add(PlayerSkillCode.A_Basic,   new PlayerSkill("사격",             "적에게 화살을 발사한다.",                                        JobCode.Archer,       1,       1.0,       0.0,      0,           false,     false));
            skills.Add(PlayerSkillCode.A_Basic1,   new PlayerSkill("집중사격",         "적에게 강한화살을 발사한다.",                                     JobCode.Archer,       2,       2.0,       0.0,      5,           false,     false));
            skills.Add(PlayerSkillCode.A_Basic2,   new PlayerSkill("차징샷",           "적에게 강한화살을 차징후 발사한다.",                               JobCode.Archer,       3,       2.5,       0.0,      10,          false,      true));
            skills.Add(PlayerSkillCode.A_Basic3,   new PlayerSkill("블리자드샷",        "냉기 속성의 화살로 적 다수를 공격한다.",                            JobCode.Archer,       4,       3.0,       0.0,      20,           true,     false));
            skills.Add(PlayerSkillCode.A_Basic4,   new PlayerSkill("에로우 블로우",     "강한 기운을 실어 화살을 발사하여 적을 멀리 밀어낸다.",                 JobCode.Archer,       5,       3.5,       0.0,      25,          false,     false));
            skills.Add(PlayerSkillCode.A_Basic5,   new PlayerSkill("애로우레인",        "화살을 허공에 쏘아 차례로 떨어지며 사방으로 퍼져나가며 떨어진다.",        JobCode.Archer,       7,       5.5,       0.0,      40,          true,      false));
             
            //                                                                            info                                                         Mage        level    a.ratio    m.ratio    mpConsum        splash    charging
            skills.Add(PlayerSkillCode.M_Basic,   new PlayerSkill("지팡이 휘두르기",    "지팡이를 휘둘러 적을 공격한다.",                                   JobCode.Mage,         1,       0.8,       0.0,      0,            false,     false));
            skills.Add(PlayerSkillCode.M_Magic1,  new PlayerSkill("파이어볼",         "불로 된 구체를 상대에게 날린다.",                                  JobCode.Mage,         1,       0.0,       3.0,      10,           false,     false));
            skills.Add(PlayerSkillCode.M_Magic2,  new PlayerSkill("아이스볼",         "얼음으로 된 구체를 상대에게 날린다.",                               JobCode.Mage,         2,       0.0,       4.0,      15,           false,     false));
            skills.Add(PlayerSkillCode.M_Magic3,  new PlayerSkill("파이어레인",        "불비를 상대에게 날린다.",                                        JobCode.Mage,         2,       0.0,       5.0,      20,           true,      false));
            skills.Add(PlayerSkillCode.M_Magic4,  new PlayerSkill("윈드스톰",         "바람으로된 폭풍을 상대에게 날린다.",                                JobCode.Mage,         5,       0.0,       5.5,      30,           true,      false));
            skills.Add(PlayerSkillCode.M_Magic5,  new PlayerSkill("메테오",          "하늘에서 불로된 운석을 여러개 떨어뜨린다.",                           JobCode.Mage,         7,       0.0,       6.5,      40,           true,       true));
        }

        public static PlayerSkill GetPSkill(PlayerSkillCode code)
        {
            //Dictionary 내장 함수 TryGetValue : 해당 key값이 없으면 false를 반환
            if (skills.TryGetValue(code, out PlayerSkill skill))
            {
                return skill;
            }

            Console.WriteLine("해당 ID의 스킬을 찾을 수 없습니다.");
            return null;
        }
    }


    //플레이어 스킬
    public class PlayerSkill
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



        public PlayerSkill(string _skillName, string _info, JobCode _useClass, int _unlockLv, double _atkRatio, double _matkRatio, int _mpConsum, bool _isSplash, bool _needCharging)
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

        public double CalcDamage(PlayerUnitData playerData)
        {
            return playerData.Atk * atkRatio + playerData.MAtk * matkRatio;
        }
    }

    public enum PlayerSkillCode
    {
        W_Basic,
        W_Basic1,
        W_Basic2,
        W_Basic3,
        W_Basic4,
        W_Basic5,


        A_Basic,
        A_Basic1,
        A_Basic2,
        A_Basic3,
        A_Basic4,
        A_Basic5,

        M_Basic,
        M_Magic1,
        M_Magic2,
        M_Magic3,
        M_Magic4,
        M_Magic5,

        MAX
    }
}
       