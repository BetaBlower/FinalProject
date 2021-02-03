using Business.Concrete;
using DataAccess.Concrete.EntityFramework;
using DataAccess.Concrete.InMemory;
using Entities.Concrete;
using System;

namespace ConsoleUI
{
    class Program
    {
        static void Main(string[] args)
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            foreach (var product in productManager.GetByUnitPrice(10,100))
            {
                Console.WriteLine(product.ProductName);
                //Console.Write(" fiyatı = "+product.UnitPrice);
                //Console.WriteLine(" stoktaki adedi = "+product.UnitsInStock + "\n");
            }
        }
    }
}
