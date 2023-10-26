using ControleEstofaria.Webapi.Filters;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ControleEstofaria.Webapi.Config
{
    public static class FiltersConfig
    {
        public static void ConfigurarFiltros(this IServiceCollection services)
        {
            services
                .AddControllers(config => { config.Filters.Add(new ValidarViewModelActionFilter()); })
                .AddJsonOptions(opt => {
                    opt.JsonSerializerOptions.Converters.Add(new TimeSpanToStringConverter());
                });
        }
    }

    public class TimeSpanToStringConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var value = reader.GetString();
            return TimeSpan.Parse(value);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString());
        }
    }
}
