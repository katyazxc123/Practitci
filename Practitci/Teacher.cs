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
        public Teacher(int id, string name, int age, string email) : base(id, name, age, email)
        {
            Kafedra = Kafedra
        }

        public override string Role
        {
            get { return "Преподаватель"; }
        }

        public overridde string Info()
        {
            return base.Info() + $", Кафедра: {Kafedra}";
        }



    }
}
