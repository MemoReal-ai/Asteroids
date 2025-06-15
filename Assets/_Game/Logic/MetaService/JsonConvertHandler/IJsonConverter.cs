namespace _Game.Logic.MetaService.JsonConvertHandler
{
    public interface IJsonConverter
    {
        string Serialize(object obj);
        T Deserialize<T>(string json);
    }
}
