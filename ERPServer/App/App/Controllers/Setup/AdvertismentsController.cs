using Data.Entities.Setup;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers.Setup
{
    [Route("api/Advertisments")]
    [ApiController]
    public class AdvertismentsController : Controller
    {
        private readonly IAdvertismentDSL _AdvertismentDSL;
        public AdvertismentsController(IAdvertismentDSL AdvertismentDSL)
        {
            _AdvertismentDSL = AdvertismentDSL;
        }
        [HttpPost, Route("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] AdvertismentSearchDTO searchCriteriaDTO) => Ok(await _AdvertismentDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]

        public async Task<IActionResult> GetById(long id) => Ok(await _AdvertismentDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]

        public async Task<IActionResult> GetAllLite() => Ok(await _AdvertismentDSL.GetAllLite());

        [HttpPost, Route("Add")]
        public async Task<IActionResult> Add([FromBody] AdvertismentDTO model) => Ok(await _AdvertismentDSL.Add(model));
       
        [HttpPost, Route("AddRange")]
        public async Task<IActionResult> AddRange([FromBody] List<AdvertismentDTO> model) => Ok(await _AdvertismentDSL.AddRang(model));


        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(AdvertismentDTO model) => Ok(await _AdvertismentDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]

        public async Task<IActionResult> Delete(int id) => Ok(await _AdvertismentDSL.Delete(id));
    }
}
