using System;
using System.IO;
using System.Xml.Serialization;

namespace Rdp.Terminal.Core.Server.Models.Controls
{
    [Serializable]
    public class Convertation
    {
        public string Name { get; set; }
        public string Extension { get; set; }

        public static byte[] ConvertToByteArray(Convertation details)
        {
            byte[] serializedDetails;

            // xml-сериализация информации
            using (var memoryStream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(typeof(Convertation));
                xmlSerializer.Serialize(memoryStream, details);

                memoryStream.Position = 0;
                serializedDetails = new byte[memoryStream.Length];

                const int memoryStreamOffset = 0;
                memoryStream.Read(serializedDetails, memoryStreamOffset, serializedDetails.Length);
            }

            // сериализованный массив байтов
            return serializedDetails;
        }

        public static Convertation ConvertToDetails(byte[] byteArrayDetails)
        {
            // xml-десериализация информации
            using (var memoryStream = new MemoryStream())
            {
                const int memoryStreamOffset = 0;

                memoryStream.Write(byteArrayDetails, memoryStreamOffset, byteArrayDetails.Length);
                memoryStream.Position = 0;

                var xmlSerializer = new XmlSerializer(typeof(Convertation));

                // десериализованная информация
                return (Convertation)xmlSerializer.Deserialize(memoryStream);
            }
        }
    }
}
