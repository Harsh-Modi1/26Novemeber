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
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();
        // Fetching Retailers from the User Tables where role is retailer
        #region Fetchretailer
        [HttpGet]
        public IHttpActionResult GetRetailer()
        {
            try
            {
                var retailer = db.UserTables.Where(w => w.Role.ToLower() == "retailer").Select(s => new Register()
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
                }).ToList();
                return Ok(retailer);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
            
        }
        #endregion

        // Adding the retailer in the User table
        #region AddRetailer
        [HttpPost]
        public IHttpActionResult PostRetailer(Register register)
        {
            try
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
            catch (Exception e)
            {
                return Ok(e);
            }
                    
        }
        #endregion

        // Fetching the retailer by passing his ID
        #region GetRetailerByID
        [HttpGet]
        public IHttpActionResult GetRetailerById(int id)
        {
            try
            {
                var retailer = db.UserTables.Where(w => w.UserID == id).Select(s => new Register()
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
            catch (Exception e)
            {
                return Ok(e);
            }
            
        }
        #endregion

        // Deleting a Retailer By getting his Id
        #region deleteretailer
        [HttpDelete]
        public IHttpActionResult DeleteRetailer(int id)
        {
            try
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
            catch (Exception e)
            {
                return Ok(e);
            }
        }
        #endregion
    }
}
