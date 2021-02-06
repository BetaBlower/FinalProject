using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;

        public InMemoryProductDal()
        {
            _products = new List<Product>
            {
                new Product{ CategoryId = 1,ProductId=1,ProductName="telefon",UnitPrice=2000,UnitsInStock=100},
                new Product{ CategoryId = 2,ProductId=1,ProductName="kulaklık",UnitPrice=15,UnitsInStock=1000},
                new Product{ CategoryId = 3,ProductId=2,ProductName="mause",UnitPrice=100,UnitsInStock=1050},
                new Product{ CategoryId = 4,ProductId=2,ProductName="klavye",UnitPrice=120,UnitsInStock=1100},
                new Product{ CategoryId = 5,ProductId=2,ProductName="kitap",UnitPrice=50,UnitsInStock=10000},
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {

            Product productToDelete = _products.SingleOrDefault(p=>p.ProductId == product.ProductId);

            _products.Remove(productToDelete);
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(prop => prop.CategoryId == categoryId).ToList();
        }

        public List<ProductDetailDto> GetProductDetails()
        {
            throw new NotImplementedException();
        }

        public void Uppdate(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductId == product.ProductId);//gönderilen ID ye sahip olan listedeki ürünü bul
            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitPrice = product.UnitPrice;
            productToUpdate.UnitsInStock = product.UnitsInStock;

        }
    }
}
