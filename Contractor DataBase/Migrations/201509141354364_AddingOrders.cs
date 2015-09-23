namespace Contractor_DataBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddingOrders : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderId = c.Int(nullable: false, identity: true),
                        Sum = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InvoiceNumber = c.String(),
                        InvoiceScan = c.Binary(),
                        ClosingDocScan = c.Binary(),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        ContractorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.OrderId)
                .ForeignKey("dbo.Contractors", t => t.ContractorId, cascadeDelete: true)
                .Index(t => t.ContractorId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "ContractorId", "dbo.Contractors");
            DropIndex("dbo.Orders", new[] { "ContractorId" });
            DropTable("dbo.Orders");
        }
    }
}
