using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class RetailersProductController : ApiController
    {
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();

        #region FetchingProductForRetailer
        [HttpGet]
        public IHttpActionResult GetProductsByRetailerId(int retailerId)
        {
            try
            {
                var products =
                        from product in db.Products
                        join retailer in db.UserTables on product.RetailerID equals retailer.UserID
                        where product.RetailerID == retailerId
                        select new ProductModel()
                        {
                            Brand = product.Brand,
                            ProductDescription = product.ProductDescription,
                            ProductCode = product.ProductCode,
                            ProductName = product.ProductName,
                            CreatedDate = product.CreatedDate,
                            Quantity = product.Quantity,
                            ProductID = product.ProductID,
                            ProductPrice = product.ProductPrice,
                            CategoryName = product.Category.CategoryName,
                            ModifiedBy = product.ModifiedBy,
                            CategoryID = product.CategoryID,
                            ModifiedDate = product.ModifiedDate,
                            InStock = product.InStock,
                            Image = product.Images.Where(w => w.ProductID == product.ProductID).Select(t => t.ProductImage).FirstOrDefault()
                        };

                return Ok(products);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
            
        }

        #endregion
    }
}
