using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("OT.EMPLOYEES")]
    public class Employee : Person
    {
        [Key]
        public int Employee_Id { get; set; }
        public int? Manager_Id { get; set; }
        [ForeignKey("Manager_Id")]
        public virtual Employee Manager { get; set; }
        public string Job_Title { get; set; }
        public DateTime Hire_Date { get; set; }
        public int? Location_Id { get; set; }
        public virtual Location Location { get; set; }
    }
}
