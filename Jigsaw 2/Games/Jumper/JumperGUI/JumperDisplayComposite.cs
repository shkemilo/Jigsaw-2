using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media.Imaging;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Jumper
{
    /// <summary>
    /// GUIElement used to represent current answer in the Jumper Game.
    /// </summary>
    public class JumperDisplayComposite : GUIElement
    {
        List<JumperDisplayLeaf> elements;

        int numberOfElements;
        int currentElementIndex;

        public JumperDisplayComposite(List<JumperDisplayLeaf> elements, int numberOfElements = 4)
        {
            this.elements = elements;

            this.numberOfElements = numberOfElements;
            currentElementIndex = -1;
        }

        public int CurrentElementIndex { get => currentElementIndex; }
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
        public JumperDisplayLeaf GetActiveElement()
        {
            return elements.ElementAt(currentElementIndex);
        }

        public void Disable()
        {
            foreach (JumperDisplayLeaf element in elements)
                element.SetEnabled(false);
        }

        /// <summary> Return a field at specified index. Returns null if index doesnt exist. </summary>
        public JumperDisplayLeaf GetFieldAtIndex(int n)
        {
            if ((n < numberOfElements) && (n >= 0))
                return elements[n];

            throw new Exception("No such element exists.");
        }

        /// <summary> Shows all the elements in row. </summary>
        public override void Show()
        {
            foreach (JumperDisplayLeaf element in elements)
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
}
