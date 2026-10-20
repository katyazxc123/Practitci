using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    internal class Libriary1
    {
        class Libriary
        {
            private List<Book> books = new List<Book>();
            private int nextId = 1;
            public int Count
            {
                get { return books.Count; }
            }
            public Book Add(string title, string author, Genre genre, int year, decimal price)
            {
                Book book = new Book(nextId, title, author, genre, year, price);
                nextId++;
                books.Add(book);
                return book;
            }

            public bool Remove(int id)
            {
                Book book = FindById(id);
                if (book == null)
                    return false;
                books.Remove(book);
                return true;
            }

            public Book FindById(int id)
            {
                return books.FirstOrDefault(b => b.Id == id);
            }

            public List<Book> GetAll()
            {
                return books.ToList();
            }

            public List<Book> FindByAuthor(string text)
            {
                return books.Where(b => b.Author.ToLower().Contains(text.ToLower()))).ToList();
            }

            public List<Book> FindByTitle(string text)
            {
                return books.Where(b => b.Title.ToLower().Contains(text.ToLower()))).ToList();
            }

            public List<Book> FindByGenre(Genre genre)
            {
                return books.Where(b => b.Genre == genre).ToList();
            }

            public List<Book> SortByTitle()
            {
                return books.OrderBy(b => b.Title).ToList();

            }

            public List<Book> SortByYear()
            {
                return books.OrderBy(b => b.Year).ToList();
            }

            public Book GetMostExpensive()
            {
                return books.OrderByDescending(b => b.Price).FirstOrDefault();
            }

            public Book GetCheapest()
            {
                return books.OrderBy(b => b.Price).FirstOrDefault();
            }

            public Dictionary<string, int> CountByAuthor()
            {
                return books.GroupBy(b => b.Author)
                            .ToDictionary(g => g.Key, g => g.Count());
            }
        }
        
    }
}
