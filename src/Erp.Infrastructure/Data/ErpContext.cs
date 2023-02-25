using Erp.Core.Entities.Account;
using Erp.Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Erp.Infrastructure.Data;

public class ErpContext : IdentityDbContext<User, Role, string>
{
    public ErpContext(DbContextOptions<ErpContext> options) : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) =>
        optionsBuilder.UseSnakeCaseNamingConvention();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        ConfigureModelBuilders(builder);
    }

    private void ConfigureModelBuilders(ModelBuilder builder)
    {
        builder.ConvertIdentityToPostgresSQLNamingConventions();
        builder.Entity<Company>(entity => { entity.ToTable("company"); });
        builder.Entity<CompanyJoinCode>(entity => { entity.ToTable("company_join_code"); });
        builder.Entity<CompanyJoinRequest>(entity => { entity.ToTable("company_join_request"); });
        builder.Entity<UserCompany>(entity => { entity.ToTable("user_company"); });
    }

    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyJoinCode> CompanyJoinCodes { get; set; }
    public DbSet<CompanyJoinRequest> CompanyJoinRequests { get; set; }
    public DbSet<UserCompany> UserCompanies { get; set; }
}