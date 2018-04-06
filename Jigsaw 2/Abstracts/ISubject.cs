namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Extends a class to be able to send a message to all its Observers.
    /// </summary>
    public interface ISubject
    {
        void Subscribe(IObserver o);

        void Unsubscribe(IObserver o);

        void Broadcast<T>(T message);
    }
}