using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace WindowsFormsApplication2_Lab4
{
    public partial class ReportBorrow : Form

    {
        private MongoDatabase database;
        private MongoCollection<Borrow> collectionBorrow;
        private MongoCollection<Member> collectionMember;
        private MongoCollection<Book> collectionBook;
        private MongoCollection<Librarian> collectionLibrarian;
        private MongoCollection<Class1> collection;
        private ObjectId Borrowid;
        private ObjectId Memberid;
        private ObjectId Bookid;
        private ObjectId Librarianid;
        private List<Listbook> listbook = new List<Listbook>();

        public ReportBorrow()
        {

            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collectionBorrow = database.GetCollection<Borrow>("Borrow");
            this.collectionMember = database.GetCollection<Member>("Member");
            this.collectionBook = database.GetCollection<Book>("Book");
            this.collectionLibrarian = database.GetCollection<Librarian>("Librarian");
            this.collection = database.GetCollection<Class1>("Class1");
        }

        private void ReportBorrow_Load(object sender, EventArgs e)
        {
        }

        private void crystalReportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void CrystalReport41_InitReport(object sender, EventArgs e)
        {
           // string username = textBox1.Text;

            /*var query = MongoDB.Driver.Builders.Query.EQ("username", username);
            var result = this.collection.FindOne(query);*/

           // CrystalReport41 = result;
        }
    }
}
