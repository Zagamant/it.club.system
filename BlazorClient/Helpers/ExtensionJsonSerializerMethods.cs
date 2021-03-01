using System.Text.Json;

namespace BlazorClient.Helpers
{
    public static class MyOptions
    {
        public static JsonSerializerOptions JsonSerializerWebOptions =>
            new()
            {
                IgnoreNullValues = true,
                PropertyNameCaseInsensitive = true,
                DefaultBufferSize = 100_000
            };
    }
}