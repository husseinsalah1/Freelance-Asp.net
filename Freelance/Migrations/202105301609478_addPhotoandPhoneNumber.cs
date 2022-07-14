namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPhotoandPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PhoneNumber", c => c.Int(nullable: false));
            AddColumn("dbo.Users", "Image", c => c.String());
            DropColumn("dbo.Users", "IsFreeLancer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "IsFreeLancer", c => c.Boolean(nullable: false));
            DropColumn("dbo.Users", "Image");
            DropColumn("dbo.Users", "PhoneNumber");
        }
    }
}
