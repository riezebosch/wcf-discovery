using Netflix.ServiceContract;
using System;

namespace Netflix.Service
{
    public class NetflixService : INetflixService
    {
        public string[] Top10()
        {
            return  new string[10];
        }
    }
}