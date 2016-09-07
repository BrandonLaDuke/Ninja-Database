namespace NinjaDomain.DataModel.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class posts : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ninjas", "ClanId", "dbo.Clans");
            DropForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas");
            DropIndex("dbo.Ninjas", new[] { "ClanId" });
            DropIndex("dbo.NinjaEquipments", new[] { "Ninja_Id" });
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ServedInOniwaban = c.Boolean(nullable: false),
                        TopicId = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Topics", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.NinjaEquipments", "Post_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.NinjaEquipments", "Post_Id");
            AddForeignKey("dbo.NinjaEquipments", "Post_Id", "dbo.Posts", "Id", cascadeDelete: true);
            DropColumn("dbo.NinjaEquipments", "Ninja_Id");
            DropTable("dbo.Clans");
            DropTable("dbo.Ninjas");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Ninjas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ServedInOniwaban = c.Boolean(nullable: false),
                        ClanId = c.Int(nullable: false),
                        DateOfBirth = c.DateTime(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Clans",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClanName = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.NinjaEquipments", "Ninja_Id", c => c.Int(nullable: false));
            DropForeignKey("dbo.NinjaEquipments", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "TopicId", "dbo.Topics");
            DropIndex("dbo.Posts", new[] { "TopicId" });
            DropIndex("dbo.NinjaEquipments", new[] { "Post_Id" });
            DropColumn("dbo.NinjaEquipments", "Post_Id");
            DropTable("dbo.Topics");
            DropTable("dbo.Posts");
            CreateIndex("dbo.NinjaEquipments", "Ninja_Id");
            CreateIndex("dbo.Ninjas", "ClanId");
            AddForeignKey("dbo.NinjaEquipments", "Ninja_Id", "dbo.Ninjas", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Ninjas", "ClanId", "dbo.Clans", "Id", cascadeDelete: true);
        }
    }
}
