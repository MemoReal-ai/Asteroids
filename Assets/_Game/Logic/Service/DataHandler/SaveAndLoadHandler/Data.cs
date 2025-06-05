using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace _Game.Gameplay.Logic.Service
{
    [Serializable]
    public class Data
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