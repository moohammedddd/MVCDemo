using DataAccessLayer.Models.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Data.Configutation
{
    internal class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
    {
        public void Configure(EntityTypeBuilder<TEntity> builder)
        {

             builder.Property(dept => dept.CreatedOn).HasDefaultValueSql("GETDATE()");

            builder.Property(dept => dept.LastModifiedOn).HasComputedColumnSql("GETDATE()");

        }
    }
    

    }

