using LogisticsApi.Model;

namespace LogisticsApi.Services
{
    public interface ICompanyRepository
    {
        Task<Company> AddCompany(Company company);
        Task<Company> UpdateCompany(Company company);
        Task<Company?> GetCompanyById(int Id);
        Task<bool> DeleteCompanyById(int Id);
        Task<List<Company>> GetAllCompanies();
    }
}