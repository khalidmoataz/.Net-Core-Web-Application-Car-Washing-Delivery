using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebTasks.Models
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public string user_id { get; set; }
        public DateTime time { get; set; }
        public string status { get; set; }
        public string plan { get; set; }

    }
}
