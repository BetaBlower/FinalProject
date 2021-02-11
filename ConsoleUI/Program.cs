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
           ProductTest();

        }

        private static void CategoryTest()
        {
            CategoryManager categoryManager = new CategoryManager(new EfCategoryDal());
            foreach (var category in categoryManager.GetAll())
            {
                Console.WriteLine(category.CategoryName);
            }
        }

        private static void ProductTest()
        {
            ProductManager productManager = new ProductManager(new EfProductDal());

            var result = productManager.GetProductDetails();
            

            if (result.Success==true)
            {
                foreach (var product in result.Data)
                {

                    Console.WriteLine("{0} / {1}", product.ProductName, product.CategoryName);
                    Console.Write(" fiyatı = " + product.UnitPrice);
                    Console.WriteLine(" stoktaki adedi = " + product.UnitsInStock + "\n");
                }
                Console.WriteLine(result.Message);
            }
            else
            {
                Console.WriteLine(result.Message);
            }
           
        }
    }
}
