using System;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Threading;

namespace Jigsaw_2.Animators
{
    internal class FrameAnimator
    {
        #region Private Fields

        private bool allowDirectNavigation;
        private double oldValue;

        private NavigatingCancelEventArgs navArgs;
        private Duration duration;

        private Frame mainFrame;
        private DependencyProperty property;

        #endregion Private Fields

        #region Constructors

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

        #endregion Constructors

        #region Events

        public void OnNavigating(object sender, NavigatingCancelEventArgs e)
        {
            if (mainFrame.Content != null && !allowDirectNavigation)
            {
                e.Cancel = true;

                navArgs = e;
                oldValue = mainFrame.ActualWidth;

                DoubleAnimation animation0 = new DoubleAnimation()
                {
                    From = mainFrame.ActualWidth,
                    To = 0,
                    Duration = duration
                };
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

                default:
                    break;
            }

            Application.Current.Dispatcher.BeginInvoke(DispatcherPriority.Loaded,
            (ThreadStart)delegate ()
                {
                    DoubleAnimation animation0 = new DoubleAnimation()
                    {
                        From = 0,
                        To = oldValue,
                        Duration = duration
                    };
                    mainFrame.BeginAnimation(property, animation0);
                });
        }

        #endregion Events
    }
}