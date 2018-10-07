using System;
using System.Collections;
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
        public override string ToString()
        {
            return lastname + " " + name;
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
    class DictionaryStudents : IEnumerable, IEnumerable<KeyValuePair<Student, int>>
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
                Console.WriteLine("Введите имя студента:");
                string name = Console.ReadLine();
                Console.WriteLine("Введите фамилию студента:");
                string lastname = Console.ReadLine();
                Console.WriteLine("Введите оценку:");
                int m = int.Parse(Console.ReadLine());
                try
                {
                    AddStudent(new Student(name, lastname), m);
                    n--;
                }
                catch (ExistException ee)
                {
                    Console.WriteLine(ee.Message);
                }
                catch (RangeException re)
                {
                    Console.WriteLine(re.Message);
                }
                catch (Exception e)
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

        public IEnumerator<KeyValuePair<Student, int>> GetEnumerator()
        {
            foreach(var st in group)
            {
                yield return st;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable)group).GetEnumerator();
        }

        public int this[Student s]
        {
            get
            {
                return group[s];
            }
            set
            {
                group[s] = value;
            }
        }
        public SortedDictionary<Student, int>.KeyCollection GetKeys()
        {
            return group.Keys;
        }
        public int Count()
        {
            return group.Count;
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

            Console.WriteLine("Вывод списка с помощью foreach:");
            foreach (var st in ds)
                Console.WriteLine("{0}  {1}", st.Key, st.Value);

            Console.WriteLine("Вывод информации о первом студенте");
            Student s = ds.GetKeys().First<Student>();

            Console.WriteLine("{0}  {1}", s, ds[s]);
            Console.WriteLine("Вывод списка группы с помощью for:");
            for (int i = 0; i < ds.Count(); i++)
            {
                s = ds.GetKeys().ElementAt<Student>(i);
                Console.WriteLine("{0}  {1}", s, ds[s]);
            }
        }
    }
}
