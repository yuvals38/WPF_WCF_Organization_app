using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using Organization_WPF.Controller.DAL;
using Organization_WPF.Models;

namespace Organization_WPF.Controller.WCF
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Reentrant)]
    public class Service : IService
    {
        public static IServiceCallback Callback;
        public static Timer Timer;
        public static Timer returnT;
        DataTransfer ser = new DataTransfer();

        private static int _clientcount;
        public static int ClientCount
        {
            get { return _clientcount; }
            set { _clientcount = value;  }
        }

        public void ReceiveData(string data)
        {
            //update/insert db
            Console.WriteLine(data);
            DataTransfer deserialize = new DataTransfer();
            deserialize.DeserializeData(data);

            Controller.Instance.data.PersonList = Controller.Instance.PrepareData();
            
        }

        public event System.EventHandler CounterChanged;

        protected virtual  void OnCounterChanged()
        {
            if (CounterChanged != null) CounterChanged(this, EventArgs.Empty);
        }

        public void OpenSession()
        {
            Console.WriteLine("> Session opened at {0}", DateTime.Now);
            Callback = OperationContext.Current.GetCallbackChannel<IServiceCallback>();

            SendData();

            ClientCount++;

           
            //Timer = new Timer(1000);
            //Timer.Elapsed += OnTimerElapsed;
            //Timer.Enabled = true;

        }

        //void OnTimerElapsed()
        //{
            
        //    Callback.OnCallback(ser.Serializer());
        //}

        void SendData()
        {
            Callback.OnCallback(ser.Serializer());
        }

        public void SendPerson(Person person)
        {
            //send only single person!!
            Callback.SendPerson(ser.SerlializePerson(person));
        }

    }
}
