using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class CategoriesController : ApiController
    {
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();
        
        #region DisplayCategory
        [HttpGet]
        public IHttpActionResult GetCategory()
        {
            try
            {
                var categories = db.Categories.Select(s => new Categories()
                {
                    CategoryID = s.CategoryID,
                    CategoryName = s.CategoryName
                }).ToList();
                return Ok(categories);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
        }
        #endregion


    }
}