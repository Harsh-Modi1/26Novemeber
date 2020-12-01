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
    public class UsersController : ApiController
    {
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();


        // POST: api/Users/RegisterUser

        #region UserRegisteration
        [HttpPost]
        public IHttpActionResult RegisterUser(Register register)
        {
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}
            try
            {
                var isEmailDuplicate = db.UserTables.Where(w => w.Email == register.Email && w.UserID != register.UserID).FirstOrDefault();
                if (isEmailDuplicate == null)
                {
                    var isMobileNoDuplicate = db.UserTables.Where(w => w.MobileNumber == register.MobileNumber && w.UserID != register.UserID).FirstOrDefault();
                    if (isMobileNoDuplicate == null)
                    {
                        UserTable userTable = new UserTable();
                        userTable.FirstName = register.FirstName;
                        userTable.LastName = register.LastName;
                        userTable.Email = register.Email;
                        userTable.MobileNumber = register.MobileNumber;
                        userTable.Password = register.Password;
                        userTable.CreatedOn = DateTime.Now;
                        userTable.Role = register.Role;
                        userTable.Status = "";
                        userTable.Gender = register.Gender;
                        db.UserTables.Add(userTable);
                        db.SaveChanges();
                        return Ok("Success");
                    }
                    else
                        return Ok("Mobile No. already exists.");
                }
                else
                    return Ok("EmailId already exists.");
            }
            catch (Exception e)
            {
                return Ok(e);

            }
            
        }

        #endregion


        #region LoginofUser
        [HttpGet]
        public IHttpActionResult Login(string email, string password)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isValidUser = false;
                var user = db.UserTables.Where(w => w.Email == email && w.Password == password && (w.Role == "User" || w.Role == "Retailer")).FirstOrDefault();
                if (user != null)
                    isValidUser = true;

                var model = new
                {
                    IsValidUser = isValidUser,
                    UserId = user != null ? user.UserID : 0,
                    UserName = user != null ? user.FirstName + " " + user.LastName : "",
                    Role = user != null ? user.Role : ""
                };
                return Ok(model);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
           
        }
        #endregion


        #region GetRetailer
        [HttpGet]
        public IHttpActionResult GetRetailer()
        {
            try
            {
                var retailer = db.UserTables.Select(s => new Register()
                {
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    Email = s.Email,
                    MobileNumber = s.MobileNumber,
                    Password = s.Password,
                    CreatedOn = DateTime.Now,
                    //Role = "Retailer",
                    //Status = "Approve",
                    Gender = s.Gender,
                }).ToList();
                return Ok(retailer);
            }
            catch (Exception e)
            {
                return Ok(e);
            }
        
        }

        #endregion
    }
}