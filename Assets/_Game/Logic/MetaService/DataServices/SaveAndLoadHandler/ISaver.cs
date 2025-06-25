using _Game.Gameplay.Logic.Service;
using Cysharp.Threading.Tasks;

namespace _Game.Logic.MetaService.DataServices.SaveAndLoadHandler
{
    public interface ISaver
    {
        UniTask SaveData(PlayerProgressData playerProgressData);
        UniTask<PlayerProgressData> LoadData();
    }
}