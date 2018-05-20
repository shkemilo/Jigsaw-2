using Jigsaw_2.Abstracts;
using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jigsaw_2.Games.Associations
{
    internal class AssociationLeaf : IHideableGUI
    {
        public static readonly SolidColorBrush RightColor = Brushes.DarkGreen;

        private Button field;
        private string toPrint;

        private readonly string hiddenSymbol;
        private string realValue;

        public AssociationLeaf(Button field, string hiddenSymbol)
        {
            this.field = field;
            this.hiddenSymbol = hiddenSymbol;

            realValue = string.Empty;

            toPrint = hiddenSymbol;
        }

        public void Enable(bool b)
        {
            field.IsEnabled = b;
        }

        public void Print()
        {
            field.Content = toPrint;
        }

        public void Hide()
        {
            toPrint = hiddenSymbol;
        }

        public void Uncover(int index = -1)
        {
            toPrint = realValue;
        }

        public void Update<T>(T message)
        {
            if(message is bool)
            {
                updateColor(Convert.ToBoolean(message));
            }
            else if (message is string)
            {
                realValue = message as string;
            }
            else
            {
                throw new ArgumentException("Invalid argument type.");
            }
        }

        private void updateColor(bool message)
        {
            if(message)
            {
                field.Background = RightColor;
            }
        }
    }
}