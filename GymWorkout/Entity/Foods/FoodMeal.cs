using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GymWorkout.Application.Interfaces;

namespace GymWorkout.Entity.Foods
{
    public class FoodMeal : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }


        #region Navigation Properties
        
        public ICollection<FoodPlanDetail> FoodPlanDetails { get; set; }

        #endregion
    }
}
