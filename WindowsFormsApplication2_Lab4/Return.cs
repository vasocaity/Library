using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace WindowsFormsApplication2_Lab4
{
    class Return
    {
        private MongoDatabase database;
        private MongoCollection<Borrow> collectionBorrow;
        private MongoCollection<Member> collectionMember;
        private MongoCollection<Book> collectionBook;
        private MongoCollection<Librarian> collectionLibrarian;
        private MongoCollection<Order> collectionOrder;
        private ObjectId Bookid;
        private ObjectId Orderid;

        public string username { get; set; }
        public string Bookname { get; set; }
        public string BookType { get; set; }
        public List<Listbook> listbook { get; set; }
        public int Amout {get; set;}
        public int x { get; set; } 
        public string DateArrive {get; set;}
        public int count = 0;
        public string sta = "รายการสมบูรณ์";

        public Return(string username,string Bookname,string Booktype, Listbook listbook,int Amout,string DateArrive, int x)
        {
            this.username = username;
            this.Bookname = Bookname;
            this.BookType = Booktype;
            this.listbook = new List<Listbook>();
            this.Amout = Amout;
            this.DateArrive = DateArrive;
            this.x = x;

            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collectionBorrow = database.GetCollection<Borrow>("Borrow");
            this.collectionMember = database.GetCollection<Member>("Member");
            this.collectionBook = database.GetCollection<Book>("Book");
            this.collectionLibrarian = database.GetCollection<Librarian>("Librarian");
            this.collectionOrder = database.GetCollection<Order>("Order");
            AddReturn(listbook,Amout,x);
        }
        public void AddReturn(Listbook listbook,int Amout,int x)
        {
                ListOrder order = new ListOrder();

                var query2 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
                Query<Book>.EQ(CS => CS.BookType, BookType));
                var result2 = this.collectionBook.FindOne(query2);

                var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
                Query<Member>.EQ(CS => CS.username, username));
                var result3 = this.collectionBorrow.FindOne(query3);

                count = result3.balance;

                listbook.Bookid = result2.id;
                listbook.Amout = Amout;
                listbook.DateArrive = DateArrive;
                listbook.DateBorrow = result3.DateBorrow;
                listbook.DateReturn = result3.DateReturn;
                listbook.status = "คืนแล้ว";

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
                /*if (x > int.Parse(d))
                {
                    listbook.mulct = 10;
                }*/
                if (result2.Bookname != null)
                {
                    Amout = result2.Amout + Amout;
                    var update3 = MongoDB.Driver.Builders.Update.Set("Amout", Amout);
                    this.collectionBook.Update(query2, update3);
                }

                if (result3 != null)
                {
                    var query = MongoDB.Driver.Builders.Query.EQ("_id", result3.id);
                    var update = MongoDB.Driver.Builders.Update.Pull("listbook",
                        MongoDB.Driver.Builders.Query.EQ("Bookid", result2.id));
                    this.collectionBorrow.Update(query, update);

                    ///update listbook
                    var update2 = Update<Borrow>.AddToSet(SC => SC.listbook, listbook);
                    this.collectionBorrow.Update(query3, update2);

                    //update amout book after return


                    var update6 = MongoDB.Driver.Builders.Update.Set("status", "ปกติ");
                    this.collectionBook.Update(query2, update6);
                    Amout = 0;

                    //update remain book
                    result3.balance = result3.balance - listbook.Amout;

                    var update4 = MongoDB.Driver.Builders.Update.Set("balance", result3.balance);
                    this.collectionBorrow.Update(query3, update4);


                    if (result3.balance == 0)
                    {
                        var update5 = MongoDB.Driver.Builders.Update.Set("status", sta);
                        this.collectionBorrow.Update(query3, update5);


                    }

                }
            }
        }
}

