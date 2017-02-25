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
    public partial class BookBorrow : Form
    {
        private MongoDatabase database;
        private MongoCollection<Borrow> collectionBorrow;
        private MongoCollection<Member> collectionMember;
        private MongoCollection<Book> collectionBook;
        private MongoCollection<Librarian> collectionLibrarian;
        private MongoCollection<Order> collectionOrder;
        private MongoCollection<Class1> collectionClass1;
        private ObjectId Borrowid;
        private ObjectId Memberid;
        private ObjectId Bookid;
        private ObjectId Librarianid;
        private List<Listbook> listbook = new List<Listbook>();
        public int Total = 0, num = 0;
        public int Amout;
        private string[] cats = { "นิยาย", "อาหาร", "วรรณคดี", "การ์ตูน", "IT" };
        public BookBorrow()
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
            this.collectionClass1 = database.GetCollection<Class1>("Class1");
        }

        private void Borrow_Load(object sender, EventArgs e)
        {
            comboBox1.Items.AddRange(cats);
            comboBox1.SelectedIndex = 0;
            GridUpdate();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            MessageBox.Show("บันทึกสำเร็จ");
            this.Hide();
            Home h = new Home();
            h.Show();
            //ReportBorrow report = new ReportBorrow();
            // report.Show();


        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Home h = new Home();
            h.Show();
        }

        public void AddList(Listbook listbook)
        {
            ListOrder a = new ListOrder();
            string username = textBox1.Text;
            string name = textBox2.Text;
            string address = textBox3.Text;
            string tel = textBox4.Text;
            string Bookname = textBox5.Text;
            string BookType = comboBox1.Text;
            string DateBorrow = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            string DateReturn = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            //string DateArrive = " ";
            int mulct = 0;
            string status = "รายการไม่สมบูรณ์";
            bool t;

                    Amout = int.Parse(textBox7.Text);
                    var query3 = Query<Borrow>.EQ(CS => CS.username, username);
                    var result3 = this.collectionBorrow.FindOne(query3);

                    var query2 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
                    Query<Book>.EQ(CS => CS.BookType, BookType));
                    var result2 = this.collectionBook.FindOne(query2);

                    if (result2.Amout == 0)
                    {
                        MessageBox.Show("ขออภัยหนังสือหมดแล้ว");
                        AddOrder order = new AddOrder();
                        order.Show();
                        this.Hide();

                    }
                    else
                    {
                        listbook.Bookid = result2.id;
                        listbook.Amout = int.Parse(textBox7.Text);
                        listbook.DateArrive = "";
                        listbook.DateBorrow = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                        listbook.DateReturn = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                        listbook.status = "ยังไม่คืน";
                        Total = Total + Amout;
                        num = Total;
                        if (result3 == null)
                        {
                            Borrow Borrow = new Borrow(username, name, address, tel, Total, DateBorrow, DateReturn, mulct, num, status
                                , "admin");
                            this.collectionBorrow.Save(Borrow);

                        }
                        if (result2 != null)
                        {
                            var query = Query.EQ("_id", Borrowid.Equals(ObjectId.Empty));
                            var result = this.collectionBorrow.FindOne(query);
                            var update2 = Update<Borrow>.AddToSet(x => x.listbook, listbook);
                            this.collectionBorrow.Update(query3, update2);

                        }
                        if (result3 != null)
                        {
                                var update4 = MongoDB.Driver.Builders.Update.Set("balance", result3.balance + Amout);
                                this.collectionBorrow.Update(query3, update4);
                                var update3 = MongoDB.Driver.Builders.Update.Set("Amout", Total + result3.Amout);
                                this.collectionBorrow.Update(query3, update3);
                                var update6 = MongoDB.Driver.Builders.Update.Set("status", status);
                                this.collectionBorrow.Update(query3, update6);

                        }
                        Amout = result2.Amout - Amout;
                        var update = MongoDB.Driver.Builders.Update.Set("Amout", Amout);
                        this.collectionBook.Update(query2, update);
                        var query6 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
                        Query<Book>.EQ(CS => CS.BookType, BookType));
                        var result6 = this.collectionBook.FindOne(query2);

                        if (result6.Amout == 0)
                        {
                            var update4 = MongoDB.Driver.Builders.Update.Set("status", "หนังสือหมด");
                            this.collectionBook.Update(query2, update4);
                            var update5 = MongoDB.Driver.Builders.Update.Set("Amout", 0);
                            this.collectionBook.Update(query2, update5);
                            GridUpdate();
                        }
                    }
                    BookOrder(a);


            /*catch
            {
                MessageBox.Show("กรุณากรอกจำนวนใหม่อีกครั้ง");
            }*/

            GridUpdate();

        }
        public void BookOrder(ListOrder order)
        {
            string username = textBox1.Text;
            string Bookname = textBox5.Text;
            string BookType = comboBox1.Text;
            int Amout = int.Parse(textBox7.Text);

            var query = Query.And(Query<Member>.EQ(CS => CS.username, username),
            Query<Order>.EQ(CS => CS.username, username));
            var result = this.collectionOrder.FindOne(query);

            var query2 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
            Query<Book>.EQ(CS => CS.BookType, BookType));
            var result2 = this.collectionBook.FindOne(query2);


            var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
            Query<Member>.EQ(CS => CS.username, username));
            var result3 = this.collectionBorrow.FindOne(query3);

            var query4 = Query<Order>.EQ(CS => CS.username, username);
            var result4 = this.collectionOrder.FindOne(query4);



            if (result != null)
            {
                //MessageBox.Show("work");
                var query5 = MongoDB.Driver.Builders.Query.EQ("_id", result4.id);
                var update = MongoDB.Driver.Builders.Update.Pull("listbook",
                    MongoDB.Driver.Builders.Query.EQ("Bookid", result2.id));
                this.collectionOrder.Update(query5, update);

                result4.balance = result4.balance - Amout;
                var update3 = MongoDB.Driver.Builders.Update.Set("balance", result4.balance);
                this.collectionOrder.Update(query4, update3);


                if (result4.balance == 0)
                {
                    ///update status
                    var update2 = MongoDB.Driver.Builders.Update.Set("status", "รายการจองสมบูรณ์");
                    this.collectionOrder.Update(query4, update2);

                }
            }



        }
        private void button5_Click(object sender, EventArgs e)
        {
            Listbook a = new Listbook();
            string username = textBox1.Text;
            string Bookname = textBox5.Text;
            string BookType = comboBox1.Text;
            string DateBorrow = dateTimePicker1.Value.ToString("dd-MM-yyyy");
            string DateReturn = dateTimePicker2.Value.ToString("dd-MM-yyyy");
            int Amout = int.Parse(textBox7.Text);
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
                    var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
                    Query<Member>.EQ(CS => CS.username, username));
                    var result3 = this.collectionMember.FindOne(query3);

                    var query2 = Query.And(Query<Book>.EQ(CS => CS.Bookname, Bookname),
                    Query<Book>.EQ(CS => CS.BookType, BookType));
                    var result2 = this.collectionBook.FindOne(query2);



                    a.Bookid = result2.id;
                    a.Amout = int.Parse(textBox7.Text);
                    a.DateArrive = "";
                    a.DateBorrow = dateTimePicker1.Value.ToString("dd-MM-yyyy");
                    a.DateReturn = dateTimePicker2.Value.ToString("dd-MM-yyyy");
                    a.status = "ยังไม่คืน";

                    AddList(a);
                    SaveClass(username,Bookname,BookType,DateBorrow,DateReturn,Amout);
                    if (result2.Amout == 0)
                    {
                        var update4 = MongoDB.Driver.Builders.Update.Set("status", "หนังสือหมด");
                        this.collectionBook.Update(query2, update4);

                    }


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
            string phone = "";
            var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
            Query<Member>.EQ(CS => CS.username, username));
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
                textBox2.Text = result3.name;
                textBox3.Text = result3.address;
                phone = result3.tel.ToString();
                textBox4.Text = phone;
            }


        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
        public void SaveClass(string username,string Bookname,string BookType,string DateBorrow,string DateReturn,int amout )
        {

            var query3 = Query.And(Query<Borrow>.EQ(CS => CS.username, username),
            Query<Member>.EQ(CS => CS.username, username));
            var result3 = this.collectionMember.FindOne(query3);


                    Class1 c = new Class1(username,result3.name,result3.tel,"admin",Bookname,BookType,DateBorrow,DateReturn,amout );
                    this.collectionClass1.Save(c);

        }

    }
}
