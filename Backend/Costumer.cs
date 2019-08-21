namespace Model
{
    public enum CostumerType
    {
        BASIC,
        PREMIUM,
        SUPER
    }
    public class Costumer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public CostumerType Type { get; set; }
    }
}