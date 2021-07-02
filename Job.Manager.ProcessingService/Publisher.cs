using Job.Manager.ProcessingService.Interface;
using System;
using System.Threading.Tasks;

namespace Job.Manager.ProcessingService
{
    public class Publisher<T> : IPublisher<T>
    {
        public event EventHandler<MessageArgs<T>> DataPublisher;

        public async Task OnDataPublisher(MessageArgs<T> args)
        {
            var handler = DataPublisher;
            if (handler != null)
                handler(this, args);
        }


        public async Task PublishData(T data)
        {
            MessageArgs<T> message = (MessageArgs<T>)Activator.CreateInstance(typeof(MessageArgs<T>), new object[] { data });
            await OnDataPublisher(message);
        }
    }
}
