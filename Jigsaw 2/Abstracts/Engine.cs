using System.Collections.Generic;

namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Superclass for engine-type classes, used for making calculations for the game.
    /// </summary>
    public abstract class Engine : ISubject
    {
        #region Private Fields

        private List<IObserver> observers;

        #endregion Private Fields

        #region Constructors

        public Engine()
        {
            observers = new List<IObserver>();
        }

        #endregion Constructors

        #region Public Methods

        public void Subscribe(IObserver o)
        {
            observers.Add(o);
        }

        public void Unsubscribe(IObserver o)
        {
            observers.Remove(o);
        }

        public void Broadcast<T>(T message)
        {
            foreach (IObserver o in observers)
                o.Update(message);
        }

        #endregion Public Methods
    }
}