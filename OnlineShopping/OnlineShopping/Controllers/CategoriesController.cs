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
        private DbonlineshoppingEntities db = new DbonlineshoppingEntities();

        [HttpGet]
        public IHttpActionResult GetCategory()
        {
            var categories = db.Categories.Select(s => new Categories()
            {
                CategoryID = s.CategoryID,
                CategoryName = s.CategoryName
            }).ToList();
            return Ok(categories);
        }


    }
}