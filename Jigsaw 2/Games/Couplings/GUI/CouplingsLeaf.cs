using Jigsaw_2.Abstracts;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jigsaw_2.Games.Couplings
{
    internal class CouplingsLeaf : IGUI
    {
        public static readonly SolidColorBrush RightColor = Brushes.DarkGreen;
        public static readonly SolidColorBrush WrongColor = Brushes.DarkRed;
        public static readonly SolidColorBrush DefaultColor = Brushes.Gray;

        private Button match;

        private SolidColorBrush color;

        public CouplingsLeaf(Button match, string content)
        {
            this.match = match;

            match.Content = content.ToUpper();
            match.IsEnabled = false;

            color = DefaultColor;
        }

        /// <summary> Sets the elements color based on if it was correctly coupled. </summary>
        public void SetColor(bool correct)
        {
            if (correct)
                color = RightColor;
            else
                color = WrongColor;

            match.IsEnabled = false;
        }

        public void Enable(bool b)
        {
            match.IsEnabled = b;
        }

        public void Print()
        {
            match.Background = color;
        }

        public void Update<T>(T message)
        {
            if (message is SolidColorBrush)
                setColor(message as SolidColorBrush);
            else
                throw new Exception("Invalid function call.");
        }

        private void setColor(SolidColorBrush message)
        {
            if (color != RightColor)
            {
                color = message;
                match.IsEnabled = false;
            }
        }
    }
}