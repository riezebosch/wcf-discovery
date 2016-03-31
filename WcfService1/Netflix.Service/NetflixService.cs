using Netflix.DataContracts;
using Netflix.DataModel;
using Netflix.ServiceContract;
using System;
using System.ServiceModel;
using System.Threading;

namespace Netflix.Service
{
    [ServiceBehavior(
        InstanceContextMode = InstanceContextMode.PerSession,
        ConcurrencyMode = ConcurrencyMode.Single,
        IncludeExceptionDetailInFaults = true)]
    public class NetflixService : INetflixService
    {
        private Guid _data;

        public Guid GetData()
        {
            return _data;
        }

        public void Search()
        {
            var callback = OperationContext
                .Current
                .GetCallbackChannel<INetflixCallback>();

            Thread.Sleep(2000);
            callback.Result(new[] { new Movie { Name = "Intouchables" } });

            Thread.Sleep(500);
            callback.Result(new[] { new Movie { Name = "Requiem for a Dream" } });

            Thread.Sleep(1500);

            var mf = new Serie { Name = "Modern Family" };
            mf.Episodes = new[] 
            {
                new Episode { Name = "Disneyland", Serie = mf },
                new Episode { Name = "Planes, Trains & Cars", Serie = mf },
                new Episode { Name = "The Last Walt", Serie = mf },
            };
            callback.Result(new[] { mf });

            callback.Done();
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

        public Title[] Top10()
        {
            var episode = new Episode { };
            var serie = new Serie
            {
                Episodes = new[] { episode }
            };
            episode.Serie = serie;

            return  new[] {
                    new Title { },
                    new Title { },
                    new Title { },
                    new Title { },
                    new Title { },
                    new Title { },
                    new Title { },
                    new Title { },
                    serie,
                    new Movie { }
                };
        }

        [OperationBehavior(TransactionScopeRequired = true, 
            TransactionAutoComplete = false)]
        public void Transaction(Guid data)
        {
            using (var context = new NetflixModel())
            {
                context.People.Add(new Person { Name = data.ToString() });
                context.SaveChanges();
            }
        }

        [OperationBehavior(TransactionScopeRequired = true, TransactionAutoComplete = false)]
        public void TransactionComplete()
        {
            OperationContext.Current.SetTransactionComplete();
        }
    }
}