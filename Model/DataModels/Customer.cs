using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("OT.CUSTOMERS")]
    public class Customer
    {
        [Key]
        public int Customer_Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string WebSite { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public double Credit_Limit { get; set; }
    }
}
