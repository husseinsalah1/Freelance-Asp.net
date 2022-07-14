namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdNumOfRatesPeop : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "NumberOfrates", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "NumberOfrates");
        }
    }
}
