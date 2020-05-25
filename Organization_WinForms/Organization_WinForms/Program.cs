using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Organization_WinForms
{
    static class Program
    {
       
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //connect to server
            ServiceProvider service = ServiceProvider.Instance;
            service.ClientConnected.OpenSession();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());


            ////connect to the service
            //ServiceCallBack callback = new ServiceCallBack();
            //InstanceContext instanceContext = new InstanceContext(callback);
            //var client = new Organization_Service.ServiceClient(instanceContext);
            //client.OpenSession();

            //ServiceProvider clientconnect = new ServiceProvider();//clientConnected = client;
            //clientconnect.ClientConnected = client;

        }
    }
}
