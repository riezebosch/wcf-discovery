using Netflix.ServiceContract;
using System;
using System.ServiceModel;

namespace Netflix.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
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

        public string[] Top10()
        {
            return  new string[10];
        }
    }
}