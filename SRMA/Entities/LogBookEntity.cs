namespace SRMA.Entities
{
    public class LogBookEntity
    {
        public long IdLog { get; set; }
        public DateTime logDate { get; set; }
        public string message { get; set; } = string.Empty;
        public string origin { get; set; } = string.Empty;
        public string log_type { get; set; } = string.Empty;

        //FK Users
        public long IdUser { get; set; }

    }
}
