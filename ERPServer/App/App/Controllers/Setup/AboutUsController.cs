using Data.Entities.Setup;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;
using System.Threading.Tasks;

namespace App.Controllers.Setup
{
    [Route("api/AboutUs")]
    [ApiController]
    public class AboutUsController : Controller
    {
        private readonly IAboutUsDSL _AboutUsDSL;
        public AboutUsController(IAboutUsDSL AboutUsDSL)
        {
            _AboutUsDSL = AboutUsDSL;
        }
        [HttpPost, Route("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] AboutUs searchCriteriaDTO) => Ok(await _AboutUsDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]

        public async Task<IActionResult> GetById(long id) => Ok(await _AboutUsDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]

        public async Task<IActionResult> GetAllLite() => Ok(await _AboutUsDSL.GetAllLite());

        [HttpPost, Route("Add")]

        public async Task<IActionResult> Add([FromBody] AboutUs model) => Ok(await _AboutUsDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(AboutUs model) => Ok(await _AboutUsDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]

        public async Task<IActionResult> Delete(int id) => Ok(await _AboutUsDSL.Delete(id));
    }
}
