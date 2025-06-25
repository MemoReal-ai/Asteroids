using _Game.Logic.MetaService.DataHandler.SaveAndLoadHandler;
using _Game.Logic.MetaService.DataServices.SaveAndLoadHandler;
using _Game.Logic.MetaService.JsonConvertHandler;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Game.Gameplay.Logic.Service
{
    public class LocalSaver : ISaver
    {
        private const string KEY = "Data";

        private readonly IJsonConverter _jsonConverter;

        private PlayerProgressData _playerProgressData;
        private string _dataSerialize;

        public LocalSaver(IJsonConverter jsonConverter)
        {
            _jsonConverter = jsonConverter;
        }

        public UniTask<PlayerProgressData> LoadData()
        {
            if (PlayerPrefs.HasKey(KEY))
            {
                var jsonFile = PlayerPrefs.GetString(KEY);
                _playerProgressData = _jsonConverter.Deserialize<PlayerProgressData>(jsonFile);
                return UniTask.FromResult(_playerProgressData);
            }

            return UniTask.FromResult(new PlayerProgressData());
        }

        public UniTask SaveData(PlayerProgressData playerProgressData)
        {
            _playerProgressData = playerProgressData;
            _dataSerialize = _jsonConverter.Serialize(_playerProgressData);
            PlayerPrefs.SetString(KEY, _dataSerialize);
            PlayerPrefs.Save();
            return UniTask.CompletedTask;
        }
    }
}