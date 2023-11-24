namespace AVH.MessageBroker.Models.Models
{
    public class Product : ModelBase
    {
        public string Name { get; set; }
        public string Color { get; set; } = string.Empty;
        public double Price { get; set; }

    }
}