using UnityEngine;

namespace _Game.Gameplay.Logic.Service
{
    public class LocalSaver : ILocalSaver
    {
        private const string Key = "Data";
        private Data _data;
        private string _dataSerialize;

        public Data LoadData()
        {
            if (PlayerPrefs.HasKey(Key))
            {
                var JsonFile = PlayerPrefs.GetString(Key);
                _data = JsonUtility.FromJson<Data>(JsonFile);
                return _data;
            }

            return new Data();
        }

        public void SaveData(Data data)
        {
            _data = data;
            _dataSerialize = JsonUtility.ToJson(_data);
            PlayerPrefs.SetString(Key, _dataSerialize);
            PlayerPrefs.Save();
        }
    }
}