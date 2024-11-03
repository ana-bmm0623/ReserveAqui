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

                    // Verifica se o papel Admin existe, caso contrário cria-o
                    if (!await roleManager.RoleExistsAsync("Admin"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Admin"));
                    }

                    // Verifica se o papel Hospede existe, caso contrário cria-o
                    if (!await roleManager.RoleExistsAsync("Hospede"))
                    {
                        await roleManager.CreateAsync(new IdentityRole("Hospede"));
                    }

                    // Cria um usuário admin, caso não exista
                    var adminUser = await userManager.FindByEmailAsync("admin@reservaqui.com");
                    if (adminUser == null)
                    {
                        adminUser = new ApplicationUser { UserName = "Administrador", Email = "admin@reservaqui.com" };
                        var result = await userManager.CreateAsync(adminUser, "Admin@123");

                        if (result.Succeeded)
                        {
                            await userManager.AddToRoleAsync(adminUser.Id, "Admin");
                        }
                        else
                        {
                            // Exibe erros de criação do usuário
                            foreach (var error in result.Errors)
                            {
                                Console.WriteLine($"Erro ao criar usuário admin: {error}");
                            }
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
