using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace System.BLL.Models.ImageManagement.Imgur
{
    public class BaseResponse
    {
        private class BaseResponseErrorConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(BaseResponseError);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
                JsonSerializer serializer)
            {
                // BaseResponseErrors can either be a string or an object containing more extensive information.
                // Internally we want to represent both version as an object, but when the source data is a simple string we only fill in the message property.

                var jtoken = serializer.Deserialize<JToken>(reader);

                if (jtoken.Type == JTokenType.String)
                {
                    var error = new BaseResponseError
                    {
                        Message = jtoken.ToObject<string>()
                    };
                    return error;
                }

                if (jtoken.Type == JTokenType.Object)
                {
                    var jobject = jtoken.ToObject<JObject>();

                    var error = new BaseResponseError
                    {
                        Code = jobject.Value<int>("code"),
                        Message = jobject.Value<string>("message"),
                        Type = jobject.Value<string>("type")
                    };
                    return error;
                }

                throw new ArgumentException("A BaseResponseError is expected to be either a string or an object");
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value, typeof(BaseResponseError));
            }
        }

        [JsonConverter(typeof(BaseResponseErrorConverter))]
        public class BaseResponseError
        {
            public BaseResponseError()
            {
            }

            public BaseResponseError(string errorString)
            {
                Message = errorString;
            }

            [JsonProperty("code")]
            public int Code { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }
        }

        public class BaseData
        {
            [JsonProperty("error")]
            public BaseResponseError Error { get; set; }

            [JsonProperty("method")]
            public string Method { get; set; }

            [JsonProperty("parameters")]
            public string Parameters { get; set; }

            [JsonProperty("request")]
            public string Request { get; set; }
        }

        [JsonProperty("success")]
        public bool Success { get; set; }

        [JsonProperty("status")]
        public int Status { get; set; }
    }
}