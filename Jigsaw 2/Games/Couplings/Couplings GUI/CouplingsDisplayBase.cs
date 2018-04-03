using System;
using System.Windows.Controls;
using System.Windows.Media;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Couplings
{
    /// <summary>
    /// Represents the base GUI class used for the Couplings Game
    /// </summary>
    class CouplingsDisplayBase : GUIElement
    {
        Button match;

        SolidColorBrush color;

        public CouplingsDisplayBase(Button match, string content)
        {
            this.match = match;

            match.Content = content.ToUpper();
            match.IsEnabled = false;

            color = Brushes.DimGray;
            Show();
        }

        /// <summary> Returns the button associated with the element. </summary>
        public Button GetMatch()
        {
            return match;
        }

        /// <summary> Returns the content of the element. </summary>
        public string GetContent()
        {
            return match.Content.ToString();
        }

        /// <summary> Enables the control and sets its active color. </summary>
        public void SetActive(SolidColorBrush activeColor)
        {
            color = activeColor;

            match.IsEnabled = true;
        }

        /// <summary> Sets the elements color based on if it was correctly coupled. </summary>
        public void SetColor(bool correct)
        {
            if (correct)
                color = Brushes.DarkGreen;
            else
                color = Brushes.Red;

            match.IsEnabled = false;
        }

        /// <summary> Checks whether the element was coupled. </summary>
        public bool IsEvaluated()
        {
            if ((color == Brushes.DarkGreen) || (color == Brushes.Red))
                return true;
            else
                return false;
        }

        /// <summary> Sets its new background color. </summary>
        public override void Show()
        {
            match.Background = color;
        }

        /// <summary> Updates if the couple was correct or not. </summary>
        public override void Update<T>(T message)
        {
            if (message is bool)
                SetColor(Convert.ToBoolean(message));
            else
                throw new Exception("Invalid function call.");
        }
    }
}
