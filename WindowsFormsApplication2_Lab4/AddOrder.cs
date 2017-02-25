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
    public partial class AddOrder : Form
    {
        private MongoDatabase database;
        private MongoCollection<Order> collectionOrder;
        private MongoCollection<Member> collectionMember;
        private MongoCollection<Book> collectionBook;
        private MongoCollection<Borrow> collectionBorrow;
        private ObjectId id;
        private ObjectId Bookid;
        private List<ListOrder> listbook = new List<ListOrder>();
        public int Total = 0;
        public AddOrder()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collectionOrder = database.GetCollection<Order>("Order");
            this.collectionMember = database.GetCollection<Member>("Member");
            this.collectionBook = database.GetCollection<Book>("Book");
            this.collectionBorrow = database.GetCollection<Borrow>("Borrow");
            GridUpdate();
        }
        private void Order_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //ListOrder a = new ListOrder();
            //Reserve(a);
            MessageBox.Show("การจองสำเร็จ");

        }
        public int Amout;
        public void Reserve(ListOrder listbook)
        {
            string username = textBox1.Text;
            string Bookname = textBox2.Text;
            //int Amout = int.Parse(textBox3.Text);
            string date = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            string status = "จอง";

                string bb2 = textBox3.Text;
                int all = int.Parse(bb2);
                    Amout = int.Parse(textBox3.Text);
                    if (all > 34)
                    {
                        MessageBox.Show(all.ToString());
                    }
                    else {

                        var query1 = Query.And(Query<Order>.EQ(CS => CS.username, username),
                        Query<Member>.EQ(CS => CS.username, username));
                        var result1 = this.collectionOrder.FindOne(query1);

                        var query2 = (Query<Book>.EQ(CS => CS.Bookname, Bookname));
                        var result2 = this.collectionBook.FindOne(query2);

                        Total = Total + Amout;

                        if (result1 == null)
                        {
                            Order order = new Order(username, Total, date, status, Total);
                            this.collectionOrder.Save(order);

                            MessageBox.Show("ข้อมูลถูกต้อง");
                        }
                        listbook.Bookid = result2.id;
                        listbook.Amout = int.Parse(textBox3.Text);
                        listbook.DateOrder = dateTimePicker1.Value.ToString("dd-MM-yyyy");

                        var update2 = Update<Order>.AddToSet(x => x.listbook, listbook);
                        this.collectionOrder.Update(query1, update2);
                        if (result1.balance != null) 
                        {
                        result1.balance += Amout;
                        var update3 = MongoDB.Driver.Builders.Update.Set("balance", result1.balance);
                        this.collectionOrder.Update(query1, update3);

                        result1.Amout += Amout;
                        var update4 = MongoDB.Driver.Builders.Update.Set("Amout", result1.Amout);
                        this.collectionOrder.Update(query1, update4);
                        }

                    }

            GridUpdate();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ListOrder a = new ListOrder();
            string username = textBox1.Text;
            string Bookname = textBox2.Text;
            string date = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            //int Amout = int.Parse(textBox3.Text);
            try
            {
                string bb2 = textBox3.Text;
                int all = int.Parse(bb2);

                if (all > 34)
                {
                    MessageBox.Show(all.ToString());
                    Amout = int.Parse(textBox3.Text);
                }
            }
            catch { return; }
                    var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
                    Query<Member>.EQ(CS => CS.username, username));
                    var result3 = this.collectionMember.FindOne(query3);

                    var query2 = Query<Book>.EQ(CS => CS.Bookname, Bookname);
                    var result2 = this.collectionBook.FindOne(query2);

                    a.Bookid = result2.id;
                    a.Amout = int.Parse(textBox3.Text);
                    a.DateOrder = dateTimePicker1.Value.ToString("dd-MM-yyyy");

                    Reserve(a);

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

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            var tmp = dataGridView1.Rows[e.RowIndex];
            this.Bookid = ObjectId.Parse(tmp.Cells["id"].Value.ToString());
            textBox2.Text = tmp.Cells["Bookname"].Value.ToString();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            string username = textBox1.Text;

            var query3 = Query<Member>.EQ(CS => CS.username, username);
            var result3 = this.collectionMember.FindOne(query3);
            if (result3 == null)
            {
                MessageBox.Show("คุณยังไม่เป็นสมาชิก");
                MemberDetail member = new MemberDetail();
                member.Show();
                this.Hide();
            }
            else
            {
                textBox4.Text = result3.name;

            }
        }
    }
}
