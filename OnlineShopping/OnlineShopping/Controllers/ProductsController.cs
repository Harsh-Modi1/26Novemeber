using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.IO;
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
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();

        #region FetchProduct
        [HttpGet]
        public IHttpActionResult GetProducts()
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
                }).ToList();
                return Ok(products);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
          
        }
        #endregion


        #region AddingProduct
        [HttpPost]
        public IHttpActionResult PostProduct(Product product)
        {
            try
            {
                var isDuplicateProduct = db.Products.Where(w => w.ProductCode == product.ProductCode && w.ProductID != product.ProductID).FirstOrDefault();
                if (isDuplicateProduct == null)
                {
                    if (product.ProductID == 0)
                    {
                        if (product.Quantity < 0)
                        {
                            return Ok("Product Quantity should not be in Negative Number");
                        }
                        else if (product.Quantity == 0)
                        {
                            product.InStock = false;
                        }
                        else
                        {
                            product.InStock = true;
                        }
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
                            if (product.Quantity < 0)
                            {
                                return Ok("Product Quantity should not be in Negative Number");
                            }
                            else if (product.Quantity == 0)
                            {
                                productData.Quantity = product.Quantity;
                                productData.InStock = false;
                            }
                            else
                            {
                                productData.Quantity = product.Quantity;
                                productData.InStock = true;
                            }
                            // productData.InStock = product.InStock;
                            productData.ModifiedDate = DateTime.Now;

                            db.Entry(productData).State = EntityState.Modified;
                            db.SaveChanges();
                        }
                    }

                    return Ok(new { ProductId = product.ProductID, Status = "Success" });
                }
                else
                    return Ok("Product Code Already Exists.");

            }
            catch (Exception e)
            {
                return Ok(e);
            }
            
        }

        #endregion

        #region deleteproduct
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            try
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
            catch(Exception e)
            {
                return Ok(e);
            }
           
        }
        #endregion


        #region GetProductByID
        [HttpGet]
        public IHttpActionResult GetProductById(int id)
        {
            try
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
                    InStock = s.InStock,
                    Image = s.Images.Where(w => w.ProductID == s.ProductID).Select(t => t.ProductImage).FirstOrDefault()
                }).FirstOrDefault();
                if (product != null)
                    return Ok(product);
                else
                    return NotFound();
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        }

        #endregion
    }
}