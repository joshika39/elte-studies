using System.Globalization;
using BL.Book;
using BL.Book.Factory;
using BL.Lib;
using Implementation.IO.Factories;
using Implementation.Logger.Factories;
using Infrastructure.Logger;

namespace Client
{
    internal static class Program
    {
        private static ILogger _logger = null!;
        private static void Main(string[] args)
        {
            var id = Guid.NewGuid();

            _logger = new LoggerFactory().CreateLogger(id);
            var ioFactory = new IOFactory();
            var writer = ioFactory.CreateWriter(_logger);
            var reader = ioFactory.CreateReader(_logger, writer);

            var bookFact = new BookFactory();
            ILibrary lib = new LibraryClass(bookFact, reader, writer);
            _ = new Manager(writer, reader, lib);
        }


        public static void DisposeApp()
        {
            _logger.Dispose();
            Environment.Exit(0);
        }
    }
}
