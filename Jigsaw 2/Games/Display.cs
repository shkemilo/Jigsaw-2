using Jigsaw_2.Abstracts;
using System.Windows.Controls;

namespace Jigsaw_2.Games
{
    /// <summary>
    /// Simple display used for displaying a message.
    /// </summary>
    public class Display : GUIElement
    {
        #region Private Fields

        private Control messageDisplay;

        private string message;

        #endregion Private Fields

        #region Constructors

        public Display(Control messageDisplay)
        {
            message = string.Empty;
            this.messageDisplay = messageDisplay;
        }

        #endregion Constructors

        #region Public Override Methods

        /// <summary> Updates the message to be shown. </summary>
        public override void Update<T>(T message)
        {
            this.message = message.ToString();
        }

        /// <summary> Shows the current message. </summary>
        public override void Show()
        {
            if (messageDisplay is TextBox)
                (messageDisplay as TextBox).Text = message.ToString();
        }

        #endregion Public Override Methods
    }
}