using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace Netflix.Service
{
    public class MessageInspectorLogger : 
        IDispatchMessageInspector
    {
        public Action<string> Log { get; set; }

        public object AfterReceiveRequest(ref Message request, IClientChannel channel, InstanceContext instanceContext)
        {
            if (Log != null)
            {
                Log(request.ToString());
            }

            return null;
        }

        public void BeforeSendReply(ref Message reply, object correlationState)
        {
            if (Log != null)
            {
                Log(reply.ToString());
            }
        }
    }
}