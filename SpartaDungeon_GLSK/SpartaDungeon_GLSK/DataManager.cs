using System.Runtime.CompilerServices;
using System.Text.Json;

namespace SpartaDungeon_GLSK
{
    public class DataManager
    {




        //저장파일 경로는 실행파일과 같음
        //저장파일 이름은 SaveData1, SaveData2, SaveData3 세개
        /*public static bool LoadData(int saveSocket, out PlayerData playerData)
        {
            playerData = null;

            if (saveSocket < 1 || saveSocket > 3)
            {
                return false;
            }

            string filePath = $"SaveData{saveSocket}";
            try
            {
                if (File.Exists(filePath))
                {
                    // JSON 파일을 읽어서 객체로 역직렬화
                    string jsonString = File.ReadAllText(filePath);
                    playerData = JsonSerializer.Deserialize<PlayerData>(jsonString);
                    Console.WriteLine($"\n - {saveSocket}번 데이터 불러오기 완료!\n");

                    return playerData;
                }
                else
                {
                    Console.SetCursorPosition(0, Const.screenH + 10);
                    Console.WriteLine("파일을 찾을 수 없습니다.");
                    Console.SetCursorPosition(0, 0);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.SetCursorPosition(0, Const.screenH + 10);
                Console.WriteLine("데이터 불러오기 중 오류가 발생했습니다: " + ex.Message);
                Console.SetCursorPosition(0, 0);
                return null;
            }
        }*/
        
    }
}
