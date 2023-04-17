using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZH1
{
    public class Student
    {
        private List<Subject> subjects;

        public string NeptunId { get; set; }
        public double Avg { get; private set; }
        public int CreditSum { get; private set; }
        public List<Subject> Subjects
        {
            get => subjects;
            set
            {
                subjects = value;
                Avg = Subjects.Average(s => s.Grade);
                CreditSum = Subjects.Sum(s => s.Credit);
            }
        }

        public Student(string neptunId)
        {
            NeptunId = neptunId;
        }

        public static bool TryParse(string line, out Student student)
        {
            var data = line.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var tmpStudent = new Student(data[0]);
            var db = int.Parse(data[1]);
            var dataList = data.ToList();
            dataList.RemoveAt(0);
            dataList.RemoveAt(0);
            var subjectList = new List<Subject>();
            var tempSubject = new Subject(); ;
            for (int i = 0; i < dataList.Count; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        tempSubject = new();
                        tempSubject.Code = dataList[i];
                        break;
                    case 1:
                        tempSubject.Credit = int.Parse(dataList[i]);
                        break;
                    case 2:
                        tempSubject.Grade = double.Parse(dataList[i]);
                        subjectList.Add(tempSubject);
                        break;
                }
            }
            tmpStudent.Subjects = subjectList;
            student = tmpStudent;
            return true;
        }
    }
}

