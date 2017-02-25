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
    public class ListOrder
    {
        public ObjectId Bookid { get; set; }
        public int Amout { get; set; }
        public string DateOrder { get; set; }
        public ListOrder() { }
        public ListOrder(string dateorder,int amout)
        {
            this.DateOrder = dateorder;
            this.Amout = Amout;
        }
    }
}
