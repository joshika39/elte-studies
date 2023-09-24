using ClassLibrary;
using Implementation.IO.Factories;
using Implementation.Logger.Factories;

namespace ZH1_Jeles
{
    internal static class Program
    {
        private static void Main()
        {
            var id = Guid.NewGuid();
            var loggerFactory = new LoggerFactory();
            var ioFactory = new IOFactory();

            using var logger = loggerFactory.CreateLogger(id);
            var writer = ioFactory.CreateWriter(logger);
            var reader = ioFactory.CreateReader(logger, writer);

            var streamReader = new StreamReader(@"Resources\inp.txt");

            var testStudents = reader.ReadAllLines<Student>(streamReader, Student.TryParse).ToList();

            var highestStud = testStudents.FirstOrDefault(stud => stud.CreditSum == testStudents.Max(s => s.CreditSum));

            var test = testStudents.Any(s => s.Subjects[0].Grade >= 0) ? "igen" : "nem";

            Console.WriteLine($"{test} {highestStud?.NeptunId} {highestStud?.CreditSum}");

        }
    }
}