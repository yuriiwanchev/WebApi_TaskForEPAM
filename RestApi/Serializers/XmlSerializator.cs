using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Domain;

namespace RestApi.Serializers
{
    public class XmlSerializator : ISerializator
    {
        public string SerializeObject<T>(T toSerialize)
        {
            var serializer = new DataContractSerializer(toSerialize.GetType());

            var output = new StringBuilder();

            using (XmlWriter writer = XmlWriter.Create(output))
            {
                serializer.WriteObject(writer, toSerialize);
            }

            return output.ToString();
        }

        public string SerializationFormat { get; } = ".xml";
    }
}