using Jigsaw_2.Abstracts;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jigsaw_2.Games.Couplings.Couplings_GUI
{
    class CouplingsLeaf : IGUI
    {
        private static readonly SolidColorBrush rightColor = Brushes.DarkGreen;
        private static readonly SolidColorBrush wrongColor = Brushes.DarkRed;
        private static readonly SolidColorBrush defaultColor = Brushes.Gray;

        private Button match;

        private SolidColorBrush color;

        public CouplingsLeaf(Button match, string content)
        {
            this.match = match;

            match.Content = content.ToUpper();
            match.IsEnabled = false;

            match.Background = defaultColor;
        }

        /// <summary> Sets the elements color based on if it was correctly coupled. </summary>
        public void SetColor(bool correct)
        {
            if (correct)
                color = rightColor;
            else
                color = wrongColor;

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
            if (message is bool)
                SetColor(Convert.ToBoolean(message));
            else
                throw new Exception("Invalid function call.");
        }
    }
}
