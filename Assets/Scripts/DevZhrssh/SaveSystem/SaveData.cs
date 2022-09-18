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
        public int ballID { get; private set; }
        public int wallpaperID { get; private set; }

        public SaveData(int highScore, int currency, int playCount, int deaths, int ballID, int wallpaperID)
        {
            this.highScore = highScore;
            this.currency = currency;
            this.playCount = playCount;
            this.deaths = deaths;
            this.ballID = ballID;
            this.wallpaperID = wallpaperID;
        }
    }
}
