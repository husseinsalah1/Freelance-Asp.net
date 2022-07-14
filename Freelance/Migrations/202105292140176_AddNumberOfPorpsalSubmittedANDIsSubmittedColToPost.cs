namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNumberOfPorpsalSubmittedANDIsSubmittedColToPost : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "NumberOfPorpsalSubmitted", c => c.Int(nullable: false));
            AddColumn("dbo.Posts", "isSubmitted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "isSubmitted");
            DropColumn("dbo.Posts", "NumberOfPorpsalSubmitted");
        }
    }
}
