using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProgrammationConformit.Models
{
    public class Event
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Person { get; set; }

        public Comment[] Comments { get; set; }
    }
}
