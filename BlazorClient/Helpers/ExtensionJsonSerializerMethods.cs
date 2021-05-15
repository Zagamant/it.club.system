using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazorClient.Helpers
{
    public static class MyOptions
    {
        public static JsonSerializerOptions JsonSerializerWebOptions =>
            new()
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                DefaultBufferSize = 100_000,
                Converters = {new JsonStringEnumConverter()}
            };
    }
}