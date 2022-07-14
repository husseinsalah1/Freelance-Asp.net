namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SavePosts", "PostId", "dbo.Posts");
            DropForeignKey("dbo.SavePosts", "UserId", "dbo.Users");
            DropIndex("dbo.SavePosts", new[] { "UserId" });
            DropIndex("dbo.SavePosts", new[] { "PostId" });
            AlterColumn("dbo.SavePosts", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.SavePosts", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.SavePosts", "UserId");
            CreateIndex("dbo.SavePosts", "PostId");
            AddForeignKey("dbo.SavePosts", "PostId", "dbo.Posts", "Id", cascadeDelete: false);
            AddForeignKey("dbo.SavePosts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavePosts", "UserId", "dbo.Users");
            DropForeignKey("dbo.SavePosts", "PostId", "dbo.Posts");
            DropIndex("dbo.SavePosts", new[] { "PostId" });
            DropIndex("dbo.SavePosts", new[] { "UserId" });
            AlterColumn("dbo.SavePosts", "PostId", c => c.Int());
            AlterColumn("dbo.SavePosts", "UserId", c => c.Int());
            CreateIndex("dbo.SavePosts", "PostId");
            CreateIndex("dbo.SavePosts", "UserId");
            AddForeignKey("dbo.SavePosts", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.SavePosts", "PostId", "dbo.Posts", "Id");
        }
    }
}
