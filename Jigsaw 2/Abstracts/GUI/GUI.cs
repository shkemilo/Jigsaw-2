namespace Jigsaw_2.Abstracts
{
    /// <summary>
    /// Superclass for GUI elements.
    /// </summary>
    public abstract class GUIElement : IObserver
    {
        #region Public Abstract Methods

        /// <summary> Shows the GUI element's message to display. </summary>
        public abstract void Show();

        /// <summary> Updates the GUI element of a change of it's message to display. </summary>
        public abstract void Update<T>(T message);

        #endregion Public Abstract Methods
    }
}