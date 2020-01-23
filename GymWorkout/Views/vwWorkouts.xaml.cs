using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using GymWorkout.Data;
using GymWorkout.Entity.Workouts;
using WPFCustomMessageBox;

namespace GymWorkout.Views
{
    /// <summary>
    /// Interaction logic for vwWorkouts.xaml
    /// </summary>
    public partial class vwWorkouts : Window
    {
        private readonly GymContext _context = new GymContext();
        private int _studentId { get; set; }
        private WorkoutDetail _workoutDetail;
        private Workout _workout;
        

        public vwWorkouts(int studentId)
        {
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            InitializeComponent();
            _studentId = studentId;
            CboWorkoutTitles.ItemsSource = _context.WorkoutTitles.ToList();

            _workoutDetail = new WorkoutDetail();
            _workout = new Workout(){StudentId = studentId};
            GbWorkoutDetail.DataContext = _workoutDetail;
            GbWorkout.DataContext = _workout;

            BindGrid();

            splashScreen.Close();

        }
        private void VwWorkouts_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_studentId != 0)
            {
                var student = _context.Students.Find(_studentId);
                if (student != null)
                {
                    this.Title = $"فهرست برنامه های {student.FullName}";
                }
            }
        }

        #region Commands

        private void DeleteCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DeleteCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (GrdList.SelectedItem is Workout workout && CustomMessageBox.ShowYesNo($"جهت حذف این رکورد اطمینان دارید ؟", "حذف رکورد", "بله", "خیر", MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                _context.Workouts.Attach(workout);
                _context.Entry(workout).State = EntityState.Deleted;

                _context.SaveChanges();
                Reset();
                CustomMessageBox.ShowOK("اطلاعات شما با موفقیت حذف گردید.", "حذف", "بستن");
            }
        }

        private void WorkoutEditCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void WorkoutEditCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (GrdList.SelectedItem is Workout workout)
            {
                _context.Workouts.Attach(workout);
                _context.Entry(workout).State = EntityState.Modified;

                _context.SaveChanges();
                Reset();
                CustomMessageBox.ShowOK("اطلاعات شما با موفقیت بروزرسانی گردید.", "بروزرسانی", "بستن");
            }
        }

        

        private void PrintCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void PrintCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            CustomMessageBox.Show($"Id: {e.Parameter}");
        }
        #endregion

        #region Methods

        private void Reset()
        {
            _workout = new Workout() { StudentId = _studentId };
            BindGrid();
        }

        private void BindGrid()
        {
            IEnumerable<Workout> result = _context.Workouts;

            if (_studentId != 0)
                result = result.Where(c => c.StudentId == _studentId);

            GrdList.ItemsSource = result;


        }

        #endregion


        private void GrdList_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GrdList.SelectedItem is Workout workout)
            {
                ListCollectionView workoutDetails = BindWorkoutDetails(workout.Id);
                ResetToAddWorkoutDetail(workout.Id, workoutDetails);
                CboWorkoutTitles.SelectedIndex = 0;
            }
            else
                GbWorkoutDetail.IsEnabled = false;

        }

        private ListCollectionView BindWorkoutDetails(int workoutId)
        {
            var result = _context.WorkoutDetails
                .Include(c => c.WorkoutTitle)
                .Where(c => c.WorkoutId == workoutId).ToList();
            ListCollectionView workoutDetails = new ListCollectionView(result);
            workoutDetails.GroupDescriptions.Clear();
            workoutDetails.GroupDescriptions.Add(new PropertyGroupDescription("Day"));
            return workoutDetails;
        }

        private void BtnAddWorkoutDetail_OnClick(object sender, RoutedEventArgs e)
        {
            if (_workoutDetail.Id == 0)
            {
                _workoutDetail.CreateDate = DateTime.Now;
                _context.WorkoutDetails.Add(_workoutDetail);
            }
            else
            {
                _context.WorkoutDetails.Attach(_workoutDetail);
                _context.Entry(_workoutDetail).State = EntityState.Modified;
            }

            _context.SaveChanges();

            ListCollectionView workoutDetails = BindWorkoutDetails(_workoutDetail.WorkoutId);
            ResetToAddWorkoutDetail(_workoutDetail.WorkoutId, workoutDetails);

            CustomMessageBox.ShowOK("اطلاعات شما با موفقیت ثبت گردید", "ثبت موفقیت آمیز", "بستن");
        }

        private void ResetToAddWorkoutDetail(int workoutId , ListCollectionView workoutDetails )
        {
            var workout = _context.Workouts.Include(c=>c.Student).SingleOrDefault(c=>c.Id == workoutId);
            if (workout != null)
            {
                _workoutDetail = new WorkoutDetail() { WorkoutId = workoutId };
                GbWorkoutDetail.DataContext = _workoutDetail;
                GbWorkoutDetail.Header = workout.Title;
                GbWorkoutDetail.IsEnabled = true;

                GrdWorkoutDetails.ItemsSource = workoutDetails;
                BtnAddWorkoutDetail.Content = new BitmapImage(new Uri("pack://application:,,,/Content/Images/add.png"));
            }
            
        }

        private void BtnSaveWorkout_OnClick(object sender, RoutedEventArgs e)
        {
            _workout.CreateDate = DateTime.Now;
            _context.Workouts.Add(_workout);
            _context.SaveChanges();

            Reset();
            CustomMessageBox.ShowOK("اطلاعات شما با موفقیت ثبت گردید", "ثبت موفقیت آمیز", "بستن");
        }


        private void WorkoutDetailEditCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void WorkoutDetailDeleteCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void WorkoutDetailDeleteCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is WorkoutDetail workoutDetail &&
                CustomMessageBox.ShowYesNo($"جهت حذف این رکورد اطمینان دارید ؟", "حذف رکورد", "بله", "خیر", MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                _context.WorkoutDetails.Attach(workoutDetail);
                _context.Entry(workoutDetail).State = EntityState.Deleted;

                _context.SaveChanges();


                ListCollectionView workoutDetails = BindWorkoutDetails(_workoutDetail.WorkoutId);
                ResetToAddWorkoutDetail(_workoutDetail.WorkoutId, workoutDetails);
            }
            
        }

        private void WorkoutDetailEditCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (e.Parameter is WorkoutDetail workoutDetail)
            {
                _workoutDetail = workoutDetail;
                GbWorkoutDetail.DataContext = workoutDetail;
                BtnAddWorkoutDetail.Content = new BitmapImage(new Uri("pack://application:,,,/Content/Images/Edit.png"));
            }
        }

        private void BtnCancelWorkoutDetail_OnClick(object sender, RoutedEventArgs e)
        {
            ListCollectionView workoutDetails = BindWorkoutDetails(_workoutDetail.WorkoutId);
            ResetToAddWorkoutDetail(_workoutDetail.WorkoutId, workoutDetails);
        }
    }
}
