using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolRegister
{
    public class Group
    {
        public string GroupName { get; set; }
        public string Lector { get; set; }

        public Group() { }

        public Group(string groupName, string lector)
        {
            this.GroupName = groupName;
            this.Lector = lector;
        }
    }
}
