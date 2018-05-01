using Jigsaw_2.Abstracts;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jigsaw_2.Games.WhoKnowsKnows
{
    internal class QuestionDisplay : IGUI
    {
        private static readonly SolidColorBrush rightColor = Brushes.DarkGreen;
        private static readonly SolidColorBrush wrongColor = Brushes.DarkRed;
        private static readonly SolidColorBrush defaultColor = Brushes.Gray;

        private Button questionField;

        private string questionText;
        private SolidColorBrush questionColor;

        public QuestionDisplay(Button questionField)
        {
            this.questionField = questionField;

            questionText = string.Empty;

            questionColor = defaultColor;
        }

        public void Enable(bool b)
        {
            questionField.IsEnabled = b;
        }

        public void Print()
        {
            questionField.Content = questionText;
            questionField.Background = questionColor;
        }

        public void Update<T>(T message)
        {
            if (message is string)
            {
                questionChange(message as string);
            }
            else if (message is Boolean)
            {
                updateColor(Convert.ToBoolean(message));
            }
            else
            {
                throw new ArgumentException("Invalid parameter. Expected string.");
            }
        }

        private void updateColor(bool correct)
        {
            if (correct)
            {
                questionColor = rightColor;
            }
            else
            {
                questionColor = wrongColor;
            }
        }

        private void questionChange(string newQuestion)
        {
            questionText = newQuestion;

            questionColor = defaultColor;
        }
    }
}