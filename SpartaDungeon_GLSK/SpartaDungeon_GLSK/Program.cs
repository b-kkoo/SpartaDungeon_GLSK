using SpartaDungeon_GLSK.Data;

namespace SpartaDungeon_GLSK
{
    public class Program
    {
        static SceneManager sceneManager = new SceneManager();

        public static SaveData saveData; //저장파일 (불러올때 json, 저장할때 게임 데이터로 초기화)
        public static PlayerData playerData = new PlayerData();
        public static IngameData ingameData = new IngameData();

        static void Main(string[] args)
        {
            //정적 클래스 초기화
            ItemData.Set();
            MonsterData.Set();

            sceneManager.Start();
        }
    }
}
