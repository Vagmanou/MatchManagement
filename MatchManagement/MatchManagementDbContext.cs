using MatchManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace MatchManagement
{
    public class MatchContext : DbContext
    {
        public MatchContext(DbContextOptions<MatchContext> options)
            : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }
        public DbSet<MatchOdd> MatchOdds { get; set; }
    }
}

