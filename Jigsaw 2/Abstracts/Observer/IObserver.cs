namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Extends a class to be able to listen to Subject updates.
    /// </summary>
    public interface IObserver
    {
        void Update<T>(T message);
    }
}