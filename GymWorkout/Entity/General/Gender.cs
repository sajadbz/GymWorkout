using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using GymWorkout.Application.Interfaces;
using GymWorkout.Entity.Foods;

namespace GymWorkout.Entity.General
{
    public class Gender : IBaseEntity
    {
        [Key]
        public int Id { get; set; }
        [Required,MaxLength(50)]
        public string Title { get; set; }
        [Required, MaxLength(50)]
        public string Display { get; set; }
        public DateTime CreateDate { get; set; }

        #region Navigation Properties
        
        public ICollection<Student> Students { get; set; }

        #endregion
    }


}
