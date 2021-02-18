using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq_Student
{
    class Program
    {
        static List<Student> students = new List<Student>
 {
    new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}},
    new Student {First="Claire", Last="O’Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}},
    new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}},
    new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}},
    new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}},
     new Student {First="Ivan", Last="Ivanov", ID=116, Scores= new List<int> {91, 72, 91, 70}},
      new Student {First="Petr", Last="Petrov", ID=117, Scores= new List<int> {92, 72, 91, 81}},
       new Student {First="Sidor", Last="Sidorov", ID=118, Scores= new List<int> {93, 72, 91, 70}},
       new Student {First="Ivanna", Last="Ivanova", ID=116, Scores= new List<int> {91, 72, 91, 70}},
        };
        static void Main(string[] args)
        {
            IEnumerable<Student> studentQuery =
             from student in students
             where student.Scores[0] > 90 && student.Scores[3] < 80
             select student;

            var studentQueryW = students.Where(st => st.Scores[0] > 90 && st.Scores[3] < 80);

            int studentCount = (
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                select student).Count();

            int studentCountW = students.Where(st => st.Scores[0] > 90 && st.Scores[3] < 80).Count();

            var studentList = (
                from student in students
                where student.Scores[0] > 90 && student.Scores[3] < 80
                // orderby student.Last ascending
                orderby student.Scores[0] descending
                select student).ToList();

            var studentQuery2 =
                from student in students
                group student by student.Last[0];

            var studentQuery3 =
                from student in students
                group student by student.Last[0];

            var studentQuery4 =
                from student in students
                group student by student.Last[0] into studentGroup
                orderby studentGroup.Key
                select studentGroup;

            var studentQuery5 =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] + student.Scores[2] + student.Scores[3]
                where totalScore / 4 < student.Scores[0]
                select student.Last + " " + student.First;

            var studentQuery6 =
                from student in students
                let totalScore = student.Scores[0] + student.Scores[1] +
                student.Scores[2] + student.Scores[3]
                select totalScore;

            IEnumerable<string> studentQuery7 =
                 from student in students
                 where student.Last == "Garcia"
                 select student.First;


            foreach (Student student in studentQuery)
            {
                Console.WriteLine("{0}, {1}", student.Last, student.First);
            }
            foreach (Student student in studentQueryW)
            {
                Console.WriteLine("{0}, {1}", student.Last, student.First);
            }
            Console.WriteLine("Количество студентов: {0}, {1}", studentCount, studentCountW);
            foreach (Student student in studentList)
            {
                //Console.WriteLine("{0}, {1}", student.Last, student.First);
                Console.WriteLine("{0}, {1} {2}", student.Last, student.First, student.Scores[0]);
            }

            foreach (var studentGroup in studentQuery2)
            {
                Console.WriteLine(studentGroup.Key);
                foreach (Student student in studentGroup)
                {
                    Console.WriteLine(" {0}, {1}",
                    student.Last, student.First);
                }
            }
            foreach (var groupOfStudents in studentQuery3)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine(" {0}, {1}",
                    student.Last, student.First);
                }
            }
            foreach (var groupOfStudents in studentQuery4)
            {
                Console.WriteLine(groupOfStudents.Key);
                foreach (var student in groupOfStudents)
                {
                    Console.WriteLine(" {0}, {1}",
                    student.Last, student.First);
                }
            }
            double averageScore = studentQuery6.Average();
            Console.WriteLine("Class average score = {0}", averageScore);

            Console.WriteLine("The Garcias in the class are:");
            foreach (string s in studentQuery7)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }
    }
}
