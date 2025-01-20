using LogisticsApi.Dtos;
using LogisticsApi.Model;
using LogisticsApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LogisticsApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;
        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }
        [HttpGet]
        [Route("GetAllCompanies")]
        public async Task<IActionResult> GetAllCompanies()
        {
            return Ok(await _companyRepository.GetAllCompanies());
        }

        [HttpGet]
        [Route("GetCompanyById/{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            if (id <= 0)
                return BadRequest();

            var company = await _companyRepository.GetCompanyById(id);
            if (company == null)
                return NotFound();

            return Ok(company);
        }

        [HttpPost]
        [Route("AddCompany")]
        public async Task<IActionResult> AddCompany(CompanyDto model)
        {
            Company company = new Company()
            {
                Name = model.Name,
                Address = model.Address,
                AddedBy = model.AddedBy,
                AddedDate = DateTime.Now,
                IsDeleted = false,
            };

            var createdCompany = await _companyRepository.AddCompany(company);
            return Ok(createdCompany);
        }

        [HttpPut]
        [Route("UpdateCompany")]
        public async Task<IActionResult> UpdateCompany(CompanyDto model)
        {
            if (model == null || model.Id == 0)
                return BadRequest();

            var result = await _companyRepository.GetCompanyById(model.Id);
            if (result == null)
                return NotFound();

            result.Name = model.Name;
            result.Address = model.Address;
            result.UpdatedBy = model.UpdatedBy;
            result.UpdatedDate = DateTime.Now;

            var updatedCompany = await _companyRepository.UpdateCompany(result);
            return Ok(updatedCompany);
        }

        [HttpDelete]
        [Route("DeleteCompanyById/{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            if (id < 1)
                return BadRequest();
            var result = await _companyRepository.GetCompanyById(id);
            if (result == null)
                return NotFound();
            var isDeleted = await _companyRepository.DeleteCompanyById(id);
            return Ok(isDeleted);
        }
    }
}
