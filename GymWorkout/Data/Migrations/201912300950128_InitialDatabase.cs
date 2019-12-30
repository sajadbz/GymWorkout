namespace GymWorkout.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDatabase : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FoodMeals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodPlanDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FoodPlanId = c.Int(nullable: false),
                        FoodMealId = c.Int(nullable: false),
                        Note = c.String(),
                        FoodUnitId = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FoodMeals", t => t.FoodMealId, cascadeDelete: true)
                .ForeignKey("dbo.FoodPlans", t => t.FoodPlanId, cascadeDelete: true)
                .ForeignKey("dbo.FoodUnits", t => t.FoodUnitId, cascadeDelete: true)
                .Index(t => t.FoodPlanId)
                .Index(t => t.FoodMealId)
                .Index(t => t.FoodUnitId);
            
            CreateTable(
                "dbo.FoodPlans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Note = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        GenderId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 50),
                        Family = c.String(nullable: false, maxLength: 100),
                        Weight = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Height = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Mobile = c.String(maxLength: 20),
                        Phone = c.String(maxLength: 20),
                        BirthDate = c.DateTime(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Genders", t => t.GenderId, cascadeDelete: true)
                .Index(t => t.GenderId);
            
            CreateTable(
                "dbo.Genders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        Display = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Workouts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        Note = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.StudentId, cascadeDelete: true)
                .Index(t => t.StudentId);
            
            CreateTable(
                "dbo.WorkoutDetails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        WorkoutId = c.Int(nullable: false),
                        WorkoutTitleId = c.Int(nullable: false),
                        Day = c.Int(nullable: false),
                        SetNumbers = c.Int(nullable: false),
                        Count = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Workouts", t => t.WorkoutId, cascadeDelete: true)
                .ForeignKey("dbo.WorkoutTitles", t => t.WorkoutTitleId, cascadeDelete: true)
                .Index(t => t.WorkoutId)
                .Index(t => t.WorkoutTitleId);
            
            CreateTable(
                "dbo.WorkoutTitles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FoodUnits",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 50),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FoodPlanDetails", "FoodUnitId", "dbo.FoodUnits");
            DropForeignKey("dbo.WorkoutDetails", "WorkoutTitleId", "dbo.WorkoutTitles");
            DropForeignKey("dbo.WorkoutDetails", "WorkoutId", "dbo.Workouts");
            DropForeignKey("dbo.Workouts", "StudentId", "dbo.Students");
            DropForeignKey("dbo.Students", "GenderId", "dbo.Genders");
            DropForeignKey("dbo.FoodPlans", "StudentId", "dbo.Students");
            DropForeignKey("dbo.FoodPlanDetails", "FoodPlanId", "dbo.FoodPlans");
            DropForeignKey("dbo.FoodPlanDetails", "FoodMealId", "dbo.FoodMeals");
            DropIndex("dbo.WorkoutDetails", new[] { "WorkoutTitleId" });
            DropIndex("dbo.WorkoutDetails", new[] { "WorkoutId" });
            DropIndex("dbo.Workouts", new[] { "StudentId" });
            DropIndex("dbo.Students", new[] { "GenderId" });
            DropIndex("dbo.FoodPlans", new[] { "StudentId" });
            DropIndex("dbo.FoodPlanDetails", new[] { "FoodUnitId" });
            DropIndex("dbo.FoodPlanDetails", new[] { "FoodMealId" });
            DropIndex("dbo.FoodPlanDetails", new[] { "FoodPlanId" });
            DropTable("dbo.FoodUnits");
            DropTable("dbo.WorkoutTitles");
            DropTable("dbo.WorkoutDetails");
            DropTable("dbo.Workouts");
            DropTable("dbo.Genders");
            DropTable("dbo.Students");
            DropTable("dbo.FoodPlans");
            DropTable("dbo.FoodPlanDetails");
            DropTable("dbo.FoodMeals");
        }
    }
}
