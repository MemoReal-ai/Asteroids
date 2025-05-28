using _Game.Firebase;
using UnityEngine;

namespace _Game.FirebaseService
{
    public interface IRemoteConfigProvider
    {
        T GetRemoteConfig<T>(KeyToRemoteConfig key);
    }
}