namespace DevZhrssh.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        // Save date can be modified
        public int highScore;
        public int currency;
        public int playCount;

        public SaveData(int highScore, int currency, int playCount)
        {
            this.highScore = highScore;
            this.currency = currency;
            this.playCount = playCount;
        }
    }
}
