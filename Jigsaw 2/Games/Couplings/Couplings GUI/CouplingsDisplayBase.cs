using System;
using System.Windows.Controls;
using System.Windows.Media;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Couplings
{
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

        public Button GetMatch()
        {
            return match;
        }

        public string GetContent()
        {
            return match.Content.ToString();
        }

        public void SetActive(SolidColorBrush activeColor)
        {
            color = activeColor;

            match.IsEnabled = true;
        }

        public override void Show()
        {
            match.Background = color;
        }

        public void SetColor(bool correct)
        {
            if (correct)
                color = Brushes.DarkGreen;
            else
                color = Brushes.Red;

            match.IsEnabled = false;
        }

        public bool IsEvaluated()
        {
            if (color == Brushes.DarkGreen || color == Brushes.Red)
                return true;
            else
                return false;
        }

        public override void Update<T>(T message)
        {
            if (message is bool)
                SetColor(Convert.ToBoolean(message));
            else
                throw new Exception("Invalid function call.");
        }
    }
}
