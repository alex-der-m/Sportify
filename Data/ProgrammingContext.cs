using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models;

namespace Sportify_solution_app.Models
{
    public class ProgrammingContext : DbContext
    {
        public ProgrammingContext (DbContextOptions<ProgrammingContext> options)
            : base(options)
        {
        }

        public DbSet<Sportify_back.Models.Programmings> Programmings { get; set; } = default!;
    }
}
