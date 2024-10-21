using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models;

namespace Sportify_solution_app.Models
{
    public class TeachersContext : DbContext
    {
        public TeachersContext (DbContextOptions<TeachersContext> options)
            : base(options)
        {
        }

        public DbSet<Sportify_back.Models.Teachers> Teachers { get; set; } = default!;
    }
}
