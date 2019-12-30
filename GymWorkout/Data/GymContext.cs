using GymWorkout.Entity.Foods;
using GymWorkout.Entity.General;
using GymWorkout.Entity.Workouts;
using System.Data.Entity;

namespace GymWorkout.Data
{
    public class GymContext : DbContext
    {
        public GymContext()
        //: base("Data Source=.\\SQLEXPRESS;AttachDbFilename=" + System.AppDomain.CurrentDomain.BaseDirectory + "Data\\dbGymWorkout.mdf;integrated security=True;User Instance=True")
        //: base("Data Source=(localDB)\\MSSQLLocalDB;AttachDbFilename=" + System.AppDomain.CurrentDomain.BaseDirectory + "Data\\localdbGymWorkout.mdf;integrated security=True;Connect Timeout=30")
        : base("name=GymConnection")
        {

        }

        public virtual DbSet<Student> Students { get; set; }
        public virtual DbSet<Gender> Genders { get; set; }

        public virtual DbSet<Workout> Workouts { get; set; }
        public virtual DbSet<WorkoutDetail> WorkoutDetails { get; set; }
        public virtual DbSet<WorkoutTitle> WorkoutTitles { get; set; }

        public virtual DbSet<FoodPlan> FoodPlans { get; set; }
        public virtual DbSet<FoodPlanDetail> FoodPlan { get; set; }
        public virtual DbSet<FoodMeal> FoodMeals { get; set; }
        public virtual DbSet<FoodUnit> FoodUnits { get; set; }
    }
}