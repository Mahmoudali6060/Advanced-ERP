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
    [Route("Api/City")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class CityController : Controller
    {
        ICityDSL _cityDSL;
        public CityController(ICityDSL cityDSL)
        {
            this._cityDSL = cityDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] CitySearchDTO searchCriteriaDTO) => Ok(await _cityDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _cityDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _cityDSL.GetAllLite());

        [HttpGet, Route("GetAllLiteByStateId/{stateId}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLiteByStateId(long stateId) => Ok(await _cityDSL.GetAllLiteByStateId(stateId));

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] CityDTO model) => Ok(await _cityDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(CityDTO model) => Ok(await _cityDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _cityDSL.Delete(id));


    }
}