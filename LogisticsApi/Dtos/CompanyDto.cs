namespace LogisticsApi.Dtos
{
    public class CompanyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public string AddedBy { get; set; }
        public DateTime? AddedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}