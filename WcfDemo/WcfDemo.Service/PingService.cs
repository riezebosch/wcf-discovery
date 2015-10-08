using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Threading;
using System.Web;
using WcfDemo.Contracts;
using WcfDemo.DataModel;

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

        public CustomReturnEnvelope OperationWithCustomEnvelope(CustomEnvelope envelope)
        {
            return new CustomReturnEnvelope();
        }

        public void Ping()
        {
        }

        public void Put(Guid data)
        {
            _data = data;
        }

        public void Save(Persoon p)
        {
        }

        public void Slow()
        {
            Thread.Sleep(10 * 1000);
        }

        public void StartProcessing()
        {
            var callback = OperationContext
                .Current
                .GetCallbackChannel<IServiceCallback>();

            for (int i = 0; i <= 100; i += 5)
            {
                callback.Update(i);
                Thread.Sleep(100);
            }

            callback.Ready();
        }

        public void Throw()
        {
            throw new InvalidOperationException("deze zou niet aangeroepen moeten mogen worden.");
        }

        public void ThrowCustomException()
        {
            throw new FaultException<CustomFaultDetails>(
                new CustomFaultDetails
                {
                    Bericht = "dat mag dus niet"
                });
        }

        [OperationBehavior(TransactionScopeRequired = true)]
        public void TransactionSupported(int id, string name)
        {
            using (var context = new SchoolContext())
            {
                var p = context.People.Find(id);
                p.FirstName = name;

                context.SaveChanges();
            }

        }
    }
}