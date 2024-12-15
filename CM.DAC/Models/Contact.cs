using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CM.DAC.Models
{
    public class Contact
    {
        public int ContactID { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int CategoryID { get; set; }
        public string CategoryName { get; set; } // Category Name
        public int UserID { get; set; }          // Add UserID
    }
}
