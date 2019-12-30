using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymWorkout.Application.Interfaces;
using GymWorkout.Entity.Foods;
using GymWorkout.Entity.Workouts;

namespace GymWorkout.Entity.General
{
    public class Student : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        public int GenderId { get; set; }
        [Required, MaxLength(50)]
        public string Name { get; set; }
        [Required, MaxLength(100)]
        public string Family { get; set; }
        public decimal Weight { get; set; }
        public decimal Height { get; set; }
        [MaxLength(20)]
        public string Mobile { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }

        public DateTime? BirthDate { get; set; }
        public DateTime CreateDate { get; set; }

        #region Readonly Properties

        public string FullName => $"{Name} {Family}";
        public int Age => BirthDate.HasValue ? DateTime.Now.Year - BirthDate.Value.Year : 0;

        #endregion

        #region Navigation Properties

        [ForeignKey(nameof(GenderId))]
        public Gender Gender { get; set; }
        public ICollection<Workout> Workouts { get; set; }
        public ICollection<FoodPlan> FoodPlans { get; set; }

        #endregion
    }
}
