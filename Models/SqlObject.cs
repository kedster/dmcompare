using System.Data;

namespace DMSRuntimeComparer.Models
{
    public class SqlObject
    {
        public string DatabaseName { get; set; }
        public string ObjectName { get; set; }
        public string ObjectType { get; set; } // e.g., Table, View
        public DataTable Data { get; set; }
    }
}
