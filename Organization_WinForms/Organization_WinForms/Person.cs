using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_WinForms
{
    public class Person
    {
        public Person(int id, string name, Roles role)
        {
            Id = id;
            Name = name;
            PersonRoles = role;

        }

        public Person()
        {

        }

        private int _id;
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private Roles _personRoles;
        public Roles PersonRoles
        {
            get { return _personRoles; }
            set { _personRoles = value;}
        }

    }

    public enum Roles
    {
        CEO,
        COO,
        HR,
        Logistics,
        Finance,
        RandD,
        Quality,
        Sales,
        Marketing,
        IT
    }

}
