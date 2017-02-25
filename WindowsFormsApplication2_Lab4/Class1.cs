using MongoDB.Bson;  //call libraly
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApplication2_Lab4
{
    class Class1
    {
        public ObjectId id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string tel { get; set; }
        public string BookName { get; set; }
        public string BookType { get; set; }
        public string DateBorrow { get; set; }
        public string DateReturn { get; set; }
        public int Amout { get; set; }
        public string nameLil { get; set; }
        public Class1() { }
        public Class1(string username,string name,string tel,string nameLil,string Bookname,string booktype,string dateborrow,string datereturn,int amout)
        {
            this.username = username;
            this.name = name;
            this.tel = tel;
            this.nameLil = nameLil;
            this.BookName = Bookname;
            this.BookType = booktype;
            this.DateBorrow = dateborrow;
            this.DateReturn = datereturn;
            this.Amout = amout;
        }
    }
}
