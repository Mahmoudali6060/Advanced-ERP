using System.Threading.Tasks;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;
using Shared.Enums;

namespace App.Controllers.Setup
{
    [Route("Api/ClientVendor")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class ClientVendorController : Controller
    {
        IClientVendorDSL _clientDSL;
        public ClientVendorController(IClientVendorDSL clientDSL)
        {
            this._clientDSL = clientDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] ClientVendorSearchDTO searchCriteriaDTO) => Ok(await _clientDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _clientDSL.GetById(id));

        [HttpGet, Route("GetAllLiteByTypeId/{typeId}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLiteByTypeId(ClientVendorTypeEnum typeId) => Ok(await _clientDSL.GetAllLiteByTypeId(typeId));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _clientDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] ClientVendorDTO model) => Ok(await _clientDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(ClientVendorDTO model) => Ok(await _clientDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _clientDSL.Delete(id));


    }
}