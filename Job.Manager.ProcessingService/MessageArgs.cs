using System;

namespace Job.Manager.ProcessingService
{
    public class MessageArgs<T> : EventArgs
    {
        public T Message { get; set; }
        public MessageArgs(T message)
        {
            Message = message;
        }
    }
}
