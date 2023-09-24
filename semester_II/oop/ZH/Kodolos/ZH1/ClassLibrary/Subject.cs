namespace ClassLibrary
{
    public class Subject
    {
        public string Code { get; set; }
        public int Credit { get; set; }
        public double Grade { get; set; }

        public Subject(string code)
        {
            Code = code;
        }

        public Subject(string code, int credit, double grade)
        {
            Code = code;
            Credit = credit;
            Grade = grade;
        }
    }
}
