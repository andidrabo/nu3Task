using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace nu3Task.Helpers
{
    public static class XmlParser
    {
        public static T ParseXml<T>(this string value) where T : class
        {
            var xmlSerializer = new XmlSerializer(typeof(T));
            using (var textReader = new StringReader(value))
            {
                return (T)xmlSerializer.Deserialize(textReader);
            }
        }
    }
}
