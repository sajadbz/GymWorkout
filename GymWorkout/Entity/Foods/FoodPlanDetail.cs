using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymWorkout.Application.Interfaces;

namespace GymWorkout.Entity.Foods
{
    public class FoodPlanDetail : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int FoodPlanId { get; set; }
        public int FoodMealId { get; set; }
        public string Note { get; set; }
        
        public int FoodUnitId { get; set; }
        public int Count { get; set; }

        public DateTime CreateDate { get; set; }

        #region Navigation Properties

        [ForeignKey(nameof(FoodPlanId))]
        public FoodPlan FoodPlan { get; set; }

        [ForeignKey(nameof(FoodMealId))]
        public FoodMeal FoodMeal { get; set; }

        [ForeignKey(nameof(FoodUnitId))]
        public FoodUnit FoodUnit { get; set; }

        #endregion
    }
}
