using Inva.LawMax.Cases;
using Inva.LawMax.Hearings;
using Inva.LawMax.LawyerCases;
using Inva.LawMax.Lawyers;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;
using Volo.Abp.AuditLogging.EntityFrameworkCore;
using Volo.Abp.BackgroundJobs.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Volo.Abp.FeatureManagement.EntityFrameworkCore;
using Volo.Abp.Identity;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.OpenIddict.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;
using Volo.Abp.SettingManagement.EntityFrameworkCore;
using Volo.Abp.TenantManagement;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Inva.LawMax.EntityFrameworkCore;

[ReplaceDbContext(typeof(IIdentityDbContext))]
[ReplaceDbContext(typeof(ITenantManagementDbContext))]
[ConnectionStringName("Default")]
public class LawMaxDbContext :
    AbpDbContext<LawMaxDbContext>,
    IIdentityDbContext,
    ITenantManagementDbContext
{
    /* Add DbSet properties for your Aggregate Roots / Entities here. */

    #region Entities from the modules

    /* Notice: We only implemented IIdentityDbContext and ITenantManagementDbContext
     * and replaced them for this DbContext. This allows you to perform JOIN
     * queries for the entities of these modules over the repositories easily. You
     * typically don't need that for other modules. But, if you need, you can
     * implement the DbContext interface of the needed module and use ReplaceDbContext
     * attribute just like IIdentityDbContext and ITenantManagementDbContext.
     *
     * More info: Replacing a DbContext of a module ensures that the related module
     * uses this DbContext on runtime. Otherwise, it will use its own DbContext class.
     */

    //Identity
    public DbSet<IdentityUser> Users { get; set; }
    public DbSet<IdentityRole> Roles { get; set; }
    public DbSet<IdentityClaimType> ClaimTypes { get; set; }
    public DbSet<OrganizationUnit> OrganizationUnits { get; set; }
    public DbSet<IdentitySecurityLog> SecurityLogs { get; set; }
    public DbSet<IdentityLinkUser> LinkUsers { get; set; }
    public DbSet<IdentityUserDelegation> UserDelegations { get; set; }

    // Tenant Management
    public DbSet<Tenant> Tenants { get; set; }
    public DbSet<TenantConnectionString> TenantConnectionStrings { get; set; }

    //
    public DbSet<Lawyer> Lawyers { get; set; }
    public DbSet<Case> Cases { get; set; }
    public DbSet<Hearing> Hearings { get; set; }
    public DbSet<LawyerCase> LawyerCases { get; set; }


    #endregion

    public LawMaxDbContext(DbContextOptions<LawMaxDbContext> options)
    : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        /* Include modules to your migration db context */

        modelBuilder.ConfigureSettingManagement();
        modelBuilder.ConfigureBackgroundJobs();
        modelBuilder.ConfigureAuditLogging();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureOpenIddict();
        modelBuilder.ConfigurePermissionManagement();
        modelBuilder.ConfigureFeatureManagement();
        modelBuilder.ConfigureTenantManagement();

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(LawMaxConsts.DbTablePrefix + "YourEntities", LawMaxConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});

        // Configure the many-to-many relationship between Lawyer and Case
        modelBuilder.Entity<LawyerCase>(b =>
        {
            b.ToTable("LawyerCases");
            b.ConfigureByConvention();

            b.HasKey(lc => new { lc.LawyerId, lc.CaseId });

            b.HasOne(lc => lc.Lawyer)
             .WithMany(l => l.LawyerCases)
             .HasForeignKey(lc => lc.LawyerId);

            b.HasOne(lc => lc.Case)
             .WithMany(c => c.LawyerCases)
             .HasForeignKey(lc => lc.CaseId);
        });

        modelBuilder.Entity<Case>(b =>
        {
            b.ToTable("Cases");
            b.ConfigureByConvention();

            b.HasMany(c => c.Hearings)
             .WithOne(h => h.Case)
             .HasForeignKey(h => h.CaseId)
             .IsRequired();
        });

        modelBuilder.Entity<Hearing>(b =>
        {
            b.ToTable("Hearings");
            b.ConfigureByConvention();
        });

        modelBuilder.Entity<Lawyer>(b =>
        {
            b.ToTable("Lawyers");
            b.ConfigureByConvention();
        });
    }
}
