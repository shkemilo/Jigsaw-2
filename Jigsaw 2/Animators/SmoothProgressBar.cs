using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Jigsaw_2.Animators
{
    /// <summary>
    /// Extension class used for animating the value of the progress bar.
    /// </summary>
    public static class SmoothProgressBar
    {
        /// <summary> Sets the Progress Bar to a specified percent. </summary>
        public static void SetPercent(this ProgressBar progressBar, double percentage, TimeSpan duration)
        {
            DoubleAnimation animation = new DoubleAnimation(percentage, duration);
            progressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }
    }
}