using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShopping.Models;


namespace OnlineShopping.Controllers
{
    public class UserProductController : ApiController
    {
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();

        #region SortFilterSearch

        [HttpPost]
        public IHttpActionResult GetUserProducts(FilterViewModel filterViewModel)
        {
            try
            {
                var products = db.Products.Select(s => new ProductModel()
                {
                    Brand = s.Brand,
                    ProductDescription = s.ProductDescription,
                    ProductCode = s.ProductCode,
                    ProductName = s.ProductName,
                    CreatedDate = s.CreatedDate,
                    Quantity = s.Quantity,
                    ProductID = s.ProductID,
                    ProductPrice = s.ProductPrice,
                    CategoryName = s.Category.CategoryName,
                    ModifiedBy = s.ModifiedBy,
                    CategoryID = s.CategoryID,
                    ModifiedDate = s.ModifiedDate,
                    InStock = s.InStock,
                    Image = s.Images.Where(w => w.ProductID == s.ProductID).Select(t => t.ProductImage).FirstOrDefault()
                }).AsQueryable();
                if (filterViewModel.Search != "")
                {
                    products = products.Where(w => w.ProductName.Contains(filterViewModel.Search) || w.ProductDescription.Contains(filterViewModel.Search)).AsQueryable();
                }
                if (filterViewModel.MinPrice != 0 && filterViewModel.MaxPrice != 0)
                {
                    products = products.Where(w => w.ProductPrice > filterViewModel.MinPrice && w.ProductPrice <= filterViewModel.MaxPrice).AsQueryable();
                }
                if (filterViewModel.SortBy == "asc" || filterViewModel.SortBy == "")
                    return Ok(products.OrderBy(o => o.ProductName).ToList());
                else
                    return Ok(products.OrderByDescending(o => o.ProductName).ToList());
            }
            catch(Exception e)
            {
                return Ok(e);
            }
           
        }

        #endregion

    }
}
