using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("OT.INVENTORIES")]
    public class Inventory_Entry
    {
        [Key, Column(Order = 0)]
        public int Product_Id { get; set; }
        [Key, Column(Order = 1)]
        public int Warehouse_Id { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public virtual Warehouse Warehouse { get; set; }
    }
    [Table("OT.WAREHOUSES")]
    public class Warehouse
    {
        [Key]
        public int Warehouse_Id { get; protected set; }
        [Column("WAREHOUSE_NAME")]
        public string Name { get; set; }
        public int Location_Id { get; set; }
        public virtual Location Location { get; set; }
        public virtual ICollection<Inventory_Entry> Inventory { get; set; }
    }
}
