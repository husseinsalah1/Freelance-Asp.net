namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeginKeyToSavepOst : DbMigration
    {
        public override void Up()
        {
        
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavePosts", "UserId", "dbo.Users");
            DropForeignKey("dbo.SavePosts", "PostId", "dbo.Posts");
            DropIndex("dbo.SavePosts", new[] { "PostId" });
            DropIndex("dbo.SavePosts", new[] { "UserId" });
            DropColumn("dbo.SavePosts", "PostId");
            DropColumn("dbo.SavePosts", "UserId");
        }
    }
}
