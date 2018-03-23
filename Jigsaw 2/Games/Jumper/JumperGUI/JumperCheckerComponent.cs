using System;
using System.Collections.Generic;
using Jigsaw_2.Abstracts;

namespace Jigsaw_2.Games.Jumper
{
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
}
