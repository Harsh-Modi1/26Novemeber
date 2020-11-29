using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using Microsoft.AspNetCore.Http;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class ImageController : ApiController
    {
        DbonlineshoppingEntities db = new DbonlineshoppingEntities();

        //public class ProductImageModel
        //{
        //    public int ProductId { get; set; }
        //    public bool IsDefault { get; set; }
        //}


        [HttpPost]
        //[Route("api/UploadImage")]
        public IHttpActionResult PostUploadImage()  /*List<IFormFile> files*/
        {
            try
            {
                string imageName = null;
                var httpRequest = System.Web.HttpContext.Current.Request;
                //Upload Image
                var postedFile = httpRequest.Files["Image"];
              // Create custom filename
                    imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                var filePath1 = "D:/HARSH/ProjectOnlineShopping/prjonlineshopping/src/assets/images/" + imageName;
                string filepath11 = "/assets/images/" + imageName;
                postedFile.SaveAs(filePath1);
                var postedFile1 = httpRequest.Files["Image1"];
                // Create custom filename
                string imageName1 = new String(Path.GetFileNameWithoutExtension(postedFile1.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName1 = imageName1 + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile1.FileName);
                var filePath2 = "D:/HARSH/ProjectOnlineShopping/prjonlineshopping/src/assets/images/" + imageName1;
                string filepath22 = "/assets/images/" + imageName1;
                postedFile.SaveAs(filePath2);
                var postedFile2 = httpRequest.Files["Image2"];
                // Create custom filename
                string imageName3 = new String(Path.GetFileNameWithoutExtension(postedFile2.FileName).Take(10).ToArray()).Replace(" ", "-");
                imageName3 = imageName3+ DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile2.FileName);
                var filePath3 = "D:/HARSH/ProjectOnlineShopping/prjonlineshopping/src/assets/images/" + imageName3;
                string filepath33 = "/assets/images/" + imageName3;
                postedFile.SaveAs(filePath3);

                //Save to DB
                Product product = new Product();
                product.ProductCode = httpRequest["ProductCode"];
                string pc = httpRequest["ProductCode"];
                product.ProductName = httpRequest["ProductName"];
                product.ProductDescription = httpRequest["ProductDescription"];
                product.ProductPrice = Convert.ToInt32(httpRequest["ProductPrice"]);
                product.Brand = httpRequest["Brand"];
                product.InStock = Convert.ToBoolean(httpRequest["Instock"]);
                product.Quantity = Convert.ToInt32(httpRequest["Quantity"]);

                Categories categories = new Categories();
                categories.CategoryID= Convert.ToInt32(httpRequest["CategoryID"]);

                Image image = new Image();
                Image image1 = new Image();
                Image image2 = new Image();

                image.ProductImage = filepath11;
                image1.ProductImage = filepath22;
                image2.ProductImage = filepath33;
                db.Products.Add(product);
                db.SaveChanges();
                int Pid = (from p in db.Products
                           where p.ProductCode == pc
                           select p.ProductID).FirstOrDefault();
                image.ProductID = Pid;
                image1.ProductID = Pid;
                image2.ProductID = Pid;

                db.Images.Add(image);
                    db.SaveChanges();
                db.Images.Add(image1);
                db.SaveChanges();
                db.Images.Add(image2);
                db.SaveChanges();
                return Ok("Success");
            }
            catch(Exception e)
            {
                return Ok(e.Message);
            }
        //    var file = System.Web.HttpContext.Current.Request.Files.Count > 0 ?
        //System.Web.HttpContext.Current.Request.Files[0] : null;

        //    string imageName = "";
        //    var httpRequest = HttpContext.Current.Request;
        //    var postedFile = httpRequest.Files["Image"];
        //    imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
        //    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
        //    var filePath = HttpContext.Current.Server.MapPath("~/Image/" + imageName);
        //    postedFile.SaveAs(filePath);

        //    Image image = new Image();
        //    image.ProductImage = "/Image/" + imageName;
        //    image.ProductID = Convert.ToInt32(HttpContext.Current.Request.Form[1]);
        //    image.IsDefault = Convert.ToBoolean(HttpContext.Current.Request.Form[2]);
        //    db.Images.Add(image);
        //    db.SaveChanges();

            //return Ok("Success");



        }
    }
}
