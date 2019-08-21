using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("OT.ORDER_ITEMS")]
    public class Order_Item
    {
        [Key, Column("ORDER_ID", Order = 0)]
        public int Order_Id { get; set; }
        [Key, Column("ITEM_ID", Order = 1)]
        public int Item_Id { get; set; }
        public virtual Product Product { get; set; }
        public double Unit_Price { get; set; }
        public int Quantity { get; set; }
        [Required]
        public int Product_Id { get; set; }
        public virtual Order Order { get; set; }
    }
    [Table("OT.ORDERS")]
    public class Order
    {
        [Key]
        public int Order_Id { get; set; }
        public int Customer_Id { get; set; }
        public int? Salesman_Id { get; set; }
        [Column("ORDER_DATE")]
        public DateTime Date { get; set; }
        public virtual Customer Customer { get; set; }
        [ForeignKey("Salesman_Id")]
        public virtual Employee Salesman { get; set; }
        public virtual ICollection<Order_Item> Items { get; set; }
        public string Status { get; set; }
        public Order()
        {
            Items = new List<Order_Item>();
        }
        public void Add(Order_Item i)
        {
            i.Item_Id = Items.Count + 1;
            Items.Add(i);
        }
        public void Add(IEnumerable<Order_Item> items)
        {
            foreach (var i in items) Add(i);
        }

    }
}
