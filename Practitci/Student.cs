
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Practitci
{
    internal class Student : Person
    {
        public string Group { get ; private set; }
        public Student(int id, string name, int age, string email, string group) : base(id, name, age, email)
        {
            Group = group;
        }

        public override string Role
        {
            get { return "Студент"; }
        }

        public override string Info()
        {
            return base.Info() + $", Group: {Group}";
        }


    }
}
