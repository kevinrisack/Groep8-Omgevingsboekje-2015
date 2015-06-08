namespace DigitaalOmgevingsboek.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changedAspNetUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsPending", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "IsPending");
        }
    }
}
