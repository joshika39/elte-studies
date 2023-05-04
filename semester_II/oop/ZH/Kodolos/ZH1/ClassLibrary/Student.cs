namespace ClassLibrary
{
    public class Student
    {
        private List<Subject> _subjects;

        public string NeptunId { get; set; }
        public double Avg { get; private set; }
        public int CreditSum { get; private set; }
        public List<Subject> Subjects
        {
            get => _subjects;
            private set
            {
                _subjects = value;
                if (_subjects.Count <= 0) return;
                
                Avg = Subjects.Average(s => s.Grade);
                CreditSum = Subjects.Sum(s => s.Credit);
            }
        }

        private Student(string neptunId)
        {
            Subjects = new List<Subject>();
            NeptunId = neptunId;
        }

        public static bool TryParse(string line, out Student student)
        {
            var data = line.Split(new [] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);
            var tmpStudent = new Student(data[0]);
            var dataList = data.ToList();
            dataList.RemoveAt(0);
            dataList.RemoveAt(0);
            var subjectList = new List<Subject>();
            var tempSubject = new Subject("");
            for (var i = 0; i < dataList.Count; i++)
            {
                switch (i % 3)
                {
                    case 0:
                        tempSubject = new(dataList[i]);
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

