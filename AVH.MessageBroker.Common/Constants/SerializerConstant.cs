using System.Text.Json;

namespace AVH.MessageBroker.Common.Constants
{
    public static class SerializerConstant
    {
        public static readonly JsonSerializerOptions DefaultJsonSerializerOptions = new()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }
}
