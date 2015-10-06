using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfDemo.Contracts;


namespace WcfDemo.Service
{
    public class PingService : IService
    {
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
    }
}