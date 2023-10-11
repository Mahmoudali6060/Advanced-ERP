using System.Threading.Tasks;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/Category")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class CategoryController : Controller
    {
        ICategoryDSL _categoryDSL;
        public CategoryController(ICategoryDSL categoryDSL)
        {
            this._categoryDSL = categoryDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] CategorySearchDTO searchCriteriaDTO) => Ok(await _categoryDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _categoryDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _categoryDSL.GetAllLite());

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] CategoryDTO model) => Ok(await _categoryDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(CategoryDTO model) => Ok(await _categoryDSL.Update(model));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _categoryDSL.Delete(id));


    }
}