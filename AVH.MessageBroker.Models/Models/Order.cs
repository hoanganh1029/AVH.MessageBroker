namespace AVH.MessageBroker.Models.Models
{
    public class Order : ModelBase
    {
        public string? Name { get; set; }

        public IEnumerable<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

        public double TotalPrice { get; set; }
    }

    public class OrderDetail : ModelBase
    {
        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }

        public int Quantity { get; set; }

    }
}
