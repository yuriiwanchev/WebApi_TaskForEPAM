namespace Domain
{
    public interface ISerializator
    {
        string SerializeObject<T>(T toSerialize);
        
        string SerializationFormat { get; }
    }
}