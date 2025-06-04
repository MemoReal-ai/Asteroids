using System;
using System.Collections.Generic;
using _Game.AuthenticatorService;
using Cysharp.Threading.Tasks;
using Unity.Services.CloudSave;
using Unity.Services.CloudSave.Models;
using UnityEngine;

namespace _Game.Gameplay.Logic.Service.SaveAndLoadHandler
{
    public class CloudSaver : ICloudSaver
    {
        private const string PLAYERDATAKEY = "PlayerData";

        private readonly IAuthenticatorService _authenticationService;
        private readonly IJsonConverter _jsonConverter;

        public CloudSaver(IJsonConverter jsonConverter, IAuthenticatorService authenticationService)
        {
            _jsonConverter = jsonConverter;
            _authenticationService = authenticationService;
        }

        public async void SaveData(Data data)
        {
            try
            {
                var jsonData = _jsonConverter.Serialize(data);
                var dataToSave = new Dictionary<string, object>
                {
                    { "PlayerData", jsonData }
                };
                await CloudSaveService.Instance.Data.Player.SaveAsync(dataToSave);
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }

        public async UniTask<Data> LoadDataCloud()
        {
            try
            {
                await _authenticationService.WaitSignIn();
                var playerData = await CloudSaveService.Instance.Data.Player.LoadAsync(new HashSet<string>
                {
                    PLAYERDATAKEY
                });
                
                if (playerData.TryGetValue(PLAYERDATAKEY, out Item data))
                {
                    var jsonData = data.Value.GetAsString();
                    var dataFromJson = _jsonConverter.Deserialize<Data>(jsonData);
                    return dataFromJson;
                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}