using System;
using System.Collections.ObjectModel;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Linq;
using System.ServiceModel.Dispatcher;

namespace Netflix.Service
{
    public class MessageInspectorLoggerBehavior : IServiceBehavior
    {
        private MessageInspectorLogger inspector;

        public MessageInspectorLoggerBehavior(MessageInspectorLogger inspector)
        {
            this.inspector = inspector;
        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        public void ApplyDispatchBehavior(
            ServiceDescription serviceDescription, 
            ServiceHostBase serviceHostBase)
        {
            foreach (var cd in serviceHostBase.ChannelDispatchers.Cast<ChannelDispatcher>())
            {
                foreach (var ep in cd.Endpoints)
                {
                    ep.DispatchRuntime.MessageInspectors.Add(inspector);
                }
            }
        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {
        }
    }
}