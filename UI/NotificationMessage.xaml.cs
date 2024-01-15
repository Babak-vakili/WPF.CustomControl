using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static UI.Notification;

namespace UI
{
    /// <summary>
    /// Interaction logic for NotificationMessage.xaml
    /// </summary>
    public partial class NotificationMessage : UserControl
    {

        internal NotificationMessage()
        {
            InitializeComponent();
        }

        private NotificationType Type;
        private string Detail;
        private string Title;
        private int CloaseAfter;
        private FlowDirection Direction;
        private DispatcherTimer InitTimer;
        private Storyboard sb;

        internal NotificationMessage(NotificationType type, string title, string detail, int cloaseAfter, FlowDirection directiion)
        {
            InitializeComponent();
            Type = type;
            switch (type)
            {
                case NotificationType.Info:
                    Theme.Style = (Style)FindResource("BorderInfo");
                    Header.Background =(Brush)FindResource("InfoBrush");
                    IconLabel.Kind = MaterialDesignThemes.Wpf.PackIconKind.InfoOutline;
                    IconLabel.Foreground = (Brush)FindResource("InfoBrush");
                    break;
                case NotificationType.Warning:
                    Theme.Style = (Style)FindResource("BorderWarning");
                    Header.Background = (Brush)FindResource("WarningBrush");
                    IconLabel.Kind = MaterialDesignThemes.Wpf.PackIconKind.WarningOutline;
                    IconLabel.Foreground = (Brush)FindResource("WarningBrush");
                    break;
                case NotificationType.Error:
                    Theme.Style = (Style)FindResource("BorderError");
                    Header.Background = (Brush)FindResource("ErrorBrush");
                    IconLabel.Kind = MaterialDesignThemes.Wpf.PackIconKind.ErrorOutline;
                    IconLabel.Foreground = (Brush)FindResource("ErrorBrush");
                    break;
                case NotificationType.Success:
                    Theme.Style = (Style)FindResource("BorderSuccess");
                    Header.Background = (Brush)FindResource("SuccessBrush");
                    IconLabel.Kind = MaterialDesignThemes.Wpf.PackIconKind.SuccessCircleOutline;
                    IconLabel.Foreground = (Brush)FindResource("SuccessBrush");
                    break;
                default:
                    break;
            }
            Title = title;
            Detail = detail;
            CloaseAfter = cloaseAfter;
            Direction = directiion;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            DetailBox.Text = Detail;
            TitleBox.Content = Title;
            DetailBox.FlowDirection = Direction;
            InitTimer = new System.Windows.Threading.DispatcherTimer();
            InitTimer.Tick += InitTimer_Tick;
            InitTimer.Interval = new TimeSpan(0, 0, CloaseAfter);
            InitTimer.Start();

            sb = new Storyboard();
            var ta = new DoubleAnimationUsingKeyFrames() { Duration = TimeSpan.FromMilliseconds(CloaseAfter * 1000) };
            Storyboard.SetTarget(ta, Progress);
            Storyboard.SetTargetProperty(ta, new PropertyPath(ProgressBar.ValueProperty));
            for (int i = 0; i <= 1; i++)
            {
                var frame = new SplineDoubleKeyFrame();
                frame.Value = 100 - (i * 100);
                frame.KeyTime = KeyTime.FromTimeSpan(TimeSpan.FromSeconds(i * CloaseAfter));
                ta.KeyFrames.Add(frame);
            }
            sb.Children.Add(ta);
            sb.Begin();
        }

        private void InitTimer_Tick(object sender, EventArgs e)
        {
            Close();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Close()
        {
            InitTimer.Stop();
            this.Visibility = Visibility.Hidden;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            InitTimer.Stop();
            sb.Pause();
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            InitTimer.Interval = new TimeSpan(0, 0, 0, (int)(Progress.Value * CloaseAfter) / 100, (int)((Progress.Value * CloaseAfter) % 100) * 10);
            InitTimer.Start();
            sb.Resume();
        }
    }
}
