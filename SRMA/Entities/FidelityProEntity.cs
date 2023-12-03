namespace SRMA.Entities
{
    public class FidelityProEntity
    {
        public long IdProgram { get; set; }
        public string startDate { get; set; } = string.Empty;
        public int earnedPoints { get; set; } 
        public long IdUser { get; set; } 
        public string codeU { get; set; } = string.Empty;

    }
}
