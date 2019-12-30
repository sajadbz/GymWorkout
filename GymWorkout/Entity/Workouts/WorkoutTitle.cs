using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using GymWorkout.Application.Interfaces;

namespace GymWorkout.Entity.Workouts
{
    public class WorkoutTitle : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Title { get; set; }
        public DateTime CreateDate { get; set; }


        #region Navigation Properties

        public ICollection<WorkoutDetail> WorkoutDetails { get; set; }

        #endregion
    }
}
