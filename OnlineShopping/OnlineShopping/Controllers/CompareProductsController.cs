using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShopping.Models;
using System.Collections;
namespace OnlineShopping.Controllers

{
    public class CompareProductsController : ApiController
    {
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();
        static List<Product> compareProducts = new List<Product>();

        #region AddProductstoCompare
        [HttpGet]
        //[Route("addCompareProduct")]
        public IHttpActionResult AddProducts(int id1)
        {
            try
            {
                int id = Convert.ToInt32(id1);
                if (db.Products.FirstOrDefault(p => p.ProductID == id) is Product p1) if (!compareProducts.Exists(p => p.ProductID == id))
                        compareProducts.Add(p1);
                return Ok();
            }
            catch(Exception e)
            {
                return Ok(e);
            }
            
            
        }
        #endregion

        #region GetProducts
        [HttpGet]
        //[Route("GetProducts")]
        public IEnumerable GetProducts()
        {
                if (compareProducts.Count > 4) compareProducts = compareProducts.Skip(Math.Max(0, compareProducts.Count() - 4)).ToList();

                var products = compareProducts.Select(s => new ProductCompare()
                {
                    Brand = s.Brand,
                    ProductDescription = s.ProductDescription,
                    ProductName = s.ProductName,
                    ProductPrice = s.ProductPrice,
                    Image = s.Images.Where(w => w.ProductID == s.ProductID).Select(t => t.ProductImage).FirstOrDefault()
                }).ToList();
                return products;
            
        }
        #endregion
    }
}
