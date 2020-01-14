namespace GymWorkout.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Update_Student_HeightType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Students", "Height", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Students", "Height", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
