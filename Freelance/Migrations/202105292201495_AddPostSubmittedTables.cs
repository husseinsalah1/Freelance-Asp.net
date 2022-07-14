namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostSubmittedTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostSubmitteds",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostSubmitteds", "UserId", "dbo.Users");
            DropForeignKey("dbo.PostSubmitteds", "PostId", "dbo.Posts");
            DropIndex("dbo.PostSubmitteds", new[] { "PostId" });
            DropIndex("dbo.PostSubmitteds", new[] { "UserId" });
            DropTable("dbo.PostSubmitteds");
        }
    }
}
