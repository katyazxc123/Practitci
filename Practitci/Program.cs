using System;
using System.Collections.Generic;
using System.Globalization;

namespace Practitci
{
    internal class Program
    {
        static Library libriary = new Library();
        static Telega telega = new Telega();

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
                Console.WriteLine("9. Вставить блок книг");
                Console.WriteLine("10. Добавить книгу в корзину");
                Console.WriteLine("11. Показать корзину");
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
                    case "9":
                        ImportBlock();
                        break;
                    case "10":
                        AddToTelega();
                        break;
                    case "11":
                        ShowTelega();
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
                telega.RemoveBook(id);
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

        static void ImportBlock()
        {
            Console.WriteLine("Вставьте книги, по одной в строке:");
            Console.WriteLine("Название;Автор;Жанр;Год;Цена");
            Console.WriteLine("Жанры: Roman, Fantasy, Detective, Poetry");
            Console.WriteLine("Чтобы закончить, нажмите Enter на пустой строке.");

            int added = 0;
            int lineNumber = 0;
            while (true)
            {
                string line = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    break;

                lineNumber++;
                string error;
                if (TryAddFromLine(line, out error))
                    added++;
                else
                    Console.WriteLine($"Строка {lineNumber} пропущена: {error}");
            }
            Console.WriteLine($"Добавлено книг: {added}");
        }

        static void AddToTelega()
        {
            int id = ReadInt("ID книги: ", 1, int.MaxValue);
            Book book = libriary.FindById(id);
            if (book == null)
            {
                Console.WriteLine("Книга с таким ID не найдена.");
                return;
            }
            telega.Add(book);
            Console.WriteLine($"В корзину добавлена: {book.GetInfo()}");
        }

        static void ShowTelega()
        {
            List<Book> items = telega.GetItems();
            if (items.Count == 0)
            {
                Console.WriteLine("Корзина пуста.");
                return;
            }
            PrintBooks(items);
            Console.WriteLine($"Итого к оплате: {telega.GetTotal()} руб.");
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

        static bool TryParseGenre(string s, out Genre genre)
        {
            foreach (Genre g in Enum.GetValues(typeof(Genre)))
            {
                if (g.ToString().ToLower() == s.Trim().ToLower())
                {
                    genre = g;
                    return true;
                }
            }
            genre = Genre.Roman;
            return false;
        }

        static bool TryAddFromLine(string line, out string error)
        {
            string[] parts = line.Split(';');
            if (parts.Length != 5)
            {
                error = "нужно 5 значений через «;»";
                return false;
            }

            string title = parts[0].Trim();
            string author = parts[1].Trim();
            if (title == "" || author == "")
            {
                error = "пустое название или автор";
                return false;
            }

            Genre genre;
            if (!TryParseGenre(parts[2], out genre))
            {
                error = $"неизвестный жанр «{parts[2].Trim()}»";
                return false;
            }

            int year;
            if (!int.TryParse(parts[3].Trim(), out year) || year < Book.MinYear || year > DateTime.Now.Year)
            {
                error = $"год должен быть от {Book.MinYear} до {DateTime.Now.Year}";
                return false;
            }

            decimal price;
            if (!TryParsePrice(parts[4], out price))
            {
                error = "цена должна быть числом больше 0";
                return false;
            }

            libriary.Add(title, author, genre, year, price);
            error = "";
            return true;
        }
    }
}