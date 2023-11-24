namespace AVH.MessageBroker.Models.Commands
{
    public class CommandMessage
    {
        public string Id { get; set; }

        public string CommandType { get; set; }

        public string CommandText { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
