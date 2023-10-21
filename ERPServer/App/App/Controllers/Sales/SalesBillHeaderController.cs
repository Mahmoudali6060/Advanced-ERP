using System.Threading.Tasks;
using Data.Entities.Sales;
using DataService.Sales.Contracts;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Sales;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/SalesBillHeader")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class SalesBillHeaderController : Controller
    {

        ISalesBillHeaderDSL _salesBillHeaderDSL;
        public SalesBillHeaderController(ISalesBillHeaderDSL salesBillHeaderDSL)
        {
            this._salesBillHeaderDSL = salesBillHeaderDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] SalesBillHeaderSearchDTO searchCriteriaDTO) => Ok(await _salesBillHeaderDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id)
        {
            var result = await _salesBillHeaderDSL.GetById(id);
            return Ok(result);
        }

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _salesBillHeaderDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] SalesBillHeaderDTO model) => Ok(await _salesBillHeaderDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(SalesBillHeaderDTO model) => Ok(await _salesBillHeaderDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _salesBillHeaderDSL.Delete(id));


    }
}