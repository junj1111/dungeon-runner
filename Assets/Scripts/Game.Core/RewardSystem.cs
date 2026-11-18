namespace Game.Core
{
    /// 던전 러너 — 스테이지 보상 정산 (레거시)
    /// 이 클래스에는 9주차에서 배운 코드 스멜이 의도적으로 심어져 있다.
    /// 10주차: 이 파일을 "고치기 전에" 현재 동작을 테스트로 고정한다(특성화 테스트).
    /// 
    public enum Difficulty
    {
        Default,
        Easy,
        Normal,
        Hard
    }

    public class RewardSystem
    {
        public string grade;   // 최근 판정 등급 ("S"/"A"/"B"/"C")
        public int lastR;      // 최근 보상액

        private const int coinMultiplier = 10;
        private const int bonusMultiplier = 50;

        // t: 클리어 시간(초), c: 코인 수, b: 보스 처치 수, diff: 난이도(1~3)
        public int ApplyStageReward(int t, int c, int b, bool isEvent, int diff)
        {
            // --- 보상 계산 ---
            int r = CalculateReward(c, b, diff, isEvent);

            // --- 등급 판정 ---
            r += CalculateGrade(t);

            // --- 기록 ---
            RecordReward(r);
            return r;
        }

        public int CalculateReward(int c, int b, int diff, bool isEvent)
        {
            int r = c * coinMultiplier + b * bonusMultiplier;
            if (diff == (int)Difficulty.Normal) r = (int)(r * 1.2f);
            if (diff == (int)Difficulty.Hard) r = (int)(r * 1.5f);
            if (isEvent) r = (int)(r * 1.5f);

            return r;
        }

        public int CalculateGrade(int t)
        {
            int r = 0;
            if (t < 60) grade = "S";
            else if (t < 90) grade = "A";
            else if (t < 120) grade = "B";
            else grade = "C";
            if (grade == "S") r += 100;

            return r;
        }

        public int RecordReward(int r)
        {
            lastR = r;
            return r;
        }
    }
}
