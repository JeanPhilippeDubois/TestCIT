using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TestProgrammationConformit.Models;

namespace TestProgrammationConformit.Infrastructures
{
    public class ConformitContext : DbContext
    {
        public ConformitContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Event> Events { get; set; }

        public DbSet<Comment> Comments { get; set; }
    }
}
