using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister
{
    public class Student
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Bid { get; set; }
        public string PhoneNumber { get; set; }
        public string Adress { get; set; }
        public string Group { set; get; }

        public Student() { }

        public Student(string name, string lastname, int bid, string phoneNumber, string adress, string group)
        {
            this.LastName = lastname;
            this.Name = name;
            this.Bid = bid;
            this.PhoneNumber = phoneNumber;
            this.Adress = adress;
            this.Group = group;
        }
    }
}
