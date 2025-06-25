using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _Game.Logic.MetaService.DataServices.SaveAndLoadHandler
{
    [Serializable]
    public class PlayerProgressData
    {
        public int CurrentScore;
        public int HightScore;
        public bool PurchasingSkipAds;
        
        [JsonConverter(typeof(IsoDateTimeConverter))]
        public DateTime SaveTime;

        public void ChangeScore()
        {
            if (CurrentScore > HightScore)
            {
                HightScore = CurrentScore;
            }
        }
    }
}