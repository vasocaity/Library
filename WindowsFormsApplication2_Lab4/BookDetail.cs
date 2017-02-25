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
    public partial class BookDetail : Form
    {
        private MongoDatabase database;
        private MongoCollection<Book> collection;
        private ObjectId id;
        public BookDetail()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collection = database.GetCollection<Book>("Book");
            GridUpdate();

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var tmp = dataGridView1.Rows[e.RowIndex];
            this.id = ObjectId.Parse(tmp.Cells["id"].Value.ToString());
            textBox1.Text = tmp.Cells["Bookname"].Value.ToString();
            comboBox1.Text = tmp.Cells["BookType"].Value.ToString();
            textBox3.Text = tmp.Cells["Amout"].Value.ToString();
            textBox4.Text = tmp.Cells["status"].Value.ToString();
        }
        void GridUpdate()
        {
            List<Book> datalist = new List<Book>();
            foreach (var item in this.collection.FindAll())
            {
                datalist.Add(item);
            }
            dataGridView1.DataSource = datalist;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string Bookname = textBox1.Text;
            string BookType = comboBox1.Text;
            int Amout = int.Parse(textBox3.Text);
            string status = textBox4.Text;
            var query = MongoDB.Driver.Builders.Query.EQ("_id", this.id);

            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            GridUpdate();
        }
        public int Amout;
        private void button2_Click(object sender, EventArgs e)
        {

            string Bookname = textBox1.Text;
            string BookType = comboBox1.Text;

            string status = textBox4.Text;
            bool t;
            try
            {
                string bb2 = textBox3.Text;
                int all = int.Parse(bb2);

                if (all > 34)
                {
                    t = true;
                    Amout = int.Parse(textBox3.Text);
                    if (this.id.Equals(ObjectId.Empty))
                    {
                        Book book = new Book(Bookname, BookType, Amout, status);
                        this.collection.Save(book);
                    }
                }

                else
                {

                    t = false;

                }
            }
            catch
            {
                MessageBox.Show("กรุณากรอกจำนวนใหม่อีกครั้ง");
            }
            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            GridUpdate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Bookname = textBox1.Text;
            string BookType = comboBox1.Text;
            int Amout = int.Parse(textBox3.Text);
            string status = textBox4.Text;
            if (!this.id.Equals(ObjectId.Empty))
            {

                var query = MongoDB.Driver.Builders.Query.EQ("_id", this.id);
                var update = MongoDB.Driver.Builders.Update.Set("Bookname", Bookname).Set("BookType", BookType).Set("Amout", Amout).Set("status", status);
                this.collection.Update(query, update);
            }

            textBox1.Clear();
            textBox3.Clear();
            textBox4.Clear();
            GridUpdate();
        }
        private void BookDetail_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(cats);
            comboBox1.SelectedIndex = 0;
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        private string[] cats = { "นิยาย", "อาหาร", "วรรณคดี", "การ์ตูน", "IT" };

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            string Bookname = textBox1.Text;
            var query = (Query<Book>.EQ(CS => CS.Bookname, Bookname));
            var result = this.collection.FindOne(query);
            if (result != null)
            {
                MessageBox.Show("ชื่อหนังสือ: " + result.Bookname.ToString() + "  ประเภทหนังสือ:  " + result.BookType.ToString()
                    + "  จำนวนหนังสือ:  " + result.Amout.ToString()
                    );
            }
            else
            {
                MessageBox.Show("ไม่พบข้อมูล");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ReportBorrow r = new ReportBorrow();
            r.Show();
        }


    }
}
