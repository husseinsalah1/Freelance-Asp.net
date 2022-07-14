namespace Freelance.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePostSubmittedTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostSubmitteds", "isAccepted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostSubmitteds", "isAccepted");
        }
    }
}
