using _Game.Gameplay.Logic.Service;
using Cysharp.Threading.Tasks;

namespace _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler
{
    public interface ISaver
    {
        void SaveData(Data data);
        UniTask<Data> LoadData();
    }
}