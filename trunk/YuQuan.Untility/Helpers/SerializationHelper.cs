using System;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace YuQuan.Helpers
{
    public static class SerializationHelper
    {
        /// <summary>
        /// Deserialize an xml string into a typed object
        /// </summary>
        public static Object Deserialize(Type type, String serializedString)
        {
            if (type == null)
                throw new ArgumentNullException("type");
            if (string.IsNullOrEmpty(serializedString))
                throw new ArgumentNullException("serializedString");
            DataContractSerializer serializer = new DataContractSerializer(type);
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(serializedString));
            return serializer.ReadObject(stream);
        }

        /// <summary>
        /// Serialize a typed object into an xml string
        /// </summary>
        public static String Serialize(ref Object obj)
        {
            DataContractSerializer serializer = new DataContractSerializer(obj.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, obj);
            String serializedString = Encoding.UTF8.GetString(stream.GetBuffer(), 0, (int)stream.Position);
            return serializedString;
        }
    }
}
