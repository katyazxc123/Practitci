using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    internal class Teacher : Person
    {
        public string Kafedra { get; private set; }
        public Teacher(int id, string name, int age, string email, string kafedra) : base(id, name, age, email)
        {
            Kafedra = kafedra;
        }

        public override string Role
        {
            get { return "Преподаватель"; }
        }

        public override string Info()
        {
            return base.Info() + $", Кафедра: {Kafedra}";
        }



    }
}
