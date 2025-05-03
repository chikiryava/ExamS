using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamShvets.Models
{
    public class PartnerDTO
    {
        public int Id { get; set; }
        public string Type { get; set; }         
        public string Name { get; set; }       
        public string Position { get; set; }    
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }       
        public int Rating { get; set; }         
        public int Discount { get; set; }
    }
}
