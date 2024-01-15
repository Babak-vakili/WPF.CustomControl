using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace UI
{
    /// <summary>
    /// Interaction logic for Notification.xaml
    /// </summary>
    public partial class Notification : UserControl
    {
        private Queue<NotificationMessage> Notifications;
        private static int Max = 4;
        private static int index = 0;
        DispatcherTimer dispatcherTimer;

        public enum NotificationType
        {
            Info,
            Warning,
            Error,
            Success,
        }

        public Notification()
        {
            InitializeComponent();

            Notifications = new Queue<NotificationMessage>();

            dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(Check);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            dispatcherTimer.Start();
        }

        public void AddNotification(NotificationType type, string title, string content, int CloaseAfter)
        {
            index++;
            NotificationMessage notification = new NotificationMessage(type, title, content, 10, FlowDirection.LeftToRight);
            notification.IsVisibleChanged += Notification_IsVisibleChanged;
            Notifications.Enqueue(notification);
        }

        private void Check(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {
                if (Notifications.Count > 0 && Panel.Children.Count < Max)
                {
                    var message = Notifications.Dequeue();
                    var sb = new Storyboard();
                    var ta = new ThicknessAnimationUsingKeyFrames() { Duration = TimeSpan.FromMilliseconds(300) };
                    Storyboard.SetTarget(ta, message);
                    Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));
                    for (int i = 0; i < 4; i++)
                    {
                        var frame = new SplineThicknessKeyFrame();
                        frame.Value = new Thickness(0, -1 * (3 - i) * 4, 0, 0);
                        frame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(i * 2 * 25));
                        ta.KeyFrames.Add(frame);
                    }
                    sb.Children.Add(ta);
                    sb.Begin();

                    Panel.Children.Add(message);
                }
            }));
        }

        private void Notification_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Dispatcher.Invoke((Action)(() =>
            {//this refer to form in WPF application 
                if (sender is NotificationMessage && (sender as NotificationMessage).Visibility == System.Windows.Visibility.Hidden)
                {
                    foreach (var item in Panel.Children)
                    {
                        var message = item as NotificationMessage;
                        var sb = new Storyboard();
                        var ta = new ThicknessAnimationUsingKeyFrames() { Duration = TimeSpan.FromMilliseconds(300) };
                        Storyboard.SetTarget(ta, message);
                        Storyboard.SetTargetProperty(ta, new PropertyPath(MarginProperty));
                        for (int i = 0; i < 4; i++)
                        {
                            var frame = new SplineThicknessKeyFrame();
                            frame.Value = new Thickness(0, (3 - i) * 4, 0, 0);
                            frame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(i * 2 * 25));
                            ta.KeyFrames.Add(frame);
                        }
                        sb.Children.Add(ta);
                        sb.Begin();
                    }

                    Panel.Children.Remove(sender as NotificationMessage);
                }
            }));


        }
    }
}
