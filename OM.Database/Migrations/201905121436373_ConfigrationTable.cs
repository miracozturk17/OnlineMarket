namespace OM.Database.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class ConfigrationTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Configrations",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Configrations");
        }
    }
}
