using Entities;
using System.Collections.Generic;

namespace SLC
{
    public interface ICategoryService
    {
        Categories CreateCategory(Categories category);
        Categories RetrieveById(int id);
        List<Categories> RetrieveAll();
        bool Update(Categories categoryToUpdate);
        bool Delete(int id);
    }
}