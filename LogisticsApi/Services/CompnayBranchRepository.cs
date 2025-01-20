using LogisticsApi.Model;
using LogisticsApi.Services.Base;
using Microsoft.EntityFrameworkCore;

namespace LogisticsApi.Services
{
    public class CompnayBranchRepository : GenericRepository<CompanyBranch>, ICompnayBranchRepository
    {
        private readonly AppDbContext _context;

        public CompnayBranchRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<CompanyBranch> AddCompanyBranch(CompanyBranch companyBranch)
        {
            return await Add(companyBranch);
        }

        public async Task<bool> DeleteCompanyBranchById(int Id)
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

        public async Task<List<CompanyBranch>> GetAllCompanyBranches()
        {
            return await GetAll(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<CompanyBranch?> GetCompanyBranchById(int Id)
        {
            return await GetAll(x => !x.IsDeleted && x.Id == Id).SingleOrDefaultAsync();
        }
        public async Task<List<CompanyBranch>?> GetBranchesByCompanyId(int Id)
        {
            return await GetAll(x => !x.IsDeleted && x.CompanyId == Id).ToListAsync();
        }

        public async Task<CompanyBranch> UpdateCompanyBranch(CompanyBranch companyBranch)
        {
            return await Update(companyBranch);
        }
    }
}
