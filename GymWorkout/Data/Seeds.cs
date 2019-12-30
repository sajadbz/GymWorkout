using System;
using System.Collections.Generic;
using GymWorkout.Entity.Foods;
using GymWorkout.Entity.General;

namespace GymWorkout.Data
{
    public class Seeds
    {
        public static List<Gender> Genders = new List<Gender>()
        {
            new Gender(){Title = "آقای", Display = "مرد",CreateDate = DateTime.Now},
            new Gender(){Title = "خانم", Display = "زن",CreateDate = DateTime.Now},
        };
        public static List<FoodMeal> FoodMeals = new List<FoodMeal>()
        {
            new FoodMeal(){Title = "صبحانه",CreateDate = DateTime.Now},
            new FoodMeal(){Title = "میان وعده اول",CreateDate = DateTime.Now},
            new FoodMeal(){Title = "میان وعده دوم",CreateDate = DateTime.Now},
            new FoodMeal(){Title = "نهار",CreateDate = DateTime.Now},
            new FoodMeal(){Title = "شام",CreateDate = DateTime.Now},
        };
        public static List<FoodUnit> FoodUnits = new List<FoodUnit>()
        {
            new FoodUnit(){Title = "عدد", CreateDate = DateTime.Now},
            new FoodUnit(){Title = "گرم", CreateDate = DateTime.Now},
            new FoodUnit(){Title = "لیتر", CreateDate = DateTime.Now},
        };
    }
}
