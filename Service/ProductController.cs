using BLL;
using Entities;
using Microsoft.AspNetCore.Mvc;
using SLC;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase, IProductService
{
    private readonly ProductLogic _productLogic;

    public ProductController(ProductLogic productLogic)
    {
        _productLogic = productLogic;
    }

    [HttpPost]
    public Products CreateProducts(Products products)
    {
        return _productLogic.Create(products);
    }

    [HttpGet("{id}")]
    public Products RetrieveById(int id)
    {
        return _productLogic.RetrieveById(id);
    }

    [HttpPut]
    public bool Update(Products productsToUpdate)
    {
        return _productLogic.Update(productsToUpdate);
    }

    [HttpDelete("{id}")]
    public bool Delete(int id)
    {
        return _productLogic.Delete(id);
    }

    [HttpGet]
    public List<Products> RetrieveAll()
    {
        return _productLogic.RetrieveAll();
    }

    public List<Products> Filter(string filterName)
    {
        throw new NotImplementedException();
    }
}