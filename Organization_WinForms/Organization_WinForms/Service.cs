using Newtonsoft.Json;
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Reflection;

namespace Organization_WinForms
{
    public class ServiceProvider
    {
        public static string xmlPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"PersonClientSide.xml");
        static ServiceProvider instance = null;
        static readonly object padlock = new object();
        XmlDocument doc = new XmlDocument();

        //singleton
        public static ServiceProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new ServiceProvider();
                        }
                    }
                }
                return instance;
            }
        }

        public ServiceProvider()
        {
            //connect to the service
            ServiceCallBack callback = new ServiceCallBack();
            InstanceContext instanceContext = new InstanceContext(callback);
            var client  = new Organization_Service.ServiceClient(instanceContext);
          
            _clientConnected = client;
        }

        private Organization_Service.ServiceClient _clientConnected;
        public Organization_Service.ServiceClient ClientConnected
        {
            get { return _clientConnected; }
            set { _clientConnected = value; }
        }


        public void SendData()
        {
            string xmlData = Serializer();
           
            ClientConnected.ReceiveData(xmlData);
        }

        public void UpdatePerson(Person updatePerson)
        {
            try
            {

            var docsave = XDocument.Load(xmlPath);
            var personToUpdate = docsave
                 .Element("ArrayOfPerson")
                 .Elements("Person")
                 .Where(e => e.Element("Id").Value == updatePerson.Id.ToString())
                 .Single();

            personToUpdate.Element("Name").Value = updatePerson.Name;
            personToUpdate.Element("PersonRoles").Value = updatePerson.PersonRoles.ToString();
           
            docsave.Save(xmlPath);
            }
            catch (Exception e)
            {

            }

        }

        public void AddRecord(Person person)
        {
            try
            {
                var docsave = XDocument.Load(xmlPath);

                XElement newPerson = new XElement("Person",
                new XElement("Id", person.Id),
                new XElement("Name", person.Name),
                new XElement("PersonRoles", person.PersonRoles));

                docsave.Element("ArrayOfPerson").Add(newPerson);
                docsave.Save(xmlPath);
            }
            catch (Exception e)
            {
                
            }

        }

        public void DeleteRecord(Person person)
        {
            try
            {
                var docsave = XDocument.Load(xmlPath);
                var deletePerson = docsave
                     .Element("ArrayOfPerson")
                     .Elements("Person")
                     .Where(e => e.Element("Id").Value == person.Id.ToString())
                     .Single();

                deletePerson.Remove();
                docsave.Save(xmlPath);
            }
            catch (Exception e)
            {
                
            }
        }

        public string Serializer()
        {
            string xml = ServiceProvider.xmlPath;
            string textXML = System.IO.File.ReadAllText(xml);
            XElement rootElement = XDocument.Parse(textXML).Root;

            string jsonText = JsonConvert.SerializeObject(rootElement);
            return jsonText;
        }

        public void DeserializeData(string data)
        {

            var personscollection = JsonConvert.DeserializeXmlNode(data);

            File.WriteAllText(xmlPath, personscollection.InnerXml);
        }

        public Person DeserializeDataPerson(string data)
        {

            Person person = JsonConvert.DeserializeObject<Person>(data);

            return person;
        }
    }
}
