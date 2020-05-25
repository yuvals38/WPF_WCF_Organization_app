using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Organization_WPF.Models
{
    [Serializable]
    public class Person : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public Person(int id, string name , Roles role)
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
            set { _id = value; OnPropertyChanged("Id"); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("Name"); }
        }

        private Roles _personRoles;
        public Roles PersonRoles
        {
            get { return _personRoles; }
            set { _personRoles = value; OnPropertyChanged("PersonRoles"); }
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
