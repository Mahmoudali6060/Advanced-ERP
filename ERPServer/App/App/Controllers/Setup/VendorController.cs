using System.Threading.Tasks;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/Vendor")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class VendorController : Controller
    {
        IVendorDSL _vendorDSL;
        public VendorController(IVendorDSL vendorDSL)
        {
            this._vendorDSL = vendorDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] VendorSearchDTO searchCriteriaDTO) => Ok(await _vendorDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _vendorDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _vendorDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] VendorDTO model) => Ok(await _vendorDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(VendorDTO model) => Ok(await _vendorDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _vendorDSL.Delete(id));


    }
}