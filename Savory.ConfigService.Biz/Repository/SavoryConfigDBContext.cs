using Savory.Repository.ConfigDB.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Savory.ConfigService.Biz.Repository
{
    public class SavoryConfigDBContext : DbContext
    {
        public SavoryConfigDBContext() : base("SavoryConfigDB")
        {
        }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    throw new System.Data.Entity.Infrastructure.UnintentionalCodeFirstException();
        //}

        public virtual DbSet<AppEntity> App { get; set; }

        public virtual DbSet<ConfigFileEntity> ConfigFile { get; set; }
    }
}
