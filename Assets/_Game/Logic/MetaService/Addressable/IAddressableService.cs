using _Game.Logic.UI.MainMenu.Factory;
using _Game.MainMenu.Logic.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Logic.MetaService.Addressable
{
    public interface IAddressableService
    {
        UniTask<T> LoadPrefab<T>(FactoryUI factoryUI);
    }
}