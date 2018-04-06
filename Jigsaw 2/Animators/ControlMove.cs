using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Jigsaw_2.Animators
{
    /// <summary>
    /// Extension class used for animating the movement of Framework Elements
    /// </summary>
    internal static class ControlMove
    {
        /// <summar> Animates the Framework Element to move from it's current position to a new position. </summary>
        public static void MoveTo(this FrameworkElement target, double newX, double newY)
        {
            Vector offset = VisualTreeHelper.GetOffset(target);
            var top = offset.Y;
            var left = offset.X;
            TranslateTransform trans = new TranslateTransform();
            target.RenderTransform = trans;
            DoubleAnimation anim1 = new DoubleAnimation(0, newY - top, TimeSpan.FromSeconds(1));
            DoubleAnimation anim2 = new DoubleAnimation(0, newX - left, TimeSpan.FromSeconds(1));
            trans.BeginAnimation(TranslateTransform.YProperty, anim1);
            trans.BeginAnimation(TranslateTransform.XProperty, anim2);
        }
    }
}