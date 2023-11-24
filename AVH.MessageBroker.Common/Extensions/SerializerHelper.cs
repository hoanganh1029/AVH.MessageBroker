using AVH.MessageBroker.Common.Constants;
using System.Text;
using System.Text.Json;

namespace AVH.MessageBroker.Common.Extensions
{
    public static class SerializerHelper
    {
        public static T? Deserialize<T>(string value)
        {
            return JsonSerializer.Deserialize<T>(value, SerializerConstant.DefaultJsonSerializerOptions);
        }

        public static object? Deserialize(string value, Type returnedType)
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes(value ?? ""));
            return JsonSerializer.Deserialize(stream, returnedType, SerializerConstant.DefaultJsonSerializerOptions);
        }

        public static bool TryDeserializeObject<T>(string value, out T? result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(value, SerializerConstant.DefaultJsonSerializerOptions);
                return true;
            }
            catch (Exception)
            {
                result = default;
                return false;
            }
        }

        public static string Serialize(object obj)
        {
            return JsonSerializer.Serialize(obj, SerializerConstant.DefaultJsonSerializerOptions);
        }
    }
}
