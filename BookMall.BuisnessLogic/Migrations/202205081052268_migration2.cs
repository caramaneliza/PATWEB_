namespace BookMall.BuisnessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PDbTables",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OwnerId = c.Int(nullable: false),
                        Author = c.String(nullable: false),
                        Title = c.String(nullable: false),
                        Description = c.String(),
                        Genre = c.String(),
                        ImageUrl = c.String(),
                        PdfFile = c.String(),
                        Price = c.Single(nullable: false),
                        Pages = c.Int(nullable: false),
                        ISBN = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.PDbTables");
        }
    }
}
