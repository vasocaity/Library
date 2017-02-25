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
    public partial class Login : Form
    {
        private MongoDatabase database;
        private MongoCollection<Librarian> collection;
        private ObjectId id;
        public Login()
        {
            InitializeComponent();
            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collection = database.GetCollection<Librarian>("Librarian");
            textBox2.PasswordChar = '*';
        }

        private void Home_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
                string name = textBox1.Text;
                string password = textBox2.Text;
                 var query = Query.And(Query<Librarian>.EQ(Phon => Phon.name, name),
                Query<Librarian>.EQ(Phon => Phon.password, password));
                 var result = this.collection.FindOne(query);
                this.collection.FindOne(query);
                if (result != null)
                {
                    Home home = new Home();
                    home.Show();
                    this.Hide();
                }
                else
                {
                MessageBox.Show("username หรือ รหัสผ่านไม่ถูกต้อง !!!");
                }


            textBox1.Clear();
            textBox2.Clear();

            

        }
    }
}
