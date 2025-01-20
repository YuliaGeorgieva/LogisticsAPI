using LogisticsApi.Model;
using LogisticsApi.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApi.Services
{
    public class CompanyRepository : GenericRepository<Company>, ICompanyRepository
    {
        private readonly AppDbContext _context;

        public CompanyRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Company> AddCompany(Company company)
        {
            return await Add(company);
        }

        public async Task<bool> DeleteCompanyById(int Id)
        {
            try
            {
                var result = await GetById(Id);
                result.IsDeleted = true;
                await Update(result);
            }
            catch (Exception ex)
            {
                ex.ToString();
                return false;
            }
            return true;
        }

        public async Task<List<Company>> GetAllCompanies()
        {
            return await GetAll(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<Company?> GetCompanyById(int Id)
        {
            return await GetAll(x => !x.IsDeleted && x.Id == Id).SingleOrDefaultAsync();
        }

        public async Task<Company> UpdateCompany(Company company)
        {
            return await Update(company);
        }
    }
}