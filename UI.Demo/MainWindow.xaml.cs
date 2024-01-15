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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace UI.Demo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddInfo_Click(object sender, RoutedEventArgs e)
        {
            //notificationHandler.AddNotification(Generals.MessageType.Info, "Info", "there is some information for you", 5);
            notification.AddNotification(Notification.NotificationType.Info, "Info", "there is some information for you", 5);
        }
        private void AddError_Click(object sender, RoutedEventArgs e)
        {
            notification.AddNotification(Notification.NotificationType.Error, "Error", "there is some data for you", 5);
        }
        private void AddSuccess_Click(object sender, RoutedEventArgs e)
        {
            notification.AddNotification(Notification.NotificationType.Success, "Success", "there is some details for you there is some details for you there is some details for you there is some details for you", 5);
        }
        private void AddWarning_Click(object sender, RoutedEventArgs e)
        {
            notification.AddNotification(Notification.NotificationType.Warning, "Warning", "there is some warnings for you", 5);
        }
    }
}
