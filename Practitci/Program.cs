using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Product> slovar = new List<Product>();

            void AddProduct(string name)
            {
                Console.WriteLine("Введите текст для добавления в словарь:");   
                name = Console.ReadLine();
                slovar.Add(new Product(name));
                
            }
            
        }
    }
}