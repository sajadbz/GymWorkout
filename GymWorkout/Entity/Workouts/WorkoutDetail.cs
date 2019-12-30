using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymWorkout.Application.Interfaces;

namespace GymWorkout.Entity.Workouts
{
    public class WorkoutDetail : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int WorkoutId { get; set; }
        public int WorkoutTitleId { get; set; }

        public int Day { get; set; }
        public int SetNumbers { get; set; }
        public int Count { get; set; }

        public DateTime CreateDate { get; set; }

        #region Navigation Properties
        [ForeignKey(nameof(WorkoutId))]
        public Workout Workout { get; set; }

        [ForeignKey(nameof(WorkoutTitleId))]
        public WorkoutTitle WorkoutTitle { get; set; }
        #endregion
    }
}
