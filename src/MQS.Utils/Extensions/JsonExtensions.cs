using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MQS.Utils.Extensions
{
	public static class JsonExtensions
	{
        public static T ToObject<T>(this string json, T defaultValue = default)
        {
            return string.IsNullOrWhiteSpace(json) ? defaultValue : JsonConvert.DeserializeObject<T>(json);
        }

        public static string ToJson<T>(this T obj) => JsonConvert.SerializeObject(obj);

        public static T SelectValue<T>(this JObject source, string path, T defaultValue = default)
        {
            var token = source.SelectToken(path);
            return token != null ? token.Value<T>() : defaultValue;
        }
    }
}
