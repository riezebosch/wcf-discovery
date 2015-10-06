using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using WcfDemo.Contracts;


namespace WcfDemo.Service
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall, IncludeExceptionDetailInFaults = true)]
    public class PingService : IService
    {
        private Guid _data;

        public Guid Get()
        {
            return _data;
        }

        public Dataz GiveMeAllTheDataz(Data data)
        {
            return new Dataz
            {
                Ids = Enumerable.Repeat(0, 10)
                    .Select(i => new Data { ZId = i })
                    .ToList()
            };
        }

        public string Hello(string input)
        {
            return $"Hello {input}";
        }

        public void Ping()
        {
        }

        public void Put(Guid data)
        {
            _data = data;
        }

        public void Throw()
        {
            throw new InvalidOperationException("deze zou niet aangeroepen moeten mogen worden.");
        }
    }
}