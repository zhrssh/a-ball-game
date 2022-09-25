namespace DevZhrssh.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        // Save date can be modified
        public int highScore;
        public int currency;
        public int playCount;
        public int deaths;

        public int currentEquippedBall;
        public bool ball2;
        public bool ball3;
        public bool ball4;
        public bool ball5;

        public bool showAds;

        public SaveData(int highScore, int currency, int playCount, int deaths, int currentEquippedBall, bool ball2, bool ball3, bool ball4, bool ball5, bool showAds)
        {
            this.highScore = highScore;
            this.currency = currency;
            this.playCount = playCount;
            this.deaths = deaths;

            this.currentEquippedBall = currentEquippedBall;

            this.ball2 = ball2;
            this.ball3 = ball3;
            this.ball4 = ball4;
            this.ball5 = ball5;
            this.showAds = showAds;
        }
    }
}
