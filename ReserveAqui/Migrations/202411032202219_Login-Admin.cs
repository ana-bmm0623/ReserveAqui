namespace ReserveAqui.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class LoginAdmin : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ServicoAdicionals", "Valor", c => c.Double(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.ServicoAdicionals", "Valor", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
