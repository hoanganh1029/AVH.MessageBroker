namespace AVH.MessageBroker.Models.Models
{
    public class Customer : ModelBase
    {
        public string Name { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? City { get; set; }
        public string? Region { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
        public string? Phone { get; set; }
    }
}
