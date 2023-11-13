using System.Collections.Generic;
using System.Threading.Tasks;
using DataService.Setup.Contracts;
using Microsoft.AspNetCore.Mvc;
using Shared.Entities.Setup;


namespace App.Controllers.Setup
{
    [Route("Api/Product")]
    [ApiController]
    //[Authorize(Roles = Roles.Admin)]
    public class ProductController : Controller
    {
        IProductDSL _productDSL;
        public ProductController(IProductDSL productDSL)
        {
            this._productDSL = productDSL;
        }

        [HttpPost, Route("GetAll")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAll([FromBody] ProductSearchDTO searchCriteriaDTO) => Ok(await _productDSL.GetAll(searchCriteriaDTO));

        [HttpGet, Route("GetById/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetById(long id) => Ok(await _productDSL.GetById(id));

        [HttpGet, Route("GetAllLite")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLite() => Ok(await _productDSL.GetAllLite());

        [HttpGet, Route("GetAllLiteByCategoryId/{categoryId}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> GetAllLiteByCategoryId(long categoryId) => Ok(await _productDSL.GetAllLiteByCategoryId(categoryId));

        [HttpPost, Route("Add")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Add([FromBody] ProductDTO model) => Ok(await _productDSL.Add(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("Update")]
        public async Task<IActionResult> Update(ProductDTO model) => Ok(await _productDSL.Update(model));

        //[Authorize(Roles = Roles.Admin + "," + Roles.Consumer)]
        [HttpPost, Route("UpdateAll")]
        public async Task<IActionResult> UpdateAll(List<ProductDTO> entityList) => Ok(await _productDSL.UpdateAll(entityList));

        [HttpDelete, Route("Delete/{id}")]
        //[Authorize(Roles = Roles.Admin)]
        public async Task<IActionResult> Delete(int id) => Ok(await _productDSL.Delete(id));


    }
}