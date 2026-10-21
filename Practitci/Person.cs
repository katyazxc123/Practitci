using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    abstract class Person
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public int Age { get; private set; }
        public string Email { get; private set; }

        protected Person(int id, string name, int age, string email)
        {
            Id = id;
            Name = name;
            Age = age;
            Email = email;
        }

        public abstract string Role { get; }
        public virtual string Info()
        {
            return $"Id: {Id}, Имя: {Name}, Возраст: {Age}, Email: {Email}, Роль: {Role}";
        }

    }
}
