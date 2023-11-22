using System.Threading.Tasks;
using DataService.Accounting.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Accouting;

namespace App.Controllers.Accounting
{
    [Route("Api/Treasury")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class TreasuryController : Controller
    {
        ITreasuryDSL _treasuryDSL;
        public TreasuryController(ITreasuryDSL treasuryDSL)
        {
            this._treasuryDSL = treasuryDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] TreasurySearchDTO searchCriteriaDTO) => Ok(await _treasuryDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _treasuryDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _treasuryDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] TreasuryDTO model) => Ok(await _treasuryDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(TreasuryDTO model) => Ok(await _treasuryDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _treasuryDSL.Delete(id));


    }
}