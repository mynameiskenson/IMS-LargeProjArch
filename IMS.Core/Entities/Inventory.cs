using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMS.Core.Entities
{
    public partial class Inventory
    {
        public int InventoryId { get; set; }
        public string? InventoryName { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
