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
    public partial class MemberDetail : Form
    {
        private MongoDatabase  database;
        private MongoCollection<Member> collection;
        private ObjectId id;

        public MemberDetail()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");   
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collection = database.GetCollection<Member>("Member");
            GridUpdate();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var tmp = dataGridView1.Rows[e.RowIndex];
            this.id = ObjectId.Parse(tmp.Cells["id"].Value.ToString());
            textBox1.Text = tmp.Cells["Username"].Value.ToString();
            textBox2.Text = tmp.Cells["Name"].Value.ToString();
            textBox3.Text = tmp.Cells["address"].Value.ToString();
            textBox4.Text = tmp.Cells["tel"].Value.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            register();
        }
        public void register()
        {
            string username = textBox1.Text;
            string name = textBox2.Text;
            string address = textBox3.Text;
            string tel = textBox4.Text;
            //Int32.Parse(tel);
           try
            {
                string bb2 = textBox4.Text;
                int all = int.Parse(bb2);

                if (all > 34)
                {
                    if (tel.Length < 10)
                    {
                        MessageBox.Show("กรุณากรอกหมายเลขโทรศัพท์ใหม่อีกครั้งค่ะ");
                    }
                    else
                    {
                        Register R = new Register(username, name, address, tel);
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        GridUpdate();
                    }
                }
            }
            catch
            {
                MessageBox.Show("กรุณากรอกหมายเลขโทรศัพท์ใหม่อีกครั้งค่ะ");
            }

           



        }
        void GridUpdate()
        {
            List<Member> datalist = new  List<Member>();
            foreach (var item in this.collection.FindAll())
            {
                datalist.Add(item);
            }
            dataGridView1.DataSource = datalist;

        }
        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (!this.id.Equals(ObjectId.Empty))
            {
                string username = textBox1.Text;
                string name = textBox2.Text;
                string address = textBox3.Text;
                string tel = textBox4.Text;
                var query = MongoDB.Driver.Builders.Query.EQ("_id", this.id);          
                this.collection.Remove(query);
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            GridUpdate();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string username = textBox1.Text;
            string name = textBox2.Text;
            string address = textBox3.Text;
            string tel = textBox4.Text;
            if (!this.id.Equals(ObjectId.Empty))
            {

                var query = MongoDB.Driver.Builders.Query.EQ("_id", this.id);
                var update = MongoDB.Driver.Builders.Update.Set("username", username).Set("name", name).Set("address", address).Set("tel",tel);
                this.collection.Update(query, update);
            }

            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            GridUpdate();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            BookDetail Form2 = new BookDetail();
            Form2.Show();
        }
        
    }
}
