using LogisticsApi.Model;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyBranchController : ControllerBase
    {
        private readonly ICompnayBranchRepository _companyBranchRepository;
        public CompanyBranchController(ICompnayBranchRepository companyBranchRepository)
        {
            _companyBranchRepository = companyBranchRepository;
        }
        [HttpGet]
        [Route("GetAllCompanyBranchs")]
        public async Task<IActionResult> GetAllCompanyBranchs()
        {
            return Ok(await _companyBranchRepository.GetAllCompanyBranches());
        }
        [HttpGet]
        [Route("GetCompanyBranchById/{id}")]
        public async Task<IActionResult> GetCompanyBranch(int id)
        {
            if (id <= 0)
                return BadRequest();

            var companyBranch = await _companyBranchRepository.GetCompanyBranchById(id);
            if (companyBranch == null)
                return NotFound();

            return Ok(companyBranch);
        }

        [HttpGet]
        [Route("GetBranchesByCompanyId/{id}")]
        public async Task<IActionResult> GetBranchesByCompanyId(int id)
        {
            if (id <= 0)
                return BadRequest();

            var branches = await _companyBranchRepository.GetBranchesByCompanyId(id);
            if (branches == null)
                return NotFound();

            return Ok(branches);
        }

        [HttpPost]
        [Route("AddCompanyBranch")]
        public async Task<IActionResult> AddCompanyBranch(CompanyBranch companyBranch)
        {
            var createdCompanyBranch = await _companyBranchRepository.AddCompanyBranch(companyBranch);
            return Ok(createdCompanyBranch);
        }

        [HttpPut]
        [Route("UpdateCompanyBranch")]
        public async Task<IActionResult> UpdateCompanyBranch(CompanyBranch companyBranch)
        {
            if (companyBranch == null || companyBranch.Id == 0)
                return BadRequest();

            var result = await _companyBranchRepository.GetCompanyBranchById(companyBranch.Id);
            if (result == null)
                return NotFound();

            result.BranchName= companyBranch.BranchName;
            result.CompanyId= companyBranch.CompanyId;
            result.PhoneNumber= companyBranch.PhoneNumber;
            result.Address= companyBranch.Address;
            result.UpdatedBy= companyBranch.AddedBy;
            result.UpdatedDate= companyBranch.UpdatedDate;

            var updatedCompanyBranch = await _companyBranchRepository.UpdateCompanyBranch(result);
            return Ok(updatedCompanyBranch);
        }

        [HttpDelete]
        [Route("DeleteCompanyBranchById/{id}")]
        public async Task<IActionResult> DeleteCompanyBranch(int id)
        {
            if (id < 1)
                return BadRequest();
            var result = await _companyBranchRepository.GetCompanyBranchById(id);
            if (result == null)
                return NotFound();
            var isDeleted = await _companyBranchRepository.DeleteCompanyBranchById(id);
            return Ok(isDeleted);
        }


    }
}