using LogisticsApi.Model;

namespace LogisticsApi.Services
{
    public interface ICompnayBranchRepository
    {
        Task<CompanyBranch> AddCompanyBranch(CompanyBranch companyBranch);
        Task<CompanyBranch> UpdateCompanyBranch(CompanyBranch companyBranch);
        Task<CompanyBranch?> GetCompanyBranchById(int Id);
        Task<bool> DeleteCompanyBranchById(int Id);
        Task<List<CompanyBranch>> GetAllCompanyBranches();
        Task<List<CompanyBranch>?> GetBranchesByCompanyId(int Id);
    }
}