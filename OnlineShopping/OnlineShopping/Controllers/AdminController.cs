using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShopping.Models;


namespace OnlineShopping.Controllers
{
    public class AdminController : ApiController
    {
        private DbonlineshoppingEntities db = new DbonlineshoppingEntities();
        // Fetching Retailers from the User Tables
        [HttpGet]
        public IHttpActionResult GetRetailer()
        {
            var retailer = db.UserTables.Where(w => w.Role.ToLower() == "retailer" ).Select(s => new register()
            {
                UserID=s.UserID,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                MobileNumber = s.MobileNumber,
                //Password = s.Password,
                CreatedOn = DateTime.Now,
                Role = s.Role,
                Status =s.Status,
                Gender = s.Gender
            }).ToList();
            return Ok(retailer);
        }
        [HttpPost]
        public IHttpActionResult PostRetailer(register register)
        {
                    var retailerdata = db.UserTables.Where(w => w.UserID == register.UserID).FirstOrDefault();
                    if (retailerdata != null)
                    {
                        retailerdata.Status = register.Status;
                        db.Entry(retailerdata).State = EntityState.Modified;
                        db.SaveChanges();
                    }
                return Ok("Success");
        }

        [HttpGet]
        public IHttpActionResult GetRetailerById(int id)
        {
            var retailer = db.UserTables.Where(w => w.UserID == id).Select(s => new register()
            {
                UserID = s.UserID,
                FirstName = s.FirstName,
                LastName = s.LastName,
                Email = s.Email,
                MobileNumber = s.MobileNumber,
                //Password = s.Password,
                CreatedOn = DateTime.Now,
                Role = s.Role,
                Status = s.Status,
                Gender = s.Gender
            }).FirstOrDefault();
            if (retailer != null)
                return Ok(retailer);
            else
                return NotFound();
        }

        // Deleting a Retailer By getting his Id
        #region deleteretailer
        [HttpDelete]
        public IHttpActionResult DeleteRetailer(int id)
        {
            var retailer = db.UserTables.Where(w => w.UserID == id).FirstOrDefault();
            if (retailer != null)
            {
                db.UserTables.Remove(retailer);
                db.SaveChanges();
                return Ok();
            }
            else
                return NotFound();
        }
        #endregion
    }
}
