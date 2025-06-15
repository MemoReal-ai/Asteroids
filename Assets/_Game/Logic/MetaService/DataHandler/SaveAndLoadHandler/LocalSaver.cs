using _Game.Logic.MetaService.JsonConvertHandler;
using UnityEngine;

namespace _Game.Gameplay.Logic.Service
{
    public class LocalSaver : ILocalSaver
    {
        private const string KEY = "Data";

        private readonly IJsonConverter _jsonConverter;

        private Data _data;
        private string _dataSerialize;

        public LocalSaver(IJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }

        public Data LoadData()
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                var jsonFile = PlayerPrefs.GetString(KEY);
                _data = _jsonConverter.Deserialize<Data>(jsonFile);
                return _data;
            }

            return new Data();
        }

        public void SaveData(Data data)
        {
            _data = data;
            _dataSerialize = _jsonConverter.Serialize(_data);
            PlayerPrefs.SetString(KEY, _dataSerialize);
            PlayerPrefs.Save();
        }
    }
}