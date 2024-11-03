using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using ReserveAqui.Models;
using System;
using System.Data.Entity.Validation;
using System.Threading.Tasks;

[assembly: OwinStartup(typeof(ReserveAqui.Startup))]
namespace ReserveAqui
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesAndAdminUser().Wait();
        }

        private async Task CreateRolesAndAdminUser()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                    var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

                    // Verifica e cria o papel "Admin" se não existir
                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Verifica e cria o papel "Hospede" se não existir
                    if (!await roleManager.RoleExistsAsync("Hospede"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Hospede"));
                    }

                    // Verifica se o usuário admin já existe
                    var adminUser = await userManager.FindByEmailAsync("admin@reservaqui.com");
                    if (adminUser == null)
                    {
                        adminUser = new ApplicationUser
                        {
                            UserName = "admin@reservaqui.com",
                            Email = "admin@reservaqui.com",
                            NomeCompleto = "Administrador",
                            CPF = "12345678901", // Insira um CPF fictício
                            Telefone = "123456789",
                            DataNascimento = new DateTime(1980, 1, 1)
                        };
                        var result = await userManager.CreateAsync(adminUser, "Admin@123");

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser.Id, "Admin");
                        }
                    }

                    context.SaveChanges();
                }
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var validationError in ex.EntityValidationErrors)
                {
                    Console.WriteLine($"Entidade com erro: {validationError.Entry.Entity.GetType().Name}");
                    foreach (var error in validationError.ValidationErrors)
                    {
                        Console.WriteLine($" - Campo: {error.PropertyName}, Erro: {error.ErrorMessage}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro geral: {ex.Message}");
            }
        }
    }
}
