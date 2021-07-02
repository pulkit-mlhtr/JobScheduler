using Job.Manager.ProcessingService.Interface;

namespace Job.Manager.ProcessingService
{
    public class Subscriber<T>
    {
        public IPublisher<T> Publisher { get; private set; }
        public Subscriber(IPublisher<T> publisher)
        {
            Publisher = publisher;
        }
    }
}
