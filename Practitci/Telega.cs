using System.Collections.Generic;
using System.Linq;

namespace Practitci
{
    class Telega
    {
        private List<Book> items = new List<Book>();

        public void Add(Book book)
        {
            items.Add(book);
        }

        public void RemoveBook(int id)
        {
            items.RemoveAll(b => b.Id == id);
        }

        public List<Book> GetItems()
        {
            return items.ToList();
        }

        public decimal GetTotal()
        {
            return items.Sum(b => b.Price);
        }
    }
}