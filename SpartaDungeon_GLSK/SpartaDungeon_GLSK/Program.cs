namespace SpartaDungeon_GLSK
{
    internal class Program
    {
        static SceneManager sceneManager = new SceneManager();

        public static SaveData saveData; //저장파일 (불러올때 json, 저장할때 게임 데이터로 초기화)
        public static PlayerData playerData;
        public static IngameData ingameData;

        static void Main(string[] args)
        {
            sceneManager.Start();
        }
    }
}
