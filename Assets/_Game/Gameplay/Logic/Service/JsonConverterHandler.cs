using Newtonsoft.Json;

namespace _Game.Gameplay.Logic.Service
{
    public class JsonConverterHandler : IJsonConverter
    {
        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string json)
        {
            var obj = JsonConvert.DeserializeObject<T>(json);
            return obj;
        }
    }
}