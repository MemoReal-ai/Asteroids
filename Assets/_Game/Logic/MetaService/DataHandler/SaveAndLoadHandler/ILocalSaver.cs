using _Game.Gameplay.Logic.Service.SaveAndLoadHandler;

namespace _Game.Gameplay.Logic.Service
{
    public interface ILocalSaver:ISaver
    {
        Data LoadData();
    }
}
