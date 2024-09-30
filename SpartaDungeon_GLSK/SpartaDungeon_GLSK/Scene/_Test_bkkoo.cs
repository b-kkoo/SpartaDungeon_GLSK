using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpartaDungeon_GLSK.Scene
{
    internal class _Test_bkkoo
    {
        public static bool Test(out Scenes next, KeyController keyController)
        {
            List<string> strList = new List<string>();
            strList.Add("가나다라");
            strList.Add("마바사아자");
            strList.Add("차카타파하");
            strList.Add("ABCDEFG");
            strList.Add("HIJKLMN");
            strList.Add("OPQRSTU");
            strList.Add("VWXYZ");

            // 인터페이스 예제 (최대 5개씩 항목을 띄워주는 메뉴)

            // < 선택 화면 >                        <- strList를 사용하여 이 화면이 나오도록 해보자
            // 1. 가나다라                           - 숫자 1 누르면 해당 화면 지우고 "가나다라"를 사용하시겠습니까? (Z : 확인, X : 취소)  출력
            // 2. 마바사아자                              그 상태에서 다시 z버튼 누르면 화면 지우고 "가나다라"가 사용되었습니다! 출력, 1초 후 선택화면으로 복귀
            // 3. 차카타파하                           or x버튼 누르면 취소(선택화면으로 복귀)
            // 4. ABCDEFG                            - Tab 누르면 리스트의 남은 항목 띄우도록(지금은 리스트가 7개니까 1,2번에 남은 2개 들어가면 됨), 다시 Tab 누르면 돌아옴
            // 5. HIJKLMN                            - 선택화면에서 x 누르면 메인화면 나오도록
            // (1~5 : 선택, Tab : 다음, X : 취소)

            next = Scenes.Main_Menu;
            return true;
        }
    }
}
