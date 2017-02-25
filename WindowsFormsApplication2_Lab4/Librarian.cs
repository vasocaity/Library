using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
namespace WindowsFormsApplication2_Lab4
{
    class Librarian
    {
        public ObjectId id { get; set; }
        public string name { get; set; }
        public string password { get; set; }
        public Librarian(){}
        public Librarian(string Password,string name) {
            this.password = Password;
            this.name = name;
        }
    }
}
