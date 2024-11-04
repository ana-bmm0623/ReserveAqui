namespace ReserveAqui.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class NomeDaMigracao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hospedes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    NomeCompleto = c.String(),
                    CPF = c.String(),
                    RG = c.String(),
                    Email = c.String(),
                    Telefone = c.String(),
                    Endereco = c.String(),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Reservas",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    QuartoId = c.Int(nullable: false),
                    HospedeId = c.Int(nullable: false),
                    QuantidadePessoas = c.Int(nullable: false),
                    DataEntrada = c.DateTime(nullable: false),
                    DataSaida = c.DateTime(nullable: false),
                    CheckInRealizado = c.Boolean(nullable: false),
                    CheckOutRealizado = c.Boolean(nullable: false),
                    Cancelada = c.Boolean(nullable: false),
                    Quarto_Id = c.Int(),
                    Hospede_Id = c.Int(),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hospedes", t => t.HospedeId)
                .ForeignKey("dbo.Quartos", t => t.Quarto_Id)
                .ForeignKey("dbo.Quartos", t => t.QuartoId)
                .ForeignKey("dbo.Hospedes", t => t.Hospede_Id)
                .Index(t => t.QuartoId)
                .Index(t => t.HospedeId)
                .Index(t => t.Quarto_Id)
                .Index(t => t.Hospede_Id);

            CreateTable(
                "dbo.Quartos",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    NumeroIdentificacao = c.String(nullable: false),
                    CapacidadeMaxima = c.Int(nullable: false),
                    Disponibilidade = c.Boolean(nullable: false),
                    HotelId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Hotels", t => t.HotelId, cascadeDelete: true)
                .Index(t => t.HotelId);

            CreateTable(
                "dbo.Hotels",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nome = c.String(nullable: false),
                    Endereço = c.String(nullable: false),
                    Email = c.String(),
                    Telefone = c.String(nullable: false),
                    Descricao = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.ReservaServicoAdicionals",
                c => new
                {
                    ReservaId = c.Int(nullable: false),
                    ServicoAdicionalId = c.Int(nullable: false),
                })
                .PrimaryKey(t => new { t.ReservaId, t.ServicoAdicionalId })
                .ForeignKey("dbo.Reservas", t => t.ReservaId, cascadeDelete: true)
                .ForeignKey("dbo.ServicoAdicionals", t => t.ServicoAdicionalId, cascadeDelete: true)
                .Index(t => t.ReservaId)
                .Index(t => t.ServicoAdicionalId);

            CreateTable(
                "dbo.ServicoAdicionals",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    Nome = c.String(),
                    Descricao = c.String(),
                    Valor = c.Decimal(nullable: false, precision: 18, scale: 2),
                })
                .PrimaryKey(t => t.Id);

            CreateTable(
                "dbo.Pagamentoes",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    ValorTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    DataPagamento = c.DateTime(nullable: false),
                    MetodoPagamento = c.Int(nullable: false),
                    ReservaId = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Reservas", t => t.ReservaId, cascadeDelete: true)
                .Index(t => t.ReservaId);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Pagamentoes", "ReservaId", "dbo.Reservas");
            DropForeignKey("dbo.Reservas", "Hospede_Id", "dbo.Hospedes");
            DropForeignKey("dbo.ReservaServicoAdicionals", "ServicoAdicionalId", "dbo.ServicoAdicionals");
            DropForeignKey("dbo.ReservaServicoAdicionals", "ReservaId", "dbo.Reservas");
            DropForeignKey("dbo.Reservas", "QuartoId", "dbo.Quartos");
            DropForeignKey("dbo.Reservas", "Quarto_Id", "dbo.Quartos");
            DropForeignKey("dbo.Quartos", "HotelId", "dbo.Hotels");
            DropForeignKey("dbo.Reservas", "HospedeId", "dbo.Hospedes");
            DropIndex("dbo.Pagamentoes", new[] { "ReservaId" });
            DropIndex("dbo.ReservaServicoAdicionals", new[] { "ServicoAdicionalId" });
            DropIndex("dbo.ReservaServicoAdicionals", new[] { "ReservaId" });
            DropIndex("dbo.Quartos", new[] { "HotelId" });
            DropIndex("dbo.Reservas", new[] { "Hospede_Id" });
            DropIndex("dbo.Reservas", new[] { "Quarto_Id" });
            DropIndex("dbo.Reservas", new[] { "HospedeId" });
            DropIndex("dbo.Reservas", new[] { "QuartoId" });
            DropTable("dbo.Pagamentoes");
            DropTable("dbo.ServicoAdicionals");
            DropTable("dbo.ReservaServicoAdicionals");
            DropTable("dbo.Hotels");
            DropTable("dbo.Quartos");
            DropTable("dbo.Reservas");
            DropTable("dbo.Hospedes");
        }
    }
}
