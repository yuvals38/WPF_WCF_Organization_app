using Organization_WinForms.Organization_Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace Organization_WinForms
{
    public class ServiceCallBack : IServiceCallback
    {
      
        public void OnCallback(string msg)
        {
            Console.WriteLine("> Received callback at {0} -- {1}", DateTime.Now, msg);
            ServiceProvider.Instance.DeserializeData(msg);
        }

        public void SendPerson(string msg)
        {
            var person = ServiceProvider.Instance.DeserializeDataPerson(msg);

            //populate textboxes
            //Form1 updatePerson = new Form1();
            (System.Windows.Forms.Application.OpenForms["Form1"] as Form1).UpdatePersonDetails(person.Id.ToString(), person.Name, person.PersonRoles);
            //updatePerson.UpdatePersonDetails(person.Id.ToString(), person.Name, person.PersonRoles.ToString());
        }
    }
}
