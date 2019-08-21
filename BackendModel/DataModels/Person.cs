using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    public abstract class Person
    {
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        [NotMapped]
        public string FullName { get { return $"{First_Name} {Last_Name}"; } }
        public override string ToString() { return base.ToString(); }
    }
}