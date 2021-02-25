using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
namespace CatCodeFirst
{

    public class Cat
    {
        public int CatId { get; set; }
        public string CatName { get; set; }
        public int CatAge { get; set; }
        public string CatBreed { get; set; }
        public string CatColor { get; set; }

        public override string ToString()
        {
            string s = CatName + ", порода: " + CatBreed;
            return s;
        }
        // Ссылка на заказы
        public virtual List<Order> Orders { get; set; }

        public Cat()
        {
            Orders = new List<Order>();
        }
    }
    public class Order
    {
        public int OrderId { get; set; }
        public string ProductType { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime PurchaseDate { get; set; }
        // Ссылка на кота
        public Cat Cat { get; set; }
        public override string ToString()
        {
            string s = ProductType+ " " + ProductName + " " + Quantity + "шт., дата: " + PurchaseDate;
            return s;
        }
    }


}
