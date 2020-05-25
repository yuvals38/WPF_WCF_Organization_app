using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Organization_WPF.Controller.DAL
{
    public class DataManagement : IDisposable
    {
        private static List<bool> UsedCounter = new List<bool>();
        private static object Lock = new object();

        public int ID { get; private set; }

        public DataManagement()
        {

            lock (Lock)
            {
                int nextIndex = GetAvailableIndex();
                if (nextIndex == -1)
                {
                    nextIndex = UsedCounter.Count;
                    UsedCounter.Add(true);
                }

                ID = nextIndex;
            }
        }

        public void Dispose()
        {
            lock (Lock)
            {
                UsedCounter[ID] = false;
            }
        }


        private int GetAvailableIndex()
        {
            for (int i = 0; i < UsedCounter.Count; i++)
            {
                if (UsedCounter[i] == false)
                {
                    return i;
                }
            }
            
            return -1;
        }
    }
}
