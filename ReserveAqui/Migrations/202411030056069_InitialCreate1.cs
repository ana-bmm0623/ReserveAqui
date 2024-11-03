namespace ReserveAqui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quartos", "ImagemUrl", c => c.String());
            AddColumn("dbo.Hotels", "ImagemUrl", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Hotels", "ImagemUrl");
            DropColumn("dbo.Quartos", "ImagemUrl");
        }
    }
}
