using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public enum ProductCategoryEnum
    {
        CPU = 1,
        Video_Card,
        RAM,
        Mother_Board,
        Storage
    }
    [Table("OT.PRODUCT_CATEGORIES")]
    public class ProductCategory
    {
        [Key]
        public ProductCategoryEnum Category_Id { get; set; }
        [Column("CATEGORY_NAME")]
        public string Name { get; set; }
    }
    [Table("OT.PRODUCTS")]
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Product_Id { get; set; }
        [Column("PRODUCT_NAME")]
        public string Name { get; set; }
        public string Description { get; set; }
        public double List_Price { get; set; }
        public double Standard_Cost { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public ProductCategoryEnum Category_Id { get; set; }
        public virtual ProductCategory Category { get; set; }
    }
}
