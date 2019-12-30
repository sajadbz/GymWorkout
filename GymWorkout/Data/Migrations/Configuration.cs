using System.Data.Entity.Migrations;
using System.Linq;

namespace GymWorkout.Data.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<GymWorkout.Data.GymContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GymWorkout.Data.GymContext context)
        {
            //  This method will be called after migrating to the latest version.

            if (!context.FoodMeals.Any())
                context.FoodMeals.AddRange(Seeds.FoodMeals);
            if (!context.FoodUnits.Any())
                context.FoodUnits.AddRange(Seeds.FoodUnits);
            if (!context.Genders.Any())
                context.Genders.AddRange(Seeds.Genders);
        }
    }
}
