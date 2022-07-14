namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addrating : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "rate", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "rate");
        }
    }
}
