using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("OT.CONTACTS")]
    public class Contact : Person
    {
        [Key]
        public int Contact_Id { get; set; }
        public int Customer_Id { get; set; }
        public override string ToString() { return base.ToString(); }
        public virtual Customer Customer { get; set; }
    }
}

