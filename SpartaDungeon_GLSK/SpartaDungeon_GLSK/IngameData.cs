using SpartaDungeon_GLSK.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK
{
    //다양한 이벤트 플래그를 관리할 멤버가 필요
    public class IngameData
    {
        public int DefeatHighestDungeonStage = -1; //격파한 최고등급 던전. -1 : 없음, 십의 자리는 대단위, 일의 자리는 소단위 스테이지

        public int DungeonUnlock = 0; //던전 해금. 0~4

        public int[] QuestFlag = new int[10]; //퀘스트 진행 상황. -1:비활성화 0:수락가능 1:진행중 2:완료가능 3:완료

        public bool[] MercenaryPurchased = new bool[10]; //모험가 구매 여부

        public IngameData()
        {
            for (int i = 0; i < QuestFlag.Length; i++)
            {
                QuestFlag[i] = -1;
            }
        }
    }

    
}
