using Practitci;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Practice5
{
    internal class Program
    {
        static University university = new University();

        static void Main(string[] args)
        {
            AddTestData();

            bool work = true;
            while (work)
            {
                Console.WriteLine();
                Console.WriteLine(" УНИВЕРСИТЕТ ");
                Console.WriteLine("--- Студенты ---");
                Console.WriteLine("1. Добавить студента");
                Console.WriteLine("2. Информация о студенте");
                Console.WriteLine("3. Записать студента на курс");
                Console.WriteLine("4. Курсы студента");
                Console.WriteLine("--- Преподаватели ---");
                Console.WriteLine("5. Добавить преподавателя");
                Console.WriteLine("6. Информация о преподавателе");
                Console.WriteLine("7. Назначить преподавателя на курс");
                Console.WriteLine("--- Курсы ---");
                Console.WriteLine("8. Создать курс");
                Console.WriteLine("9. Информация о курсе");
                Console.WriteLine("10. Студенты курса");
                Console.WriteLine("--- Полные списки ---");
                Console.WriteLine("11. Все студенты");
                Console.WriteLine("12. Все преподаватели");
                Console.WriteLine("13. Все курсы");
                Console.WriteLine("14. Все люди университета");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadLine())
                {
                    case "1": AddStudent(); 
                        break;
                    case "2": ShowStudent(); 
                        break;
                    case "3": EnrollStudent(); 
                        break;
                    case "4": ShowStudentCourses(); 
                        break;
                    case "5": AddTeacher();
                        break;
                    case "6": ShowTeacher();
                        break;
                    case "7": AssignTeacher();
                        break;
                    case "8": AddCourse();
                        break;
                    case "9": ShowCourse(); 
                        break;
                    case "10": ShowCourseStudents();
                        break;
                    case "11": ShowAllStudents();
                        break;
                    case "12": ShowAllTeachers(); 
                        break;
                    case "13": ShowAllCourses();
                        break;
                    case "14": ShowAllPeople();
                        break;
                    case "0": work = false; 
                        break;
                    default: Console.WriteLine("Неверный выбор. Попробуйте снова."); 
                        break;
                }
            }
        }

        static void AddTestData()
        {
            Student s1 = university.AddStudent("Петров Иван", 19, "petrov@mail.ru", "ИС-224");
            Student s2 = university.AddStudent("Смирнова Анна", 20, "smirnova@mail.ru", "ИС-224");
            university.AddStudent("Кузнецов Олег", 18, "kuznetsov@mail.ru", "ПИ-231");

            Teacher t1 = university.AddTeacher("Иванова Мария", 45, "ivanova@univ.ru", "Информатика");
            university.AddTeacher("Орлов Сергей", 38, "orlov@univ.ru", "Математика");

            Course c1 = university.AddCourse("Программирование на C#", 72);
            university.AddCourse("Базы данных", 54);

            c1.SetTeacher(t1);
            c1.AddStudent(s1);
            c1.AddStudent(s2);
        }


        static void AddStudent()
        {
            string name = ReadName("ФИО: ");
            int age = ReadInt("Возраст: ", 16, 100);
            string email = ReadEmail();
            string group = ReadText("Группа: ");

            Student student = university.AddStudent(name, age, email, group);
            Console.WriteLine($"Студент добавлен: {student.Info()}");
        }

        static void ShowStudent()
        {
            Student student = ChooseStudent();
            if (student == null)
                return;

            Console.WriteLine(student.Info());
            PrintCourses(university.GetCoursesStudent(student), "Не записан ни на один курс.");
        }

        static void EnrollStudent()
        {
            Student student = ChooseStudent();
            if (student == null)
                return;
            Course course = ChooseCourse();
            if (course == null)
                return;

            if (course.AddStudent(student))
                Console.WriteLine($"{student.Name} записан(а) на курс «{course.Title}».");
            else
                Console.WriteLine("Студент уже записан на этот курс.");
        }

        static void ShowStudentCourses()
        {
            Student student = ChooseStudent();
            if (student == null)
                return;

            Console.WriteLine($"Курсы студента {student.Name}:");
            PrintCourses(university.GetCoursesStudent(student), "Не записан ни на один курс.");
        }

     

        static void AddTeacher()
        {
            string name = ReadName("Фамилия и Имя: ");
            int age = ReadInt("Возраст: ", 22, 100);
            string email = ReadEmail();
            string kafedra = ReadText("Кафедра: ");

            Teacher teacher = university.AddTeacher(name, age, email, kafedra);
            Console.WriteLine($"Преподаватель добавлен: {teacher.Info()}");
        }

        static void ShowTeacher()
        {
            Teacher teacher = ChooseTeacher();
            if (teacher == null)
                return;

            Console.WriteLine(teacher.Info());
            Console.WriteLine("Ведёт курсы:");
            PrintCourses(university.GetCoursesTeacher(teacher), "Не ведёт ни одного курса.");
        }

        static void AssignTeacher()
        {
            Teacher teacher = ChooseTeacher();
            if (teacher == null)
                return;
            Course course = ChooseCourse();
            if (course == null)
                return;

            if (course.Teacher == teacher)
            {
                Console.WriteLine("Этот преподаватель уже ведёт этот курс.");
                return;
            }
            if (course.Teacher != null)
                Console.WriteLine($"Прежний преподаватель {course.Teacher.Name} снят с курса.");

            course.SetTeacher(teacher);
            Console.WriteLine($"{teacher.Name} назначен(а) на курс "{course.Title}".");
        }

      
        static void AddCourse()
        {
            string title;
            while (true)
            {
                title = ReadText("Название курса: ");
                if (!university.CourseExists(title))
                    break;
                Console.WriteLine("Курс с таким названием уже есть.");
            }
            int hours = ReadInt("Количество часов: ", 1, 1000);

            Course course = university.AddCourse(title, hours);
            Console.WriteLine($"Курс создан: {course.Info()}");
        }

        static void ShowCourse()
        {
            Course course = ChooseCourse();
            if (course == null)
                return;
            Console.WriteLine(course.Info());
        }

        static void ShowCourseStudents()
        {
            Course course = ChooseCourse();
            if (course == null)
                return;

            List<Student> list = course.GetStudents();
            Console.WriteLine($"Студенты курса «{course.Title}»:");
            if (list.Count == 0)
            {
                Console.WriteLine("  На курс никто не записан.");
                return;
            }
            foreach (Student s in list)
                Console.WriteLine("  " + s.Info());
        }


        static void ShowAllStudents()
        {
            List<Student> list = university.GetStudents();
            if (list.Count == 0)
            {
                Console.WriteLine("Студентов пока нет.");
                return;
            }
            foreach (Student s in list)
                Console.WriteLine(s.Info());
        }

        static void ShowAllTeachers()
        {
            List<Teacher> list = university.GetTeachers();
            if (list.Count == 0)
            {
                Console.WriteLine("Преподавателей пока нет.");
                return;
            }
            foreach (Teacher t in list)
                Console.WriteLine(t.Info());
        }

        static void ShowAllCourses()
        {
            PrintCourses(university.GetCourses(), "Курсов пока нет.");
        }

     
        static void ShowAllPeople()
        {
            List<Person> people = university.GetAllPeople();
            if (people.Count == 0)
            {
                Console.WriteLine("В университете пока никого нет.");
                return;
            }
            foreach (Person p in people)
                Console.WriteLine(p.Info());
        }
        //Методы для методов
        static void PrintCourses(List<Course> list, string emptyMessage)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("  " + emptyMessage);
                return;
            }
            foreach (Course c in list)
                Console.WriteLine("  " + c.Info());
        }

        

        static Student ChooseStudent()
        {
            List<Student> list = university.GetStudents();
            if (list.Count == 0)
            {
                Console.WriteLine("Студентов пока нет.");
                return null;
            }
            foreach (Student s in list)
                Console.WriteLine($"  {s.Id} - {s.Name}");

            Student student = university.FindStudent(ReadInt("ID студента: ", 1, int.MaxValue));
            if (student == null)
                Console.WriteLine("Студент с таким ID не найден.");
            return student;
        }

        static Teacher ChooseTeacher()
        {
            List<Teacher> list = university.GetTeachers();
            if (list.Count == 0)
            {
                Console.WriteLine("Преподавателей пока нет.");
                return null;
            }
            foreach (Teacher t in list)
                Console.WriteLine($"  {t.Id} - {t.Name}");

            Teacher teacher = university.FindTeacher(ReadInt("ID преподавателя: ", 1, int.MaxValue));
            if (teacher == null)
                Console.WriteLine("Преподаватель с таким ID не найден.");
            return teacher;
        }

        static Course ChooseCourse()
        {
            List<Course> list = university.GetCourses();
            if (list.Count == 0)
            {
                Console.WriteLine("Курсов пока нет.");
                return null;
            }
            foreach (Course c in list)
                Console.WriteLine($"  {c.Id} - {c.Title}");

            Course course = university.FindCourse(ReadInt("ID курса: ", 1, int.MaxValue));
            if (course == null)
                Console.WriteLine("Курс с таким ID не найден.");
            return course;
        }

       

        static string ReadText(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s))
                    return s.Trim();
                Console.WriteLine("Значение не может быть пустым.");
            }
        }

        static string ReadName(string prompt)
        {
            while (true)
            {
                string name = ReadText(prompt);
                if (name.All(c => char.IsLetter(c) || c == ' ' || c == '-'))
                    return name;
                Console.WriteLine("Имя может содержать только буквы, пробелы и дефис.");
            }
        }

        static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                int value;
                if (int.TryParse(Console.ReadLine(), out value) && value >= min && value <= max)
                    return value;
                Console.WriteLine($"Введите целое число от {min} до {max}.");
            }
        }

        static string ReadEmail()
        {
            while (true)
            {
                string email = ReadText("Email: ");
                int at = email.IndexOf('@');

                if (at <= 0 || email.LastIndexOf('.') < at + 2 || email.EndsWith(".") || email.Contains(" "))
                    Console.WriteLine("Неверный формат email. Пример: ivanov@mail.ru");
                else if (university.EmailExists(email))
                    Console.WriteLine("Такой email уже есть в системе.");
                else
                    return email;
            }
        }
    }
}