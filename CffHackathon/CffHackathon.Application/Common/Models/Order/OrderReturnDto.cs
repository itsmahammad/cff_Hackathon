using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Models.Order
{
    public class OrderReturnDto
    {
        public string TableName { get; set; }
       
        public string Status { get; set; }
        public decimal TotalPrice { get; set; }


      
    }
}
