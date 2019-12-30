using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using GymWorkout.Data;
using GymWorkout.Views;

namespace GymWorkout
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

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            LblTime.Content = DateTime.Now.ToString("dddd dd MMMM yyyy - HH:mm:ss");
            DispatcherTimer timer = new DispatcherTimer(){Interval = TimeSpan.FromSeconds(1),IsEnabled = true};
            timer.Tick += Timer_Tick;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            LblTime.Content = DateTime.Now.ToString("dddd dd MMMM yyyy - HH:mm:ss");
        }

        private void BtnStudents_OnClick(object sender, RoutedEventArgs e)
        {
            new vwStudents() {Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner}.ShowDialog();
            
        }
    }
}
