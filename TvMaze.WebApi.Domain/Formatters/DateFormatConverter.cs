using Newtonsoft.Json.Converters;

namespace TvMaze.WebApi.Core.Formatters
{
    public class DateFormatConverter : IsoDateTimeConverter
    {
        public DateFormatConverter(string format)
        {
            DateTimeFormat = format;
        }
    }
}
