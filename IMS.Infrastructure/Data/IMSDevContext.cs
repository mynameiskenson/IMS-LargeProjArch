using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMS.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace IMS.Infrastructure.Data
{
    public partial class IMSDevContext : DbContext
    {
        public IMSDevContext()
        {
        }

        public IMSDevContext(DbContextOptions<IMSDevContext> options) : base(options)
        {
        }

        public virtual DbSet<Inventory> Inventories { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=34.193.3.15;Database=GettyXDEV;User ID=DEV;Password=ScwLNgJM9g3EPbtnE85c6kfCDpRYZA7j");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Inventory>().HasData(
                    new Inventory { InventoryId = 1, InventoryName = "Engine", Price = 1000, Quantity = 1},
                    new Inventory { InventoryId = 2, InventoryName = "Body", Price = 400, Quantity = 1 },
                    new Inventory { InventoryId = 3, InventoryName = "Wheel", Price = 100, Quantity = 4 },
                    new Inventory { InventoryId = 4, InventoryName = "Seat", Price = 50, Quantity = 5 }
                );
        }
    }
}
