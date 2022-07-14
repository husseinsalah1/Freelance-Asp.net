namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateSavePostTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SavePosts", "UserId", "dbo.Users");
            DropIndex("dbo.SavePosts", new[] { "UserId" });
            AlterColumn("dbo.SavePosts", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.SavePosts", "UserId");
            AddForeignKey("dbo.SavePosts", "UserId", "dbo.Users", "Id", cascadeDelete: false);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavePosts", "UserId", "dbo.Users");
            DropIndex("dbo.SavePosts", new[] { "UserId" });
            AlterColumn("dbo.SavePosts", "UserId", c => c.Int());
            CreateIndex("dbo.SavePosts", "UserId");
            AddForeignKey("dbo.SavePosts", "UserId", "dbo.Users", "Id");
        }
    }
}
