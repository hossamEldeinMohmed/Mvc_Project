namespace Mvc_Project.Models
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public int RecordId { get; set; }
        public string Action { get; set; } // 'insert', 'update', 'delete'
        public string ChangedBy { get; set; }
        public DateTime ChangedAt { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }

}
