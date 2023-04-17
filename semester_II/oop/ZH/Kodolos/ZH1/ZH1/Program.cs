using Core;

namespace ZH1
{
    internal static class Program
    {
        private static void Main()
        {
            Bootstrapper.Initialize(out var reader);
            var path = @"Resources";
            var files = Directory.GetFiles(path);

            foreach (var file in files)
            {
                using var streamReader = new StreamReader(file);
                
                var testStudents = reader.ReadLine<Student>(streamReader, Student.TryParse).ToList();

                // 1. Task, Get the highest CreditSum from those, who have less than 4 subjects
                var maxCredit = testStudents
                    .Where(s => s.Subjects.Count < 4)
                    .MaxBy(s => s.CreditSum);
            
                // 2. Task, Is there any student with more than 4 subject with an average of 5.0?
                var test = testStudents
                    .Any(st => st.Subjects.Count >= 4 && Math.Abs(st.Avg - 5.0) == 0) ? "igen" : "nem";

                Console.WriteLine($"{maxCredit?.CreditSum} {test}");
            }
        }
    }
}