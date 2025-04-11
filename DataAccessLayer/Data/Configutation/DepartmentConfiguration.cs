

namespace DataAccessLayer.Data.Configutation
{
    internal class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {

        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.Property(dept => dept.Id).UseIdentityColumn(10,10);
            builder.Property(dept => dept.Code).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Name).HasColumnType("varchar(20)");
            builder.Property(dept => dept.Description).HasColumnType("varchar(100)");
            builder.Property(dept => dept.CreatedBy).HasColumnType("varchar(100)");
            builder.Property(dept => dept.LastModifiedBy).HasColumnType("varchar(100)");
            // If row is inserted without values , the default value will be used
            // On Inserted
            // Cant Reference Other Columns
            // Can be Overriden
            builder.Property(dept => dept.CreatedOn).HasDefaultValueSql("GETDATE()"); // Default Values
            // Value Is Computed Every Time The Record Changed
            builder.Property(dept => dept.LastModifiedOn).HasComputedColumnSql("GETDATE()");


        }
      

    }
}
