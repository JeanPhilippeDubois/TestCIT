using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestProgrammationConformit.Models
{
    public class Event
    {
        public int Id { get; set; }

        [StringLength(100, ErrorMessage = "Title length can't be more than 100.")]
        public string Title { get; set; }

        public string Description { get; set; }

        public Person Person { get; set; }

        public Comment[] Comments { get; set; }
    }
}
