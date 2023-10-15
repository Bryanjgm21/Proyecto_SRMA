namespace SRMA.Entities
{
    public class EmployeeEntity
    {
        public long IdEmpl { get; set; }
        public decimal salary { get; set; }
        public int workHours { get; set; }
        public string jobTitle { get; set; } = string.Empty;
        public decimal absences { get; set; }

    }
}
