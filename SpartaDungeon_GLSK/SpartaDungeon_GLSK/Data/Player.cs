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

    public static class JobDatabase
    {
        private static readonly Dictionary<JobCode, Job> jobs;

        static JobDatabase()
        {
            jobs = new Dictionary<JobCode, Job>();

            //                                 name      hp       atk     matk    def    speed  cri
            jobs.Add(JobCode.Warrior, new Job( "전사",   100,     10,     2,      7,     5,     20));
            jobs.Add(JobCode.Archer, new Job(  "궁수",   80,      8,      3,      4,     8,     30));
            jobs.Add(JobCode.Mage, new Job(    "마법사", 70,      5,      6,      3,     3,     20));
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
        //기본 스텟
        public string jobName { get; }
        public int initialHp { get; }
        public int initialAtk { get; }
        public int initialMAtk { get; }
        public int initialDef { get; }
        public int initialSpeed { get; }
        public int initialCriRate {  get; }

        //레벨업 시 상승 스텟
        //public int lvUpHp { get; }

        public Job(string _name, int _hp, int _atk, int _matk, int _def, int _speed, int _CriRate)
        {
            jobName = _name;
            initialHp = _hp;
            initialAtk = _atk;
            initialMAtk = _matk;
            initialDef = _def;
            initialSpeed = _speed;
            initialCriRate = _CriRate;
        }
    }

    public enum JobCode
    {
        Warrior,
        Archer,
        Mage
    }


    //////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static class PSkillDatabase
    {
        private static readonly Dictionary<PSkillCode, PSkill> skills;

        static PSkillDatabase()
        {
            skills = new Dictionary<PSkillCode, PSkill>();

            //                                                            Warrior         level    ratio    splash     info
            skills.Add(PSkillCode.W_Basic, new PSkill("베기",               JobCode.Warrior,   1,       1.0,     false,     "전방의 적을 벤다."));

            //                                                            Archer          level    ratio    splash
            skills.Add(PSkillCode.A_Basic, new PSkill("사격",               JobCode.Archer,    1,       1.0,     false,     "적에게 화살을 발사한다."));

            //                                                            Mage            level    ratio    splash
            skills.Add(PSkillCode.M_Basic, new PSkill("지팡이 휘두르기",    JobCode.Mage,      1,       0.8,     false,     "지팡이를 휘둘러 적을 공격한다."));
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
        public string skillName {  get; }
        public JobCode useClass { get; }
        public int unlockLv { get; }
        public double atkRatio { get; }
        public bool isSplash { get; }

        public string info { get; }


        public PSkill(string _skillName, JobCode _useClass, int _unlockLv, double _atkRatio, bool _isSplash, string _info)
        {
            skillName = _skillName;
            useClass = _useClass;
            unlockLv = _unlockLv;
            atkRatio = _atkRatio;
            isSplash = _isSplash;

            info = _info;
        }
    }

    public enum PSkillCode
    {
        W_Basic,

        A_Basic,

        M_Basic
    }
}
       