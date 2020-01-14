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
    /// Interaction logic for vwFoodMeals.xaml
    /// </summary>
    public partial class vwFoodMeals : Window
    {
        private readonly GymContext _context = new GymContext();
        private FoodMeal _foodMeal = new FoodMeal();

        public vwFoodMeals()
        {
            var splashScreen = new SplashScreenWindow();
            splashScreen.Show();

            InitializeComponent();
            BindGrid();
            GbAddEdit.DataContext = _foodMeal;

            splashScreen.Close();
        }

        #region Commands

        private void DeleteCommandBinding_OnCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DeleteCommandBinding_OnExecuted(object sender, ExecutedRoutedEventArgs e)
        {
            var student = GrdList.SelectedItem as FoodMeal;
            if (student != null && CustomMessageBox.ShowYesNo($"جهت حذف رکورد '{student.Title}' اطمینان دارید ؟", "حذف رکورد", "بله", "خیر", MessageBoxImage.Warning)
                == MessageBoxResult.Yes)
            {
                _context.FoodMeals.Attach(student);
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
                _foodMeal = (FoodMeal)GrdList.SelectedItem;
                GbAddEdit.DataContext = _foodMeal;
                GbAddEdit.Header = $"ویرایش '{_foodMeal.Title}'";
                BtnSave.Content = "ویرایش";
                BtnSave.Background = new SolidColorBrush(Color.FromRgb(247, 194, 44));
            }
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (CustomMessageBox.ShowYesNo("جهت ثبت اطلاعات اطمینان دارید ؟", "ثبت اطلاعات", "بله", "خیر", MessageBoxImage.Information)
                == MessageBoxResult.Yes)
            {
                if (_foodMeal.Id == 0)
                {
                    _foodMeal.CreateDate = DateTime.Now;
                    _context.FoodMeals.Add(_foodMeal);
                }
                else
                {
                    _context.FoodMeals.Attach(_foodMeal);
                    _context.Entry(_foodMeal).State = EntityState.Modified;
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
            _foodMeal = new FoodMeal();
            GbAddEdit.DataContext = _foodMeal;
            BtnSave.Content = GbAddEdit.Header = "افزودن";
            BtnSave.Background = new SolidColorBrush(Color.FromRgb(92, 184, 92));
            BindGrid();
        }

        private void BindGrid()
        {
            GrdList.ItemsSource = _context.FoodMeals.ToList();
        }

        #endregion
    }
}
