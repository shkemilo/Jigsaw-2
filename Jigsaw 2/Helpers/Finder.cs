
using System.Collections.Generic;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Jigsaw_2.Helpers
{
    /// <summary>
    /// Static class used for finding elements and sets of elements of a Windows form by their tag, and returns their references.
    /// </summary>
    public static class Finder
    {
        static List<Control> allControls;
        static List<Timer> timers;

        /// <summary> Sets the All Controls filed so that the class has a reference to all the elements of a Windows form. </summary>
        public static void SetAllControls(List<Control> newControls)
        {
            allControls = newControls;
        }

        /// <summary> Sets the Timers field so the class has a reference to all the Timers of a Windows form. </summary>
        public static void SetTimers(List<Timer> newTimers)
        {
            timers = newTimers;
        }

        /// <summary> Finds and returns a Control in the whole Windows form by it's tag. Returns null if not found. </summary>
        public static Control FindElementWithTag(string tag)
        {
            foreach (Control c in allControls)
                if (c.Tag != null)
                    if (c.Tag.ToString() == tag)
                        return c;

            return null;
        }

        /// <summary> Finds and returns a Control in a specified collection by it's tag. Returns null if not found. </summary>
        public static Control FindElementWithTag(List<Control> targetList, string tag)
        {
            foreach (Control c in targetList)
                if (c.Tag != null)
                    if (c.Tag.ToString() == tag)
                        return c;

            return null;
        }

        /// <summary> Finds and returns a collection of Controls in the whole Windows form with the same tag. Returns null if not found. </summary>
        public static List<Control> FindElementsWithTag(string tag)
        {
            List<Control> tList = new List<Control>();

            foreach (Control c in allControls)
                if (c.Tag != null)
                    if (c.Tag.ToString() == tag)
                        tList.Add(c);

            if (tList.Count == 0)
                return null;
            else
                return tList;
        }

        /// <summary> Finds and returns a collection of Controls in a specified collection with the same tag. Returns null if not found. </summary>
        public static List<Control> FindElementsWithTag(List<Control> targetList, string tag)
        {
            List<Control> tList = new List<Control>();

            foreach (Control c in targetList)
                if (c.Tag != null)
                    if (c.Tag.ToString() == tag)
                        tList.Add(c);

            if (tList.Count == 0)
                return null;
            else
                return tList;
        }

        /// <summary> Finds and returns a list of all visual childrens of an object. </summary>
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

    }
}
