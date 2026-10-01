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
            List<String> slovar = new List<String>();
            slovar.Add("Вась");
            slovar.Add("как");
            slovar.Add("ты");

            void AddProduct()
            {
                Console.WriteLine("Введите текст для добавления в словарь:");
                string text = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(text))
                {
                    Console.WriteLine("Ошибка: введена пустая строка. Попробуйте снова.");
                    return;
                }
                if (text.Length < 100) 
                {
                    Console.WriteLine("Ошибка: введенная строка слишком короткая. Попробуйте снова.");
                    return;
                }

                string[] words = text.Split(' ');
                slovar.AddRange(words);  

                Console.WriteLine("Текущий словарь:");
                foreach (var item in slovar)
                {
                    Console.WriteLine(item);
                }
                Console.WriteLine(" ");
            }
            void KolvoSlov()
            {
                Console.WriteLine($"Количество элементов в словаре: {slovar.Count}");
                Console.WriteLine(" ");
            }
            void Minslovo()
            {
                int minLength = int.MaxValue;
                string minWord = "";

                foreach (var item in slovar)
                {
                    if (item.Length < minLength)
                    {
                        minLength = item.Length;
                        minWord = item;
                    }
                    
                }
                Console.WriteLine($"Самое короткое слово: {minWord}");
                Console.WriteLine(" ");
            }
            void KolvoPred()
            {

            }
            void Menu()
            {
                while (true)
                {
                    Console.WriteLine("Выберите действие:");
                    Console.WriteLine("1. Добавить текст в словарь");
                    Console.WriteLine("2. Посчитать количество элементов в словаре");
                    Console.WriteLine("3. Найти самое короткое слово в словаре");
                    Console.WriteLine("4. Выход");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            AddProduct();
                            break;
                        case "2":
                            Kolvo();
                            break;
                        case "3":
                            Minslovo();
                            break;
                        case "4":
                            return;
                        default:
                            Console.WriteLine("Некорректный выбор. Попробуйте снова.");
                            break;
                    }
                }
            }
            Menu();
        }
    }
}