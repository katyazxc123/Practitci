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
            var Spisok = new Dictionary<string, int>();
            int Kol = 0;
            Console.WriteLine("Введите кол-во операций: ");
            Kol = Convert.ToInt16(Console.ReadLine());
            if (Kol >= 2 && Kol <= 40)
            {
                Console.WriteLine("Введите по шаблону ( Название услуги или товара; Количество денег(через enter)");

                for (int i = 0; i < Kol; i++)
                {
                    Spisok.Add(Console.ReadLine(), Convert.ToInt16(Console.ReadLine()));
                }

            }
            else
            {
                Console.WriteLine("Вы неправильно ввели данные!");
            }

            void Stat()
            {
                Sr();
                Max();
                Min();
                Sum();
                Menu();

            }

            void Sr()
            {
                double b = Spisok.Values.Average();
                Console.WriteLine($"Среднее значение: {b}");

            }

            void Max()
            {
                int max = Spisok.Values.Max();
                Console.WriteLine($"Максимальное значение: {max}");

            }

            void Min()

            {
                int min = Spisok.Values.Min();
                Console.WriteLine($"Минимальное значение: {min}");

            }

            void Sum()
            {
                int sum = Spisok.Values.Sum();
                Console.WriteLine($"Сумма всех значений: {sum}");

            }

            void Vivod()
            {
                foreach (var paciki in Spisok)
                {
                    Console.WriteLine($"Товар: {paciki.Key}, Потрачено средств: {paciki.Value}");
                }
                Menu();
            }

            void Sort()
            {
                List<KeyValuePair<string, int>> list = Spisok.ToList();

                for (int i = 0; i < list.Count - 1; i++)
                {
                    for (int j = 0; j < list.Count - 1 - i; j++)
                    {
                        if (list[j].Value > list[j + 1].Value)
                        {
                            var temp = list[j];
                            list[j] = list[j + 1];
                            list[j + 1] = temp;
                        }
                    }
                }

                Console.WriteLine("Сортировка по цене:");

                foreach (var item in list)
                {
                    Console.WriteLine($"{item.Key} - {item.Value}");
                }
                Menu();
            }

            void ConvertDeneg()
            {
                Console.WriteLine("Введите курс валюты, которую хотите получить: ");
                double Kurs = Convert.ToDouble(Console.ReadLine());
                double vivod = Spisok.Values.Max() / Kurs;
                Console.WriteLine($"Вывод: {vivod} ");
                Menu();
            }

            void Poisk()
            {
                Console.WriteLine("Введите название операции: ");
                string vvod = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(vvod))
                {
                    Console.WriteLine("Вы ничего не ввели.");
                    Menu();
                    return;
                }

                bool naideno = false;

                foreach (var item in Spisok)
                {
                    if (item.Key.IndexOf(vvod, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        Console.WriteLine($"Товар: {item.Key}, Потрачено средств: {item.Value}");
                        naideno = true;
                    }
                }

                if (!naideno)
                {
                    Console.WriteLine("Совпадений не найдено.");
                }

                Menu();

            }



            void Menu()
            {
                Console.WriteLine("Выберите действие: 0 - Вывод списка, 1 - Вывод статистики, 2 - Сортировка по цене,  3 - Конвертация рубля, 4 - Поиск по названию, 5 - Выход. ");
                int b = Convert.ToInt16(Console.ReadLine());
                if (b > 6)
                {
                    Console.WriteLine("Error!");
                }
                else
                {
                    switch (b)
                    {
                        case 0:
                            Vivod();
                            break;
                        case 1:
                            Stat();
                            break;
                        case 2:
                            Sort();
                            break;
                        case 3:
                            ConvertDeneg();
                            break;
                        case 4:
                            Poisk();
                            break;
                        case 5:
                            Console.WriteLine("Досвидос!");
                            break;
                    }
                }
            }
            Menu();
        }
    }
}
