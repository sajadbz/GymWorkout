using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymWorkout.Application.Interfaces;
using GymWorkout.Entity.General;

namespace GymWorkout.Entity.Workouts
{
    public class Workout : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int StudentId { get; set; }
        public string Title { get; set; }

        public DateTime CreateDate { get; set; }

        #region Navigation Properties
        
        [ForeignKey(nameof(StudentId))]
        public Student Student { get; set; }

        public ICollection<WorkoutDetail> WorkoutDetails { get; set; }

        #endregion
    }
}
