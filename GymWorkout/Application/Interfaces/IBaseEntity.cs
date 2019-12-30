using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GymWorkout.Application.Interfaces
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime CreateDate { get; set; }
    }
}
