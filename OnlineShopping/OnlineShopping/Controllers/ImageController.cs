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
        DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();

        //public class ProductImageModel
        //{
        //    public int ProductId { get; set; }
        //    public bool IsDefault { get; set; }
        //}


        [HttpPost]
        public IHttpActionResult UploadImage()
        {
            try
            {
                for (int i = 0; i < System.Web.HttpContext.Current.Request.Files.Count; i++)
                {
                    string imageName = "";
                    var postedFile = System.Web.HttpContext.Current.Request.Files[i];
                    imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                    var filePath = System.Web.HttpContext.Current.Server.MapPath("~/Image/" + imageName);
                    postedFile.SaveAs(filePath);

                    Image image = new Image();
                    image.ProductImage = "/Image/" + imageName;
                    image.ProductID = Convert.ToInt32(System.Web.HttpContext.Current.Request.Form[0]);
                    image.IsDefault = Convert.ToBoolean(System.Web.HttpContext.Current.Request.Form[1]);
                    db.Images.Add(image);
                    db.SaveChanges();
                }

                return Ok("Success");
            }
            catch (Exception e)
            {
                return Ok(e);
            }
           
        }
    }
}
