using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using Business.Constants;
using Core.Utilities.Results;
using FluentValidation;
using Business.ValiDationRules.FluentValidation;
using Core.CrossCuttingConcerns;
using Core.Aspects.Autofac.Validation;
using System.Linq;
using Core.Utilities.Business;
using DataAccess.Concrete.EntityFramework;
using Business.BusinessAspect.Autofac;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Performance;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;
        
        #region Constructers
        public ProductManager(IProductDal productDal,ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }
        #endregion

        #region Metods

        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            //if (DateTime.Now.Hour == 1)
            //{
            //    return new ErrorDataResult<List<Product>>(null,Messages.MaintenanceTime);
            //}
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.ProductId == id),Messages.ProductsListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            return new SuccessDataResult<List<Product>>(_productDal.GetAll(p => p.UnitPrice > min && p.UnitPrice < max),Messages.ProductsListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductDetails());
        }

        [SecuredOperation("product.add,admin")]
        [ValidationAspect(typeof(ProductValidator))]
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Add(Product product)
        {

            IResult result = BusinessRules.Run(CheckIfProductCountOfCategoryCorrect(product.CategoryId),
                CheckIfProductName(product.ProductName),
                CheckIfCategoryLimit(15));

            if (result != null)
            {
                return result;
            }
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
           
        }
        [CacheAspect]
        [PerformanceAspect(5)]
        public IDataResult<Product> GetById(int Id)
        {
            return new SuccessDataResult<Product>(_productDal.Get(p => p.ProductId == Id),Messages.ProductsListed);
        }
        [CacheRemoveAspect("IProductService.Get")]
        public IResult Uppdate(Product product)
        {
           
            if (CheckIfProductCountOfCategoryCorrect(product.CategoryId).Success)
            {
                _productDal.Uppdate(product);
                return new SuccessResult(Messages.ProductAdded);
            }
            return new ErrorResult();
        }
        #endregion

        #region BusinessRules

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            var result = GetAllByCategoryId(categoryId);
            if (result.Data.Count <= 10)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }

        private IResult CheckIfProductName(string Name)
        {
            var result = _productDal.GetAll(p => p.ProductName == Name);
            if (result.Count < 1)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
            
        }

        private IResult CheckIfCategoryLimit(int maxValue)
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count <= maxValue)
            {
                return new SuccessResult();
            }
            return new ErrorResult();
        }
        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            throw new NotImplementedException();
        }
        #endregion


    }
}
