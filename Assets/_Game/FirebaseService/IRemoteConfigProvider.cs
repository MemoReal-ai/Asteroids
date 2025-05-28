using _Game.Firebase;
using UnityEngine;

namespace _Game.FirebaseService
{
    public interface IRemoteConfigProvider
    {
        ScriptableObject GetRemoteConfig<T>(KeyToRemoteConfig key);
    }
}