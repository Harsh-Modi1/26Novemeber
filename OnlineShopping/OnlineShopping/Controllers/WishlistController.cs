using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlineShopping.Models;

namespace OnlineShopping.Controllers
{
    public class WishlistController : ApiController
    {
        DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();

        #region AddingToWishlist
        [HttpGet]
        public IHttpActionResult WishlistProduct(int userId)
        {
            try
            {
                var products = db.WishLists.Where(w => w.UserID == userId).Select(s => new WishlistModel()
                {
                    WishListID = s.WishListID,
                    ProductDescription = s.Product.ProductDescription,
                    ProductCode = s.Product.ProductCode,
                    ProductName = s.Product.ProductName,
                    ProductPrice = s.Product.ProductPrice,
                    ProductID = s.Product.ProductID,
                    Image = s.Product.Images.Where(w => w.ProductID == s.ProductID).Select(t => t.ProductImage).FirstOrDefault()
                }).ToList();
                return Ok(products);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
            
        }
        #endregion

        #region Post Wishlist

        [HttpPost]
        public IHttpActionResult AddtoWishlist(WishList wishList)
        {
            try
            {
                var isDuplicateWishlist = db.WishLists.Where(w => w.ProductID == wishList.ProductID
             && w.UserID == wishList.UserID).FirstOrDefault();
                if (isDuplicateWishlist == null)
                {
                    if (wishList.WishListID == 0)
                    {
                        WishList obwishList1 = new WishList();
                        obwishList1.ProductID = wishList.ProductID;
                        obwishList1.UserID = wishList.UserID;
                        db.WishLists.Add(obwishList1);
                        db.SaveChanges();
                    }
                    return Ok("Success");
                }
                else
                {
                    return Ok("ProductID Already Exists in Wishlist.");
                }
            }
            catch (Exception e)
            {
                return Ok(e);
            }
            
        }
        #endregion

        #region DeletingFromWishlist
        [HttpDelete]
        public IHttpActionResult RemoveWishList(int id)
        {
            try
            {
                var wish = db.WishLists.Where(w => w.WishListID == id).FirstOrDefault();
                if (wish != null)
                {
                    db.WishLists.Remove(wish);
                    db.SaveChanges();
                    return Ok("Success");
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception e)
            {
                return Ok(e);
            }
           
        }
        #endregion

    }
}
