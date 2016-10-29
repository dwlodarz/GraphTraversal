using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace GraphTraversal.DataLoader
{
    /// <summary>
    /// Serialization/Deserialization helper
    /// </summary>
    internal static class SerializationHelper
    {
        /// <summary>
        /// Deserializes string to object.
        /// </summary>
        /// <typeparam name="T">Target type.</typeparam>
        /// <param name="objectData">String xml.</param>
        /// <returns>Typed object</returns>
        public static T XmlDeserializeFromString<T>(this string objectData) where T : class, new()
        {
            return (T)XmlDeserializeFromString(objectData, typeof(T));
        }

        /// <summary>
        /// Not typed deserialization of string.
        /// </summary>
        /// <param name="objectData">String data.</param>
        /// <param name="type">Target type.</param>
        /// <returns>The object.</returns>
        public static object XmlDeserializeFromString(this string objectData, Type type)
        {
            var serializer = new XmlSerializer(type);
            object result;

            using (TextReader reader = new StringReader(objectData))
            {
                result = serializer.Deserialize(reader);
            }

            return result;
        }

        /// <summary>
        /// Serialiazes data to json.
        /// </summary>
        /// <typeparam name="T">Type in which data is stored.</typeparam>
        /// <param name="data">The data to be serialized.</param>
        /// <returns>The output JSON.</returns>
        public static string Serialize<T>(T data) where T : class
        {
            if (data != null)
            {
                return JsonConvert.SerializeObject(new { node = data });
            }

            return null;
        }
    }
}
