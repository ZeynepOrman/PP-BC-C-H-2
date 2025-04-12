using System.Collections.Generic;
using PP_BC_C_H_2.Entity;

namespace PP_BC_C_H_2.Services
{
    public interface IProductService
    {
        IEnumerable<Product> List(string name = null, string sortBy = null, string sortOrder = "asc");
        Product GetById(int id);
        Product Create(Product product);
        Product Update(int id, Product product);
        void Delete(int id);
        Product Patch(int id, Product product);
    }
}
