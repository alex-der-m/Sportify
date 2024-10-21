using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sportify_back.Models;

namespace Sportify_solution_app.Models
{
    public class ProfileContext : DbContext
    {
        public ProfileContext (DbContextOptions<ProfileContext> options)
            : base(options)
        {
        }

        public DbSet<Sportify_back.Models.Profiles> Profiles { get; set; } = default!;
    }
}
