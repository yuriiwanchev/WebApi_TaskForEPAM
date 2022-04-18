using System.Text.Json;
using Domain;

namespace RestApi.Serializers
{
    public class JsonSerializator : ISerializator
    {
        public string SerializeObject<T>(T toSerialize)
        {
            return JsonSerializer.Serialize(toSerialize);
        }
        
        public string SerializationFormat { get; } = ".json";
    }
}