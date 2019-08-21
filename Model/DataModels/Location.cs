using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public enum RegionId
    {
        Europe = 1,
        Americas,
        Asia,
        Middle_East_Africa
    }

    [Table("OT.COUNTRIES")]
    public class Country
    {
        [Key]
        public string Country_Id { get; set; }
        [Column("COUNTRY_NAME")]
        public string Name { get; set; }
        public RegionId Region_Id { get; set; }
        public virtual Region Region { get; set; }
    }
    [Table("OT.REGIONS")]
    public class Region
    {
        [Key]
        public RegionId Region_Id { get; set; }
        [Column("REGION_NAME")]
        public string Name { get; set; }
    }


    [Table("OT.LOCATIONS")]
    public class Location
    {
        [Key]
        public int Location_Id { get; set; }
        public string Address { get; set; }
        public string Postal_Code { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country_Id { get; set; }
        public virtual Country Country { get; set; }
        [NotMapped]
        public Region Region { get { return Country.Region; } }
        public override string ToString()
        {
            string ret = "";
            ret += Address + ", ";
            if (Postal_Code != null) ret += Postal_Code + ", ";
            ret += City + ", ";
            if (State != null) ret += State + ", ";
            ret += Country.Name;
            return ret;
        }

    }
}