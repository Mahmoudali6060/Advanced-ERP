using System.Threading.Tasks;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/UnitOfMeasurement")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class UnitOfMeasurementController : Controller
    {
        IUnitOfMeasurementDSL _unitOfMeasurementDSL;
        public UnitOfMeasurementController(IUnitOfMeasurementDSL unitOfMeasurementDSL)
        {
            this._unitOfMeasurementDSL = unitOfMeasurementDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] UnitOfMeasurementSearchDTO searchCriteriaDTO) => Ok(await _unitOfMeasurementDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _unitOfMeasurementDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _unitOfMeasurementDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] UnitOfMeasurementDTO model) => Ok(await _unitOfMeasurementDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(UnitOfMeasurementDTO model) => Ok(await _unitOfMeasurementDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _unitOfMeasurementDSL.Delete(id));


    }
}