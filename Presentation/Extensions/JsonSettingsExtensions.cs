using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Presentation.Extensions;

public static class JsonSettingsExtensions
{
    public static IServiceCollection AddJsonSettings(this IServiceCollection collection)
    {
        var jsonSettings = new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            },
            NullValueHandling = NullValueHandling.Ignore,
            Formatting = Formatting.Indented,
        };

        collection.AddSingleton(jsonSettings);

        return collection;
    }
}