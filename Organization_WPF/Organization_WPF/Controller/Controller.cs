using Organization_WPF.Controller.DAL;
using Organization_WPF.Controller.WCF;
using Organization_WPF.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Organization_WPF.Controller
{
    public class Controller
    {
        //singleton members
        static Controller instance = null;
        static readonly object padlock = new object();
        public Data data;

        private int _clientc;
        public int ClientC
        {
            get { return _clientc; }
            set { _clientc = value;}
        }
        
        //singleton
        public static Controller Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new Controller();
                        }
                    }
                }
                return instance;
            }
        }

        public Controller()
        {
            //start wcf
            var host = new ServiceHost(typeof(Service));
            host.Open();
 
            //db instance
            data = new Data();
            if (data.PersonList == null)
                PrepareData();
        }

       
        public ObservableCollection<Person> PrepareData()
        {
            // if (data == null)
            //data = new Data();
            data.xmlGet();

            PersonService ps = new PersonService();
            ps.GetAll();
            //PersonService.PersonList = data.PersonList;
            return data.PersonList;

            
        }

        public void SendData(Person person)
        {
            Service sendD = new Service();
            sendD.SendPerson(person);
        }
    }
}
