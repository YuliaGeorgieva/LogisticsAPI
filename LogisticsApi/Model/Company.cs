using System.Collections.Generic;

namespace LogisticsApi.Model
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public virtual ICollection<CompanyBranch>? CompanyBranches { get; set; }

        public bool IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
