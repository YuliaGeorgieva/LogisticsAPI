namespace LogisticsApi.Model
{
    public class CompanyBranch
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public string Location { get; set; }
        public string PhoneNumber { get; set; }

        public int CompanyId { get; set; }
       // public Company Company { get; set; }

        public bool IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}