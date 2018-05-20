using Jigsaw_2.Abstracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace Jigsaw_2.Games.Associations
{
    internal class AssociationComposite : IHideableGUI
    {
        private List<IHideableGUI> fields;

        public AssociationComposite(IEnumerable<IHideableGUI> fields)
        {
            this.fields = new List<IHideableGUI>(fields);
        }

        public void Enable(bool b)
        {
            foreach (IHideableGUI element in fields)
            {
                element.Enable(b);
            }
        }

        public void Uncover(int index = -1)
        {
            if (index == -1)
            {
                uncoverAll();
            }
            else if (index < fields.Count)
            {
                fields.ElementAt(index).Uncover();
            }
            else
            {
                throw new ArgumentOutOfRangeException("Element does not exist.");
            }
        }

        public void Hide()
        {
            foreach (IHideableGUI element in fields)
            {
                element.Hide();
            }
        }

        public void Print()
        {
            foreach (IHideableGUI element in fields)
            {
                element.Print();
            }
        }

        public void Update<T>(T message)
        {
            if(message is bool)
            {
                updateFields(Convert.ToBoolean(message));
            }
            else if (message is Association)
            {
                updateFields(message as Association);
            }
            else
            {
                throw new ArgumentException("Invalid argument.");
            }
        }

        private void updateFields(bool message)
        {
            foreach(IHideableGUI element in fields)
            {
                element.Update(message);
            }
        }

        private void updateFields(Association message)
        {
            int i = 0;

            foreach (IHideableGUI element in fields)
            {
                if (i != fields.Count - 1)
                {
                    element.Update(message.Fields.ElementAt(i++));
                }
                else
                {
                    element.Update(message.Answer);
                }
            }
        }

        /*private void updateFields(List<Association> message)
        {
            int i = 0;

            foreach (IHideableGUI element in fields)
            {
                element.Update(message.ElementAt(i++));
            }
        }*/

        private void uncoverAll()
        {
            foreach (IHideableGUI element in fields)
            {
                element.Uncover();
            }
        }
    }
}