using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// GUIEelement used to represent one field for displaying one element of the current answer in the Jumper Game.
    /// </summary>
    public class JumperDisplayElement : GUIElement
    {
        Button field;
        Image elementImage;

        BitmapImage source;

        public JumperDisplayElement(Button field)
        {
            this.field = field;

            elementImage = field.Content as Image;
        }

        /// <summary> Changes the controls enabled state. </summary>
        public void SetEnabled(bool b)
        {
            field.IsEnabled = b;
        }

        public Button GetField()
        {
            return field;
        }

        /// <summary> Draws this element. </summary>
        public override void Show()
        {
            elementImage.Source = source;
        }

        /// <summary> Updates the image the element will show. </summary>
        public override void Update<T>(T message)
        {
            if (message == null)
                source = null;
            else if (message is BitmapImage)
                source = message as BitmapImage;
            else
                throw new Exception("This function only accepts BitmapImages");
        }
    }
}

