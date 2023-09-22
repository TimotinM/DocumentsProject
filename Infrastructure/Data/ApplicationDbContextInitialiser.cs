using DocumentsProject.Infrastructure.Data;
using Domain.Constants;
using Domain.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Data;

public static class InitialiserExtensions
{
    public static async Task InitialiseDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var initialiser = scope.ServiceProvider.GetRequiredService<ApplicationDbContextInitialiser>();

        await initialiser.InitialiseAsync();

        await initialiser.SeedAsync();
    }
}

public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole<int>> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole<int>> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            await _context.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole<int>(Roles.Administrator);
        var cedacriOperatorRole = new IdentityRole<int>(Roles.CedacriOperator);
        var bankOperatorRole = new IdentityRole<int>(Roles.BankOperator);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        if (_roleManager.Roles.All(r => r.Name != cedacriOperatorRole.Name))
        {
            await _roleManager.CreateAsync(cedacriOperatorRole);
        }

        if (_roleManager.Roles.All(r => r.Name != bankOperatorRole.Name))
        {
            await _roleManager.CreateAsync(bankOperatorRole);
        }

        // Default users
        var administrator = new ApplicationUser { UserName = "administrator@localhost", Email = "administrator@localhost", Name = "Admin", Surname = "Test"};

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "@Admin1");           
            if (!string.IsNullOrWhiteSpace(administratorRole.Name))
            {
                await _userManager.AddToRolesAsync(administrator, new [] { administratorRole.Name });
            }
        }

        if (_context.DocumentTypes.IsNullOrEmpty())
        {

            var service = new DocumentType()
            {
                Name = "Service",
                IsMacro = true,
                IsDateGrouped = true,
            };
            var sla = new DocumentType()
            {
                Name = "SLA",
                IsMacro = true,
                IsDateGrouped = true,
            };
            var project = new DocumentType()
            {
                Name = "Project",
                IsMacro = true,
                IsDateGrouped = true,
            };
            var network = new DocumentType()
            {
                Name = "Network",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { service }
            };
            var safety = new DocumentType()
            {
                Name = "Safety",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { service }
            };
            var change = new DocumentType()
            {
                Name = "Change",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { service }
            };
            var backup = new DocumentType()
            {
                Name = "Backup",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { service }
            };
            var analysis = new DocumentType()
            {
                Name = "Analysis",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { project }
            };
            var transition = new DocumentType()
            {
                Name = "Transition",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { project }
            };
            var production = new DocumentType()
            {
                Name = "Production",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { project }
            };
            var test = new DocumentType()
            {
                Name = "Test",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { project }
            };
            var monitoring = new DocumentType()
            {
                Name = "Monitoring",
                IsMacro = false,
                IsDateGrouped = true,
                Macro = new List<DocumentType> { project }
            };

            service.Micro.Add(network);
            service.Micro.Add(safety);
            service.Micro.Add(change);
            service.Micro.Add(backup);

            project.Micro.Add(analysis);
            project.Micro.Add(transition);
            project.Micro.Add(production);
            project.Micro.Add(test);
            project.Micro.Add(monitoring);

            await _context.DocumentTypes.AddRangeAsync(new List<DocumentType>()
            {
                service, sla, project, network, safety, change, backup, analysis, transition, production, test, monitoring
            });

            _context.SaveChanges();
        }
    }
}
