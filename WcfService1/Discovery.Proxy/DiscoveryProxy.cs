using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Discovery;
using System.Threading.Tasks;

namespace Discovery.Proxy
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class DiscoveryProxy : System.ServiceModel.Discovery.DiscoveryProxy
    {
        public IList<string> Services { get; } = new List<string>();

        protected override IAsyncResult OnBeginFind(FindRequestContext findRequestContext, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override IAsyncResult OnBeginOfflineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override IAsyncResult OnBeginOnlineAnnouncement(DiscoveryMessageSequence messageSequence, EndpointDiscoveryMetadata endpointDiscoveryMetadata, AsyncCallback callback, object state)
        {
            return Task.Run(() => Services.Add(endpointDiscoveryMetadata.Address.Uri.AbsolutePath)).AsApm(callback, state);
        }

        protected override IAsyncResult OnBeginResolve(ResolveCriteria resolveCriteria, AsyncCallback callback, object state)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndFind(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndOfflineAnnouncement(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

        protected override void OnEndOnlineAnnouncement(IAsyncResult result)
        {
            
        }

        protected override EndpointDiscoveryMetadata OnEndResolve(IAsyncResult result)
        {
            throw new NotImplementedException();
        }

       
    }
}