using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
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

        private void BtnFoodMeals_OnClick(object sender, RoutedEventArgs e)
        {
            new vwFoodMeals() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner }.ShowDialog();
        }

        private void BtnFoodUnits_OnClick(object sender, RoutedEventArgs e)
        {
            new vwFoodUnits() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner }.ShowDialog();
        }

        private void BtnWorkoutTitles_OnClick(object sender, RoutedEventArgs e)
        {
            new vwWorkoutTitles() { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner }.ShowDialog();
        }
    }

    /// <summary>
    ///    Class used to manage generic scoping of access keys
    /// </summary>
    public static class AccessKeyScoper
    {
        /// <summary>
        ///    Identifies the IsAccessKeyScope attached dependency property
        /// </summary>

        public static readonly DependencyProperty IsAccessKeyScopeProperty =

            DependencyProperty.RegisterAttached("IsAccessKeyScope", typeof(bool), typeof(AccessKeyScoper), new PropertyMetadata(false, HandleIsAccessKeyScopePropertyChanged));

        /// <summary>
        ///    Sets the IsAccessKeyScope attached property value for the specified object
        /// </summary>
        /// <param name="obj">The object to retrieve the value for</param>
        /// <param name="value">Whether the object is an access key scope</param>
        public static void SetIsAccessKeyScope(DependencyObject obj, bool value)

        {

            obj.SetValue(AccessKeyScoper.IsAccessKeyScopeProperty, value);

        }

        /// <summary>
        ///    Gets the value of the IsAccessKeyScope attached property for the specified object
        /// </summary>
        /// <param name="obj">The object to retrieve the value for</param>
        /// <returns>The value of IsAccessKeyScope attached property for the specified object</returns>
        public static bool GetIsAccessKeyScope(DependencyObject obj)

        {

            return (bool)obj.GetValue(AccessKeyScoper.IsAccessKeyScopeProperty);

        }

        private static void HandleIsAccessKeyScopePropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)

        {

            if (e.NewValue.Equals(true))

            {

                AccessKeyManager.AddAccessKeyPressedHandler(d, HandleScopedElementAccessKeyPressed);

            }

            else

            {

                AccessKeyManager.RemoveAccessKeyPressedHandler(d, HandleScopedElementAccessKeyPressed);

            }

        }

        private static void HandleScopedElementAccessKeyPressed(object sender, AccessKeyPressedEventArgs e)

        {

            if (!Keyboard.IsKeyDown(Key.LeftAlt) && !Keyboard.IsKeyDown(Key.RightAlt) && GetIsAccessKeyScope((DependencyObject)sender))

            {

                e.Scope = sender;

                e.Handled = true;

            }

        }

    }
}
