using Organization_WPF.Controller.DAL;
using Organization_WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_WPF.Models
{
    public class PersonService : ObservableCollection<Person>, INotifyPropertyChanged
    {

        // event colection changed
        public event EventHandler PersonServiceCollectionChanged;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        //event subscribe to controller
        private  ObservableCollection<Person> _personsList;
        public  ObservableCollection<Person> PersonList
        {
            get { return _personsList; }
            set { _personsList = value; OnPropertyChanged("PersonList");  }
        }

        public PersonService()
        {
            Controller.Controller.Instance.CollectionChanged += PersonCollectionChanged;
            _personsList = Controller.Controller.Instance.data.PersonList;
        }

        public void PersonCollectionChanged(object sender, EventArgs e)
        {
             //refresh collection
            PersonList = Controller.Controller.Instance.data.PersonList;
            PersonServiceCollectionChanged(this, null);
        }

        public ObservableCollection<Person> GetAll()
        {
            //PersonViewModel pvm = new PersonViewModel();
            //pvm.PersonCollection = _personsList;
            return _personsList;
        }

        public bool Add(Person newPerson)
        {
            //_personsList.Add(newPerson);
            //send add action to DAL
            Controller.Controller.Instance.data.AddRecord(newPerson);
            
            return true;
        }

        public bool Update(Person persontoUpdate)
        {
            bool isUpdated = false;
            var personUpdate = _personsList.FirstOrDefault(x => x.Id == persontoUpdate.Id);

            if (personUpdate != null)
            {
                personUpdate.Name = persontoUpdate.Name;
                personUpdate.PersonRoles = persontoUpdate.PersonRoles;
                //send update to DAL
                Controller.Controller.Instance.data.Update(persontoUpdate);
            }

            return isUpdated;
        }

        public bool Delete(int id)
        {
            bool IsDeleted = false;
            var personToDelete = _personsList.FirstOrDefault(x => x.Id == id);

            if (personToDelete != null)
                _personsList.Remove(personToDelete);

            //send delete to DAL
            Controller.Controller.Instance.data.Delete(personToDelete);

            return IsDeleted;
        }

        public Person Search(int id)
        {
            var personFound = _personsList.FirstOrDefault(x => x.Id == id);
            return personFound;
        }

        public bool Send(Person persontoSend)
        {
            bool isSent = false;
            var personSent = _personsList.FirstOrDefault(x => x.Id == persontoSend.Id);

            if (personSent != null)
            {
                //send update to DAL
                Controller.Controller.Instance.SendData(personSent);
            }

            return isSent;
        }
    }
}
