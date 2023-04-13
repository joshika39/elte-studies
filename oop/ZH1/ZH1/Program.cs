using Implementation.Logger.Factories;
using Implementation.IO.Factories;
using System.Linq;

namespace ZH1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var id = Guid.NewGuid();
            var loggerFactory = new LoggerFactory();
            var ioFactory = new IOFactory();

            using var logger = loggerFactory.CreateLogger(id);
            var writer = ioFactory.CreateWriter(logger);
            var reader = ioFactory.CreateReader(logger, writer);

            var strmReader = new StreamReader(@"Resources\inp.txt");

            var testStudents = reader.ReadLine<Student>(strmReader, Student.TryParse).ToList();

            var osszKredStud = testStudents.Where(s => s.Subjects.Count < 4);
            var osszKred = osszKredStud.FirstOrDefault(stud => stud.CreditSum == osszKredStud.Max(s => s.CreditSum));
            var test = testStudents.Any(st => (st.Subjects.Count >= 4) && st.Avg == 5.0f) ? "igen" : "nem";

            Console.WriteLine($"{osszKred?.CreditSum} {test}");
        }
    }
}