

using Infrastrucutre;

namespace Infrastructure.Impl
{
    public class Logger : ILogger
    {
        private readonly Guid _id;
        private readonly string _logPath;

        public Logger(Guid id, string logPath)
        {
            if(id.Equals(Guid.Empty))
            {
                throw new ArgumentNullException(nameof(id));
            }

            _id = id;
            _logPath = logPath ?? throw new ArgumentNullException(nameof(logPath));
        }

        public string LogWrite(string message)
        {
            CreateFileStreamAndWriter(out var fileStream, out var streamWriter);

            Console.SetOut(streamWriter);
            Console.Write(message);

            streamWriter.Close();
            fileStream.Close();

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            return message;
        }

        public string LogWriteLine(string message)
        {
            CreateFileStreamAndWriter(out var fileStream, out var streamWriter);

            Console.SetOut(streamWriter);
            Console.WriteLine(message);
            streamWriter.Close();
            fileStream.Close();

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()));
            return message;
        }

        private void CreateFileStreamAndWriter(out FileStream fileStream, out StreamWriter streamWriter)
        {
            if (!File.Exists(_logPath))
            {
                fileStream = File.Open(_logPath, FileMode.Create);
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine(_id.ToString());
            }
            else
            {
                using (var reader = new StreamReader(_logPath))
                {
                    var line = reader.ReadLine();
                    if (Guid.TryParse(line, out var testId) && testId.Equals(_id))
                    {
                        fileStream = File.Open(_logPath, FileMode.Append);
                    }
                    else
                    {
                        fileStream = File.Open(_logPath, FileMode.Create);
                    }
                    streamWriter = new StreamWriter(fileStream);
                }
            }
        }
    }
}
