using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineShopping.Models
{
    public class MyOrderModel
    {
        public int OrderID { get; set; }
        public int UserID { get; set; }
        public double OrderTotal { get; set; }
        public DateTime OrderDate { get; set; }
        public string Image { get; set; }
        public List<CartModel> CartModel { get; set; }
    }
}