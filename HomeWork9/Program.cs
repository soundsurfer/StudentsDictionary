using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork9
{
    class Student : IComparable<Student>
    {
        private string name;
        private string lastname;
        public Student(string name, string lastname)
        {
            Name = name;
            Lastname = lastname;
        }

        public string Name { get => name; set => name = value; }
        public string Lastname { get => lastname; set => lastname = value; }
      
        public int CompareTo(Student other)
        {
            string s1 = lastname + " " + name;
            string s2 = other.lastname + " " + other.name;
            return s1.CompareTo(s2);
        }
    }
    class RangeException : Exception
    {
        int X { get; }
        public override string Message
        {
            get
            {
                return "Некорректный ввод оценки!";
            }
        }
        public RangeException(int x)
        {
            this.X = x;
        }
    }
    class ExistException : Exception
    {
        public override string Message
        {
            get
            {
                return "Этот студент уже есть в списке!";
            }
        }
    }
    class DictionaryStudents
    {
        private SortedDictionary<Student, int> group;

        public DictionaryStudents()
        {
            group = new SortedDictionary<Student, int>();
        }

        private void AddStudent(Student st, int mark)
        {
            if (group.Keys.Contains<Student>(st))
                throw new ExistException();
            else
                if (mark < 0 || mark > 100)
                throw new RangeException(mark);
            else
                group.Add(st, mark);
        }
        public void AddStudents(int n)
        {
            while (n > 0)
            {
                Console.WriteLine("Введите имя и фамилию студента:");
                string name = Console.ReadLine();
                string lastname = Console.ReadLine();
                Console.WriteLine("Введите оценку:");
                int m = int.Parse(Console.ReadLine());
                try
                {
                    AddStudent(new Student(name, lastname), m);
                    n--;
                }
                catch(ExistException ee)
                {
                    Console.WriteLine(ee.Message);
                }
                catch(RangeException re)
                {
                    Console.WriteLine(re.Message);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            
            
        }
        public void ListStudents()
        {
            foreach (var ls in group)
                Console.WriteLine("Student: {1} {0}, Mark: {2}", ls.Key.Lastname, ls.Key.Name, ls.Value);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryStudents ds = new DictionaryStudents();
            Console.WriteLine("Введите количество студентов: ");
            int n = int.Parse(Console.ReadLine());
            ds.AddStudents(n);
            ds.ListStudents();
        }
    }
}
