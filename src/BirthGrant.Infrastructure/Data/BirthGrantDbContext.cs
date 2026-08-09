using BirthGrant.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BirthGrant.Infrastructure.Data;

public class BirthGrantDbContext : DbContext
{
    public BirthGrantDbContext(DbContextOptions<BirthGrantDbContext> options)
        : base(options)
    {
    }

    public DbSet<BirthGrantCase> BirthGrantCases => Set<BirthGrantCase>();
    public DbSet<BirthGrantCaseHistory> BirthGrantCaseHistories => Set<BirthGrantCaseHistory>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<BirthGrantCase>(entity =>
        {
            entity.ToTable("BirthGrantCases");
            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.CaseNo).IsUnique();

            entity.Property(x => x.CaseNo).HasMaxLength(30).IsRequired();
            entity.Property(x => x.ChildIdNo).HasMaxLength(20).IsRequired();
            entity.Property(x => x.ChildName).HasMaxLength(50).IsRequired();
            entity.Property(x => x.ApplicantIdNo).HasMaxLength(20).IsRequired();
            entity.Property(x => x.ApplicantName).HasMaxLength(50).IsRequired();
            entity.Property(x => x.ApplicantPhone).HasMaxLength(20).IsRequired();
            entity.Property(x => x.ApplicantAddress).HasMaxLength(200).IsRequired();

            entity.Property(x => x.SpouseIdNo).HasMaxLength(20);
            entity.Property(x => x.SpouseName).HasMaxLength(50);
            entity.Property(x => x.SpousePhone).HasMaxLength(20);
            entity.Property(x => x.SpouseAddress).HasMaxLength(200);

            entity.Property(x => x.AgentIdNo).HasMaxLength(20);
            entity.Property(x => x.AgentName).HasMaxLength(50);
            entity.Property(x => x.AgentPhone).HasMaxLength(20);

            entity.Property(x => x.PayeeName).HasMaxLength(50).IsRequired();
            entity.Property(x => x.PayeePhone).HasMaxLength(20);

            entity.Property(x => x.PaymentMethod).HasMaxLength(20).IsRequired();
            entity.Property(x => x.BankCode).HasMaxLength(10);
            entity.Property(x => x.BankAccount).HasMaxLength(30);

            entity.Property(x => x.ReceiptOfficePhone).HasMaxLength(30).IsRequired();
            entity.Property(x => x.ReceiptQrText).HasMaxLength(200).IsRequired();

            entity.Property(x => x.GrantAmount).HasColumnType("decimal(18,0)");
            entity.Property(x => x.Status).HasConversion<int>();
        });

        modelBuilder.Entity<BirthGrantCaseHistory>(entity =>
        {
            entity.ToTable("BirthGrantCaseHistories");
            entity.HasKey(x => x.Id);

            entity.Property(x => x.ActionType).HasMaxLength(50).IsRequired();
            entity.Property(x => x.ActionDescription).HasMaxLength(200).IsRequired();
            entity.Property(x => x.OperatorName).HasMaxLength(50).IsRequired();

            entity.HasOne(x => x.BirthGrantCase)
                  .WithMany(x => x.Histories)
                  .HasForeignKey(x => x.BirthGrantCaseId)
                  .OnDelete(DeleteBehavior.Cascade);
        });
    }
}