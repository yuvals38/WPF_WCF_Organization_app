using Newtonsoft.Json;
using Organization_WPF.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Organization_WPF.Controller.DAL
{
    public class DataTransfer
    {
        XmlDocument doc = new XmlDocument();

        public string Serializer()
        {
            string xml = Data.XmlPath;
            string textXML = System.IO.File.ReadAllText(xml);
            XElement rootElement = XDocument.Parse(textXML).Root;
           
            string jsonText = JsonConvert.SerializeObject(rootElement);
            return jsonText;
        }

        public void DeserializeData(string data)
        {

            var personscollection = JsonConvert.DeserializeXmlNode(data);

            File.WriteAllText(Data.XmlPath, personscollection.InnerXml);
        }

        public string SerlializePerson(Person person)
        {
            //var docsave = XDocument.Load(Data.XmlPath);
            //var sendPerson = docsave
            //     .Element("ArrayOfPerson")
            //     .Elements("Person")
            //     .Where(e => e.Element("Id").Value == person.Id.ToString())
            //     .Single();

            //sendPerson.Element("Id").Value = person.Id.ToString();
            //sendPerson.Element("Name").Value = person.Name;
            //sendPerson.Element("PersonRoles").Value = person.PersonRoles.ToString();

            string jsonText = JsonConvert.SerializeObject(person);
            return jsonText;

        }
    }
}
