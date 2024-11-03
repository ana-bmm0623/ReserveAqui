using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ReserveAqui.Models
{
    // Classe de usuário estendida com propriedades adicionais
    public class ApplicationUser : IdentityUser
    {
        [Required]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required]
        [Display(Name = "CPF")]
        [StringLength(11, ErrorMessage = "O CPF deve ter 11 dígitos", MinimumLength = 11)]
        public string CPF { get; set; }

        [Display(Name = "Endereço")]
        public string Endereco { get; set; }

        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Telefone")]
        public string Telefone { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        // Relacionamento com Reservas (1:N - um usuário pode ter várias reservas)
        public virtual ICollection<Reserva> Reservas { get; set; } = new List<Reserva>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public DbSet<Hospede> Hospedes { get; set; }
        public DbSet<Hotel> Hoteis { get; set; }
        public DbSet<Quarto> Quartos { get; set; }
        public DbSet<Reserva> Reservas { get; set; }
        public DbSet<ServicoAdicional> ServicosAdicionais { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<ReservaServicoAdicional> ReservaServicosAdicionais { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReservaServicoAdicional>()
                .HasKey(rsa => new { rsa.ReservaId, rsa.ServicoAdicionalId });

            modelBuilder.Entity<Reserva>()
                .HasRequired(r => r.Quarto)
                .WithMany()
                .HasForeignKey(r => r.QuartoId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Reserva>()
                .HasRequired(r => r.Hospede)
                .WithMany()
                .HasForeignKey(r => r.HospedeId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<ReservaServicoAdicional>()
                .HasRequired(rsa => rsa.Reserva)
                .WithMany(r => r.ReservaServicosAdicionais)
                .HasForeignKey(rsa => rsa.ReservaId);

            modelBuilder.Entity<ReservaServicoAdicional>()
                .HasRequired(rsa => rsa.ServicoAdicional)
                .WithMany(sa => sa.ReservaServicosAdicionais)
                .HasForeignKey(rsa => rsa.ServicoAdicionalId);

            modelBuilder.Entity<Reserva>()
                .HasRequired(r => r.ApplicationUser)
                .WithMany(u => u.Reservas)
                .HasForeignKey(r => r.ApplicationUserId)
                .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}
