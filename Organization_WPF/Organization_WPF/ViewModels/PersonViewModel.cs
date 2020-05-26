using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Organization_WPF.Models;
using System.Collections.ObjectModel;
using Organization_WPF.Commands;
using Organization_WPF.Controller.WCF;

namespace Organization_WPF.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        PersonService PersonService;
       
        public PersonViewModel()
        {
            //create the controller instance (singleton)
            Controller.Controller manager = Controller.Controller.Instance;

            //observablec.
            //_personCollection = Controller.Controller.Instance.data.PersonList;
            
            PersonService = new PersonService();
            _personCollection = PersonService.PersonList;
            _currentPerson = new Person();

            PersonService.PersonServiceCollectionChanged += PSCollectionChanged;

            _saveCommand = new RelayCommand(Save);
            _searchCommand = new RelayCommand(Search);
            _updateCommand = new RelayCommand(Update);
            _deleteCommand = new RelayCommand(Delete);
            _sendCommand = new RelayCommand(SendData);
            //LoadData();
        }


        public Person SelectedPerson { get; set; }

        #region INotifypropertyChange implementation
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private  ObservableCollection<Person> _personCollection;

        public  ObservableCollection<Person> PersonCollection
        {
            get { return _personCollection; }
            set { _personCollection = value; OnPropertyChanged("PersonCollection");  }
        }

        public void PSCollectionChanged(object sender, EventArgs e)
        {
            //refresh collection
            PersonCollection = PersonService.PersonList;
            Message = "Data recieved from Client";
        }

        private Person _currentPerson;

        public Person CurrentPerson
        {
            get { return _currentPerson; }
            set { _currentPerson = value; OnPropertyChanged("CurrentPerson"); }
        }

        private int _clientCounter;
        public int ClientCounter
        {
            get { return Service.ClientCount; }
            set { _clientCounter = value; OnPropertyChanged("ClientCounter"); }
        }


        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; OnPropertyChanged("Message"); }
        }

        #region Save/Add
        private RelayCommand _saveCommand;

        public RelayCommand SaveCommand
        {
            get { return _saveCommand; }
        }

        public void Save()
        {
            try
            {
                PersonCollection.Add(CurrentPerson);
                PersonService.Add(CurrentPerson);
                

                Message = "Person saved to db";
            }
            catch (Exception e)
            {
                PersonCollection.Remove(CurrentPerson);
                Message = e.Message;
            }
        }
        #endregion

        #region Search
        private RelayCommand _searchCommand;
        public RelayCommand SearchCommand
        {
            get { return _searchCommand; }
        }

        public void Search()
        {
            try
            {
                var searchPerson = PersonService.Search(CurrentPerson.Id);
                if (searchPerson != null)
                {
                    CurrentPerson.Name = searchPerson.Name;
                    CurrentPerson.PersonRoles = searchPerson.PersonRoles;
                }
                else
                {
                    Message = "Person does not exist";
                }
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        #endregion

        #region Update
        private RelayCommand _updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return _updateCommand; }
        }

        public void Update()
        {
            try
            {
               // PersonCollection.Add(CurrentPerson);
                PersonService.Update(CurrentPerson);

               
                Message = "Person updated in db";
            }
            catch (Exception e)
            {
               Message = e.Message;
            }
        }
        #endregion

        #region Delete
        private RelayCommand _deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return _deleteCommand; }
        }

        public void Delete()
        {
            try
            {
                // PersonCollection.Add(CurrentPerson);
                PersonService.Delete(CurrentPerson.Id);


                Message = "Person has been removed from the db";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        #endregion

        #region Send
        private RelayCommand _sendCommand;

        public RelayCommand SendCommand
        {
            get { return _sendCommand; }
        }

        public void SendData()
        {
            try
            {
                // PersonCollection.Add(CurrentPerson);
                if(SelectedPerson != null)
                    PersonService.Send(SelectedPerson);

                //_personCollection = PersonService.PersonList;

                Message = "Person data sent to client";
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
        }
        #endregion
    }
}
