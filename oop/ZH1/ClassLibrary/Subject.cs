
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZH1
{
    public class Subject
    {
        public string Code { get; set; }
        public int Credit { get; set; }
        public double Grade { get; set; }

        public Subject()
        {

        }

        public Subject(string code, int credit, double grade)
        {
            Code = code;
            Credit = credit;
            Grade = grade;
        }
    }
}
