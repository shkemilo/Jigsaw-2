using Jigsaw_2.Abstracts;
using System;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// GUIElement used for representing a field of a checker used for displaying the validity of a element of the answer in the Jumper Game.
    /// </summary>
    public class JumperCheckerLeaf : GUIElement
    {
        #region Private Fields

        private Ellipse field;

        private SolidColorBrush color;

        #endregion Private Fields

        #region Constructors

        public JumperCheckerLeaf(Ellipse field)
        {
            this.field = field;

            color = Brushes.Gray;

            field.Fill = color;
        }

        #endregion Constructors

        #region Public Override Methods

        /// <summary> Shows the element. </summary>
        public override void Show()
        {
            field.Fill = color;
        }

        /// <summary> Updates the color of the element. </summary>
        public override void Update<T>(T message)
        {
            if (message is string)
            {
                color = new BrushConverter().ConvertFromString(message as string) as SolidColorBrush;
            }
            else
            {
                throw new ArgumentException("This function only accepts Colors");
            }
        }

        #endregion Public Override Methods
    }
}