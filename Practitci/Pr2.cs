using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Pr2
{
    internal class Pr2
    {
        public static void Start()
        {
            List<Product> products = new List<Product>();
            products.Add(new Product("Телевизор", 50000, 10, ProductCategory.Electronics));
            products.Add(new Product("Тишка", 3500, 5, ProductCategory.Clothing));
            products.Add(new Product("Хлеб", 50, 100, ProductCategory.Food));
            products.Add(new Product("Телефон", 30000, 15, ProductCategory.Electronics));
            products.Add(new Product("Джинсы", 2000, 20, ProductCategory.Clothing));

            void AddProduct()
            {

                Console.Write("Введите название товара: ");
                string name = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(name))
                {
                    Console.WriteLine("Ошибка: название товара не может быть пустым.");
                    return;
                }

                name = name.Trim();

                Console.Write("Введите цену товара: ");
                if (!double.TryParse(Console.ReadLine(), out double price) ||
                    double.IsNaN(price) ||
                    double.IsInfinity(price))
                {
                    Console.WriteLine("Ошибка: введено некорректное значение цены.");
                    return;
                }

                if (price < 0)
                {
                    Console.WriteLine("Ошибка: цена не может быть отрицательной.");
                    return;
                }

                Console.Write("Введите количество товара: ");
                if (!int.TryParse(Console.ReadLine(), out int quantity))
                {
                    Console.WriteLine("Ошибка: количество должно быть целым числом.");
                    return;
                }

                if (quantity < 0)
                {
                    Console.WriteLine("Ошибка: количество не может быть отрицательным.");
                    return;
                }

                Console.Write(
                    "Введите категорию товара (0 — Техника, 1 — Одежда, 2 — Еда): ");

                if (!int.TryParse(Console.ReadLine(), out int category) ||
                    category < 0 ||
                    category > 2)
                {
                    Console.WriteLine("Ошибка: категория должна быть числом от 0 до 2.");
                    return;
                }

                Product product = new Product(name, price, quantity, (ProductCategory)category);
                products.Add(product);
                Info();
            }
            void Info()
            {
                Console.WriteLine("Список товаров: ");
                foreach (Product product in products)
                {
                    Console.WriteLine($"ID: {product.Id}, Название: {product.Name}, Цена: {product.Price}, Количество: {product.Quantity}, Категория: {product.Category}");
                }

            }
            void deleteProduct()
            {
                Console.WriteLine("Введите ID товара, который хотите удалить: ");
                int id = Convert.ToInt16(Console.ReadLine());
                Product productToRemove = products.FirstOrDefault(p => p.Id == id);
                if (productToRemove != null)
                {
                    products.Remove(productToRemove);
                    Console.WriteLine($"Товар с ID {id} удален.");
                }
                else
                {
                    Console.WriteLine($"Товар с ID {id} не найден.");
                }
                Info();
            }
            void OrderSupply()
            {
                Console.Write("Введите ID товара: ");

                int id;
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Ошибка: ID должен быть целым числом.");
                    return;
                }

                Product product = products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    Console.WriteLine("Товар с таким ID не найден.");
                    return;
                }

                Console.Write("Введите количество поставляемого товара: ");

                int amount;
                if (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                {
                    Console.WriteLine("Ошибка: количество должно быть больше нуля.");
                    return;
                }

                if (product.Quantity > int.MaxValue - amount)
                {
                    Console.WriteLine("Ошибка: указано слишком большое количество.");
                    return;
                }

                product.Quantity += amount;

                Console.WriteLine("Поставка успешно оформлена.");
                Console.WriteLine("Новое количество: " + product.Quantity);
            }
            void SellProduct()
            {
                Console.Write("Введите ID товара: ");

                int id;
                if (!int.TryParse(Console.ReadLine(), out id))
                {
                    Console.WriteLine("Ошибка: ID должен быть целым числом.");
                    return;
                }

                Product product = products.FirstOrDefault(p => p.Id == id);

                if (product == null)
                {
                    Console.WriteLine("Товар с таким ID не найден.");
                    return;
                }

                Console.Write("Введите количество для продажи: ");

                int amount;
                if (!int.TryParse(Console.ReadLine(), out amount) || amount <= 0)
                {
                    Console.WriteLine("Ошибка: количество должно быть больше нуля.");
                    return;
                }

                if (amount > product.Quantity)
                {
                    Console.WriteLine(
                        "Недостаточно товара. Доступно: " + product.Quantity);
                    return;
                }

                product.Quantity -= amount;

                Console.WriteLine("Товар успешно продан.");
                Console.WriteLine("Остаток: " + product.Quantity);
            }
            void SearchProduct()
            {
                Console.WriteLine("1 — Поиск по ID");
                Console.WriteLine("2 — Поиск по названию");
                Console.WriteLine("3 — Поиск по категории");
                Console.Write("Выберите способ поиска: ");

                string command = Console.ReadLine();
                List<Product> foundProducts = new List<Product>();

                switch (command)
                {
                    case "1":
                        Console.Write("Введите ID: ");

                        int id;
                        if (!int.TryParse(Console.ReadLine(), out id))
                        {
                            Console.WriteLine("Ошибка: некорректный ID.");
                            return;
                        }

                        Product product = products.FirstOrDefault(p => p.Id == id);

                        if (product != null)
                        {
                            foundProducts.Add(product);
                        }

                        break;

                    case "2":
                        Console.Write("Введите название или его часть: ");
                        string name = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Название не может быть пустым.");
                            return;
                        }

                        foundProducts = products
                            .Where(p => p.Name.IndexOf(
                                name.Trim(),
                                StringComparison.OrdinalIgnoreCase) >= 0)
                            .ToList();

                        break;

                    case "3":
                        Console.WriteLine("0 — Техника");
                        Console.WriteLine("1 — Одежда");
                        Console.WriteLine("2 — Еда");
                        Console.Write("Введите категорию: ");

                        int category;
                        if (!int.TryParse(Console.ReadLine(), out category) ||
                            !Enum.IsDefined(typeof(ProductCategory), category))
                        {
                            Console.WriteLine("Ошибка: такой категории нет.");
                            return;
                        }

                        foundProducts = products
                            .Where(p => p.Category == (ProductCategory)category)
                            .ToList();

                        break;

                    default:
                        Console.WriteLine("Такой команды нет.");
                        return;
                }

                if (foundProducts.Count == 0)
                {
                    Console.WriteLine("Товары не найдены.");
                    return;
                }

                foreach (Product foundProduct in foundProducts)
                {
                    Console.WriteLine("жжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжжж");
                    Console.WriteLine($"ID:{foundProduct.Id}");
                    Console.WriteLine($"Название: {foundProduct.Name}");
                    Console.WriteLine($"Цена: {foundProduct.Price}");
                    Console.WriteLine($"Количество: {foundProduct.Quantity}");
                    Console.WriteLine($"Категория: {foundProduct.Category}");
                }

            }
            void Menu()
            {
                while (true)
                {
                    Console.WriteLine("1 — Добавить товар");
                    Console.WriteLine("2 — Удалить товар");
                    Console.WriteLine("3 — Оформить поставку");
                    Console.WriteLine("4 — Продать товар");
                    Console.WriteLine("5 — Поиск товара");
                    Console.WriteLine("6 — Вывести список товаров");
                    Console.WriteLine("0 — Выход");
                    Console.Write("Выберите команду: ");
                    string command = Console.ReadLine();
                    switch (command)
                    {
                        case "1":
                            AddProduct();
                            break;
                        case "2":
                            deleteProduct();
                            break;
                        case "3":
                            OrderSupply();
                            break;
                        case "4":
                            SellProduct();
                            break;
                        case "5":
                            SearchProduct();
                            break;
                        case "6":
                            Info();
                            break;
                        case "0":
                            return;
                        default:
                            Console.WriteLine("Такой команды нет.");
                            break;
                    }
                }
            }
            Menu();
        }

    }
            
}
    

    enum ProductCategory
    {
        Electronics = 0,
        Clothing = 1,
        Food = 2
    }
    class Product
    {
        private static int count = 1;
        public int Id { get; private set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
        public bool Exists 
        {
            get { return Quantity > 0; }
        }
        public ProductCategory Category { get; set; }

        public Product(string name, double price, int quantity, ProductCategory category)
        {
            Id = count++;
            Name = name;
            Price = price;
            Quantity = quantity;
            Category = category;
        }
       
        
    }


