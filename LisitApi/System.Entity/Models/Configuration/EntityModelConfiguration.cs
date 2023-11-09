using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SystemTheLastBugSpa.Data.Procedure;

namespace SystemQuickzal.Data.Models.Configuration
{
    public static class EntityModelConfiguration
    {
        public static void ContinueModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ServicesByPeopleDescription>(entity =>
            {
                entity.ToTable("GetPointSaleReceipt", "Reports");
                entity.HasKey(x => x.Id);

                entity.Property(x => x.Id).HasColumnName("Id");
                entity.Property(x => x.FullName).HasColumnName("FullName");
                entity.Property(x => x.Services).HasColumnName("Services");
                entity.Property(x => x.CreateDate).HasColumnName("CreateDate");
             });

         
        }
    }
}
