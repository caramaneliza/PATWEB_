namespace BookMall.BuisnessLogic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_update : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PDbTables", "PdfUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PDbTables", "PdfUrl");
        }
    }
}
