namespace Netflix.Service
{
    public class MessageInspectorLogger
    {
        public MessageInspectorLogger()
        {
        }

        public System.Action<string> Log { get; set; }
    }
}