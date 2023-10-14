namespace SRMA.Entities
{
    public class UserEntity
    {
        public long IdUser { get; set; }
        public string email { get; set; } = string.Empty;
        public string passwordU { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string Id { get; set; } = string.Empty;
        public string cellphone { get; set; } = string.Empty;
        public DateOnly startDate { get; set; }
        public int ptoDays { get; set; }
        public bool statusU { get; set; }
        public byte IdRol { get; set; }
    }
}
