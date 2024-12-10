using Entities;
using System.Collections.Generic;

namespace SLC
{
    public interface IProductService
    {
        Products CreateProducts(Products products);
        Products RetrieveById(int id);
        List<Products> RetrieveAll();
        List<Products> Filter(string filterName);
        bool Update(Products productsToUpdate);
        bool Delete(int id);
    }
}