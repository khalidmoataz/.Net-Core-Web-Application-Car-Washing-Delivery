using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTasks.Models
{
    public class Shop
    {
        [Key]
        public int Id { get; set; }
        public String title { get; set; }
        public int price { get; set; }
    }
}
