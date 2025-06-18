using _Game.Logic.MetaService.JsonConvertHandler;
using Newtonsoft.Json;

namespace _Game.Logic.MetaService.JsonConvertService
{
    public class JsonConverterService : IJsonConverter
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