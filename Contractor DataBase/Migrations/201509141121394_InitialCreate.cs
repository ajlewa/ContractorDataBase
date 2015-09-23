namespace Contractor_DataBase.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contractors",
                c => new
                    {
                        ContractorId = c.Int(nullable: false, identity: true),
                        ContractorName = c.String(),
                        ManagerName = c.String(),
                        ContactPhone = c.String(),
                        ContactEmail = c.String(),
                        ListId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ContractorId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Contractors");
        }
    }
}
