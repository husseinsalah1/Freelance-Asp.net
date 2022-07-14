namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class test2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PostSubmitteds", "PostId", "dbo.Posts");
            DropForeignKey("dbo.PostSubmitteds", "UserId", "dbo.Users");
            DropIndex("dbo.PostSubmitteds", new[] { "UserId" });
            DropIndex("dbo.PostSubmitteds", new[] { "PostId" });
            AlterColumn("dbo.PostSubmitteds", "UserId", c => c.Int(nullable: false));
            AlterColumn("dbo.PostSubmitteds", "PostId", c => c.Int(nullable: false));
            CreateIndex("dbo.PostSubmitteds", "UserId");
            CreateIndex("dbo.PostSubmitteds", "PostId");
            AddForeignKey("dbo.PostSubmitteds", "PostId", "dbo.Posts", "Id", cascadeDelete: false);
            AddForeignKey("dbo.PostSubmitteds", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }

        public override void Down()
        {
            DropForeignKey("dbo.PostSubmitteds", "UserId", "dbo.Users");
            DropForeignKey("dbo.PostSubmitteds", "PostId", "dbo.Posts");
            DropIndex("dbo.PostSubmitteds", new[] { "PostId" });
            DropIndex("dbo.PostSubmitteds", new[] { "UserId" });
            AlterColumn("dbo.PostSubmitteds", "PostId", c => c.Int());
            AlterColumn("dbo.PostSubmitteds", "UserId", c => c.Int());
            CreateIndex("dbo.PostSubmitteds", "PostId");
            CreateIndex("dbo.PostSubmitteds", "UserId");
            AddForeignKey("dbo.PostSubmitteds", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.PostSubmitteds", "PostId", "dbo.Posts", "Id");
        }
    }
}