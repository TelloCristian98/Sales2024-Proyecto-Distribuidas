using DAL;
using Entities;

namespace BLL
{
    public class ProductLogic
    {
        private readonly IRepository _repository;

        // Constructor injection for IRepository
        public ProductLogic(IRepository repository)
        {
            _repository = repository;
        }

        public Products Create(Products product)
        {
            var existing = _repository.Retrieve<Products>(p => p.productname == product.productname);
            if (existing == null)
            {
                return _repository.Create(product);
            }
            throw new Exception("Product already exists.");
        }

        public Products RetrieveById(int id)
        {
            return _repository.Retrieve<Products>(p => p.productid == id);
        }

        public List<Products> RetrieveAll()
        {
            return _repository.Filter<Products>(_ => true);
        }

        public bool Update(Products productToUpdate)
        {
            return _repository.Update(productToUpdate);
        }

        public bool Delete(int id)
        {
            var product = RetrieveById(id);
            return _repository.Delete(product);
        }
    }
}