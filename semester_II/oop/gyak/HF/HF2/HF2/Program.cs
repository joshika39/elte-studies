using Core;

namespace HF2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Bootstrapper.Initialize(out var reader);
            var streamReader = new StreamReader(@"Res\inp.txt");
            var list = reader.ReadAllLines<int>(streamReader, int.TryParse, ' ', '\t').ToList();

            var hasPrime = list.Any(l => l.Any(n => Tools.IsPrime(n))) ? "van" : "nincs";
            var smallest = list.SelectMany(x => x).Where(x => x % 2 == 0).Min();


            Console.WriteLine($"A legkisseb szam: {smallest} es prim szam {hasPrime}");
        }
    }
}