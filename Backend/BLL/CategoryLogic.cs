using DAL;
using Entities;

namespace BLL
{
    public class CategoryLogic
    {
        private readonly IRepository _repository;

        // Constructor injection for IRepository
        public CategoryLogic(IRepository repository)
        {
            _repository = repository;
        }

        public Categories Create(Categories category)
        {
            var existing = _repository.Retrieve<Categories>(c => c.categoryname == category.categoryname);
            if (existing == null)
            {
                return _repository.Create(category);
            }
            throw new Exception("Category already exists.");
        }

        public Categories RetrieveById(int id)
        {
            return _repository.Retrieve<Categories>(c => c.categoryid == id);
        }

        public List<Categories> RetrieveAll()
        {
            return _repository.Filter<Categories>(_ => true);
        }

        public bool Update(Categories categoryToUpdate)
        {
            return _repository.Update(categoryToUpdate);
        }

        public bool Delete(int id)
        {
            var category = RetrieveById(id);
            return _repository.Delete(category);
        }
    }
}