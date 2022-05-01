using System;
using System.IO;
using System.Xml.Serialization;

namespace Rdp.Terminal.Core.Server.Models.Controls
{
    /// <summary>
    ///     Serialize data
    /// </summary>
    [Serializable]
    public class Convertation
    {
        public string Name { get; set; }

        public string Extension { get; set; }

        /// <summary>
        ///     Convert to byte array
        /// </summary>
        ///
        /// <param name="details">Details</param>
        /// 
        /// <returns>byte[]</returns>
        public static byte[] ConvertToByteArray(Convertation details)
        {
            byte[] serializedDetails;

            using (var memoryStream = new MemoryStream())
            {
                var xmlSerializer = new XmlSerializer(typeof(Convertation));
                xmlSerializer.Serialize(memoryStream, details);

                memoryStream.Position = 0;
                serializedDetails = new byte[memoryStream.Length];

                const int memoryStreamOffset = 0;
                memoryStream.Read(serializedDetails, memoryStreamOffset, serializedDetails.Length);
            }

            return serializedDetails;
        }

        /// <summary>
        ///     Convert to details
        /// </summary>
        ///
        /// <param name="byteArrayDetails">Byte array</param>
        /// 
        /// <returns>Convertation</returns>
        public static Convertation ConvertToDetails(byte[] byteArrayDetails)
        {
            using (var memoryStream = new MemoryStream())
            {
                const int memoryStreamOffset = 0;

                memoryStream.Write(byteArrayDetails, memoryStreamOffset, byteArrayDetails.Length);
                memoryStream.Position = 0;

                var xmlSerializer = new XmlSerializer(typeof(Convertation));

                return (Convertation)xmlSerializer.Deserialize(memoryStream);
            }
        }
    }
}
