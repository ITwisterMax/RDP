using System.IO;

namespace Rdp.Terminal.Core.Server.Models.Controls
{
    public class Transmitted
    {
        private readonly Convertation _details;
        private readonly string _path;

        // Импорт методов и свойст класса Convertation
        private Transmitted()
        {
            _details = new Convertation();
        }

        // Инициализация информации
        public Transmitted(string path) : this()
        {
            _path = path;

            var info = new FileInfo(path);
            _details.Name = Path.GetFileNameWithoutExtension(info.Name);
            _details.Extension = Path.GetExtension(info.Name);
        }

        // Преобразование из массива байт
        public Transmitted(byte[] byteArrayDetails) : this()
        {
            _details = Convertation.ConvertToDetails(byteArrayDetails);
        }

        // Преобразование в массив байт 
        public byte[] GetByteArrayDetails()
        {
            return Convertation.ConvertToByteArray(_details);
        }

        // Получение размера
        public long GetContentLength()
        {
            var info = new FileInfo(_path);
            return info.Length;
        }

        // Чтение данных в буфер
        public void ReadBytes(byte[] buffer, int bufferOffset, int count, long offset)
        {
            using (var stream = new FileStream(_path, FileMode.Open, FileAccess.Read))
            {
                stream.Seek(offset, SeekOrigin.Begin);
                stream.Read(buffer, bufferOffset, count);
            }
        }

        // Запись данных из буфера
        public void WriteBytes(string path, byte[] content, int bufferOffset, int count)
        {
            using (var stream = new FileStream(path + _details.Name + _details.Extension, FileMode.Append, FileAccess.Write, FileShare.Write))
            {
                stream.Write(content, bufferOffset, count);
            }
        }
    }
}
