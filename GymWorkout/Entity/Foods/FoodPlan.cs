using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymWorkout.Application.Interfaces;
using GymWorkout.Entity.General;

namespace GymWorkout.Entity.Foods
{
    public class FoodPlan : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Note { get; set; }

        public DateTime CreateDate { get; set; }

        #region Navigation Properties

        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        public ICollection<FoodPlanDetail> FoodPlanDetails { get; set; }

        #endregion
    }
}
