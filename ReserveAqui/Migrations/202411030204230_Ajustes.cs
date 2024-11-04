namespace ReserveAqui.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class Ajustes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quartos", "Preco", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }

        public override void Down()
        {
            DropColumn("dbo.Quartos", "Preco");
        }
    }
}
