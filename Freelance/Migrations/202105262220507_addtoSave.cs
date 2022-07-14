namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addtoSave : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SavePosts", "PostId", "dbo.Posts");
            DropForeignKey("dbo.SavePosts", "UserId", "dbo.Users");
            DropIndex("dbo.SavePosts", new[] { "UserId" });
            DropIndex("dbo.SavePosts", new[] { "PostId" });
            DropTable("dbo.SavePosts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.SavePosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateIndex("dbo.SavePosts", "PostId");
            CreateIndex("dbo.SavePosts", "UserId");
            AddForeignKey("dbo.SavePosts", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SavePosts", "PostId", "dbo.Posts", "Id", cascadeDelete: true);
        }
    }
}
