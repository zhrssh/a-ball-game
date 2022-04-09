namespace DevZhrssh.SaveSystem
{
    [System.Serializable]
    public class SaveData
    {
        // Save date can be modified
        public int highScore;
        public int currency;

        public SaveData(int highScore, int currency)
        {
            this.highScore = highScore;
            this.currency = currency;
        }
    }
}
