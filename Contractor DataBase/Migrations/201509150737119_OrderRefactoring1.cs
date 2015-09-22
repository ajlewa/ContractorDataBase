namespace Contractor_DataBase.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class OrderRefactoring1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PositionsQties",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        position = c.String(),
                        qty = c.Int(nullable: false),
                        Order_OrderId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.Order_OrderId)
                .Index(t => t.Order_OrderId);
            
            DropColumn("dbo.Orders", "PositionsQty_position");
            DropColumn("dbo.Orders", "PositionsQty_qty");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "PositionsQty_qty", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "PositionsQty_position", c => c.String());
            DropForeignKey("dbo.PositionsQties", "Order_OrderId", "dbo.Orders");
            DropIndex("dbo.PositionsQties", new[] { "Order_OrderId" });
            DropTable("dbo.PositionsQties");
        }
    }
}
