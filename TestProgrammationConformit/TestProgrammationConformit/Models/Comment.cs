using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProgrammationConformit.Models
{
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public Event Event { get; set; }
    }
}
