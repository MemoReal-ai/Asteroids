using Cysharp.Threading.Tasks;

namespace _Game.Gameplay.Logic.Service.SaveAndLoadHandler
{
    public interface ICloudSaver:ISaver
    {
       UniTask<Data> LoadDataCloud();
    }
}
