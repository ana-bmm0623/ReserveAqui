namespace ReserveAqui.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddingUser : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Usuarios",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    NomeCompleto = c.String(nullable: false),
                    CPF = c.String(nullable: false, maxLength: 11),
                    Endereco = c.String(),
                    Telefone = c.String(),
                    DataNascimento = c.DateTime(),
                    Email = c.String(),
                    EmailConfirmed = c.Boolean(nullable: false),
                    PasswordHash = c.String(),
                    SecurityStamp = c.String(),
                    PhoneNumber = c.String(),
                    PhoneNumberConfirmed = c.Boolean(nullable: false),
                    TwoFactorEnabled = c.Boolean(nullable: false),
                    LockoutEndDateUtc = c.DateTime(),
                    LockoutEnabled = c.Boolean(nullable: false),
                    AccessFailedCount = c.Int(nullable: false),
                    UserName = c.String(),
                })
                .PrimaryKey(t => t.Id);

            AddColumn("dbo.Reservas", "UsuarioId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUserRoles", "Usuario_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserClaims", "Usuario_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "Usuario_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Reservas", "UsuarioId");
            CreateIndex("dbo.AspNetUserClaims", "Usuario_Id");
            CreateIndex("dbo.AspNetUserLogins", "Usuario_Id");
            CreateIndex("dbo.AspNetUserRoles", "Usuario_Id");
            AddForeignKey("dbo.AspNetUserClaims", "Usuario_Id", "dbo.Usuarios", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "Usuario_Id", "dbo.Usuarios", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "Usuario_Id", "dbo.Usuarios", "Id");
            AddForeignKey("dbo.Reservas", "UsuarioId", "dbo.Usuarios", "Id");
        }

        public override void Down()
        {
            DropForeignKey("dbo.Reservas", "UsuarioId", "dbo.Usuarios");
            DropForeignKey("dbo.AspNetUserRoles", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.AspNetUserLogins", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.AspNetUserClaims", "Usuario_Id", "dbo.Usuarios");
            DropIndex("dbo.AspNetUserRoles", new[] { "Usuario_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Usuario_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "Usuario_Id" });
            DropIndex("dbo.Reservas", new[] { "UsuarioId" });
            DropColumn("dbo.AspNetUserLogins", "Usuario_Id");
            DropColumn("dbo.AspNetUserClaims", "Usuario_Id");
            DropColumn("dbo.AspNetUserRoles", "Usuario_Id");
            DropColumn("dbo.Reservas", "UsuarioId");
            DropTable("dbo.Usuarios");
        }
    }
}
