using BLL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SLC;

namespace Service.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase, ICategoryService
    {
        private readonly CategoryLogic _categoryLogic;

        public CategoriesController(CategoryLogic categoryLogic)
        {
            _categoryLogic = categoryLogic;
        }

        [HttpPost]
        public Categories CreateCategory(Categories category)
        {
            return _categoryLogic.Create(category);
        }

        [HttpGet("{id}")]
        public Categories RetrieveById(int id)
        {
            return _categoryLogic.RetrieveById(id);
        }

        [HttpGet]
        public List<Categories> RetrieveAll()
        {
            return _categoryLogic.RetrieveAll();
        }

        [HttpPut]
        public bool Update(Categories categoryToUpdate)
        {
            return _categoryLogic.Update(categoryToUpdate);
        }

        [HttpDelete("{id}")]
        public bool Delete(int id)
        {
            return _categoryLogic.Delete(id);
        }
    }
}