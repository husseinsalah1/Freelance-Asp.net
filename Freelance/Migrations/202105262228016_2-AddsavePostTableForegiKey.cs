namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2AddsavePostTableForegiKey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SavePosts", "UserId", c => c.Int());
            AddColumn("dbo.SavePosts", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.SavePosts", "UserId");
            CreateIndex("dbo.SavePosts", "PostId");
            AddForeignKey("dbo.SavePosts", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SavePosts", "UserId", "dbo.Users", "Id");
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
