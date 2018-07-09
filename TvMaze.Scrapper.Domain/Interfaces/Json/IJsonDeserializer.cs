namespace TvMaze.Scrapper.Domain.Interfaces.Json
{
    public interface IJsonDeserializer
    {
        T DeserializeObject<T>(string str);
    }
}
