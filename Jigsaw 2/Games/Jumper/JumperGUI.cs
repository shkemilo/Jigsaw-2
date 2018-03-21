using System;
using System.Collections;
using System.Linq;
using System.Windows.Controls;
using Jigsaw_2.Helpers;
using Jigsaw_2.Abstracts;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Collections.Generic;

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

    /// <summary>
    /// GUIElement used to represent current answer in the Jumper Game.
    /// </summary>
    public class JumperDisplayComponent : GUIElement
    {
        List<JumperDisplayElement> elements;

        int numberOfElements;
        int currentElementIndex;

        public JumperDisplayComponent(List<JumperDisplayElement> elements, int numberOfElements = 4)
        {
            this.elements = elements;

            this.numberOfElements = numberOfElements;
            currentElementIndex = -1;
        }

        public int CurrentElementIndex { get => currentElementIndex;  }
        public int NumberOfElements { get => numberOfElements; }

        public void PreviousElement()
        {
            if (currentElementIndex >= 0)
                currentElementIndex--;
        }

        public void NextElement()
        {
            if (currentElementIndex < numberOfElements - 1)
                currentElementIndex++;
        }

        /// <summary> Returns the current element to be set. </summary>
        public JumperDisplayElement GetActiveElement()
        {
            return elements.ElementAt(currentElementIndex);

        }

        public void Disable()
        {
            foreach (JumperDisplayElement element in elements)
                element.SetEnabled(false);
        }

        /// <summary> Return a field at specified index. Returns null if index doesnt exist. </summary>
        public JumperDisplayElement GetFieldAtIndex(int n)
        {
            if ((n < numberOfElements) && (n >= 0))
                return elements[n];

            throw new Exception("No such element exists.");
        }

        /// <summary> Shows all the elements in row. </summary>
        public override void Show()
        {
            foreach (JumperDisplayElement element in elements)
                element.Show();
        }

        /// <summary> Updates the Image of the current element. </summary>
        public override void Update<T>(T message)
        {
            if (message is BitmapImage || message == null)
                elements[currentElementIndex].Update(message);
            else
                throw new Exception("This function only accepts BitmapImages");
        }
    }

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

    /// <summary>
    /// GUIElement used for representing the validity of the whole answer in the Jumper Game.
    /// </summary>
    public class JumperCheckerComponent : GUIElement
    {
        List<JumperCheckerElement> elements;

        int numberOfElements;

        public JumperCheckerComponent(List<JumperCheckerElement> elements, int numberOfElements = 4)
        {
            this.elements = elements;

            this.numberOfElements = numberOfElements;
        }

        public void SetActive()
        {
            string[] activeColors = new string[numberOfElements];
            for (int i = 0; i < numberOfElements; i++)
                activeColors[i] = "White";

            Update(activeColors);
        }

        /// <summary> Shows the whole componenet. </summary>
        public override void Show()
        {
            foreach (JumperCheckerElement element in elements)
                element.Show();
        }

        /// <summary> Set the colors of every element in the comonenet. </summary>
        public override void Update<T>(T message)
        {
            if (message is string[])
            {
                string[] colorArray = message as string[];

                for (int i = 0; i < numberOfElements; i++)
                    elements[i].Update(colorArray[i]);
            }
            else
                throw new Exception("This function only accepts Brush arrays");
        }
    }

    /// <summary>
    /// GUIElement used for representing the whole Jumper Game display.
    /// </summary>
    public class JumperDisplay : GUIElement
    {
        List<JumperDisplayComponent> displays;
        List<JumperCheckerComponent> checkers;

        int numberOfRows;
        int currentRow;

        public JumperDisplay(List<JumperDisplayComponent> displays, List<JumperCheckerComponent> checkers, int numberOfRows = 6)
        {
            this.displays = displays;
            this.checkers = checkers;

            this.numberOfRows = numberOfRows;

            currentRow = 0;
        } 

        public int CurrentRow { get => currentRow;  }

        public void NextRow()
        {
            if (currentRow < numberOfRows - 1)
                currentRow++;     
        }

        public void SetActiveRowVisual()
        {
            checkers[currentRow].SetActive();
        }

        /// <summary> Manualy show the checker. (TODO: remove the need for this function) </summary>
        public void ManualChekerShow()
        {
            checkers[currentRow].Show();
        }

        /// <summary> Returns the current element to be set. </summary>
        public JumperDisplayElement GetActiveElement()
        {
            return displays[currentRow].GetActiveElement();
        }

        /// <summary> Returns the current row. </summary>
        public JumperDisplayComponent GetCurrentRow()
        {
            return displays[currentRow];
        }

        /// <summary> Shows the displays and checkers. </summary>
        public override void Show()
        {
                displays[currentRow].Show();
                checkers[currentRow].Show();
        }

        /// <summary> Sets the displays or checker depending on the type of the message. </summary>
        public override void Update<T>(T message)
        {
            if (message is BitmapImage || message == null)
                displays[currentRow].Update(message);
            else if (message is string[])
                checkers[currentRow].Update(message);
            else
                throw new Exception("This function only accepts Images of Color arrays");
        }
    }

}
