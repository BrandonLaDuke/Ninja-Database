namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Posts", "DateOfBirth");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "DateOfBirth", c => c.DateTime(nullable: false));
        }
    }
}
