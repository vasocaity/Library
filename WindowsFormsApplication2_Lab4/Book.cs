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
    class Book
    {
        public ObjectId id { get; set; }
        public string Bookname { get; set; }
        public string BookType { get; set; }
        public int Amout { get; set; }
        public string status { get; set; }
        public Book() { }
        public Book(string Bookname, string BookType,int Amout,string status)
        {   this.Bookname = Bookname;
            this.BookType = BookType;
            this.Amout = Amout;
            this.status = status;

        }
    }
}
