using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    internal class Telega
    {
        private List<Book> items = new List<Book>();

        public void AddItem(Book book)
        {
            items.Add(item);
        }

        public void RemoveItem(int id)
        {
            items.RemoveAll(b=>b.Id == id);
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
