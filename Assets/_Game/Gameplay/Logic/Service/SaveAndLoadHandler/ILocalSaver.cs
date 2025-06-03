namespace _Game.Gameplay.Logic.Service
{
    public interface ILocalSaver
    {
        public void SaveData(Data data);
        public Data LoadData();
        
    }
}
