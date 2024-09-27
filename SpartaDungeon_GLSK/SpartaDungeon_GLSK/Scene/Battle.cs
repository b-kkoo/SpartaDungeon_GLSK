using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;
using static SpartaDungeon_GLSK.Scene.BattleScene;

namespace SpartaDungeon_GLSK.Scene
{
    internal class BattleScene
    {
        private BattleTable battleTable;
        //배틀 진행 정보
        public class BattleTable
        {
            //아군 진영
            public PlayerData[] Ally;
            //적군 진영
            public WorldMonster[] Hostile;

            public BattleTable(/*PlayerData playerData, WorldMonster[] enemies*/)
            {
                //Ally = new PlayerData[1]
                //Ally[0] = playerData;
                //Hostile = enemies;
            }
        }

        public bool TutorialBattle(out Scenes next, KeyController keyController/*플레이어 데이터*/)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;

            MonsterCode[] enemies = new MonsterCode[] { MonsterCode.CommonMonster1, MonsterCode.CommonMonster2};
            battleTable = new BattleTable(PlayerData.WorldPlayer/*나중에 다대다 전추 구현 가능하면 PlayerData에 함수 추가*/, IngameData.GetWorldMonster(enemies));

            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻

            int screenTop = Console.GetCursorPosition().Top;
            bool loop = true;
            while (loop)
            {   
                if (battleTable.Hostile[0].HPCur > 0)
                {
                    Console.SetCursorPosition(screenTop, 50);
                    Console.WriteLine($"LV {battleTable.Hostile[0].monster.level}   {battleTable.Hostile[0].monster.name}");
                    Console.SetCursorPosition(screenTop + 1, 0);
                    Console.WriteLine($"{battleTable.Hostile[0].HPCur} / {battleTable.Hostile[0].monster.HP}");
                }
                if (battleTable.Hostile[1].HPCur > 0)
                {
                    Console.SetCursorPosition(screenTop, 50);
                    Console.WriteLine($"LV {battleTable.Hostile[1].monster.level}   {battleTable.Hostile[1].monster.name}");
                    Console.SetCursorPosition(screenTop + 1, 50);
                    Console.WriteLine($"{battleTable.Hostile[1].HPCur} / {battleTable.Hostile[1].monster.HP}");
                }
                Console.SetCursorPosition(screenTop + 5, 0);
                Console.WriteLine($"LV {battleTable.Ally[0].name}");
                Console.SetCursorPosition(screenTop + 6, 0);
                Console.WriteLine($"{battleTable.Ally[0].HPCur} / {battleTable.Ally[0].HP}");
                Console.SetCursorPosition(screenTop + 8, 0);
                Console.WriteLine("Z를 눌러 적을 공격!");

                Console.SetCursorPosition(screenTop + 50, 0);
                Console.SetCursorPosition(screenTop, 0);

                bool loop2 = true;
                while (loop2)
                {
                    keyFilter = new ConsoleKey[] { ConsoleKey.Z };
                    keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                    switch (keyInput)
                    {
                        case ConsoleKey.Z:
                            next = Scenes.Test_Inventory;
                            break;

                    }
                }
            }

            while (true)
            {
                keyFilter = new ConsoleKey[] { ConsoleKey.Z, ConsoleKey.X };
                keyInput = keyController.GetUserInput(keyFilter, out cheatActivated);

                switch (keyInput)
                {
                    case ConsoleKey.Z:
                        next = Scenes.Test_Inventory;
                        return true;

                    case ConsoleKey.X:
                        next = Scenes.Test_Main; //false를 반환하는 순간 next Scene은 중요치 않음
                        return false;
                }
            }
        }
    }
}
