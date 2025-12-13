using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Models.MenuItem
{
    public class MenuItemReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string CategoryName { get; set; }
        public string? PhotoUrl { get; internal set; }
        public bool IsAvailable { get; internal set; }
    }
}
