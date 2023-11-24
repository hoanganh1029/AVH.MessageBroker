namespace AVH.MessageBroker.Models.Models
{
    public class ModelBase
    {
        public string Id { get; set; }        

        public DateTime CreatedDate { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime ModifieddDate { get; set;}

        public string? ModifiedBy { get; set;}
    }
}
