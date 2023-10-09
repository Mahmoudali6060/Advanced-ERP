using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Setup.DataServiceLayer;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/Country")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class CountryController : Controller
    {
        ICountryDSL _countryDSL;
        public CountryController(ICountryDSL countryDSL)
        {
            this._countryDSL = countryDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] CountrySearchDTO searchCriteriaDTO) => Ok(await _countryDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _countryDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _countryDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] CountryDTO model) => Ok(await _countryDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(CountryDTO model) => Ok(await _countryDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _countryDSL.Delete(id));


    }
}