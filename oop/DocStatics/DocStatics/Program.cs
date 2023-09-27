using Implementation.IO.Factories;
using Implementation.Logger.Factories;

namespace DocStatics
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var ioFactory = new IOFactory();
            var id = Guid.NewGuid();
            var logger = new LoggerFactory().CreateLogger(id);
            var writer = ioFactory.CreateWriter(logger);
            var reader = ioFactory.CreateReader(logger, writer);
            var text = reader.ReadAllLines("Kerek egy hosszu szoveget: ");

            Console.WriteLine(text);
        }
    }
}