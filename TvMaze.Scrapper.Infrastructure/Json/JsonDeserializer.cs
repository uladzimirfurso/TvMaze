using Newtonsoft.Json;
using TvMaze.Scrapper.Domain.Interfaces.Json;

namespace TvMaze.Scrapper.Infrastructure.Json
{
    public class JsonDeserializer: IJsonDeserializer
    {
        public T DeserializeObject<T>(string value)
        {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
