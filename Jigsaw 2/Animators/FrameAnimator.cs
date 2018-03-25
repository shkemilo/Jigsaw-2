using System;
using System.Windows;
using System.Threading;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Windows.Media.Animation;

namespace Jigsaw_2.Animators
{
    class FrameAnimator
    {
        bool allowDirectNavigation;
        double oldValue;

        NavigatingCancelEventArgs navArgs;
        Duration duration;

        Frame mainFrame;
        DependencyProperty property;

        public FrameAnimator(Frame mainFrame, DependencyProperty property)
        {
            this.mainFrame = mainFrame;
            this.property = property;

            oldValue = 0;
            duration = new Duration(TimeSpan.FromSeconds(0.7));
            navArgs = null;
            allowDirectNavigation = false;

            mainFrame.Navigating += OnNavigating;
        }

        public void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (mainFrame.Content != null && !allowDirectNavigation)
            {
                e.Cancel = true;

                navArgs = e;
                oldValue = mainFrame.ActualWidth;

                DoubleAnimation animation0 = new DoubleAnimation();
                animation0.From = mainFrame.ActualWidth;
                animation0.To = 0;
                animation0.Duration = duration;
                animation0.Completed += SlideCompleted;
                mainFrame.BeginAnimation(property, animation0);
            }
            allowDirectNavigation = false;
        }

        public void SlideCompleted(object sender, EventArgs e)
        {
            allowDirectNavigation = true;
            switch (navArgs.NavigationMode)
            {
                case NavigationMode.New:
                    if (navArgs.Uri == null)
                        mainFrame.Navigate(navArgs.Content);
                    else
                        mainFrame.Navigate(navArgs.Uri);
                    break;
                case NavigationMode.Back:
                    mainFrame.GoBack();
                    break;
                case NavigationMode.Forward:
                    mainFrame.GoForward();
                    break;
                case NavigationMode.Refresh:
                    mainFrame.Refresh();
                    break;
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
                (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation();
                    animation0.From = 0;
                    animation0.To = oldValue;
                    animation0.Duration = duration;
                    mainFrame.BeginAnimation(property, animation0);
                });
        }
    }
}
