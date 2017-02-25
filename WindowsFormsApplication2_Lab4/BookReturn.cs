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
    public partial class BookReturn : Form
    {
        private MongoDatabase database;
        private MongoCollection<Borrow> collectionBorrow;
        private MongoCollection<Member> collectionMember;
        private MongoCollection<Book> collectionBook;
        private MongoCollection<Librarian> collectionLibrarian;
        private MongoCollection<Order> collectionOrder;
        private ObjectId Bookid;
        private ObjectId Orderid;
        //private ObjectId Borrowid;
        //private ObjectId Personid;
        //private ObjectId Librarianid;
        public int Totalmulct = 0;
        public int remain = 0;
        private string[] cats = { "นิยาย", "อาหาร", "วรรณคดี", "การ์ตูน", "IT" };
        public List<Listbook> listbook;
        public int Amout,count = 0;
        public char[] a;
        public BookReturn()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collectionBorrow = database.GetCollection<Borrow>("Borrow");
            this.collectionMember = database.GetCollection<Member>("Member");
            this.collectionBook = database.GetCollection<Book>("Book");
            this.collectionLibrarian = database.GetCollection<Librarian>("Librarian");
            this.collectionOrder = database.GetCollection<Order>("Order");
        }

        private void Return_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(cats);
            comboBox1.SelectedIndex = 0;
            GridUpdate();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
         }

        public void AddList(Listbook listbook)
        {
            ListOrder order = new ListOrder(); 
            string username = textBox1.Text;
            string Bookname = textBox5.Text;
            string BookType = comboBox1.Text;
           // int Amout = int.Parse(textBox7.Text);

            try
            {
                string bb2 = textBox7.Text;
                int all = int.Parse(bb2);

                if (all > 34)
                {
                    MessageBox.Show(all.ToString());
                    Amout = int.Parse(textBox7.Text);
                }
            }
            catch
            {
                MessageBox.Show("กรุณากรอกจำนวนใหม่อีกครั้ง");
                return;
            }
            var query2 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
            Query<Book>.EQ(CS => CS.BookType, BookType));
            var result2 = this.collectionBook.FindOne(query2);
            var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
            Query<Member>.EQ(CS => CS.username, username));
            var result3 = this.collectionBorrow.FindOne(query3);
            if (result3 == null)
            {
                MessageBox.Show("ไม่พบข้อมูลในการยืม ไม่สามารถดำเนินการได้");
            }
            else
            {
                listbook.Bookid = result2.id;
                listbook.Amout = int.Parse(textBox7.Text);
                listbook.DateArrive = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                listbook.DateBorrow = result3.DateBorrow;
                listbook.DateReturn = result3.DateReturn;
                listbook.status = "คืนแล้ว";
                string DateArrive = dateTimePicker2.Value.ToString("dd-MM-yyyy");

                //MessageBox.Show(textBox7.Text);
                string d = listbook.DateArrive.Substring(0, 2);
                //คิดค่าปรับ
                if (d.IndexOf("0", 2).Equals("0"))
                {
                    d = listbook.DateArrive.Substring(0, 1);
                }
                else
                {
                    d = listbook.DateArrive.Substring(0, 2);
                }               

                //เรียกฟังก์ชันค่าปรับ
                 string c = result3.DateReturn.Substring(0, 2);
                //คำนวณวันในการคิดค่าปรับ
                if (c.IndexOf("0", 2).Equals("0"))
                {
                    c = result3.DateReturn.Substring(0, 1);
                }
                else
                {
                    c = result3.DateReturn.Substring(0, 2);
                }
               
                if (int.Parse(d) > int.Parse(c))
                {
                    x = (int.Parse(d) - int.Parse(c)) * 3;
                }

                if (result3.balance == 0)
                {
                    MessageBox.Show("สมาชิกท่านนี้คืนหนังสือครบแล้ว ");
                }
                else 
                {                    
                Return re = new Return(username, Bookname, BookType, listbook, int.Parse(textBox7.Text), DateArrive,x);

                Fine fine = new Fine(listbook, username, Bookname, BookType, Amout, x);
                GridUpdate();                
                }               
            }
        }
        public int x;

        public void BookOrder(ListOrder order)
        {
            //ตรวจสอบว่ามีใครจองหนังสือไว้หรือไม่
            string username = textBox1.Text;
            string Bookname = textBox5.Text;
            string BookType = comboBox1.Text;
            int Amout = int.Parse(textBox7.Text);
            //string sta = "รายการสมบูรณ์";

            var query = Query.And(Query<Member>.EQ(CS => CS.username, username),
            Query<Order>.EQ(CS => CS.username, username));
            var result = this.collectionMember.FindOne(query);

            var query2 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
            Query<Book>.EQ(CS => CS.BookType, BookType));
            var result2 = this.collectionBook.FindOne(query2);


            var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
            Query<Member>.EQ(CS => CS.username, username));
            var result3 = this.collectionBorrow.FindOne(query3);

            var query4 = Query<Order>.EQ(CS => CS.username, username);
            var result4 = this.collectionOrder.FindOne(query4);

            if(result != null)
            {
            MessageBox.Show("หนังสือว่างแล้ว สามารถทำการยืมได้");

            }

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Listbook a = new Listbook();
            AddList(a);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var tmp = dataGridView1.Rows[e.RowIndex];
            this.Bookid = ObjectId.Parse(tmp.Cells["id"].Value.ToString());
            textBox5.Text = tmp.Cells["Bookname"].Value.ToString();
            comboBox1.Text = tmp.Cells["BookType"].Value.ToString();
            //textBox3.Text = tmp.Cells["Amout"].Value.ToString();
            //textBox4.Text = tmp.Cells["status"].Value.ToString();
        }
        void GridUpdate()
        {
            List<Book> datalist = new List<Book>();
        foreach (var item in this.collectionBook.FindAll())
            {
                datalist.Add(item);
            }
            dataGridView1.DataSource = datalist;

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            string username = textBox1.Text;

            var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
            Query<Member>.EQ(CS => CS.username, username));
            var result3 = this.collectionMember.FindOne(query3);

            textBox2.Text = result3.name;
            textBox3.Text = result3.address;
            textBox4.Text = result3.tel;
        }

    }
}
