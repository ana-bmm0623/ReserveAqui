namespace ReserveAqui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveUsuario : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUserClaims", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.AspNetUserLogins", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.AspNetUserRoles", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Reservas", "UsuarioId", "dbo.Usuarios");
            DropIndex("dbo.Reservas", new[] { "UsuarioId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "Usuario_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "Usuario_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "Usuario_Id" });
            AddColumn("dbo.Reservas", "ApplicationUserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.AspNetUsers", "NomeCompleto", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "CPF", c => c.String(nullable: false, maxLength: 11));
            AddColumn("dbo.AspNetUsers", "Endereco", c => c.String());
            AddColumn("dbo.AspNetUsers", "Telefone", c => c.String());
            AddColumn("dbo.AspNetUsers", "DataNascimento", c => c.DateTime());
            CreateIndex("dbo.Reservas", "ApplicationUserId");
            AddForeignKey("dbo.Reservas", "ApplicationUserId", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Reservas", "UsuarioId");
            DropColumn("dbo.AspNetUserClaims", "Usuario_Id");
            DropColumn("dbo.AspNetUserLogins", "Usuario_Id");
            DropColumn("dbo.AspNetUserRoles", "Usuario_Id");
            DropTable("dbo.Usuarios");
        }
        
        public override void Down()
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
            
            AddColumn("dbo.AspNetUserRoles", "Usuario_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserLogins", "Usuario_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.AspNetUserClaims", "Usuario_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Reservas", "UsuarioId", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.Reservas", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Reservas", new[] { "ApplicationUserId" });
            DropColumn("dbo.AspNetUsers", "DataNascimento");
            DropColumn("dbo.AspNetUsers", "Telefone");
            DropColumn("dbo.AspNetUsers", "Endereco");
            DropColumn("dbo.AspNetUsers", "CPF");
            DropColumn("dbo.AspNetUsers", "NomeCompleto");
            DropColumn("dbo.Reservas", "ApplicationUserId");
            CreateIndex("dbo.AspNetUserRoles", "Usuario_Id");
            CreateIndex("dbo.AspNetUserLogins", "Usuario_Id");
            CreateIndex("dbo.AspNetUserClaims", "Usuario_Id");
            CreateIndex("dbo.Reservas", "UsuarioId");
            AddForeignKey("dbo.Reservas", "UsuarioId", "dbo.Usuarios", "Id");
            AddForeignKey("dbo.AspNetUserRoles", "Usuario_Id", "dbo.Usuarios", "Id");
            AddForeignKey("dbo.AspNetUserLogins", "Usuario_Id", "dbo.Usuarios", "Id");
            AddForeignKey("dbo.AspNetUserClaims", "Usuario_Id", "dbo.Usuarios", "Id");
        }
    }
}
