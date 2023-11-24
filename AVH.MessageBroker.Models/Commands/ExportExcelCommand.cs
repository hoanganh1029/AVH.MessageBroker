using AVH.MessageBroker.Models.Models;

namespace AVH.MessageBroker.Models.Commands
{
    public class ExportExcelCommand
    {
        public IEnumerable<ModelBase> Data { get; set; } = new List<ModelBase>();
    }
}
