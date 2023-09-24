using System.Linq;

namespace Horgaszverseny
{
    public class Catch
    {
        public string Name { get; set; }
        public string Time { get; set; }
        public double Weight { get; set; }
        public double Length { get; set; }

        public Catch()
        { }
        
        public Catch(string name, string time, double weight, double length)
        {
            Name = name;
            Time = time;
            Weight = weight;
            Length = length;
        }
    }
    
    
}
