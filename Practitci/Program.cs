using System;
using System.Collections.Generic;
using System.Globalization;

namespace Practitci
{
    internal class Program
    {
        static Library libriary = new Library();
   
        static void Main(string[] args)
        {
            AddTestBooks();

            bool work = true;
            while (work)
            {
                Console.WriteLine();
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Удалить книгу");
                Console.WriteLine("3. Показать все книги");
                Console.WriteLine("4. Найти книги");
                Console.WriteLine("5. Сортировать по названию");
                Console.WriteLine("6. Сортировать по году");
                Console.WriteLine("7. Самая дорогая книга и самая дешёвая книга");
                Console.WriteLine("8. Количество книг по авторам");
                Console.WriteLine("0. Выход");
                Console.Write("Ваш выбор: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        DeleteBook();
                        break;
                    case "3":
                        PrintBooks(libriary.GetAll());
                        break;
                    case "4":
                        SearchBooks();
                        break;
                    case "5":
                        PrintBooks(libriary.SortByTitle());
                        break;
                    case "6":
                        PrintBooks(libriary.SortByYear());
                        break;
                    case "7":
                        ShowPriceExtremes();
                        break;
                    case "8":
                        ShowAuthors();
                        break;
                    case "0":
                        work = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }   
        }       

        static void AddTestBooks()
        {
            libriary.Add("Война и мир", "Лев Толстой", Genre.Roman, 1869, 500);
            libriary.Add("Гарри Поттер и философский камень", "Джоан Роулинг", Genre.Fantasy, 1997, 300);
            libriary.Add("Шерлок Холмс", "Артур Конан Дойл", Genre.Detective, 1892, 400);
            libriary.Add("Стихи", "Александр Пушкин", Genre.Poetry, 1820, 200);
            libriary.Add("Анна Каренина", "Лев Толстой", Genre.Roman, 1878, 450);
        }

        static void AddBook()
        {
            string title = ReadText("Введите название книги: ");
            string author = ReadText("Введите автора книги: ");
            Genre genre = ReadGenre();
            int year = ReadInt("Введите год издания: ", Book.MinYear, DateTime.Now.Year);
            decimal price = ReadPrice("Введите цену книги: ");

            Book book = libriary.Add(title, author, genre, year, price);
            Console.WriteLine($"Книга добавлена: {book.GetInfo()}");
        }

        static void DeleteBook()
        {
            int id = ReadInt("ID книги для удаления: ", 1, int.MaxValue);
            if (libriary.Remove(id))
            {
              
                Console.WriteLine("Книга удалена.");
            }
            else
            {
                Console.WriteLine("Книга с таким ID не найдена.");
            }
        }

        static void SearchBooks()
        {
            Console.WriteLine("Искать по: 1 - названию, 2 - автору, 3 - жанру");
            int choice = ReadInt("Ваш выбор: ", 1, 3);

            List<Book> found;
            if (choice == 1)
                found = libriary.SearchByTitle(ReadText("Часть названия: "));
            else if (choice == 2)
                found = libriary.SearchByAuthor(ReadText("Часть имени автора: "));
            else
                found = libriary.SearchByGenre(ReadGenre());

            PrintBooks(found);
        }

        static void ShowPriceExtremes()
        {
            if (libriary.Count == 0)
            {
                Console.WriteLine("В библиотеке нет книг.");
                return;
            }
            Console.WriteLine($"Самая дорогая: {libriary.GetMostExpensive().GetInfo()}");
            Console.WriteLine($"Самая дешёвая: {libriary.GetCheapest().GetInfo()}");
        }

        static void ShowAuthors()
        {
            Dictionary<string, int> authors = libriary.CountByAuthor();
            if (authors.Count == 0)
            {
                Console.WriteLine("В библиотеке нет книг.");
                return;
            }
            foreach (KeyValuePair<string, int> pair in authors)
                Console.WriteLine($"{pair.Key}: {pair.Value} шт.");
        }
            
        static void PrintBooks(List<Book> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Книги не найдены.");
                return;
            }
            foreach (Book book in list)
                Console.WriteLine(book.GetInfo());
        }

        // ---------- Ввод с проверками ----------

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

        static decimal ReadPrice(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                decimal value;
                if (TryParsePrice(Console.ReadLine(), out value))
                    return value;
                Console.WriteLine("Цена должна быть числом больше 0.");
            }
        }

        static Genre ReadGenre()
        {
            Console.WriteLine("Жанры:");
            foreach (Genre g in Enum.GetValues(typeof(Genre)))
                Console.WriteLine($"  {(int)g} - {g}");

            int count = Enum.GetValues(typeof(Genre)).Length;
            return (Genre)ReadInt("Номер жанра: ", 1, count);
        }

        static bool TryParsePrice(string s, out decimal price)
        {
            price = 0;
            if (s == null)
                return false;
            string normalized = s.Trim().Replace(',', '.');
            return decimal.TryParse(normalized, NumberStyles.Number, CultureInfo.InvariantCulture, out price)
                   && price > 0;
        }
    }
}