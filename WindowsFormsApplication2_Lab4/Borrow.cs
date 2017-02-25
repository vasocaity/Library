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
    class Borrow
    {
        public ObjectId id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string address  { get; set; }
        public List<Listbook> listbook { get; set; }
        public int Amout { get; set; }
        public string DateBorrow { get; set; }
        public string DateReturn { get; set; }
        public int mulct { get; set; }
        public string status { get; set; }
        public string nameLil { get; set; }
        public int balance { get; set; }
        public Borrow() {
            this.listbook = new List<Listbook>();
        }

        public Borrow(string username, string name, string address, string tel, int Amout,
            string DateBorrow, string DateReturn, int mulct, int balance, string status, string nameLil)
        {
            this.username =username;
            this.name= name;
            this.address=address;
            this.tel=tel;
            this.Amout=Amout;
            this.DateBorrow =DateBorrow;
            this.DateReturn = DateReturn;
            this.status = status;
            this.mulct = mulct;
            this.balance = balance;
            this.nameLil = nameLil;
            this.listbook = new List<Listbook>();
        }

    }
}