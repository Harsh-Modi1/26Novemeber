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
    public class CartsController : ApiController
    {
        private DbonlineshoppingEntities db = new DbonlineshoppingEntities();

        #region Cart
        [HttpGet]
        public IHttpActionResult GetCart(int userId)
        {
            var products = db.Carts.Where(w => w.UserID == userId).Select(s => new CartModel()
            {
                CartID = s.CartID,
                ProductDescription = s.Product.ProductDescription,
                ProductCode = s.Product.ProductCode,
                ProductName = s.Product.ProductName,
                Quantity = s.Quantity,
                ProductPrice = s.Product.ProductPrice,
                ProductID = s.Product.ProductID,
                TotalPrice = s.TotalPrice
            }).ToList();
            return Ok(products);
        }

        [HttpPost]
        public IHttpActionResult AddToCart(Cart cart)
        {
            var isDuplicateCart = db.Carts.Where(w => w.ProductID == cart.ProductID
            && w.UserID == cart.UserID).FirstOrDefault();
            var ProdQuantity = db.Products.Where(w => w.ProductID == cart.ProductID && w.Quantity >= cart.Quantity).Any();
            if (ProdQuantity)
            {
                if (isDuplicateCart == null)
                {
                    if (cart.CartID == 0)
                    {
                        Cart objcl = new Cart();
                        objcl.ProductID = cart.ProductID;
                        objcl.Quantity = cart.Quantity;
                        objcl.TotalPrice = cart.TotalPrice * cart.Quantity;
                        objcl.UserID = cart.UserID;
                        db.Carts.Add(objcl);
                        db.SaveChanges();
                    }
                    return Ok("Success");
                }
                else
                {
                    return Ok("ProductID Already Exists in Cart.");
                }
            }
            else
            {
                return Ok("Product is Out of Stock. Please reduce the quantity.");
            }
        }

        [HttpPut]
        public IHttpActionResult UpdateCart(UpdateCartModel updateCartModel)
        {
            var cartData = db.Carts.Where(w => w.UserID == updateCartModel.UserID && w.ProductID == updateCartModel.ProductID).FirstOrDefault();
            if (cartData != null)
            {
                var ProdQuantity = db.Products.Where(w => w.ProductID == updateCartModel.ProductID && w.Quantity >= updateCartModel.Quantity).Any();
                if (ProdQuantity)
                {
                    cartData.Quantity = updateCartModel.Quantity;
                    cartData.TotalPrice = updateCartModel.TotalPrice;
                    db.Entry(cartData).State = EntityState.Modified;
                    db.SaveChanges();
                    return Ok("Success");
                }
                else
                {
                    return Ok("Product is Out of Stock. Please reduce the quantity.");
                }
            }
            else
            {
                return Ok("Error updating cart, Please try again later.");
            }
        }
        #endregion
        [HttpDelete]
        public IHttpActionResult RemovefromCart(int id)
        {
            var cart = db.Carts.Where(w => w.CartID == id).FirstOrDefault();
            if (cart != null)
            {
                db.Carts.Remove(cart);
                db.SaveChanges();
                return Ok("Success");
            }
            else
            {
                return NotFound();
            }
        }


    }
}