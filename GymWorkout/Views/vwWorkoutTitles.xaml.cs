using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GymWorkout.Data;
using GymWorkout.Entity.Workouts;
using WPFCustomMessageBox;

namespace GymWorkout.Views
{
    /// <summary>
    /// Interaction logic for vwWorkoutTitles.xaml
    /// </summary>
    public partial class vwWorkoutTitles : Window
    {
        private readonly GymContext _context = new GymContext();
        private WorkoutTitle _workoutTitle = new WorkoutTitle();

        public vwWorkoutTitles()
        {
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            InitializeComponent();
            BindGrid();
            GbAddEdit.DataContext = _workoutTitle;

            splashScreen.Close();
        }

        #region Commands

        private void DeleteCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DeleteCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var student = GrdList.SelectedItem as WorkoutTitle;
            if (student != null && CustomMessageBox.ShowYesNo($"جهت حذف رکورد '{student.Title}' اطمینان دارید ؟", "حذف رکورد", "بله", "خیر", MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                _context.WorkoutTitles.Attach(student);
                _context.Entry(student).State = EntityState.Deleted;

                _context.SaveChanges();
                Reset();
                CustomMessageBox.ShowOK("اطلاعات شما با موفقیت حذف گردید.", "حذف", "بسیار خوب");
            }
        }

        #endregion

        #region Events Methods

        private void GrdList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GrdList.Items.Count > 0)
            {
                _workoutTitle = (WorkoutTitle)GrdList.SelectedItem;
                GbAddEdit.DataContext = _workoutTitle;
                GbAddEdit.Header = $"ویرایش '{_workoutTitle.Title}'";
                BtnSave.Content = "ویرایش";
                BtnSave.Background = new SolidColorBrush(Color.FromRgb(247,194,44));
            }
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo("جهت ثبت اطلاعات اطمینان دارید ؟", "ثبت اطلاعات", "بله", "خیر", MessageBoxImage.Information)
                == MessageBoxResult.Yes)
            {
                if (_workoutTitle.Id == 0)
                {
                    _workoutTitle.CreateDate = DateTime.Now;
                    _context.WorkoutTitles.Add(_workoutTitle);
                }
                else
                {
                    _context.WorkoutTitles.Attach(_workoutTitle);
                    _context.Entry(_workoutTitle).State = EntityState.Modified;
                }
                _context.SaveChanges();
                Reset();
                CustomMessageBox.ShowOK("اطلاعات شما با موفقیت ثبت گردید.", "موفقیت آمیز", "بسیار خوب");
            }
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            Reset();
        }

        private void BtnClose_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        #endregion

        #region Methods

        private void Reset()
        {
            _workoutTitle = new WorkoutTitle();
            GbAddEdit.DataContext = _workoutTitle;
            BtnSave.Content = GbAddEdit.Header = "افزودن";
            BtnSave.Background = new SolidColorBrush(Color.FromRgb(92, 184, 92));
            BindGrid();
        }

        private void BindGrid()
        {
            GrdList.ItemsSource = _context.WorkoutTitles.ToList();
        }

        #endregion
    }
}
