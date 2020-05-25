using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Organization_WPF.Controller.WCF
{
    public interface IServiceCallback
    {
        [OperationContract]
        void OnCallback(string msg);

        [OperationContract]
        void SendPerson(string msg);
    }
}
