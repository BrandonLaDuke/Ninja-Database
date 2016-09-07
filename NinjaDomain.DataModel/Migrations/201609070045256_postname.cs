namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class postname : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "TopicSubject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "TopicSubject");
        }
    }
}
