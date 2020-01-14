using System;
using System.Data.Entity;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using GymWorkout.Data;
using GymWorkout.Entity.Foods;
using WPFCustomMessageBox;

namespace GymWorkout.Views
{
    /// <summary>
    /// Interaction logic for vwFoodUnits.xaml
    /// </summary>
    public partial class vwFoodUnits : Window
    {
        private readonly GymContext _context = new GymContext();
        private FoodUnit _foodUnit = new FoodUnit();

        public vwFoodUnits()
        {
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            InitializeComponent();
            BindGrid();
            GbAddEdit.DataContext = _foodUnit;

            splashScreen.Close();
        }
        #region Commands

        private void DeleteCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DeleteCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var student = GrdList.SelectedItem as FoodUnit;
            if (student != null && CustomMessageBox.ShowYesNo($"جهت حذف رکورد '{student.Title}' اطمینان دارید ؟", "حذف رکورد", "بله", "خیر", MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                _context.FoodUnits.Attach(student);
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
                _foodUnit = (FoodUnit)GrdList.SelectedItem;
                GbAddEdit.DataContext = _foodUnit;
                GbAddEdit.Header = $"ویرایش '{_foodUnit.Title}'";
                BtnSave.Content = "ویرایش";
                BtnSave.Background = new SolidColorBrush(Color.FromRgb(247, 194, 44));
            }
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo("جهت ثبت اطلاعات اطمینان دارید ؟", "ثبت اطلاعات", "بله", "خیر", MessageBoxImage.Information)
                == MessageBoxResult.Yes)
            {
                if (_foodUnit.Id == 0)
                {
                    _foodUnit.CreateDate = DateTime.Now;
                    _context.FoodUnits.Add(_foodUnit);
                }
                else
                {
                    _context.FoodUnits.Attach(_foodUnit);
                    _context.Entry(_foodUnit).State = EntityState.Modified;
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

        #endregion

        #region Methods

        private void Reset()
        {
            _foodUnit = new FoodUnit();
            GbAddEdit.DataContext = _foodUnit;
            BtnSave.Content = GbAddEdit.Header = "افزودن";
            BtnSave.Background = new SolidColorBrush(Color.FromRgb(92, 184, 92));
            BindGrid();
        }

        private void BindGrid()
        {
            GrdList.ItemsSource = _context.FoodUnits.ToList();
        }

        #endregion
    }
}
