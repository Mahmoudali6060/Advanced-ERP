using System.Threading.Tasks;
using Data.Entities.Purchases;
using DataService.Purchases.Contracts;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Purchases;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/PurchasesBillHeader")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class PurchasesBillHeaderController : Controller
    {

        IPurchasesBillHeaderDSL _purchasesBillHeaderDSL;
        public PurchasesBillHeaderController(IPurchasesBillHeaderDSL purchasesBillHeaderDSL)
        {
            this._purchasesBillHeaderDSL = purchasesBillHeaderDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] PurchasesBillHeaderSearchDTO searchCriteriaDTO) => Ok(await _purchasesBillHeaderDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _purchasesBillHeaderDSL.GetById(id);
            return Ok(result);
        }

        [HttpGet, Route("GetByNumber/{number}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetByNumber(string number)
        {
            var result = await _purchasesBillHeaderDSL.GetByNumber(number);
            return Ok(result);
        }

        [HttpGet, Route("GetAllByVendorId/{vendorId}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllByVendorId(long vendorId)
        {
            var result = await _purchasesBillHeaderDSL.GetAllByVendorId(vendorId);
            return Ok(result);
        }
        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _purchasesBillHeaderDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public  IActionResult Add([FromBody] PurchasesBillHeaderDTO model) => Ok( _purchasesBillHeaderDSL.AddNormal(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(PurchasesBillHeaderDTO model) => Ok(await _purchasesBillHeaderDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _purchasesBillHeaderDSL.Delete(id));


    }
}