using DataAccessLayer.Models.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configutation
{
    internal class EmployeeConfiguration : BaseEntityConfiguration<Employee>, IEntityTypeConfiguration<Employee>
    {
        public  new void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.Property(e => e.Name).HasColumnType("varchar(50)");
            builder.Property(e => e.Address).HasColumnType("varchar(150)");
            builder.Property(e => e.Salary).HasColumnType("decimal(10,2)");
            builder.Property(e => e.Email).HasColumnType("varchar(30)");
            builder.Property(e => e.PhoneNumber).HasColumnType("varchar(11)");

            //Logic for Store Enum in DB
            builder.Property(e => e.EmployeeType).HasConversion(
                  valueToAddInDb => valueToAddInDb.ToString(),
                  valueToReadFromDb => (EmployeeType)Enum.Parse(typeof(EmployeeType), valueToReadFromDb))
                .HasColumnType("varchar(10)");

            builder.Property(e => e.Gender).HasConversion(
                  valueToAddInDb => valueToAddInDb.ToString(),
                  valueToReadFromDb => (Gender)Enum.Parse(typeof(Gender), valueToReadFromDb))
                 .HasColumnType("varchar(10)");
        }
    }
}
