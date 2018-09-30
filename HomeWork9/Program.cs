using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeWork9
{
    class Student
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
        public class CompareByLastName : IComparer<Student>
        {
            public int Compare(Student x, Student y)
            {
                if ((x.Lastname == y.Lastname) && (x.Name == y.Name))
                    throw new ExistException();
                else
                {
                    if(x.Lastname == y.Lastname)
                    {
                        return String.Compare(x.Name, y.Name);
                    }
                    return String.Compare(x.Lastname, y.Lastname);
                }
                
            }
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
        SortedDictionary<Student, int> ds = new SortedDictionary<Student, int>(new Student.CompareByLastName());
        private void AddStudent(Student st, int mark)
        {
            try
            {
                ds.Add(st, mark);
            }
            catch (ExistException ee) 
            {
                Console.WriteLine("Exception: {0}", ee.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }
        public void AddStudents(int n)
        {
            try
            {
                for (int i = 0; i < n; i++)
                {
                    Console.WriteLine("Set name");
                    string sn = Console.ReadLine();
                    Console.WriteLine("Set last name");
                    string sln = Console.ReadLine();
                    Console.WriteLine("Set mark");
                    int m = int.Parse(Console.ReadLine());
                    if(m > 100 || m < 0)
                    {
                        throw new RangeException(m);
                    }
                    AddStudent(new Student(sn, sln), m);
                }
            }
            catch(RangeException re)
            {
                Console.WriteLine("Exception: {0}", re.Message);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: {0}", e.Message);
            }
        }
        public void ListStudents()
        {
            foreach (var ls in ds)
                Console.WriteLine("Student: {1} {0}, Mark: {2}", ls.Key.Name, ls.Key.Lastname, ls.Value);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            DictionaryStudents ds = new DictionaryStudents();
            ds.AddStudents(3);
            ds.ListStudents();
        }
    }
}
