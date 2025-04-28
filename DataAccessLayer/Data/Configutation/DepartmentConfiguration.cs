

using DataAccessLayer.Models.Department;

namespace DataAccessLayer.Data.Configutation
{
    internal class DepartmentConfiguration : BaseEntityConfiguration<Department>, IEntityTypeConfiguration<Department>
    {

        public new  void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(dept => dept.Id).UseIdentityColumn(10,10);
            builder.Property(dept => dept.Code).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Name).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Description).HasColumnType("varchar(100)");
            builder.Property(dept => dept.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(dept => dept.LastModifiedBy).HasColumnType("varchar(100)");
            builder.HasMany(dept => dept.Employees)
                .WithOne(emp => emp.Department)
                .HasForeignKey(emp => emp.DepartmentId)
                .OnDelete(DeleteBehavior.SetNull);
            base.Configure(builder);



        }
      

    }
}
