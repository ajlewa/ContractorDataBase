namespace Contractor_DataBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class OrderRefactoring : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "PositionsQty_position", c => c.String());
            AddColumn("dbo.Orders", "PositionsQty_qty", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "PositionsQty_qty");
            DropColumn("dbo.Orders", "PositionsQty_position");
        }
    }
}
