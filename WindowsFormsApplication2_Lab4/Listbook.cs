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
    public class Listbook
    {
        public ObjectId Bookid { get; set; }
        public int Amout { get; set; }
        public string DateBorrow { get; set; }
        public string DateReturn { get; set; }
        public string DateArrive { get; set; }
        public string status { get; set; }
        public Listbook() { }
        public Listbook(int Amout, string DateBorrow, string DateReturn, string DateArrive, string status)
        {
            this.Amout = Amout;
            this.DateBorrow = DateBorrow;
            this.DateReturn = DateReturn;
            this.DateArrive = DateArrive;
            this.status = status;
        }
    }
}
