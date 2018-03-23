using System;
using System.Windows.Media;
using System.Windows.Shapes;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// GUIElement used for representing a field of a cheker used for displaying the validity of a element of the answer in the Jumper Game.
    /// </summary>
    public class JumperCheckerElement : GUIElement
    {
        Ellipse field;

        SolidColorBrush color;

        public JumperCheckerElement(Ellipse field)
        {
            this.field = field;

            color = Brushes.Gray;

            field.Fill = color;
        }

        /// <summary> Shows the element. </summary>
        public override void Show()
        {
            field.Fill = color;
        }

        /// <summary> Updates the color of the element. </summary>
        public override void Update<T>(T message)
        {
            if (message is string)
                color = new BrushConverter().ConvertFromString(message as string) as SolidColorBrush;
            else
                throw new Exception("This function only accepts Colors");
        }
    }
}
