namespace Contractor_DataBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Contractors", "ListId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Contractors", "ListId", c => c.Int(nullable: false));
        }
    }
}
