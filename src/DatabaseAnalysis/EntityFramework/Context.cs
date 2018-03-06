using DatabaseAnalysis.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseAnalysis.EntityFramework
{
    public class Context : DbContext
    {
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
            => base.OnModelCreating(modelBuilder);


        public DbSet<Base> Base { get; set; }

        public DbSet<BaseStructures> BaseStructures { get; set; }

        public DbSet<BaseStructureDB> BaseStructureDB { get; set; }

        public DbSet<StructureDB> StructureDB { get; set; }

        public DbSet<Fields> Fields { get; set; }

        public DbSet<Indexes> Indexes { get; set; }
    }
}
