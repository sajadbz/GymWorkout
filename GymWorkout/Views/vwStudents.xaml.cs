using System.Linq;
using System.Windows;
using GymWorkout.Data;

namespace GymWorkout.Views
{
    /// <summary>
    /// Interaction logic for vwStudents.xaml
    /// </summary>
    public partial class vwStudents : Window
    {
        public vwStudents()
        {
            InitializeComponent();
        }

        private void VwStudents_OnLoaded(object sender, RoutedEventArgs e)
        {
            using (var db = new GymContext())
            {
                GrdList.ItemsSource = db.Students.ToList();
            }
        }
    }
}
