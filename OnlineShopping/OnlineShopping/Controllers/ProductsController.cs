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
    public class ProductsController : ApiController
    {
        private DbonlineshoppingEntities db = new DbonlineshoppingEntities();

        [HttpGet]
        public IHttpActionResult GetProducts()
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
                CategoryID = s. CategoryID,
                ModifiedDate = s.ModifiedDate,
                InStock = s.InStock
            }).ToList();
            return Ok(products);
        }

        [HttpPost]
        public IHttpActionResult PostProduct(Product product)
        {
            var isDuplicateProduct = db.Products.Where(w => w.ProductCode == product.ProductCode && w.ProductID != product.ProductID).FirstOrDefault();
            if (isDuplicateProduct == null)
            {
                if (product.ProductID == 0)
                {
                    product.CreatedDate = DateTime.Now;

                    db.Products.Add(product);
                    db.SaveChanges();
                }
                else
                {
                    var productData = db.Products.Where(w => w.ProductID == product.ProductID).FirstOrDefault();
                    if (productData != null)
                    {
                        productData.Brand = product.Brand;
                        productData.ProductCode = product.ProductCode;
                        productData.ProductName = product.ProductName;
                        productData.ProductDescription = product.ProductDescription;
                        productData.CategoryID = product.CategoryID;
                        productData.Quantity = product.Quantity;
                        productData.ProductPrice = product.ProductPrice;
                        productData.InStock = product.InStock;
                        productData.ModifiedDate = DateTime.Now;

                        db.Entry(productData).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                }
                return Ok("Success");
            }
            else
                return Ok("Product Code Already Exists.");
        }
        #region deleteproduct
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var product = db.Products.Where(w => w.ProductID == id).FirstOrDefault();
            if (product != null)
            {
                db.Products.Remove(product);
                db.SaveChanges();
                return Ok();
            }
            else
                return NotFound();
        }
        #endregion

        [HttpGet]
        public IHttpActionResult GetProductById(int id)
        {
            var product = db.Products.Where(w => w.ProductID == id).Select(s => new ProductModel()
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
                   InStock = s.InStock
               }).FirstOrDefault();
            if (product != null)
                return Ok(product);
            else
                return NotFound();
        }
    }
}