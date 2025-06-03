using UnityEngine;

namespace _Game.Gameplay.Logic.Service.SaveAndLoadHandler
{
    public class CloudSaver : ICloudSaver
    {
        public void Save()
        {
            Debug.Log("Cloud Saved");
        }
    }
}