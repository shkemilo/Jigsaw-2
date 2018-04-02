using System;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Jigsaw_2.Animators
{
    public static class SmoothProgressBar
    {
        public static void SetPercent(this ProgressBar progressBar, double percentage, TimeSpan duration)
        {
            DoubleAnimation animation = new DoubleAnimation(percentage, duration);
            progressBar.BeginAnimation(ProgressBar.ValueProperty, animation);
        }
    }
}
