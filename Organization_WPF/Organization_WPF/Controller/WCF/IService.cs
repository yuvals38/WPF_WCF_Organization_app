using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Organization_WPF.Controller.WCF
{
    [ServiceContract(CallbackContract = typeof(IServiceCallback))]
    public interface IService
    {
        [OperationContract]
        void OpenSession();

        [OperationContract]
        void ReceiveData(string data);
    }
}
