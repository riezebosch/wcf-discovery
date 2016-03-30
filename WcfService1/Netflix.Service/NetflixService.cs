using Netflix.DataContracts;
using Netflix.ServiceContract;
using System;
using System.ServiceModel;
using System.Threading;

namespace Netflix.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall,
        ConcurrencyMode = ConcurrencyMode.Single)]
    public class NetflixService : INetflixService
    {
        private Guid _data;

        public Guid GetData()
        {
            return _data;
        }

        public void SetState(Guid data)
        {
            _data = data;
        }

        public void Slow()
        {
            Thread.Sleep(5000);
        }

        public void Throw()
        {
            throw new FaultException<NetflixFault>(new NetflixFault(), "oeioeioei");
        }

        public string[] Top10()
        {
            return  new string[10];
        }
    }
}