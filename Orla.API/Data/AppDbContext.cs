using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Orla.Api.Models;
using Orla.Core.Models.Youtube;
using System.Reflection;
namespace Orla.Api.Data;


public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<User, IdentityRole<long>, long, IdentityUserClaim<long>,
        IdentityUserRole<long>,
        IdentityUserLogin<long>,
        IdentityRoleClaim<long>,
        IdentityUserToken<long>>(options)
{
        
    public DbSet<YouTubeDetail> YouTubeDetails { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.HasDefaultSchema("dbo");

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable(name: "IdentityUser");
        });
        modelBuilder.Entity<IdentityRole<long>>(entity =>
        {
            entity.ToTable(name: "IdentityRole");
        });
        modelBuilder.Entity<IdentityUserRole<long>>(entity =>
        {
            entity.ToTable("IdentityUserRole");
        });
        modelBuilder.Entity<IdentityUserClaim<long>>(entity =>
        {
            entity.ToTable("IdentityUserClaim");
        });
        modelBuilder.Entity<IdentityUserLogin<long>>(entity =>
        {
            entity.ToTable("IdentityUserLogin");
        });
        modelBuilder.Entity<IdentityRoleClaim<long>>(entity =>
        {
            entity.ToTable("IdentityRoleClaim");
        });
        modelBuilder.Entity<IdentityUserToken<long>>(entity =>
        {
            entity.ToTable("IdentityUserToken");
        });                

        long userId1 = 1;
        

        long roleId = 1;
        //long categoriaId = 1;

        PasswordHasher<User> hasher = new();
        User adminUser1 = new()
        {
            Id = userId1,
            UserName = "rogercomp@gmail.com",
            FullName = "Rogerio Siqueira de Miranda",            
            NormalizedUserName = "rogercomp@gmail.com",
            Email = "rogercomp@gmail.com",
            NormalizedEmail = "ROGERCOMP@GMAIL.COM",
            EmailConfirmed = true,
            PhoneNumber = "34988584634",
            PhoneNumberConfirmed = true,
            SecurityStamp = Guid.NewGuid().ToString(),
        };      


        adminUser1.PasswordHash = hasher.HashPassword(adminUser1, "Z@nthe10");
      
        Role adminRole1 = new()
        {
            Id = roleId,
            Name = "Admin",
            NormalizedName = "ADMIN"
        };        
              

        IdentityUserRole<long> adminUserRole1 = new()
        {
            RoleId = roleId,
            UserId = userId1
        };

        modelBuilder.Entity<User>()
        .HasData(adminUser1);        

        modelBuilder.Entity<Role>()
        .HasData(adminRole1);    

        modelBuilder.Entity<IdentityUserRole<long>>()
        .HasData(adminUserRole1); 

    }   
}
