using Microsoft.EntityFrameworkCore;
using Models;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Collections.Generic;

namespace Data
{
    public class dbContext : DbContext
    {
        public DbSet<Kunde> Kunder => Set<Kunde>();


        public dbContext (DbContextOptions<dbContext> options)
            : base(options)
        {
            // Den her er tom. Men ": base(options)" sikre at constructor
            // på DbContext super-klassen bliver kaldt.
        }
    }
}