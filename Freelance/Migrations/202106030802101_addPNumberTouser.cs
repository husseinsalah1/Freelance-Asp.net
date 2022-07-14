namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPNumberTouser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PNumber", c => c.String());
            DropColumn("dbo.Users", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ConfirmPassword", c => c.String(nullable: false, maxLength: 255));
            DropColumn("dbo.Users", "PNumber");
        }
    }
}
