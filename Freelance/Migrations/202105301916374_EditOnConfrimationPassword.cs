namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EditOnConfrimationPassword : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Users", "ConfirmPassword");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ConfirmPassword", c => c.String(nullable: false, maxLength: 255));
        }
    }
}
