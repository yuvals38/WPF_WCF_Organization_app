using Organization_WPF.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace Organization_WPF.Controller.DAL
{
    public class Data : ObservableCollection<Person> , INotifyPropertyChanged
    {

        public static string  XmlPath =  Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"Persons.xml");
        private ObservableCollection<Person> _personList;
        public ObservableCollection<Person> PersonList
        {
            get
            {
                return _personList;
            }
            set
            {
                if (_personList == null)
                    return;
                //notify prop change
                _personList = value; OnPropertyChanged("PersonList");
            
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
       // DataManagement dm = new DataManagement();
        public Data() 
        {
            
            xmlGet();
           
        }

        void xmlPrep(ObservableCollection<Person> col)
        {
            //first time
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
            using (StreamWriter wr = new StreamWriter(XmlPath))
            {
                xs.Serialize(wr, col);
                
            }
        }

        public void xmlGet()
        {
            XmlSerializer xs = new XmlSerializer(typeof(ObservableCollection<Person>));
            using (StreamReader wr = new StreamReader(XmlPath))
            {
                _personList = xs.Deserialize(wr) as ObservableCollection<Person>;

            }

          

        }

        public void AddRecord(Person person)
        {
            var docsave =  XDocument.Load(XmlPath);
           
            XElement newPerson = new XElement("Person",
            new XElement("Id",person.Id),
            new XElement("Name", person.Name),
            new XElement("PersonRoles", person.PersonRoles));
         
            docsave.Element("ArrayOfPerson").Add(newPerson);
            docsave.Save(XmlPath);
           
        }

        public void Update(Person person)
        {
            
            var docsave = XDocument.Load(XmlPath);
            var updatePerson = docsave
                 .Element("ArrayOfPerson")
                 .Elements("Person")
                 .Where(e => e.Element("Id").Value == person.Id.ToString())
                 .Single();

            updatePerson.Element("Name").Value = person.Name;
            updatePerson.Element("PersonRoles").Value = person.PersonRoles.ToString();

            docsave.Save(XmlPath);

        }

        public void Delete(Person person)
        {

            var docsave = XDocument.Load(XmlPath);
            var deletePerson = docsave
                 .Element("ArrayOfPerson")
                 .Elements("Person")
                 .Where(e => e.Element("Id").Value == person.Id.ToString())
                 .Single();

            deletePerson.Remove();
           
            docsave.Save(XmlPath);

        }
    }
}
