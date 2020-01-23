namespace GymWorkout.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWorkoutWorkoutDetailTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Workouts", "Title", c => c.String());
            AlterColumn("dbo.WorkoutDetails", "Count", c => c.String(maxLength: 150));
            DropColumn("dbo.Workouts", "Note");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Workouts", "Note", c => c.String());
            AlterColumn("dbo.WorkoutDetails", "Count", c => c.Int(nullable: false));
            DropColumn("dbo.Workouts", "Title");
        }
    }
}
