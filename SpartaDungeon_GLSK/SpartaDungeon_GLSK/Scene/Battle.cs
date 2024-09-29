

using SpartaDungeon_GLSK.Data;

namespace SpartaDungeon_GLSK.Scene
{
    //전투 관련 씬
    public class BattleScene
    {
        //배틀 진행 정보
        public class BattleTable
        {
            //아군 진영
            public PlayerData[] Ally;
            //적군 진영
            public WorldMonster[] Hostile;

            public BattleTable(PlayerData playerData, WorldMonster[] enemies)
            {
                Ally = new PlayerData[1];
                Ally[0] = playerData;
                Hostile = enemies;
            }
        }

        public static bool TutorialBattle(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            MonsterCode[] enemies = new MonsterCode[] { MonsterCode.CommonMonster1, MonsterCode.CommonMonster2};
            BattleTable battleTable = new BattleTable(Program.playerData/*나중에 다대다 전투 구현 가능하면 PlayerData에 함수 추가*/, IngameData.GetWorldMonsters(enemies));

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            int screenTop = Console.GetCursorPosition().Top;
            bool loop = true;
            while (loop)
            {   
                if (battleTable.Hostile[0].isAlive)
                {
                    Console.SetCursorPosition(0, screenTop);
                    Console.WriteLine($"LV {battleTable.Hostile[0].monster.level}   {battleTable.Hostile[0].monster.name}");
                    Console.SetCursorPosition(0, screenTop + 1);
                    Console.WriteLine($"{battleTable.Hostile[0].currentHp} / {battleTable.Hostile[0].monster.hp}");
                }
                if (battleTable.Hostile[1].isAlive)
                {
                    Console.SetCursorPosition(50, screenTop);
                    Console.WriteLine($"LV {battleTable.Hostile[1].monster.level}   {battleTable.Hostile[1].monster.name}");
                    Console.SetCursorPosition(50, screenTop + 1);
                    Console.WriteLine($"{battleTable.Hostile[1].currentHp} / {battleTable.Hostile[1].monster.hp}");
                }
                Console.SetCursorPosition(0, screenTop + 5);
                Console.WriteLine($"LV {battleTable.Ally[0].Lv}   {battleTable.Ally[0].Name}");
                Console.SetCursorPosition(0, screenTop + 6);
                Console.WriteLine($"{battleTable.Ally[0].currentHp} / {battleTable.Ally[0].Hp}");
                Console.SetCursorPosition(0, screenTop + 8);
                Console.WriteLine("Z를 눌러 적을 공격!");

                Console.SetCursorPosition(0, screenTop + 50);
                Console.SetCursorPosition(0, screenTop);
                Console.SetCursorPosition(0, screenTop + 9);

                bool loop2 = true;
                while (loop2)
                {
                    keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                    switch (keyInput)
                    {
                        case ConsoleKey.Z:
                            battleTable.Hostile[0].currentHp -= 10;
                            if (battleTable.Hostile[0].currentHp <= 0) battleTable.Hostile[0].isAlive = false;
                            battleTable.Hostile[1].currentHp -= 10;
                            if (battleTable.Hostile[1].currentHp <= 0) battleTable.Hostile[1].isAlive = false;
                            if (battleTable.Hostile[0].isAlive == false && battleTable.Hostile[1].isAlive == false)
                            {
                                next = Scenes.Start_TutoEnd;
                                return true;
                            }
                            loop2 = false;
                            break;
                    }
                }
            }

            next = Scenes.Start_TutoEnd;
            return true;
        }
    }
}
