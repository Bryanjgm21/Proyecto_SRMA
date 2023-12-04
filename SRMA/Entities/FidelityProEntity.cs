namespace SRMA.Entities
{
    public class FidelityProEntity
    {
        public long IdProgram { get; set; }
        public DateTime startDate { get; set; }
        public int earnedPoints { get; set; } 
        public long IdUser { get; set; } 
        public string codeU { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public int qty { get; set; }
    }
}
