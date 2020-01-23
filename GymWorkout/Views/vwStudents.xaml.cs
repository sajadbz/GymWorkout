using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GymWorkout.Data;
using GymWorkout.Entity.General;
using WPFCustomMessageBox;

namespace GymWorkout.Views
{
    /// <summary>
    /// Interaction logic for vwStudents.xaml
    /// </summary>
    public partial class vwStudents : Window
    {
        private readonly GymContext _context = new GymContext();
        private Student _student = new Student();

        public vwStudents()
        {
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            InitializeComponent();
            BindGrid();
            CboGender.ItemsSource = _context.Genders.ToList();
            GbAddEdit.DataContext = _student;

            splashScreen.Close();
        }


        #region Commands

        private void DeleteCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = GrdList.SelectedItem is Student student;
        }

        private void DeleteCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (GrdList.SelectedItem is Student student && CustomMessageBox.ShowYesNo($"جهت حذف رکورد '{student.FullName}' اطمینان دارید ؟", "حذف رکورد", "بله", "خیر", MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                _context.Students.Attach(student);
                _context.Entry(student).State = EntityState.Deleted;

                _context.SaveChanges();
                Reset();
                CustomMessageBox.ShowOK("اطلاعات شما با موفقیت حذف گردید.", "حذف", "بسیار خوب");
            }
        }

        private void WorkoutCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = (GrdList.SelectedItem is Student);
        }

        private void WorkoutCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            if (GrdList.SelectedItem is Student student)
            {
                new vwWorkouts(student.Id) { Owner = this, WindowStartupLocation = WindowStartupLocation.CenterOwner }.ShowDialog();
            }
        }
        #endregion

        #region Events Methods

        private void VwStudents_OnLoaded(object sender, RoutedEventArgs e)
        {
            CboGender.SelectedIndex = 1;
        }

        private void GrdList_OnMouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (GrdList.Items.Count > 0)
            {
                _student = (Student)GrdList.SelectedItem;
                GbAddEdit.DataContext = _student;
                GbAddEdit.Header = $"ویرایش '{_student.FullName}'";
                BtnSave.Content = "ویرایش";
                BtnSave.Background = new SolidColorBrush(Color.FromRgb(247, 194, 44));
            }
        }

        private void BtnSearch_OnClick(object sender, RoutedEventArgs e)
        {
            BindGrid(TxtSearchFullName.Text, TxtSearchMobile.Text);
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo("جهت ثبت اطلاعات اطمینان دارید ؟", "ثبت اطلاعات", "بله", "خیر", MessageBoxImage.Information)
                == MessageBoxResult.Yes)
            {
                if (_student.Id == 0)
                {
                    _student.CreateDate = DateTime.Now;
                    _context.Students.Add(_student);
                }
                else
                {
                    _context.Students.Attach(_student);
                    _context.Entry(_student).State = EntityState.Modified;
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
            this.Close();
        }

        private void BtnSearchCancel_OnClick(object sender, RoutedEventArgs e)
        {
            TxtSearchFullName.Text = TxtSearchMobile.Text = "";
            BindGrid();
        }
        #endregion

        #region Methods

        private void Reset()
        {
            _student = new Student();
            GbAddEdit.DataContext = _student;
            BtnSave.Content = GbAddEdit.Header = "افزودن";
            BtnSave.Background = new SolidColorBrush(Color.FromRgb(92, 184, 92));
            BindGrid(TxtSearchFullName.Text, TxtSearchMobile.Text);
        }

        private void BindGrid(string fullname = "", string mobile = "")
        {
            IEnumerable<Student> result = _context.Students;
            if (!string.IsNullOrWhiteSpace(fullname))
                result = result.Where(c => c.FullName.Contains(fullname));
            if (!string.IsNullOrWhiteSpace(mobile))
                result = result.Where(c => c.Mobile.Contains(mobile));

            GrdList.ItemsSource = result.ToList();
        }

        #endregion


        
    }
}
