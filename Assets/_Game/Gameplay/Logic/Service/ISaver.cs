namespace _Game.Gameplay.Logic.Service
{
    public interface ISaver
    {
        public void SaveData(Data data);
        public Data LoadData();
        
    }
}
