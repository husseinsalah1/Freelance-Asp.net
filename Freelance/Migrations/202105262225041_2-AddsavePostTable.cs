namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2AddsavePostTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SavePosts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.SavePosts");
        }
    }
}
