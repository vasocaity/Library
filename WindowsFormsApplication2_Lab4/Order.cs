using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;  //call libraly
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace WindowsFormsApplication2_Lab4
{
    class Order
    {
        public ObjectId id { get; set; }
        public string username { get; set; } 
        public int Amout { get; set; }
        public string DateOrder { get; set; }
        public List<ListOrder> listbook { get; set; }
        public string status { get; set; }
        public int balance { get; set; }
        public Order() {
            this.listbook = new List<ListOrder>();
        }
        public Order(string MemberName, int Amout, string DateOrder, string status, int balance)
        {   
            this.username = MemberName;
            this.Amout = Amout;
            this.DateOrder = DateOrder;
            this.listbook = new List<ListOrder>();
            this.status = status;
            this.balance = balance;
        }

    }
}
