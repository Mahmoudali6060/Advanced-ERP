using Data.Entities.Setup;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;
using System.Threading.Tasks;

namespace App.Controllers.Setup
{
    [Route("api/ContactUs")]
    [ApiController]
    public class ContactUsController : Controller
    {
        private readonly IContactUsDSL _ContactUsDSL;
        public ContactUsController(IContactUsDSL ContactUsDSL)
        {
            _ContactUsDSL = ContactUsDSL;
        }
        [HttpPost, Route("GetAll")]
        public async Task<IActionResult> GetAll([FromBody] ContactUsSearch searchCriteriaDTO) => Ok(await _ContactUsDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]

        public async Task<IActionResult> GetById(long id) => Ok(await _ContactUsDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]

        public async Task<IActionResult> GetAllLite() => Ok(await _ContactUsDSL.GetAllLite());

        [HttpPost, Route("Add")]

        public async Task<IActionResult> Add([FromBody] ContactUs model) => Ok(await _ContactUsDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(ContactUs model) => Ok(await _ContactUsDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]

        public async Task<IActionResult> Delete(int id) => Ok(await _ContactUsDSL.Delete(id));
    }
}
