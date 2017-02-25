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
    class Member
    {
        public ObjectId id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string tel { get; set; }
        public Member() { }
        public Member(string username, string name,string address, string tel)
        {   this.username = username;
            this.name = name;
            this.address = address;
            this.tel = tel;
        }

    }
}
