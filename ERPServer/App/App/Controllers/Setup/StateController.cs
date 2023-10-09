using System.Threading.Tasks;
using Accout.DataServiceLayer;
using Data.Constants;
using Entities.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Setup.DataServiceLayer;
using Shared.Entities.Setup;
using Shared.Entities.Shared;


namespace App.Controllers.Setup
{
    [Route("Api/State")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class StateController : Controller
    {
        IStateDSL _stateDSL;
        public StateController(IStateDSL stateDSL)
        {
            this._stateDSL = stateDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] StateSearchDTO searchCriteriaDTO) => Ok(await _stateDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _stateDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _stateDSL.GetAllLite());

        [HttpGet, Route("GetAllLiteByCountryId/{countryId}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLiteByCountryId(long countryId) => Ok(await _stateDSL.GetAllLiteByCountryId(countryId));

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] StateDTO model) => Ok(await _stateDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(StateDTO model) => Ok(await _stateDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _stateDSL.Delete(id));


    }
}