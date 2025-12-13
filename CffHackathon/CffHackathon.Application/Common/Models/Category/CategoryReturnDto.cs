using CffHackathon.Application.Common.Models.MenuItem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CffHackathon.Application.Common.Models.Category
{
    public class CategoryReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<MenuItemInCategoryDto> MenuItems { get; set; }
    }

    public class MenuItemInCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PhotoUrl { get; set; }
        public decimal Price { get; set; }
    }
}
