using System;
using System.Threading.Tasks;

namespace Job.Manager.ProcessingService.Interface
{
    public interface IPublisher<T>
    {
        event EventHandler<MessageArgs<T>> DataPublisher;
        Task OnDataPublisher(MessageArgs<T> args);
        Task PublishData(T data);
    }
}
