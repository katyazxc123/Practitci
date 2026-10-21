using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    internal class University
    {
        private List<Student> people = new List<Student>();
        private List<Teacher> teachers = new List<Teacher>();
        private List<Course> courses = new List<Course>();  
        private int nextIdStudent = 1;
        private int nextIdTeacher = 1;
        private int nextIdCourse = 1;

        public Student AddStudent(string name, int age, string email, string group)
        {
            Student student = new Student(nextIdStudent, name, age, email, group);
            nextIdStudent++;
            students.Add(student);
            return student;
        }

        public Teacher AddTeacher(string name, int age, string email, string kafedra)
        {
            Teacher teacher = new Teacher(nextIdTeacher, name, age, email, kafedra);
            nextIdTeacher++;
            teachers.Add(teacher);
            return teacher;
        }

        public Course AddCourse(string title, int hours)
        {
            Course course = new Course(nextIdCourse, title, hours);
            nextIdCourse++;
            courses.Add(course);
            return course;
        }

        public Student FindStudent(int id)
        {
            return students.FirstOrDefault(s => s.Id == id);
        }
        
        public Teacher FindTeacher(int id)
        {
            return teachers.FirstOrDefault(t => t.Id == id);
        }

        public Course FindCourse(int id)
        {
            return courses.FirstOrDefault(c => c.Id == id);
        }

        public bool EmailExists(string email)
        {
            return GetAllPeople().Any(s => s.Email.ToLower() == email.ToLower());
        }

        public bool CourseExists(string title)
        {
            return courses.Any(c => c.Title.ToLower() == title.ToLower());
        }

        public List<Student> GetStudents()
        {
            return students.OrderBy(s => s.Name).ToList();
        }

        public List<Teacher> GetTeachers()
        {
            return teachers.OrderBy(t => t.Name).ToList();
        }

        public List<Course> GetCourses()
        {
            return courses.OrderBy(c => c.Title).ToList();
        }

        public List<Person> GetAllPeople()
        {
            List<Person> allPeople = new List<Person>();
            allPeople.AddRange(students);
            allPeople.AddRange(teachers);
            return allPeople.OrderBy(p => p.Name).ToList();
        }
        public List<Course> GetCoursesStudent(Student student)
        {
            return courses.Where(c => c.HasStudent(student)).ToList();
        }

        public List<Course> GetCoursesTeacher(Teacher teacher)
        {
            return courses.Where(c => c.Teacher == teacher).ToList();
        }


    }
}
