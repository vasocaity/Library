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
    class Register
    {
        public ObjectId id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string tel { get; set; }
        private MongoDatabase database;
        private MongoCollection<Member> collection;


        public Register(string username, string name, string address, string tel)
        {   this.username = username;
            this.name = name;
            this.address = address;
            this.tel = tel;

            var client = new MongoClient("mongodb://localhost:27017");
            var server = client.GetServer();
            this.database = server.GetDatabase("db");

            this.collection = database.GetCollection<Member>("Member");

            AddMember();
        }

        public void AddMember()
        {
                string bb2 = tel;
                int all = int.Parse(bb2);
                if (all > 34)
                {
                            Member member = new Member(username, name, address, tel);
                            this.collection.Save(member);
                }

        }
    }
}
