using System.Threading.Tasks;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/Company")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class CompanyController : Controller
    {
        ICompanyDSL _companyDSL;
        public CompanyController(ICompanyDSL companyDSL)
        {
            this._companyDSL = companyDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] CompanySearchDTO searchCriteriaDTO) => Ok(await _companyDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _companyDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _companyDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] CompanyDTO model) => Ok(await _companyDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(CompanyDTO model) => Ok(await _companyDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _companyDSL.Delete(id));


    }
}