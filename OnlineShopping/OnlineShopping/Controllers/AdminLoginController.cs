using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShopping.Models;


namespace OnlineShopping.Controllers
{
    public class AdminLoginController : ApiController
    {
        private DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();
        #region AdminLogin
        [HttpGet]
        public IHttpActionResult AdminLogin(string email, string password)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var isValidUser = false;
                var user = db.UserTables.Where(w => w.Email == email && w.Password == password && w.Role == "Admin").FirstOrDefault();
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
            catch(Exception e)
            {
                return Ok(e);
            }
           

        }
        #endregion
    }
}
