using System.IO;

namespace Rdp.Terminal.Core.Server.Models.Controls
{
    /// <summary>
    ///     Trasnmitted data
    /// </summary>
    public class Transmitted
    {
        private readonly Convertation _details;
        
        private readonly string _path;

        /// <summary>
        ///     Default constructor
        /// </summary>
        private Transmitted()
        {
            _details = new Convertation();
        }

        /// <summary>
        ///     Initialize properties
        /// </summary>
        ///
        /// <param name="path">Path</param>
        public Transmitted(string path) : this()
        {
            _path = path;

            var info = new FileInfo(path);
            _details.Name = Path.GetFileNameWithoutExtension(info.Name);
            _details.Extension = Path.GetExtension(info.Name);
        }

        /// <summary>
        ///     Convert to server details
        /// </summary>
        ///
        /// <param name="byteArrayDetails">Byte array</param>
        public Transmitted(byte[] byteArrayDetails) : this()
        {
            _details = Convertation.ConvertToDetails(byteArrayDetails);
        }

        /// <summary>
        ///     Convert to byte array
        /// </summary>
        ///
        /// <returns>byte[]</returns>
        public byte[] GetByteArrayDetails()
        {
            return Convertation.ConvertToByteArray(_details);
        }

        /// <summary>
        ///     Get content length
        /// </summary>
        ///
        /// <returns>long</returns>
        public long GetContentLength()
        {
            var info = new FileInfo(_path);
            return info.Length;
        }

        /// <summary>
        ///     Read byte array
        /// </summary>
        ///
        /// <param name="buffer">Server buffer</param>
        /// <param name="bufferOffset">Buffer offset</param>
        /// <param name="count">Count</param>
        /// <param name="offset">Offset</param>
        public void ReadBytes(byte[] buffer, int bufferOffset, int count, long offset)
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                stream.Seek(offset, SeekOrigin.Begin);
                stream.Read(buffer, bufferOffset, count);
            }
        }

        /// <summary>
        ///     Writes data to buffer
        /// </summary>
        ///
        /// <param name="path">Path</param>
        /// <param name="content">Content</param>
        /// <param name="bufferOffset">Buffer offset</param>
        /// <param name="count">Count</param>
        public void WriteBytes(string path, byte[] content, int bufferOffset, int count)
        {
            using (var stream = new FileStream(path + _details.Name + _details.Extension, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                stream.Write(content, bufferOffset, count);
            }
        }
    }
}
