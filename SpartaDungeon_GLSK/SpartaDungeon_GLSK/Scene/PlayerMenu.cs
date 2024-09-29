using SpartaDungeon_GLSK.Data;

namespace SpartaDungeon_GLSK.Scene
{
    //플레이어 관련 메뉴 : 스테이터스, 인벤토리, 장비, 스킬
    public class PlayerMenuScene
    {
        /*public static bool DisplyInventory(out Scenes next, KeyController keyController)
        {
            ConsoleKey[] keyFilter = new ConsoleKey[] { ConsoleKey.NoName };
            ConsoleKey keyInput;

            int cheatActivated;
            
            keyController.GetUserInput(keyFilter, out cheatActivated); //반환값 안받으면 입력버퍼 지우라는 뜻
            foreach (KeyValuePair<int, int> item in Program.playerData.invetory)
            {
                Item i = Program.itemData.GetItem((IC)item.Key);
                Console.WriteLine(i.name);
            }

            Program.playerData.UseItem(Program.playerData.invetory[0].Key);

            Thread.Sleep(3000);

            next = Scenes.MainScene; //false를 반환하는 순간 next Scene은 중요치 않음
            return false;
        }*/
    }
}
