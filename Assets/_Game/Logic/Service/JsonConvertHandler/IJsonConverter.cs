using UnityEngine;

namespace _Game.Gameplay.Logic.Service
{
    public interface IJsonConverter
    {
        string Serialize(object obj);
        T Deserialize<T>(string json);
    }
}
