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

    public class MyOrdersController : ApiController
    {
        DbproonlineshoppingEntities db = new DbproonlineshoppingEntities();

        #region PlaceOrder
        [HttpPost]
        public IHttpActionResult PlaceOrder(MyOrderModel myOrderModel)
        {
            try
            {
                if (myOrderModel.OrderID == 0)
                {
                    MyOrder objcl = new MyOrder();
                    objcl.OrderID = myOrderModel.OrderID;
                    objcl.UserID = myOrderModel.UserID;
                    objcl.OrderTotal = myOrderModel.OrderTotal;
                    objcl.OrderDate = DateTime.Now;
                    db.MyOrders.Add(objcl);
                    db.SaveChanges();

                    int id = objcl.OrderID;
                    foreach (var item in myOrderModel.CartModel)
                    {
                        OrderDetail orderDetail = new OrderDetail();
                        orderDetail.OrderDate = DateTime.Now;
                        orderDetail.TotalPrice = (int)item.TotalPrice;
                        orderDetail.Quantity = item.Quantity;
                        orderDetail.OrderID = id;
                        orderDetail.ProductID = item.ProductID;
                        db.OrderDetails.Add(orderDetail);
                        db.SaveChanges();
                        try
                        {
                            var productData = db.Products.Where(p => p.ProductID == item.ProductID).FirstOrDefault();
                            {
                                // Product product = new Product();
                                productData.Quantity = productData.Quantity - item.Quantity;
                                if (productData.Quantity == 0)
                                {
                                    productData.InStock = false;
                                }
                                else
                                {
                                    productData.InStock = true;

                                }
                                productData.ModifiedDate = DateTime.Now;

                                db.Entry(productData).State = EntityState.Modified;
                                db.SaveChanges();
                            }
                        }
                        catch (Exception exp)
                        {
                            return Ok(exp);

                        }
                        var cart = db.Carts.Where(w => w.CartID == item.CartID).FirstOrDefault();
                        if (cart != null)
                        {
                            db.Carts.Remove(cart);
                            db.SaveChanges();
                        }
                    }

                }
                return Ok("Success");

            }
            catch (Exception e)
            {
                return Ok(e);
            }

           
        }

        #endregion

        #region DisplayOrderHistory
        [HttpGet]
        public IHttpActionResult GetMyOrders(int userId)
        {
            try
            {
                var orders =
                        from myOrder in db.MyOrders
                        join orderDetail in db.OrderDetails on myOrder.OrderID equals orderDetail.OrderID
                        where myOrder.UserID == userId
                        group myOrder by myOrder.OrderID into groupOrder
                        select new MyOrderModel()
                        {
                            OrderID = groupOrder.FirstOrDefault().OrderID,
                            UserID = groupOrder.FirstOrDefault().UserID,
                            OrderTotal = groupOrder.FirstOrDefault().OrderTotal,
                            OrderDate = groupOrder.FirstOrDefault().OrderDate,
                            CartModel = db.OrderDetails.Where(w => w.OrderID == groupOrder.FirstOrDefault().OrderID).Select(s => new CartModel()
                            {
                                ProductName = s.Product.ProductName,
                                ProductDescription = s.Product.ProductDescription,
                                ProductPrice = s.Product.ProductPrice,
                                Quantity = s.Quantity,
                                TotalPrice = s.TotalPrice,
                                ProductID = s.Product.ProductID,
                                Image = s.Product.Images.Where(w => w.ProductID == s.ProductID).Select(t => t.ProductImage).FirstOrDefault()
                            }).ToList()
                        };
                return Ok(orders);
            }
            catch(Exception e)
            {
                return Ok(e);
            }
            
        }
        #endregion
    }
}
