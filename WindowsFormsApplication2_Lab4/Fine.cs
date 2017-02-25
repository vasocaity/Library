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
    class Fine
    {
        private MongoDatabase database;
        private MongoCollection<Borrow> collectionBorrow;
        private MongoCollection<Member> collectionMember;
        private MongoCollection<Book> collectionBook;
        private MongoCollection<Librarian> collectionLibrarian;
        private MongoCollection<Order> collectionOrder;

        public List<Listbook> listbook { get; set; }
        public string username { get; set; }
        public string Bookname { get; set; }
        public string BookType { get; set; }
        public int Amout { get; set; }
        public int x { get; set; }
        public int Totalmulct;

        //public ObjectId id;
        public Fine(Listbook listbook, string username, string Bookname, string BookType, int Amout, int x)
        {
            this.listbook = new List<Listbook>();
            this.username = username;
            this.Bookname = Bookname;
            this.BookType = BookType;
            this.Amout = Amout;
            this.x = x;
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collectionBorrow = database.GetCollection<Borrow>("Borrow");
            this.collectionMember = database.GetCollection<Member>("Member");
            this.collectionBook = database.GetCollection<Book>("Book");
            this.collectionLibrarian = database.GetCollection<Librarian>("Librarian");
            this.collectionOrder = database.GetCollection<Order>("Order");
            AddFine(listbook,username,Bookname,BookType,Amout,x);
        }

        public void AddFine(Listbook listbook, string username, string Bookname, string BookType, int Amout, int x)
        {
            var query2 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
            Query<Book>.EQ(CS => CS.BookType, BookType));
            var result2 = this.collectionBook.FindOne(query2);


            var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
            Query<Member>.EQ(CS => CS.username, username));
            var result3 = this.collectionBorrow.FindOne(query3);

            string d = result3.DateReturn.Substring(0, 2);
            //คิดค่าปรับ
            if (d.IndexOf("0", 2).Equals("0"))
            {
                d = result3.DateReturn.Substring(0, 1);
            }
            else
            {
                d = result3.DateReturn.Substring(0, 2);
            }

            if (x > int.Parse(d))
            {
                x = (x - int.Parse(d)) * 3;
            }
           
           Totalmulct = result3.mulct + x;
            //update mulct total
            var update4 = MongoDB.Driver.Builders.Update.Set("mulct", Totalmulct);
            this.collectionBorrow.Update(query3, update4);
        }
    }
}
