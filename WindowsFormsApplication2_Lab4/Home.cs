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
    public partial class Home : Form
    {
        private MongoDatabase database;
        private MongoCollection<Borrow> collectionBorrow;
        private MongoCollection<Book> collectionBook;
        private ObjectId Borrowid;
        private ObjectId Bookid;
        public string Bookname { get; set; }
        public string DateBorrow { get; set; }
        public Home()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collectionBorrow = database.GetCollection<Borrow>("Borrow");
            this.collectionBook = database.GetCollection<Book>("Book");
            this.Bookname = Bookname;
            this.DateBorrow = DateBorrow; 
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            BookBorrow Bform = new BookBorrow();
            Bform.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MemberDetail MFrom = new MemberDetail();
            MFrom.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        private void button3_Click(object sender, EventArgs e)
        {
            BookReturn re = new BookReturn();
            re.Show();
            this.Hide();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BookDetail detail = new BookDetail();
            detail.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            AddOrder order = new AddOrder();
            order.Show();
        }


        public IMongoQuery query { get; set; }

        private void Home_Load(object sender, EventArgs e)
        {


            //this.reportViewer1.RefreshReport();
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            ReportBorrow r = new ReportBorrow();
            r.Show();
        }
    }
}
