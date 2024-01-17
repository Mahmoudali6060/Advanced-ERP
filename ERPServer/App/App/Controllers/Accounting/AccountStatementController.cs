using System.Threading.Tasks;
using DataService.Accounting.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Accouting;

namespace App.Controllers.Accounting
{
    [Route("Api/AccountStatement")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class AccountStatementController : Controller
    {
        IAccountStatementDSL _accountStatementDSL;
        public AccountStatementController(IAccountStatementDSL accountStatementDSL)
        {
            this._accountStatementDSL = accountStatementDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] AccountStatementSearchDTO searchCriteriaDTO) => Ok(await _accountStatementDSL.GetAll(searchCriteriaDTO));

        [HttpPost, Route("GetAllForGrid")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllForGrid([FromBody] AccountStatementSearchDTO searchCriteriaDTO) => Ok(await _accountStatementDSL.GetAllForGrid(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _accountStatementDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _accountStatementDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] AccountStatementDTO model) => Ok(await _accountStatementDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(AccountStatementDTO model) => Ok(await _accountStatementDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _accountStatementDSL.Delete(id));


    }
}