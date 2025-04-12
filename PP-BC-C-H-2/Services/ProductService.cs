using System;
using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using PP_BC_C_H_2.Entity;

namespace PP_BC_C_H_2.Services
{
    public class ProductService : IProductService
    {
        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Product1", Price = 10 },
            new Product { Id = 2, Name = "Product2", Price = 20 }
        };

        private readonly IValidator<Product> _validator;

        public ProductService(IValidator<Product> validator)
        {
            _validator = validator;
        }

        public IEnumerable<Product> List(string name = null, string sortBy = null, string sortOrder = "asc")
        {
            var products = Products.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                products = products.Where(p => p.Name.Contains(name, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortOrder.Equals("desc", StringComparison.OrdinalIgnoreCase))
                {
                    products = sortBy switch
                    {
                        "name" => products.OrderByDescending(p => p.Name),
                        "price" => products.OrderByDescending(p => p.Price),
                        _ => products
                    };
                }
                else
                {
                    products = sortBy switch
                    {
                        "name" => products.OrderBy(p => p.Name),
                        "price" => products.OrderBy(p => p.Price),
                        _ => products
                    };
                }
            }

            return products.ToList();
        }

        public Product GetById(int id)
        {
            return Products.FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            var result = _validator.Validate(product);
            if (!result.IsValid)
                throw new ArgumentException("Invalid product data");

            Products.Add(product);
            return product;
        }

        public Product Update(int id, Product product)
        {
            var result = _validator.Validate(product);
            if (!result.IsValid)
                throw new ArgumentException("Invalid product data");

            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found");

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;
            return existingProduct;
        }

        public void Delete(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
                throw new KeyNotFoundException("Product not found");

            Products.Remove(product);
        }

        public Product Patch(int id, Product product)
        {
            var existingProduct = Products.FirstOrDefault(p => p.Id == id);
            if (existingProduct == null)
                throw new KeyNotFoundException("Product not found");

            if (!string.IsNullOrEmpty(product.Name))
                existingProduct.Name = product.Name;

            if (product.Price > 0)
                existingProduct.Price = product.Price;

            return existingProduct;
        }
    }
}
