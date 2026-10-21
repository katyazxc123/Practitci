using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    internal class Course
    {
        private List<Student> students = new List<Student>();

        public int Id { get; private set; }
        public string Title { get; private set; }
        public int Hours { get; private set; }
        public Teacher Teacher { get; private set; }

        public int StudentCount
        {
            get { return students.Count; }
        }

        public Course(int id, string title, int hours)
        {
            Id = id;
            Title = title;
            Hours = hours;
        }

        public void SetTeacher(Teacher teacher)
        {
            Teacher = teacher;
        }

        public bool HasStudent(Student student)
        {
            return students.Contains(student);
        }

        public bool AddStudent(Student student)
        {
            if (students.Contains(student))
            {
                return false;
            }
            students.Add(student);
            return true;
        }

        public List<Student> GetStudents()
        {
            return students.OrderBy(s => s.Name).ToList();
        }

        public string Info()
        {
            string a = "Нет препода";
            if (Teacher != null)
            {
                a = Teacher.Name;
            }
            return $"Id:{Id}, Название курса: {Title}, Часы: {Hours} ч, Преподаватель: {a}, Студентов: {StudentCount}";
        }

       
    }

    
}
