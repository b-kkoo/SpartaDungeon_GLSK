using SpartaDungeon_GLSK.Data;
using SpartaDungeon_GLSK.Scene;
using System.Runtime.CompilerServices;

namespace SpartaDungeon_GLSK
{
    public class Program
    {
        static SceneManager sceneManager = new SceneManager();

        public static PlayerData playerData;
        public static IngameData ingameData = new IngameData();

        public ShopScene shopScene = new ShopScene();

        static void Main(string[] args)
        {
            ShopScene.Set();
            GuildScene.Set();

            Console.CursorVisible = false;
            try //윈도우 환경에서만 창 크기 변경 (다른 os 에서는 에러 뜸)
            {
                Console.SetWindowSize(200, 50);
            }
            catch { }

            sceneManager.Start();
        }
    }
}
